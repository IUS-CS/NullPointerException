[assembly: WebActivator.PreApplicationStartMethod(typeof(Chronos.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Chronos.App_Start.NinjectWebCommon), "Stop")]

namespace Chronos.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Microsoft.Owin.Security;
    using Chronos.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;

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
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            System.Web.Mvc.DependencyResolver.SetResolver(new
                Chronos.Infrastructure.NinjectDependencyResolver(kernel));

            kernel.Bind<ApplicationDbContext>().ToSelf();

            kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>().WithConstructorArgument("context",
                kernel.Get<ApplicationDbContext>());

            kernel.Bind<UserManager<ApplicationUser>>().To<ApplicationUserManager>().WithConstructorArgument("store",
                kernel.Get<IUserStore<ApplicationUser>>());

            kernel.Bind<IAuthenticationManager>().ToMethod(context =>
            {
                var contextBase = new HttpContextWrapper(HttpContext.Current);
                return contextBase.GetOwinContext().Authentication;
            });

            kernel.Bind<SignInManager<ApplicationUser, string>>().To<ApplicationSignInManager>()
                .WithConstructorArgument("userManager", kernel.Get<UserManager<ApplicationUser>>())
                .WithConstructorArgument("authenticationManager", 
                kernel.Get<IAuthenticationManager>());

            

        }        
    }
}
