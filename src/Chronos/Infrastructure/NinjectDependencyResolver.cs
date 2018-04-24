using System;
using System.Collections.Generic;
using Ninject;
using System.Web.Mvc;
using Moq;
using Chronos.Abstract;
using Chronos.Entities;
using Chronos.Concrete;

namespace Chronos.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        /// <summary>
        /// Tells the DI container which interfaces bind to certain concrete classes
        /// </summary>
        public void AddBindings()
        {
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            mock.Setup(m => m.Users).Returns(new List<User>
            {
                new User { Id = 1, Username = "Nathan" },
                new User { Id = 2, Username = "Nathaniel" },
                new User { Id = 3, Username = "Garrett" },
                new User { Id = 4, Username = "Reza" }
            });

            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IGroupRepository>().To<GroupRepository>();
            kernel.Bind<IMemberItemsRepository>().To<MemberItemsRepository>();
            kernel.Bind<IInviteRepository>().To<InviteRepository>();
            
        }
    }
}