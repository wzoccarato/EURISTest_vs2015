using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURIS.Entities;

namespace EURIS.Service.Abstract
{
    public interface IPricelistRepository
    {
        IQueryable<Listino> Pricelists { get; }
        void SavePriceList(Listino pricelist);
        Prodotto DeletePricelist(int Id);
        bool CodeIsConsistent(Listino pricelist);
    }
}
