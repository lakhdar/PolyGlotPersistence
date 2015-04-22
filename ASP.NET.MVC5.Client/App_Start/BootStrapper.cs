using ASP.NET.MVC5.Client.Models;
using Infrastructure.CrossCutting.Core;
using Infrastructure.CrossCutting.IoC;
using Microsoft.Owin.Security;
using Pedago.ASP.NET.MVC5.Client.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;


[assembly: PreApplicationStartMethod(typeof(ASP.NET.MVC5.Client.BootStrapper), "Start")]

namespace ASP.NET.MVC5.Client 
{
    public static class BootStrapper
    {
        public static void Start()
        {
            IoCFactory.Instance.CurrentContainer.RegisterInstance<IApplicationUserStore, ApplicationUserStore>();
            IoCFactory.Instance.CurrentContainer.RegisterType(typeof(ApplicationUserManager));
            IoCFactory.Instance.CurrentContainer.RegisterType(typeof(ApplicationSignInManager));
            IoCFactory.Instance.CurrentContainer.RegisterType(typeof(EmailManagementServices));
            IoCFactory.Instance.CurrentContainer.RegisterInstance<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication);
            DependencyResolver.SetResolver((IDependencyResolver)new UnityWebResolver(IoCFactory.Instance.CurrentContainer));
        }

        public static void Stop()
        {
        }

        private static void RegisterControllers(IContainer _currentContainer)
        {
            IEnumerable<Type> controllerTypes = Assembly.GetExecutingAssembly().GetExportedTypes().Where(x => typeof(IController).IsAssignableFrom(x));
            foreach (Type type in controllerTypes)
            {
                _currentContainer.RegisterType(type);
            }
        }
    }
}