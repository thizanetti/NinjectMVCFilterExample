using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using log4net.Core;

namespace NinjectFilterConstructor.Filters
{
    public class LogFilter : IActionFilter
    {
        private readonly ILog log;
        private readonly Level level;

        public LogFilter(ILog log, Level level)
        {
            this.log = log;
            this.level = level;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string message = string.Format(
                CultureInfo.InvariantCulture,
                "Executing action {0}.{1}",
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                filterContext.ActionDescriptor.ActionName);
            this.log.Logger.Log(typeof(LogFilter), this.level, message, null);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string message = string.Format(
                CultureInfo.InvariantCulture,
                "Executed action {0}.{1}",
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                filterContext.ActionDescriptor.ActionName);
            this.log.Logger.Log(typeof(LogFilter), this.level, message, null);
        }
    }
}