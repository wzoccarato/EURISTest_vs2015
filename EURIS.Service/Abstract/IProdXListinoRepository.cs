using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURIS.Entities;

namespace EURIS.Service.Abstract
{
    public interface IProdXListinoRepository
    {
        IQueryable<Prodotti_x_listino> ProdXListino { get; }
        void Save(Prodotti_x_listino pxl);
        Prodotti_x_listino Delete(int id);
        List<Prodotto> FilterProducts(List<Prodotto> tofilter, int idlistino);
    }
}
