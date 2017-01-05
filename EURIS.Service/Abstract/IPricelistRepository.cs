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
        Listino GetPricelist(int pricelistid);
        void SavePriceList(Listino pricelist);
        Listino DeletePricelist(int pricelistid);
        bool CodeIsConsistent(Listino pricelist);
        string GetCodice(int pricelistid);
        string GetDescrizione(int pricelistid);

    }
}
