using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Web.Common;
using NinjectFilterConstructor.Implementation;
using NinjectFilterConstructor.Interface;
using Ninject.Web.Mvc.FilterBindingSyntax;
using NinjectFilterConstructor.Filters;
using log4net.Core;
using log4net;
using Ninject.Web.Mvc.Filter;

namespace NinjectFilterConstructor
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            //Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        /*
        protected override Ninject.IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IPeople>().To<Irish>();
            kernel.Bind<ILog>().ToMethod(GetLogger);
            kernel.BindFilter<LogFilter>(FilterScope.Controller, 0)
                  .WithConstructorArgument("level", Level.Info);
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }

        private static ILog GetLogger(Ninject.Activation.IContext arg)
        {
            var filterContext = arg.Request.ParentRequest.Parameters
                                .OfType<FilterContextParameter>().SingleOrDefault();
            return LogManager.GetLogger(filterContext == null ?
                arg.Request.Target.Member.DeclaringType :
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerType);
        }
         */
    }
}