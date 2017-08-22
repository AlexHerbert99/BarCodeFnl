using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using barCode.Models;

namespace barCode.Controllers
{
    public class CarritoController : Controller

    {
        barCodePruebaEntities db = new barCodePruebaEntities();
        List<Carrito> carro = new List<Carrito>();

        // GET: Carrito

        public ActionResult Index()
        {
            if (Session["carro"] == null)
            {
                Session["carro"] = carro;
            }
            else
            {
                carro = (List<Carrito>)Session["carro"];
            }

            return View(carro);
        }

        public ActionResult Agregar(int id)
        {
            Producto p = db.Producto.Find(id);            

            if (Session["carro"] != null)
                carro = (List<Carrito>)Session["carro"];

            if (carro.Exists(x => x.IdProducto == id))
            {
                carro.SingleOrDefault(z => z.IdProducto == id).Cantidad++;
                //var Chela = carro.SingleOrDefault(x => x.IdProducto == id);
                //carro.SingleOrDefault(x => x.IdProducto == id).montoTotal = Chela.cantidad * Chela.Precio;
                
            }
            else
            {
                //p.montoTotal = p.Precio * p.cantidad;

                carro.Add(new Carrito { Cantidad=1, IdProducto=id, PrecioUnitario=p.Precio, Categoria=p.IdCategoria, ImagenProducto=p.Foto, NombreProducto=p.NombreProd, Marca=p.Marca });
            }

            Session["carro"] = carro;
            return View("Index", carro);
        }
        
        public ActionResult Borrar(int id)
        {
            if (Session["carro"] != null)
            carro = (List<Carrito>)Session["carro"];
            Carrito pro = carro.SingleOrDefault(x => x.IdProducto == id);
            carro.Remove(pro);
            return View("Index", carro);
        }

        public ActionResult Buscador(string Nombre)
        {
            var Birra = db.Producto.Where(x => x.NombreProd.Contains(Nombre));
            return View("Carrito", Birra);
        }

        public ActionResult cancelarCarrito()
        {
            Session["carro"] = null;
            return View("Index");
        }

        public ActionResult Pendiente()
        {
            return View("Pendiente");
        } 

        public int totalCarrito()
        {
            int total = 0;
            //Producto p = new Producto();
            //ViewBag.total = p.Precio * p.Cantidad;
            return (total);
        }
    }
}