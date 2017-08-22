using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using barCode.Models;

namespace barCode.Controllers
{
    public class LoginController : Controller
    {
        barCodePruebaEntities db = new barCodePruebaEntities();

        public ActionResult Index()
        {
            return View();
        }     

        public ActionResult noEncontrado()
        {
            return View();
        } 

        public ActionResult logout()
        {
            int ok = 0;
            if (Session["Usuario"] != null) { Session["Usuario"] = null; ok++; }
            if (Session["NomUsuario"] != null) { Session["NombUsuario"] = null; ok++; }
         

            if (ok == 2)
                return Redirect("~/Catalogo/Index");
            else
                return Redirect("~/Login/noEncontrado");
        }
      

        [HttpPost]
        public ActionResult Validar(string Login, string Contraseña)
        {
            var buscarLogin = db.Cliente.SingleOrDefault(x => x.User == Login && x.Pass == Contraseña);

            if (buscarLogin == null)
            {
                ViewBag.error = "Usuario y/o Contraseña Incorrecto";
                return View("noEncontrado");
            }
            else
            {
                Session["Usuario"] = buscarLogin;
                Session["NomUsuario"] = buscarLogin.Nombres;
                return Redirect("~/Carrito/Index/");
            }
        }
    }
}