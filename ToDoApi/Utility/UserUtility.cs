using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ToDoApi.Utility
{
    public class UserUtility
    {
        public static string GetUserId(ClaimsPrincipal user)
        {
            return (user.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
        }
    }
}
