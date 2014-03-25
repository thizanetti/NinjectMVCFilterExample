using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NinjectFilterConstructor.Interface;
using NinjectFilterConstructor.POCO;

namespace NinjectFilterConstructor.Filters
{
    public class TritonActionFilter : IActionFilter
    {
        private readonly IPeople peps;
        private readonly int args;
        private readonly bool enabled;

        public TritonActionFilter(IPeople _people, int _args, bool _enabled)
        {
            peps = _people;
            args = _args;
            enabled = _enabled;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (enabled) //this.Enabled
            {
                //var securityHelper = new SecurityHelper();
                bool hasPermission = peps.IsAlive();// _securityHelper.CheckPermission(_actionID); //ActionID

                if (!hasPermission)
                {
                    // If user does not have permission to execute specific action, redirect them to home page
                    var redirectTargetDictionary = new RouteValueDictionary();
                    //redirectTargetDictionary.Add("action", "ContestList");
                    //redirectTargetDictionary.Add("controller", "ContestAdmin");

                    //filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                }
                else
                {
                    // Call the base
                    //base.OnActionExecuting(filterContext); --not sure what to do with this
                }
            }
            else
            {
                //base.OnActionExecuting(filterContext); --not sure what to do with this
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //do nothing
        }
    }
}