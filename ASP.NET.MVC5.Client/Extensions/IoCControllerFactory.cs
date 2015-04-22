

namespace Pedago.ASP.NET.MVC5.Client.Extension
{
    using Infrastructure.CrossCutting.Core;
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    public class IoCControllerFactory : DefaultControllerFactory
    {
        private IContainer _Container;

        public IoCControllerFactory(IContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("serviceFactory");
            this._Container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType != (Type)null)
                return this._Container.Resolve(controllerType) as IController;
            else
                return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}
