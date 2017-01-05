using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using EURIS.Service.Abstract;
using EURIS.Service.Concrete;
using Ninject;

namespace EURISTest.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver,IDisposable
    {
        private bool _isdisposed = false;

        private readonly IKernel _kernel;

        #region ctor - dtor
        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        ~NinjectDependencyResolver()
        {
            Dispose(false);
        }

        #endregion

        #region implementazione interfacci IDependencyResolver
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        } 

        private void AddBindings()
        {
            // aggiungere qui tutti i binding
            _kernel.Bind<IProductRepository>().To<EfProductRepository>();
            _kernel.Bind<IPricelistRepository>().To<EfPricelistRepository>();
            _kernel.Bind<IProdXListinoRepository>().To<EfProdXListinoRepository>();
        }

        #endregion

        #region implementazione interfaccia IDispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isdisposed)
            {
                if (disposing)
                {
                    _kernel.Dispose();
                }
                // qui deve ripulire eventuali oggetti non gestiti
            }
            _isdisposed = true;
        }

        #endregion

    }
}