
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;


namespace Chronos.Models
{
    public class Calendar
    {
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Chronos";
        private DateTime startTime;
        private DateTime endTime;

        ///<summary>
        ///Accesses the Google API to get times that are marked as "busy" on the user's calendar.
        ///Currently, this method can only retrieve events that are within 1 month. 
        ///For example, if the range is 1/22/2018 t0 2/2/2018, only the events through 1/31/2018 will be retrieved.
        ///</summary>
        ///<returns>Returns a list of times that are "busy"</returns>
        public List<String> GetTimesBusy ()
        {
            UserCredential credential;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", "client_secret.json");
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/calendar.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)
                ).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer() {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            FreeBusyRequest requestBody = new FreeBusyRequest {
                TimeMin = startTime,
                TimeMax = endTime,
                Items = new List<FreeBusyRequestItem> {
                    new FreeBusyRequestItem { Id = "primary" } //only get the "primary" calendar
                }
            };
            //create request
            FreebusyResource.QueryRequest request = service.Freebusy.Query(requestBody);
            //execute request           
            FreeBusyResponse queryResult = request.Execute();

            //get the primary calendar out
            queryResult.Calendars.TryGetValue("primary", out FreeBusyCalendar primaryCalendar);
            List<string> events = new List<string>();
            //make sure there were no errors
            if (primaryCalendar.Errors == null) {

                DateTime last = startTime.AddDays(-1);

                foreach (TimePeriod time in primaryCalendar.Busy) {
                    DateTime start = DateTime.Parse(time.Start.ToString());
                    DateTime end = DateTime.Parse(time.End.ToString());
                    //check if this event is on the same day as the last event
                    if (!last.Day.Equals(start.Day)) {
                        events.Add(start.Date.ToShortDateString() +
                            " - Busy from " + start.ToShortTimeString() + " to " + end.ToShortTimeString());
                    } else {
                        string lastDay = events.ElementAt(events.Count - 1);
                        events.RemoveAt(events.Count - 1);
                        events.Add(lastDay + " and " + start.ToShortTimeString() + " to " + end.ToShortTimeString());
                    }
                    last = start;
                }
            } else {
                foreach (Error error in primaryCalendar.Errors) {
                    Console.WriteLine("Error:");
                    Console.WriteLine(error.Domain);
                    Console.WriteLine(error.Reason);
                }
            }
            
            return events;
        }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
    }
}