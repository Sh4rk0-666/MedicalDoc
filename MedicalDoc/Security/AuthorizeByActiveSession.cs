using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

namespace MedicalDoc.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizeBySession : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.FilterDescriptors.Any(x => x.Filter.GetType() == typeof(Microsoft.AspNetCore.Mvc.Authorization.AllowAnonymousFilter)))
            {
                return;
            }

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                // User is authenticated
                // Check the session
                if (context.HttpContext.Session.IsAvailable)
                {
                    if (context.HttpContext.Session.GetString(SessionConstants.UserId) == null)
                    {
                        // User is not in a valid session, clear cookies and redirect to login page
                        context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        context.HttpContext.Session.Clear();
                        if (!context.HttpContext.Request.Path.Value.Contains("/Account/Login"))
                        {
                            context.Result = new RedirectToActionResult("Login", "Account", null);
                        }
                    }
                }
                else
                {
                    // Session is not available, clear cookies and redirect to login page
                    context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    context.HttpContext.Session.Clear();
                    if (!context.HttpContext.Request.Path.Value.Contains("/Account/Login"))
                    {
                        context.Result = new RedirectToActionResult("Login", "Account", null);
                    }
                }
            }
            else
            {
                // User is not authenticated, redirect to login page
                if (!context.HttpContext.Request.Path.Value.Contains("/Account/Login"))
                {
                    context.Result = new RedirectToActionResult("Login", "Account", null);
                }
            }
        }

    }
}
