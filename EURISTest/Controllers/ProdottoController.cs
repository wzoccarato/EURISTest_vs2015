﻿using System;
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

        [HttpPost]
        public ActionResult Index(Prodotto prodotto)
        {
            if (Request.Form["BottoneCrea"] != null)
            {
                return RedirectToAction("Edit",new {@id = 0});
            }
            else if (Request.Form["BottoneDelete"] != null)
            {
                if (prodotto != null)
                {
                    var result =_ipr.DeleteProduct(prodotto.id);
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
            else
            {
                throw new NotImplementedException();
            }
        }

        public ViewResult Edit(int id)
        {
            
            Prodotto prod = id>0 ? _ipr.Products.FirstOrDefault(p => p.id == id) : new Prodotto();
            return View(prod);
        }

        [HttpPost]
        public ActionResult Edit(Prodotto prodotto)
        {
            if (Request.Form["BottoneSalva"] != null)
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
