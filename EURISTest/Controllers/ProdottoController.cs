using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EURIS.Service;
using EURIS.Entities;
using EURIS.Service.Abstract;
using EURISTest.Models;

namespace EURISTest.Controllers
{
    public class ProdottoController : Controller
    {
        private IPricelistRepository _iplr;
        private IProductRepository _ipr;
        private IProdXListinoRepository _ipxlr;

        
        public ProdottoController(IPricelistRepository iplr, IProductRepository ipr, IProdXListinoRepository ipxlr)
        {
            _iplr = iplr;
            _ipr = ipr;
            _ipxlr = ipxlr;
        }


        public ViewResult Index()
        {
            //ProdottoService prod = new ProdottoService();
            //List<Prodotto> prodotti = prod.GetProdotti();

            //ViewBag.Prodotti = prodotti;

            return View(new CommonViewModel
            {
                Proddata = _ipr
            });
        }

        [HttpPost]
        public ActionResult Index(Prodotto prodotto)
        {
            if (Request.Form["BtnCrea"] != null)
            {
                return RedirectToAction("Edit",new {@id = 0});
            }
            else if (Request.Form["BtnDelete"] != null)
            {
                if (prodotto != null)
                {
                    if (_ipxlr.ProdXListino.Any(pxl => pxl.id_prodotto == prodotto.id))
                    {
                        TempData["message"] = $"Impossibile rimuovere {prodotto.codice}, in quanto è presente in uno o più listini. Rimuovere prima il prodotto dal/i listini.";
                    }
                    else
                    {
                        var result = _ipr.DeleteProduct(prodotto.id);
                        if (result != null)
                        {
                            TempData["message"] = $"{result.codice} è stato rimosso dal database";
                        }
                        else
                        {
                            TempData["message"] = "Errore nella rimozione del prodotto dal database";
                        }
                    }

                    return RedirectToAction("Index");
                }
                else
                    throw new NullReferenceException();
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
                Prodotto prod = id > 0 ? _ipr.Products.FirstOrDefault(p => p.id == id) : new Prodotto();
                return View(prod);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Prodotto prodotto)
        {
            if (Request.Form["BtnSalva"] != null)
            {
                if (ModelState.IsValid)
                {
                    // innanzitutto verifica che i dati inseriti dall'utente non contrastino
                    // con altri records gia' presenti nel database
                    if (_ipr.CodeIsConsistent(prodotto))
                    {
                        _ipr.SaveProduct(prodotto);
                        TempData["message"] = $"{prodotto.codice} è stato salvato";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["message"] = $"Esiste già un prodotto con il codice {prodotto.codice} a meno di maiuscole/minuscole. il prodotto non è stato salvato";
                        return View(prodotto);
                    }
                }
                else
                {
                    // i valori passati non sono corretti
                    return View(prodotto);
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
    }
}
