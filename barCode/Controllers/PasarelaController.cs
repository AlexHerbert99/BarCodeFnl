using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace barCode.Controllers
{
    public class PasarelaController : Controller
    {
        // GET: Pasarela
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginBancoFalso()
        {
            return View();
        }

        public ActionResult LoginKhipu()
        {
            return View();
        }
    }
}