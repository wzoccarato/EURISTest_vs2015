
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EURIS.Service.Abstract;

namespace EURISTest.Controllers
{
    public class ListinoController : Controller
    {
        //
        // GET: /Listino/
        private IPricelistRepository _iplr;


        public ListinoController(IPricelistRepository iplr)
        {
            _iplr = iplr;
        }




        public ActionResult Index()
        {
            return View();
        }

    }
}
