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
            Prodotti_x_listino dbEntry = _context.Prodotti_x_listino.Find(id);
            if (dbEntry != null)
            {
                _context.Prodotti_x_listino.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void Update(Prodotti_x_listino pxl)
        {
            if (pxl.id == 0)
            {
                _context.Prodotti_x_listino.Add(pxl);
            }
            else
            {
                Prodotti_x_listino dbEntry = _context.Prodotti_x_listino.Find(pxl.id);
                if (dbEntry != null)
                {
                    dbEntry.id_listino = pxl.id_listino;
                    dbEntry.id_prodotto = pxl.id_prodotto;
                    dbEntry.notes = pxl.notes;
                    dbEntry.valid_until = pxl.valid_until;
                    dbEntry.insert_date = pxl.insert_date;
                    dbEntry.valid_from = pxl.valid_from;
                } 
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// ritorna la lista dei prodotti passati in argomento, dalla quale sono stati filtrati tutti
        /// i prodotti contenuti in idlistino
        /// </summary>
        /// <param name="tofilter">Lista dei prodotti da filtrare</param>
        /// <param name="idlistino">id del listino da utilizzare come filtro</param>
        /// <returns>Lista dei prodotti filtrata</returns>
        public List<Prodotto> FilterProducts(List<Prodotto> tofilter,int idlistino)
        {
            if (tofilter?.Count == 0)
            {
                return new List<Prodotto>();
            }
            else if (idlistino == 0)
            {
                return tofilter;
            }
            else
            {
                List<Prodotto> result = tofilter;
                List<Prodotti_x_listino> ppl = _context.Prodotti_x_listino.Where(p => p.id_listino == idlistino).ToList();
                if (ppl.Count > 0)
                {
                    //foreach (var el in ppl)
                    //{
                    //    var product = tofilter.FirstOrDefault(pr => pr.id == el.id_prodotto);
                    //    // se lo trova in entrambi gli oggetti, lo toglie dalla lista da ritornare
                    //    if (product != null)
                    //    {
                    //        result?.Remove(product);
                    //    }
                    //}

                    // se lo trova in entrambi gli oggetti, lo toglie dalla lista da ritornare
                    foreach (var product in ppl.Select(el => tofilter.FirstOrDefault(pr => pr.id == el.id_prodotto)).Where(product => product != null))
                    {
                        result?.Remove(product);
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// per ogni prodotto_x_listino relativo all'idlistino passato in argomento,
        /// verifica se il relativo prodotto e' presente nella lista di prodotti passata in argomento
        /// SE NON E' PRESENTE lo inserisce dalla lista da ritornare.
        /// Ritorna la lista degli prodotti presenti in prodotti_x_listino,
        /// e non presenti nella lista passata in argomento 
        /// e' l'inverso del metodo precedente.
        /// </summary>
        /// <param name="idlistino">id del listino da filtrare</param>
        /// <param name="tofilter">Lista dei prodotti confrontare</param>
        /// <returns>lista dei prodotti_x_listino filtrata</returns>
        public List<Prodotti_x_listino> FilterPricelists(int idlistino,List<Prodotto> tofilter)
        {
            List<Prodotti_x_listino> result = new List<Prodotti_x_listino>();
            if (idlistino == 0)
            {
                return result;
            }
            else if (tofilter.Count == 0)
            {
                return _context.Prodotti_x_listino.Where(l => l.id_listino == idlistino).ToList();
            }
            else
            {
                var ppl = _context.Prodotti_x_listino.Where(l => l.id_listino==idlistino).ToList();
                if (ppl.Count > 0)
                {
                    //foreach (var el in ppl)
                    //{
                    //    // se non lo trova nella lista dei prodotti lo inserische nella lista filtrata
                    //    if (tofilter.FirstOrDefault(p => p.id == el.Prodotto.id) == null)
                    //    {
                    //        result.Add(el);
                    //    }
                    //}
                    // se non lo trova nella lista dei prodotti lo inserische nella lista filtrata
                    result.AddRange(ppl.Where(el => tofilter.FirstOrDefault(p => p.id == el.Prodotto.id) == null));
                }
                return result;
            }
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
