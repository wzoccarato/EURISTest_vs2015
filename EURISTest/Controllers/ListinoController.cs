
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
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
            return View(new CommonViewModel
            {
                Pricelistdata = _iplr
            });
        }

        [HttpPost]
        public ActionResult Index(Listino pl)
        {
            if (Request.Form["BtnCrea"] != null)
            {
                return RedirectToAction("Edit", new { @id = 0 });
            }
            else if (Request.Form["BtnDelete"] != null)
            {
                if (pl != null)
                {
                    var pxl = _ipxlr.GetPriceListProducts(pl.id);
                    if (pxl?.Count > 0)
                    {
                        TempData["message"] = $"Impossibile rimuovere {pl.codice}, perché contiene dei prodotti configurati al suo interno. Rimuovere prima i prodotti.";
                    }
                    else
                    {
                        var result = _iplr.DeletePricelist(pl.id);
                        if (result != null)
                        {
                            TempData["message"] = $"{result.codice} è stato rimosso dal database";
                        }
                        else
                        {
                            TempData["message"] = "Errore nella rimozione del listino dal database";
                        }
                    }
                    return RedirectToAction("Index");
                }
                else
                    throw new NullReferenceException();
            }
            else if (Request.Form["BtnManage"] != null)
            {
                return RedirectToAction("Manage", new {@id = pl.id});
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                Listino pricelist = id > 0 ? _iplr.Pricelists.FirstOrDefault(p => p.id == id) : new Listino();
                return View(pricelist);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Listino pl)
        {
            if (Request.Form["BtnSalva"] != null)
            {
                if (ModelState.IsValid)
                {
                    var pxl = _ipxlr.GetPriceListProducts(pl.id);
                    if (pxl?.Count > 0)
                    {
                        TempData["message"] = $"Impossibile modificare {pl.codice}, perché contiene dei prodotti configurati al suo interno. Rimuovere prima i prodotti.";
                        return View(pl);
                    }
                    else
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
                }
                else
                {
                    // i valori passati non sono corretti
                    return View(pl);
                }
            }
            else if (Request.Form["BtnAnnulla"] != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public ActionResult Manage(int? id)
        {
            if (id != null)
            {
                ViewBag.Listino = id;
                return View(new CommonViewModel
                {
                    Pricelistdata = _iplr,
                    Proddata = _ipr,
                    Prodxlistinodata = _ipxlr
                });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JavaScriptResult RequestUpdateListino(string idlistino, string jsonids)
        {
            UnicodeEncoding uniEncoding = new UnicodeEncoding();
            MemoryStream stream = new MemoryStream(uniEncoding.GetBytes(jsonids));
            stream.Position = 0;
            DataContractJsonSerializer JSC = new DataContractJsonSerializer(typeof(int[]));
            int[] items = (int[])JSC.ReadObject(stream);
            var idl = Convert.ToInt32(idlistino);

            // in products la lista completa dei prodotti che devono costituire il nuovo listino
            var products = items.Select(el => _ipr.Products.FirstOrDefault(p => p.id == el)).ToList();

            // questa e' la lista degli elementi da cancellare dal listino originale, 
            // cioè quelli che sono stati rimossi dall'utente
            var todelete = _ipxlr.FilterPricelists(idl, products);
            foreach (var el in todelete)
            {
                _ipxlr.Delete(el.id);
            }

            if (items.Count() > 0)
            {
                // esegue l'aggiornamento del nuovo listino
                foreach (var el in items)
                {
                    var idp = Convert.ToInt32(el);
                    Prodotti_x_listino ppl = _ipxlr.ProdXListino.FirstOrDefault(p => p.id_listino == idl && p.id_prodotto == idp);
                    if (ppl == null)
                    {
                        ppl = new Prodotti_x_listino();
                        ppl.id_listino = idl;
                        ppl.id_prodotto = idp;
                        ppl.insert_date = DateTime.Now;
                        _ipxlr.Update(ppl);
                    }
                    else
                    {
                        _ipxlr.Update(ppl);
                    }
                }

                var pl = _iplr.GetPricelist(idl);
                TempData["Message"] = $"{pl.codice} è stato salvato";
            }
            else
            {
                var pl = _iplr.GetPricelist(idl);
                TempData["Message"] = $"{pl.codice} non è stato salvato, in quanto non contiene prodotti";
            }
            

            var script = string.Format("PageReload()");
            return JavaScript(script);
        }
    }
}
