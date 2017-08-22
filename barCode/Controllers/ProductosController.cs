using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using barCode.Models;
using System.IO;

namespace barCode.Controllers
{
    public class ProductosController : Controller
    {
        private barCodePruebaEntities db = new barCodePruebaEntities();

        //GET: Productos

        int itemXpag = 3; //Elementos por pagina

        public ActionResult Index(int pagina = 1)
        {
            decimal count = db.Producto.Count();
            decimal total = Math.Ceiling(count / itemXpag);

            ViewBag.Total = total + 1;
            int salto = (pagina - 1) * itemXpag;

            var producto = db.Producto.OrderBy(x=> x.IdProducto).Skip(salto).Take(itemXpag);
            return View(producto.ToList());
        }

        //BUSCADOR CON PAGINACION
        public ActionResult Buscador(string nombre)
        {
            var query = db.Producto.Where(x => x.NombreProd.Contains(nombre));
            decimal count = query.Count();
            decimal total = Math.Ceiling(count / itemXpag);
            return View("Index", query.OrderBy(x=> x.IdProducto).Skip(0).Take(itemXpag));
        }

        //GUARDAR IMAGEN
        //[HttpPost]
        //public ActionResult agregar(Producto imgModel)
        //{
        //    string fileName = Path.GetFileNameWithoutExtension(imgModel.ImgFile.FileName);
        //    string extension = Path.GetExtension(imgModel.ImgFile.FileName);
        //    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
        //    imgModel.Foto = "~/Imagenes/" + fileName;

        //    fileName = Path.Combine(Server.MapPath("~/Imagenes/"), fileName);
        //    imgModel.ImgFile.SaveAs(fileName);
        //    {
        //        db.Producto.Add(imgModel);
        //        db.SaveChanges();
        //    }
        //    ModelState.Clear();
        //    return RedirectToAction("Index"); ;
        //}

        [HttpGet]
        public ActionResult ver(int id)
        {
            Producto imageModel = new Producto();
            {
                imageModel = db.Producto.Where(x => x.IdProducto == id).FirstOrDefault();
            }
            return View(imageModel);
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }


        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProducto,IdBoleta,IdCategoria,Foto,NombreProd,Marca,Stock,Precio,Descripcion,Eliminado")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producto);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProducto,IdBoleta,IdCategoria,Foto,NombreProd,Marca,Stock,Precio,Descripcion,Eliminado")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            db.Producto.Remove(producto);
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
    }
}
