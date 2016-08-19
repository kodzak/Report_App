using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using ReportApp.Domain.Entities;
using ReportApp.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using ReportApp.Domain.Concrete;
using Moq;

namespace ReportApp.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext
        requestContext, Type controllerType)
        {
            return controllerType == null
            ? null
            : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            ninjectKernel.Bind<IEventRepository>().To<EFEventRepository>();
            ninjectKernel.Bind<IReportRepository>().To<EFReportRepository>();
            ninjectKernel.Bind<ISubjectRepository>().To<EFSubjectRepository>();
        }
    }
}