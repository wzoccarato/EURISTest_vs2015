﻿using System;
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
        /// verifica che nel database non esistano prodotti con lo stesso codice del prodotto passato in rgomento
        /// ls funzione torna con successo in due casi>
        /// 1. il prodotto passato in argomento esiste gia' all-interno del db, e codice e id corrispondono
        /// 2. il prodotto passato in argomento contioene un codice che non [ p[resente in alcun altro prodotto presente nel db
        /// questo metodo e' utilizzato negli update dei prodotti
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool CodeIsConsistent(Prodotto product)
        {
            var prod =_context.Prodotto.FirstOrDefault(p => p.codice == product.codice);
            if(prod!= null)     
            {
                if(product.id == 0)     // questo proidotto non e' ancora stato inserito nel database
                    return false;       // esiste gia' un prodotto con questo codice
                else
                {
                    // il prodotto e' gia' stato inserito, bisogna verificare con non ce ne sia
                    // gia' un altro con lo stesso codice
                    // ritorna true se id e codice corrispondono, cioe' e' lo stesso prodotto
                    return product.codice == prod.codice.Trim() && product.id == prod.id;
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
