using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Chronos.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Calendar.v3.Data;
using Chronos.Abstract;

namespace Chronos.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private IGroupRepository groupRepository;

        public CalendarController (IGroupRepository groupRepositoryParam)
        {
            groupRepository = groupRepositoryParam;
        }

        private UserCredential GetCredentialForApi(ApplicationUser user)
        {
            var initializer = new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = MyClientSecrets.ClientId,
                    ClientSecret = MyClientSecrets.ClientSecret,
                },
                Scopes = RequestedScopes.Scopes,
            };
            var flow = new GoogleAuthorizationCodeFlow(initializer);

            var token = new TokenResponse {
                AccessToken = user.AccessToken,
                RefreshToken = user.RefreshToken
            };

            return new UserCredential(flow, user.Id, token);
        }
        /// <summary>
        /// Gets the TimePeriods an ApplicationUser, with a given Id is busy, on a given interval of time
        /// </summary>
        /// <param name="username">The Id of an ApplicationUser</param>
        /// <param name="start">The lower bound of time on the time interval</param>
        /// <param name="end">The upper bound of time on the time interval</param>
        /// <returns>A List of TimePeriods when the AppicationUser's primary calendar is marked as busy.</returns>
        private async Task<IList<TimePeriod>> GetTimesBusyAsync(string username, DateTime start, DateTime end)
        {

            var context = new ApplicationDbContext();
            var user = context.Users.FirstOrDefault(u => u.UserName == username);

            var credential = GetCredentialForApi(user);

            var service = new CalendarService(new BaseClientService.Initializer() {
                HttpClientInitializer = credential,
                ApplicationName = "Chronos",
            });
            // Define parameters of request.
            FreeBusyRequest requestBody = new FreeBusyRequest
            {
                TimeMin = start,
                TimeMax = end,
                Items = new List<FreeBusyRequestItem> {
                    new FreeBusyRequestItem { Id = "primary" } //only get the "primary" calendar
                }
            };
            //create request
            var request = service.Freebusy.Query(requestBody);

            try
            {
                //execute request
                var queryResult = await request.ExecuteAsync();

                queryResult.Calendars.TryGetValue("primary", out FreeBusyCalendar primaryCalendar);

                //make sure there were no errors
                if (primaryCalendar.Errors == null)
                {
                    return primaryCalendar.Busy;
                }
                else
                {
                    foreach (Error error in primaryCalendar.Errors)
                    {
                        Console.WriteLine("Error:");
                        Console.WriteLine(error.Domain);
                        Console.WriteLine(error.Reason);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error");
                Console.WriteLine(e);

            }
            return new List<TimePeriod>();
        }

        private IList<TimePeriod> GetOpenTimes(IList<TimePeriod> timesBusy, DateTime start, DateTime end)
        {
            var sortedTimes = timesBusy.OrderBy(t => t.Start);
            IList<TimePeriod> timesFree = new List<TimePeriod>();

            //holds the beginning of a period of free time
            DateTime begin = start.ToLocalTime();

            foreach (TimePeriod period in sortedTimes)
            {
                DateTime pStart = DateTime.Parse(period.Start.ToString());
                DateTime pEnd = DateTime.Parse(period.End.ToString());

                if (pStart.CompareTo(begin) <= 0)
                {
                    if (pEnd.CompareTo(begin) >= 0)
                        begin = pEnd;
                }
                else if (pStart.CompareTo(end) <= 0)
                {
                    if (!pStart.Equals(end)) //don't create a TimePeriod where Start == End
                        timesFree.Add(new TimePeriod { Start = begin, End = pStart });
                    begin = pEnd;
                }
            }

            return timesFree;
        }

        public async Task<ActionResult> FindMeetingTimes()
        {
            var groupId = Int32.Parse(RouteData.Values["id"].ToString());
            var model = new FindMeetingTimesModel();

            //var appUsers = new ApplicationDbContext().Users;
            var tasks = new List<Task<IList<TimePeriod>>>();
            IList<TimePeriod> timesBusy = new List<TimePeriod>();
            var usernames = groupRepository.GetUsernamesOfGroupById(groupId);
            foreach (string username in usernames)
                tasks.Add(GetTimesBusyAsync(username, model.StartTime, model.EndTime));
            
            
            foreach (var task in await Task.WhenAll(tasks))
                timesBusy = new List<TimePeriod> (timesBusy.Concat(task));

            model.TimesFree = GetOpenTimes(timesBusy, model.StartTime, model.EndTime);

            return PartialView(model);
        }
    }
}
