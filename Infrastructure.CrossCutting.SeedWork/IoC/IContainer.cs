namespace Infrastructure.CrossCutting.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Base contract for locator and register dependencies
    /// </summary>
    public interface IContainer : IDisposable
    {
        /// <summary>
        /// Solve TService dependency
        /// </summary>
        /// <typeparam name="TService">Type of dependency</typeparam>
        /// <returns>instance of TService</returns>
        TService Resolve<TService>();

        /// <summary>
        /// Solve type construction and return the object as a TService instance
        /// </summary>
        /// <returns>instance of this type</returns>
        object Resolve(Type type);

        /// <summary>
        /// Register type into service locator
        /// </summary>
        /// <param name="type">Type to register</param>
        void RegisterType(Type type);

        /// <summary>
        /// Solve type construction and return the list of resolved objects  
        /// </summary>
        /// <returns>instance of this type</returns>
        IEnumerable<object> ResolveAll(Type type);

        /// <summary>
        /// create a new child container  
        /// </summary>
        /// <returns>child as instance of IContainer</returns>
        IContainer CreateChildContainer();

        /// <summary>
        /// register a type with specific member
        /// </summary>
        /// <typeparam name="TInterface">type interface</typeparam>
        /// <param name="instance">instance member</param>
        void RegisterInstance<TInterface>(Func<IDisposable, object> instance);

        /// <summary>
        /// Regiter a type mmappin with a given container
        /// </summary>
        /// <typeparam name="TFrom">type interface </typeparam>
        /// <typeparam name="TTo">type instance </typeparam>
        void RegisterInstance<TFrom, TTo>() where TTo : TFrom;

    }
}

