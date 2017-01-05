
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EURIS.Service.Abstract;
using EURIS.Entities;
using EURISTest.Models;

namespace EURISTest.Controllers
{
    public class ListinoController : Controller
    {
        //
        // GET: /Listino/
        private IPricelistRepository _iplr;
        private IProductRepository _ipr;
        private IProdXListinoRepository _ipxlr;


        public ListinoController(IPricelistRepository iplr, IProductRepository ipr, IProdXListinoRepository ipxlr)
        {
            _iplr = iplr;
            _ipr = ipr;
            _ipxlr = ipxlr;
        }


        public ActionResult Index()
        {
            //ProdottoService prod = new ProdottoService();
            //List<Prodotto> prodotti = prod.GetProdotti();

            //ViewBag.Prodotti = prodotti;

            return View(new CommonViewModel
            {
                Pricelistdata = _iplr
            });
        }

        [HttpPost]
        public ActionResult Index(Listino pl)
        {
            if (Request.Form["BottoneCrea"] != null)
            {
                return RedirectToAction("Edit", new { @id = 0 });
            }
            else if (Request.Form["BottoneDelete"] != null)
            {
                if (pl != null)
                {
                    var result = _iplr.DeletePricelist(pl.id);
                    if (result != null)
                    {
                        TempData["message"] = $"{result.codice} è stato rimosso dal database";
                    }
                    else
                    {
                        TempData["message"] = "Errore nella rimozione del prodotto dal database";
                    }
                    return RedirectToAction("Index");
                }
                else
                    throw new NullReferenceException();
            }
            else if (Request.Form["BottoneManage"] != null)
            {
                return RedirectToAction("Manage", new {@id = pl.id});
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public ViewResult Edit(int id)
        {

            Listino pricelist = id > 0 ? _iplr.Pricelists.FirstOrDefault(p => p.id == id) : new Listino();
            return View(pricelist);
        }

        [HttpPost]
        public ActionResult Edit(Listino pl)
        {
            if (Request.Form["BottoneSalva"] != null)
            {
                if (ModelState.IsValid)
                {
                    // innanzitutto verifica che i dati inseriti dall'utente non contrastino
                    // con altri records gia' presenti nel database
                    if (_iplr.CodeIsConsistent(pl))
                    {
                        _iplr.SavePriceList(pl);
                        TempData["message"] = $"{pl.codice} è stato salvato";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["message"] = $"Esiste già un listino con il codice {pl.codice} a meno di minuscole/maiuscole. il listino non è stato salvato";
                        return View(pl);
                    }
                }
                else
                {
                    // i valori passati non sono corretti
                    return View(pl);
                }
            }
            else if (Request.Form["BottoneAnnulla"] != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public ActionResult Manage(int id)
        {
            ViewBag.Listino = id;
            return View(new CommonViewModel
            {
                Pricelistdata = _iplr,
                Proddata = _ipr,
                Prodxlistinodata = _ipxlr
            });
        }
    }
}
