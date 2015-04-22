

namespace Infrastructure.CrossCutting.IoC.Unity
{
    using Application.BoundedConext.ERPModule;
    using Application.BoundedConext.StoreModule;
    using Application.BoundedContext.MembershipModule;
    using Domain.BoundedContext.ERPModule;
    using Domain.BoundedContext.StoreModule;
    using Domain.BoundedContext.MembershipModule;
    using Infrastructure.CrossCutting.Core;
    using Infrastructure.CrossCutting.IoC.Resources;
    using Infrastructure.CrossCutting.NetFramework.Logging;
    using Infrastructure.Data.BoundedContext.ERPModule;
    using Infrastructure.Data.BoundedContext.StoreModule;
    using Infrastructure.Data.BoundedContext.MembershipModule.Repositories;
    using Infrastructure.Data.BoundedContext.Repositories.ERPModule;
    using Infrastructure.Data.BoundedContext.UnitOfWork;
    using Infrastructure.Data.BoundedContext.UnitOfWork.Lucene;
    using Infrastructure.Data.BoundedContext.UnitOfWork.Mongo;
    using Microsoft.Practices.Unity;
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    /// <summary>
    /// Implemented container in Microsoft Practices Unity
    /// </summary>
    sealed class IoCUnityContainer
        : IContainer
    {
        #region Members

        IDictionary<string, IUnityContainer> _ContainersDictionary;


        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of IoCUnitContainer
        /// </summary>
        public IoCUnityContainer()
        {
            _ContainersDictionary = new Dictionary<string, IUnityContainer>();

            //Create root container
            IUnityContainer rootContainer = new UnityContainer();
            _ContainersDictionary.Add("RootContext", rootContainer);

            //Create container for real context, child of root container
            IUnityContainer realAppContainer = rootContainer.CreateChildContainer();
            _ContainersDictionary.Add("RealApplicationContext", realAppContainer);

            //Create container for testing, child of root container
            IUnityContainer fakeAppContainer = rootContainer.CreateChildContainer();
            _ContainersDictionary.Add("FakeAppContext", fakeAppContainer);

            ConfigureRootContainer(rootContainer);
            ConfigureRealContainer(realAppContainer);
            ConfigureFakeContainer(fakeAppContainer);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Configure root container.Register types and life time managers for unity builder process
        /// </summary>
        /// <param name="container">Container to configure</param>
        void ConfigureRootContainer(IUnityContainer container)
        {
            //repositories
            container.RegisterType<IAddressRepository, AddressRepository>(new TransientLifetimeManager());
            container.RegisterType<ICustomerRepository, CustomerRepository>(new TransientLifetimeManager());
            container.RegisterType<IDepartmentRepository, DepartmentRepository>(new TransientLifetimeManager());
            container.RegisterType<IOrganizationRepository, OrganizationRepository>(new TransientLifetimeManager());
            container.RegisterType<IPositionRepository, PositionRepository>(new TransientLifetimeManager());
            container.RegisterType<IUserRepository, UserRepository>(new TransientLifetimeManager());
            container.RegisterType<IEquipmentAggregateRepository, EquipmentAggregateRepository>(new TransientLifetimeManager());



            //Managers
            container.RegisterType<IUserManagementServices, UserManagementServices>(new TransientLifetimeManager());
            container.RegisterType<IOrganizationManagementService, OrganizationManagementService>(new TransientLifetimeManager());
            container.RegisterType<IEquipmentAggregateManagementService, EquipmentAggregateManagementService>(new TransientLifetimeManager());
            container.RegisterType<IDepartmentAggregateRepository, DepartmentAggregateRepository>(new TransientLifetimeManager());
            container.RegisterType<IDepartmentAggregateManagementService, DepartmentAggregateManagementService>(new TransientLifetimeManager());

            
            container.RegisterType<IAzureBlobUnitOfWork, AzureBlobUnitOfWork>(new HierarchicalLifetimeManager(), new InjectionConstructor());

            container.RegisterType<ILogger, TraceManager>(new TransientLifetimeManager());
        }

        /// <summary>
        /// Configure real container. Register types and life time managers for unity builder process
        /// </summary>
        /// <param name="container">Container to configure</param>
        void ConfigureRealContainer(IUnityContainer container)
        {
            container.RegisterType<IMainBCUnitOfWork, MainBCUnitOfWork>(new HierarchicalLifetimeManager(), new InjectionConstructor());
            container.RegisterType<ILuceneBCUnitOfWork, LuceneBcUnitOfWork>(new HierarchicalLifetimeManager(), new InjectionConstructor());
            container.RegisterType<IMongoUnitOfWork, MongoUnitOfWork>(new HierarchicalLifetimeManager(), new InjectionConstructor());
            
        }

        /// <summary>
        /// Configure fake container.Register types and life time managers for unity builder process
        /// </summary>
        /// <param name="container">Container to configure</param>
        void ConfigureFakeContainer(IUnityContainer container)
        {
            container.RegisterType<IMainBCUnitOfWork, FakeMainBCUnitOfWork>(new HierarchicalLifetimeManager(), new InjectionConstructor());
            container.RegisterType<ILuceneBCUnitOfWork, FakeLucenBCUnitOfWork>(new HierarchicalLifetimeManager(), new InjectionConstructor());
            container.RegisterType<IMongoUnitOfWork, MongoUnitOfWork>(new HierarchicalLifetimeManager(), new InjectionConstructor());

        }

        #endregion

        #region IServiceFactory Members

        /// <summary>
        /// <see cref="M:Infrastructure.CrossCutting.Core.IContainer.Resolve{TService}"/>
        /// </summary>
        /// <typeparam name="TService"><see cref="M:Infrastructure.CrossCutting.Core.IContainer.Resolve{TService}"/></typeparam>
        /// <returns><see cref="M:Infrastructure.CrossCutting.Core.IContainer.Resolve{TService}"/></returns>
        public TService Resolve<TService>()
        {
            //We use the default container specified in AppSettings
            string containerName = ConfigurationManager.AppSettings["defaultIoCContainer"];

            if (String.IsNullOrEmpty(containerName)
                ||
                String.IsNullOrWhiteSpace(containerName))
            {
                containerName = "RealApplicationContext";
            }

            if (!_ContainersDictionary.ContainsKey(containerName))
                throw new InvalidOperationException(Messages.ContainerNotFoundException);

            IUnityContainer container = _ContainersDictionary[containerName];

            return container.Resolve<TService>();
        }
        /// <summary>
        /// <see cref="M:Infrastructure.CrossCutting.Core.IContainer.Resolve"/>
        /// </summary>
        /// <param name="type"><see cref="M:Infrastructure.CrossCutting.Core.IContainer.Resolve"/></param>
        /// <returns><see cref="M:Infrastructure.CrossCutting.Core.IContainer.Resolve"/></returns>
        public object Resolve(Type type)
        {
            //We use the default container specified in AppSettings
            string containerName = ConfigurationManager.AppSettings["defaultIoCContainer"];

            if (String.IsNullOrEmpty(containerName)
                ||
                String.IsNullOrWhiteSpace(containerName))
            {
                containerName = "RealApplicationContext";
            }

            if (!_ContainersDictionary.ContainsKey(containerName))
                throw new InvalidOperationException(Messages.ContainerNotFoundException);

            IUnityContainer container = _ContainersDictionary[containerName];

            return container.Resolve(type, null);
        }

        /// <summary>
        /// <see cref="M:Infrastructure.CrossCutting.Core.IContainer.RegisterType"/>
        /// </summary>
        /// <param name="type"><see cref="M:Infrastructure.CrossCutting.Core.IContainer.RegisterType"/></param>
        public void RegisterType(Type type)
        {
            IUnityContainer container = this._ContainersDictionary["RootContext"];

            if (container != null)
                container.RegisterType(type, new TransientLifetimeManager());
        }

        /// <summary>
        /// <see cref="M:Infrastructure.CrossCutting.Core.IContainer.ResolveAll"/>
        /// </summary>
        /// <param name="type"><see cref="M:Infrastructure.CrossCutting.Core.IContainer.RegisterType"/></param>
        public IEnumerable<object> ResolveAll(Type type)
        {
            string containerName = ConfigurationManager.AppSettings["defaultIoCContainer"];

            if (String.IsNullOrEmpty(containerName)
                ||
                String.IsNullOrWhiteSpace(containerName))
            {
                containerName = "RealApplicationContext";
            }

            if (!_ContainersDictionary.ContainsKey(containerName))
                throw new InvalidOperationException(Messages.ContainerNotFoundException);

            IUnityContainer container = _ContainersDictionary[containerName];

            return container.ResolveAll(type);
        }

        /// <summary>
        /// <see cref="M:Infrastructure.CrossCutting.Core.IContainer.CreateChildContainer"/>
        /// </summary>
        /// <return><see cref="M:Infrastructure.CrossCutting.Core.IContainer.CreateChildContainer"/></return>
        public IContainer CreateChildContainer()
        {
            return this;
        }

        /// <summary>
        /// <see cref="M:Infrastructure.CrossCutting.Core.IContainer"/>
        /// </summary>
        /// <typeparam name="TFrom"> <see cref="M:Infrastructure.CrossCutting.Core.IContainer"/></typeparam>
        /// <typeparam name="TTo"> <see cref="M:Infrastructure.CrossCutting.Core.IContainer"/></typeparam>
        public void RegisterInstance<TInterface>(Func<IDisposable, object> instance)
        {
            IUnityContainer container = this._ContainersDictionary["RootContext"];
            if (container == null)
                return;
            container.RegisterType<TInterface>(new InjectionFactory((Func<IUnityContainer, object>)instance));
        }

        /// <summary>
        /// <see cref="M:Infrastructure.CrossCutting.Core.IContainer"/>
        /// </summary>
        /// <typeparam name="TFrom"> <see cref="M:Infrastructure.CrossCutting.Core.IContainer"/></typeparam>
        /// <typeparam name="TTo"> <see cref="M:Infrastructure.CrossCutting.Core.IContainer"/></typeparam>
        public void RegisterInstance<TFrom, TTo>() where TTo : TFrom
        {
            IUnityContainer container = this._ContainersDictionary["RootContext"];
            if (container == null)
                return;

            container.RegisterType<TFrom, TTo>(new TransientLifetimeManager());
        }


        public void Dispose()
        {
        }

        #endregion





    }
}

