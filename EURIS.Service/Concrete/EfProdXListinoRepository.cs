using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURIS.Entities;
using EURIS.Service.Abstract;

namespace EURIS.Service.Concrete
{
    public class EfProdXListinoRepository : IProdXListinoRepository, IDisposable
    {
        private bool _isdisposed = false;

        private readonly LocalDbEntities _context = new LocalDbEntities();

        #region ctor = dtor

        ~EfProdXListinoRepository()
        {
            Dispose(false);
        }

        #endregion

        #region implementazione interfaccia IProdXListinoRepository

        public IQueryable<Prodotti_x_listino> ProdXListino => _context.Prodotti_x_listino;

        public Prodotti_x_listino Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Prodotti_x_listino pxl)
        {
            throw new NotImplementedException();
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
                    _context.Dispose();
                }
                // qui deve ripulire eventuali oggetti non gestiti
            }
            _isdisposed = true;
        }

        #endregion

    }
}
