using System;
using Banking.Common.IoC;
using Banking.Core.Abstract;
using Banking.Core.Impl;
using Banking.DataAccess.Repositories;
using Banking.DataAccess.Repositories.Impl;
using Banking.DataAccess.UnitOfWork;
using Banking.Models;
using Microsoft.Practices.Unity;

namespace Banking.Api
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = Ioc.Instance;
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<ISecurityService, SecurityService>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<ITransactionRepository, TransactionRepository>();
            container.RegisterType<ITransactionService, TransactionService>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IUserService, UserService>();
           //container.RegisterType<IJsonWebToken, Banking.Common.Services.JWT.JsonWebToken>();
        }
    }
}
