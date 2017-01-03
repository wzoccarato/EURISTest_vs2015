using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURIS.Entities;

namespace EURIS.Service.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Prodotto> Products { get; }
        void SaveProduct(Prodotto product);
        Prodotto DeleteProduct(int productId);
        bool CodeIsConsistent(Prodotto product);
    }
}
