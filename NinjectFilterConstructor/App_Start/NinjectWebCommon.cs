[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectFilterConstructor.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectFilterConstructor.App_Start.NinjectWebCommon), "Stop")]

namespace NinjectFilterConstructor.App_Start
{
    using System;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using log4net;
    using log4net.Core;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using NinjectFilterConstructor.Filters;
    using NinjectFilterConstructor.Implementation;
    using NinjectFilterConstructor.Interface;
    using Ninject.Web.Mvc.FilterBindingSyntax;
    using Ninject.Activation;
    using System.Linq;
    using Ninject.Web.Mvc.Filter;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IPeople>().To<Irish>();
            kernel.Bind<ILog>().ToMethod(GetLogger);
            kernel.BindFilter<TritonActionFilter>(FilterScope.Action, 0)
                   .WhenActionMethodHas<TritonActionAttribute>()
                   .WithConstructorArgumentFromActionAttribute<TritonActionAttribute>("_args", o => o.Args)
                   .WithConstructorArgumentFromActionAttribute<TritonActionAttribute>("_enabled", o => o.Enabled);
            kernel.BindFilter<LogFilter>(FilterScope.Controller, 0)
                  .WithConstructorArgument("level", Level.Info);
            kernel.Load(Assembly.GetExecutingAssembly());
            //return kernel;
        }

        private static ILog GetLogger(IContext arg)
        {
            var filterContext = arg.Request.ParentRequest.Parameters
                                .OfType<FilterContextParameter>().SingleOrDefault();
            return LogManager.GetLogger(filterContext == null ?
                arg.Request.Target.Member.DeclaringType :
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerType);
        }
    }
}
