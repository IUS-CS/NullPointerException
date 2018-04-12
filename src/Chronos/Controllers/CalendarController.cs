using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

using Chronos.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Calendar.v3.Data;

namespace Chronos.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private readonly IDataStore dataStore = new FileDataStore(GoogleWebAuthorizationBroker.Folder);

        private async Task<UserCredential> GetCredentialForApiAsync()
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

            var identity = await HttpContext.GetOwinContext().Authentication.GetExternalIdentityAsync(
                DefaultAuthenticationTypes.ApplicationCookie);
            var userId = identity.FindFirstValue(MyClaimTypes.GoogleUserId);

            var token = await dataStore.GetAsync<TokenResponse>(userId);
            return new UserCredential(flow, userId, token);
        }
        // GET: /Calendar/UpcomingEvents
        public async Task<ActionResult> TimesBusy ()
        {         
            var model = new CalendarModel();

            var credential = await GetCredentialForApiAsync();

            var service = new CalendarService(new BaseClientService.Initializer() {
                HttpClientInitializer = credential,
                ApplicationName = "Chronos",
            });
            // Define parameters of request.
            FreeBusyRequest requestBody = new FreeBusyRequest
            {
                TimeMin = model.StartTime,
                TimeMax = model.EndTime,
                Items = new List<FreeBusyRequestItem> {
                    new FreeBusyRequestItem { Id = "primary" } //only get the "primary" calendar
                }
            };
            //create request
            FreebusyResource.QueryRequest request = service.Freebusy.Query(requestBody);
            //execute request
            List<string> events = new List<string>();
            try
            {
                var queryResult = await request.ExecuteAsync();
                queryResult.Calendars.TryGetValue("primary", out FreeBusyCalendar primaryCalendar);
                
                //make sure there were no errors
                if (primaryCalendar.Errors == null)
                {

                    DateTime last = model.StartTime.AddDays(-1);

                    foreach (TimePeriod time in primaryCalendar.Busy)
                    {
                        DateTime start = DateTime.Parse(time.Start.ToString());
                        DateTime end = DateTime.Parse(time.End.ToString());
                        //check if this event is on the same day as the last event
                        if (!last.Day.Equals(start.Day))
                        {
                            events.Add(start.Date.ToShortDateString() +
                                " - Busy from " + start.ToShortTimeString() + " to " + end.ToShortTimeString());
                        }
                        else
                        {
                            string lastDay = events.ElementAt(events.Count - 1);
                            events.RemoveAt(events.Count - 1);
                            events.Add(lastDay + " and " + start.ToShortTimeString() + " to " + end.ToShortTimeString());
                        }
                        last = start;
                    }
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
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Error");
                Console.WriteLine(e);
                
                //throw;
            }
            model.TimesBusy = events;
            return View(model);
        }
    }

}
