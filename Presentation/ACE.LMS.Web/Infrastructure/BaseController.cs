using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using ACE.LMS.Web.Models;

namespace ACE.LMS.Web.Infrastructure
{
    public class BaseController : ACE.LMS.Web.Framework.Mvc.BaseController
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (controllerName != "Home" && this.User.IsInRole("Student"))
            {
                var currentUserId = User.Identity.GetUserId();
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var currentUser = manager.FindById(currentUserId);

                //if (Session["HasCheckedTerms"] == null)
                //if(!currentUser.UserProfile.HasCheckedTerms)
                //{
                //    // FormsAuthentication.SignOut();
                //    // return RedirectToAction("Index", "Home");
                //    AuthenticationManager.SignOut();

                //    filterContext.Result = new RedirectToRouteResult(
                //       new RouteValueDictionary 
                //{ 
                //    { "controller", "Account" }, 
                //    { "action", "Login" } 
                //});
                //    return;

                //}

                if (HttpContext.User != null && (HttpContext != null && HttpContext.User.Identity.IsAuthenticated))
                // throws error
                {
                    string actionName = this.ControllerContext.RouteData.Values["action"].ToString();

                    if (!currentUser.UserProfile.HasCheckedTerms
                        && !actionName.Equals("TermsAndConditions", StringComparison.OrdinalIgnoreCase))
                    {
                        // Session["Redirect"] = true;
                        //  RedirectToAction("TermsAndConditions", "Library");
                        filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary 
                { 
                    { "controller", "Library" }, 
                    { "action", "TermsAndConditions" } 
                });
                    }
                }
            }
        }
    }
}