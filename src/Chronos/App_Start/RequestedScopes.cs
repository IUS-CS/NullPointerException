using Google.Apis.Calendar.v3;
using Google.Apis.Plus.v1;

namespace Chronos
{
    internal static class RequestedScopes
    {
        public static string[] Scopes
        {
            get
            {
                return new[] {
                    PlusService.Scope.PlusLogin,
                    PlusService.Scope.UserinfoEmail,
                    CalendarService.Scope.CalendarReadonly,
                };
            }
        }
    }
}