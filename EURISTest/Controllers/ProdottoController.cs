using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EURIS.Service;
using EURIS.Entities;

namespace EURISTest.Controllers
{
    public class ProdottoController : Controller
    {
        //
        // GET: /Prodotto/

        public ActionResult Index()
        {
            ProdottoService prod = new ProdottoService();
            List<Prodotto> prodotti = prod.GetProdotti();

            ViewBag.Prodotti = prodotti;

            return View();
        }

    }
}
