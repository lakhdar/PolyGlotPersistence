using Infrastructure.CrossCutting.Core;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Pedago.ASP.NET.MVC5.Client.Extension
{
    public class UnityWebResolver : IDependencyResolver
    {
        protected IContainer _Container;

        public UnityWebResolver(IContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            this._Container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return this._Container.Resolve(serviceType);
            }
            catch (Exception ex)
            {
                return (object)null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this._Container.ResolveAll(serviceType);
            }
            catch (Exception ex)
            {
                return (IEnumerable<object>)new List<object>();
            }
        }

        public void Dispose()
        {
            this._Container.Dispose();
        }
    }
}
