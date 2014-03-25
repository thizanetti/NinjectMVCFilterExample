using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NinjectFilterConstructor.POCO;

namespace NinjectFilterConstructor.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=true)]
    public class TritonActionAttribute : ActionFilterAttribute
    {
        public readonly int Args;
        public readonly bool Enabled;
        
        public TritonActionAttribute(int args, bool enabled)
        {
            Args = args;
            Enabled = enabled;
        }
    }
}