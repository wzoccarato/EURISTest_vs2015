using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURIS.Service.Abstract;
using EURIS.Entities;


namespace EURIS.Service.Concrete
{
    public class EfProductRepository : IProductRepository, IDisposable
    {
        private bool _isdisposed = false;

        private readonly LocalDbEntities _context = new LocalDbEntities();

        #region ctor = dtor

        ~EfProductRepository()
        {
            Dispose(false);
        }

        #endregion

        #region implementazione interfaccia IProductRepository

        public IQueryable<Prodotto> Products => _context.Prodotto;

        public Prodotto DeleteProduct(int productId)
        {
            Prodotto dbEntry = _context.Prodotto.Find(productId);
            if (dbEntry != null)
            {
                _context.Prodotto.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }


        public void SaveProduct(Prodotto product)
        {
            if (product.id == 0)
            {
                _context.Prodotto.Add(product);
            }
            else
            {
                Prodotto dbEntry = _context.Prodotto.Find(product.id);
                if (dbEntry != null)
                {
                    dbEntry.codice = product.codice.Trim();
                    dbEntry.descrizione = product.descrizione;
                }
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// verifica che nel database non esista gia' un prodotto con lo stesso codice del prodotto passato in argomento,
        /// che non sia il prodotto stesso.
        /// la funzione torna con successo in due casi:
        /// 1. il prodotto passato in argomento esiste gia' all'interno del db, e codice e id corrispondono
        /// 2. il prodotto passato in argomento contiene un codice che non e' presente in alcun altro prodotto nel db
        /// questo metodo e' utilizzato negli update dei prodotti
        /// scelgo di farlo cosi', invece che imporre una chiave univoca id-codice nel database
        /// </summary>
        /// <param name="product">l'oggetto Prodotto da verificare</param>
        /// <returns>true se consistente, false in caso contrario</returns>
        public bool CodeIsConsistent(Prodotto product)
        {
            // attenzione che la comparazione e' "case insensitive"
            var prod =_context.Prodotto.FirstOrDefault(p => p.codice == product.codice.Trim());
            if(prod!= null)     
            {
                if(product.id == 0)     // questo prodotto non e' ancora stato inserito nel database
                    return false;       // esiste gia' un prodotto con questo codice
                else
                {
                    // il prodotto e' gia' stato inserito, bisogna verificare con non ce ne sia
                    // gia' un altro con lo stesso codice
                    // ritorna true se id e codice corrispondono, cioe' e' lo stesso prodotto
                    return product.id == prod.id;
                }
            }
            return true;
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
