using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EURIS.Service;
using EURIS.Entities;
using EURIS.Service.Abstract;
using EURISTest.Models;
using Ninject.Parameters;

namespace EURISTest.Controllers
{
    public class ProdottoController : Controller
    {
        private IProductRepository _ipr;

        public ProdottoController(IProductRepository ipr)
        {
            _ipr = ipr;
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

        public ViewResult Edit(int id)
        {
            Prodotto prod = _ipr.Products.FirstOrDefault(p => p.id == id);
            return View(prod);
        }

        [HttpPost]
        public ActionResult Edit(Prodotto prodotto)
        {
            if (Request.Form["BottoneSalva"] != null)
            {
                if (ModelState.IsValid)
                {
                    if (_ipr.CodeIsConsistent(prodotto))
                    {
                        _ipr.SaveProduct(prodotto);
                        TempData["message"] = $"{prodotto.codice} è stato salvato";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["message"] = $"Esiste già un prodotto con il codice {prodotto.codice}. il prodotto non è stato salvato";
                        return View(prodotto);
                    }
                }
                else
                {
                    // i valori passati non sono corretti
                    return View(prodotto);
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
    }
}
