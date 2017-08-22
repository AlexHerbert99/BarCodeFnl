using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using barCode.Models;
using System.Net;

namespace barCode.Controllers
{
    public class CatalogoController : Controller
    {

        barCodePruebaEntities db = new barCodePruebaEntities();

        int artXpag = 4;
        // GET: Catalogo
        public ActionResult Index(int pagina = 1)
        {
            List<Producto> x = db.Producto.ToList();

            decimal count = db.Producto.Count();
            decimal total = Math.Ceiling(count / artXpag);

            ViewBag.Total = total + 1;
            int salto = (pagina - 1) * artXpag;

            var pro = db.Producto.OrderBy(z=> z.IdProducto).Skip(salto).Take(artXpag);

            return View(pro.ToList());
        }

        //BUSCADOR
        public ActionResult Buscador(string nombre)
        {
            var query = db.Producto.Where(x => x.NombreProd.Contains(nombre));
            decimal count = query.Count();
            decimal total = Math.Ceiling(count / artXpag);
            return View("Index", query.OrderBy(x => x.IdProducto).Skip(0).Take(artXpag));
        }

    }
}