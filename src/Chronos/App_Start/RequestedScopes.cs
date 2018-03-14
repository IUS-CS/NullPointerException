using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Google.Apis.Calendar.v3;

namespace Chronos
{
    internal static class RequestedScopes
    {
        public static string[] Scopes
        {
            get
            {
                return new[] {
                    "openid",
                    "email",
                    CalendarService.Scope.CalendarReadonly,
                };
            }
        }
    }
}