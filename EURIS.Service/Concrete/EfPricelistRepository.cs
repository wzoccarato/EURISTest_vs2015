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

        #region implementazione interfaccia IPricelistRepository

        public IQueryable<Listino> Pricelists => _context.Listino;

        public void SavePriceList(Listino pricelist)
        {
            if (pricelist.id == 0)
            {
                _context.Listino.Add(pricelist);
            }
            else
            {
                Listino dbEntry = _context.Listino.Find(pricelist.id);
                if (dbEntry != null)
                {
                    dbEntry.codice = pricelist.codice.Trim();
                    dbEntry.descrizione = pricelist.descrizione;
                }
            }
            _context.SaveChanges();
        }

        public Listino  DeletePricelist(int pricelistId)
        {
            Listino dbEntry = _context.Listino.Find(pricelistId);
            if (dbEntry != null)
            {
                _context.Listino.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }


        /// <summary>
        /// verifica che nel database non esista gia' un listino con lo stesso codice del listino passato in argomento,
        /// che non sia il Listino stesso.
        /// la funzione torna con successo in due casi:
        /// 1. il listino passato in argomento esiste gia' all'interno del db, e codice e id corrispondono
        /// 2. il listino passato in argomento contiene un codice che non e' presente in alcun altro listino nel db
        /// questo metodo e' utilizzato negli update dei listini
        /// scelgo di farlo cosi', invece che imporre una chiave univoca id-codice nel database
        /// </summary>
        /// <param name="pricelist">l'oggetto Listino da verificare</param>
        /// <returns>true se consistente, false in caso contrario</returns>
        public bool CodeIsConsistent(Listino pricelist)
        {
            // attenzione che la comparazione e' "case insensitive"
            var pl = _context.Listino.FirstOrDefault(p => p.codice == pricelist.codice.Trim());
            if (pl != null)
            {
                if (pricelist.id == 0)  // questo listino non e' ancora stato inserito nel database
                    return false;       // esiste gia' un listino (diverso da quello passato in argomento) con questo codice
                else
                {
                    // il listino e' gia' stato inserito, bisogna verificare con non ce ne sia
                    // gia' un altro con lo stesso codice
                    // ritorna true se id e codice corrispondono, cioe' e' lo stesso listino
                    return pricelist.id == pl.id;
                }
            }
            return true;
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
