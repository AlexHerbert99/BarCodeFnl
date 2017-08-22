using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using barCode.Models;

namespace barCode.Controllers
{
    public class ClientesController : Controller
    {
        private barCodePruebaEntities db = new barCodePruebaEntities();

        int itemXpag = 3;
        public ActionResult Index(int pagina = 1)
        {
            decimal count = db.Cliente.Count();
            decimal total = Math.Ceiling(count / itemXpag);
            ViewBag.Total = total + 1;
            int salto = (pagina - 1) * itemXpag;
            var cli = db.Cliente.OrderBy(x => x.Rut).Skip(salto).Take(itemXpag);
            return View(db.Cliente.ToList());
        }

        public ActionResult Buscador(string nombre)
        {
            var query = db.Cliente.Where(x => x.Nombres.Contains(nombre));
            decimal count = query.Count();
            decimal total = Math.Ceiling(count / itemXpag);
            return View("index", query);
        }

        //VALIDAR RUT
        public bool validarRut(string rut)
        {
            bool validacion = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
            }
            return validacion;
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult Create([Bind(Include = "IdCliente,Rut,Nombres,ApPaterno,ApMaterno,Telefono,User,Pass,fechaNacimiento")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Cliente.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index", "Login");
            }

            return RedirectToAction("Create");
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCliente,Rut,Nombres,ApPaterno,ApMaterno,Telefono,User,Pass,fechaNacimiento")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            db.Cliente.Remove(cliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult Compras()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("");
            }
            else
            {
                Cliente cli = (Cliente)Session["Usuario"];
                int uid = cli.IdCliente;

                List<Boleta> bol = new List<Boleta>();

                bol = db.Boleta.Where(i => i.IdCliente == uid).Where(e => e.estado==1).ToList();

                return View(bol);
            }

        }

        public ActionResult DetalleVenta(int id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("");
            }
            else
            {

                List<Carrito> car = new List<Carrito>();

                foreach (Detalle d in db.Detalle.Where(i => i.idDetalle == id).ToList())
                {
                    Producto p = db.Producto.Where(i => i.IdProducto == d.IdProducto).FirstOrDefault();


                    car.Add(new Carrito
                    {
                        Cantidad = d.Cantidad,
                        PrecioUnitario = (d.Total / d.Cantidad),
                        Categoria = p.IdCategoria,
                        ImagenProducto = p.Foto,
                        NombreProducto = p.NombreProd,
                        Marca = p.Marca
                    });
                }

                return View(car);
            }
        }


    }
}
