using System;
using System.Collections.Generic;

namespace CSBusiness.Resolver
{
    /// <summary>
    /// Inversion of Control factory implementation.
    /// </summary>
    public static class CSResolve
    {
        #region Fields

        private static IDependencyResolver _resolver;

        #endregion

        #region Methods

        public static void InitializeWith(IDependencyResolverFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            _resolver = factory.CreateInstance();
        }

        public static void Register<T>(T instance)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            _resolver.Register(instance);
        }

        public static void Inject<T>(T existing)
        {
            if (existing == null)
                throw new ArgumentNullException("existing");

            _resolver.Inject(existing);
        }

        public static T Resolve<T>(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return _resolver.Resolve<T>(type);
        }

        public static T Resolve<T>(Type type, string name)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (name == null)
                throw new ArgumentNullException("name");

            return _resolver.Resolve<T>(type, name);
        }

        public static T Resolve<T>()
        {
            return _resolver.Resolve<T>();
        }

        public static T Resolve<T>(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            return _resolver.Resolve<T>(name);
        }

        public static IEnumerable<T> ResolveAll<T>()
        {
            return _resolver.ResolveAll<T>();
        }

        #endregion
    }

    public interface IDependencyResolver
    {
        /// <summary>
        /// Register instance
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="instance">Instance</param>
        void Register<T>(T instance);

        /// <summary>
        /// Inject
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="existing">Type</param>
        void Inject<T>(T existing);

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="type">Type</param>
        /// <returns>Result</returns>
        T Resolve<T>(Type type);

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="type">Type</param>
        /// <param name="name">Name</param>
        /// <returns>Result</returns>
        T Resolve<T>(Type type, string name);

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Result</returns>
        T Resolve<T>();

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="name">Name</param>
        /// <returns>Result</returns>
        T Resolve<T>(string name);

        /// <summary>
        /// Resolve all
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Result</returns>
        IEnumerable<T> ResolveAll<T>();
    }

    public interface IDependencyResolverFactory
    {
        /// <summary>
        /// Create dependency resolver
        /// </summary>
        /// <returns>Dependency resolver</returns>
        IDependencyResolver CreateInstance();
    }
}