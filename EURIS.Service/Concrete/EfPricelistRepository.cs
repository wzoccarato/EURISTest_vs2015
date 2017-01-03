using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURIS.Entities;
using EURIS.Service.Abstract;

namespace EURIS.Service.Concrete
{
    public class EfPricelistRepository:IPricelistRepository,IDisposable
    {
        private bool _isdisposed = false;

        private readonly LocalDbEntities _context = new LocalDbEntities();

        #region ctor = dtor

        ~EfPricelistRepository()
        {
            Dispose(false);
        }

        #endregion

        #region implementazione interfaccia IProductRepository

        public IQueryable<Listino> Pricelists { get; }
        public void SavePriceList(Listino pricelist)
        {
            throw new NotImplementedException();
        }

        public Prodotto DeletePricelist(int Id)
        {
            throw new NotImplementedException();
        }

        public bool CodeIsConsistent(Listino pricelist)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region implementazione interfaccia IDispose 
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
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
                    _context.Dispose();
                }
                // qui deve ripulire eventuali oggetti non gestiti
            }
            _isdisposed = true;
        }

        #endregion
    }
}
