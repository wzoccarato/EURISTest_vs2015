using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EURIS.Service;
using EURIS.Entities;
using EURIS.Service.Abstract;

namespace EURISTest.Controllers
{
    public class ProdottoController : Controller
    {
        private IProductRepository _ipr;

        public ProdottoController(IProductRepository ipr)
        {
            _ipr = ipr;
        }

        
        
        public ActionResult Index()
        {
            //ProdottoService prod = new ProdottoService();
            //List<Prodotto> prodotti = prod.GetProdotti();

            //ViewBag.Prodotti = prodotti;

            return View();
        }

    }
}
