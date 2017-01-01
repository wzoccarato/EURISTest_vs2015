using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURIS.Entities;
using System.Data.Entity;

namespace EURIS.Service
{
    public class ProdottoService
    {
        LocalDbEntities context = new LocalDbEntities(); 

        public List<Prodotto> GetProdotti()
        {
            List<Prodotto> prodotti = new List<Prodotto>();
            
            prodotti = (from item in context.Prodotto
                          select item).ToList();

            return prodotti;
        }
    }
}
