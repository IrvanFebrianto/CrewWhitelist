using Business.Abstract;
using Business.Concrete;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUI.Infrastructure.Abstract;
using WebUI.Infrastructure.Concrete;

namespace WebUI.Infrastructure
{
    public class NinjectDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
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

        private void AddBindings()
        {
            //user management
            kernel.Bind<IActionRepository>().To<EFActionRepository>();
            kernel.Bind<IModuleRepository>().To<EFModuleRepository>();
            kernel.Bind<IRoleRepository>().To<EFRoleRepository>();
            kernel.Bind<IModulesInRoleRepository>().To<EFModulesInRoleRepository>();


            //others
            kernel.Bind<ILogRepository>().To<EFLogRepository>();
            kernel.Bind<IContractorRepository>().To<EFContractorRepository>();
            //kernel.Bind<ICompanyRepository>().To<EFCompanyRepository>();
            kernel.Bind<IMProjectRepository>().To<EFMProjectRepository>();
            kernel.Bind<IProjectRepository>().To<EFProjectRepository>();
            kernel.Bind<ICurrencyRepository>().To<EFCurrencyRepository>();
            kernel.Bind<IAuthProvider>().To<DummyAuthProvider>();
            kernel.Bind<ICrewRepository>().To<EFCrewRepository>();
            kernel.Bind<ICrewWhitelistRepository>().To<EFCrewWhitelistRepository>();
            kernel.Bind<IUserRepository>().To<EFUserRepository>();
        }
    }
}