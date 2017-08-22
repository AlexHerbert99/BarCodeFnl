using barCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using Newtonsoft.Json;

namespace barCode.Controllers
{
    public class BancoFalsoController : Controller
    {
        barCodePruebaEntities db = new barCodePruebaEntities();
        string urlbase = "http://www.devkairos.com/BancoFalsoportal/serviciosbancofalso/";
        string rut = "16243551-5";
        string clave = "123456";
        string apik = "3333333";
        List<Carrito> carro = new List<Carrito>();

        CuentasCli cmod = new CuentasCli();
        List<CuentasModel> ctasmod = new List<CuentasModel>();


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


        public JsonResult loginBanco(string user, string pass)
        {
            WebClient wc = new WebClient();
            NameValueCollection nv = new NameValueCollection();
            nv.Add("apikey", apik);
            nv.Add("user", user);
            nv.Add("pass", pass);

            byte[] result = wc.UploadValues(urlbase + "Login", nv);
            string JsonLogin = Encoding.UTF8.GetString(result);
            RespuestaLogin r = JsonConvert.DeserializeObject<RespuestaLogin>(JsonLogin);
            if (r.estado)
            {
                Session["user"] = user;
                Session["pass"] = pass;
            }
            else
            {
                Session["user"] = null;
                Session["pass"] = null;
            }

            return Json(r.estado, JsonRequestBehavior.AllowGet);
        }


        //public JsonResult traerCuentas(string rut, string clave)
        //{
        //    WebClient wc = new WebClient();
        //    NameValueCollection nvc = new NameValueCollection();
        //    nvc.Add("apikey", apik);
        //    nvc.Add("user", Session["user"].ToString());
        //    nvc.Add("pass", Session["pass"].ToString());

        //    byte[] result = wc.UploadValues(urlbase + "TraerCuentas", nvc);
        //    string JsonResult = Encoding.UTF8.GetString(result);
        //    RespuestaCuentas rc = JsonConvert.DeserializeObject<RespuestaCuentas>(JsonResult);
        //    return Json(rc, JsonRequestBehavior.AllowGet);
        //}


        public ActionResult traerCuentas(string rut, string clave)
        {
            WebClient wc = new WebClient();
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("apikey", apik);
            nvc.Add("user", Session["user"].ToString());
            nvc.Add("pass", Session["pass"].ToString());

            byte[] result = wc.UploadValues(urlbase + "TraerCuentas", nvc);
            string JsonResult = Encoding.UTF8.GetString(result);


            cmod = JsonConvert.DeserializeObject<CuentasCli>(JsonResult);
            foreach (var cta in cmod.cuentas)
            {
                ctasmod = cmod.cuentas.ToList();
            }

            //RespuestaCuentas rc = JsonConvert.DeserializeObject<RespuestaCuentas>(JsonResult);
            //return Json(rc, JsonRequestBehavior.AllowGet);
            return PartialView(ctasmod);
        }

        public JsonResult Pagar(string monto, string cuenta)
        {
            WebClient wc = new WebClient();
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("apikey", apik);
            nvc.Add("monto", monto);
            nvc.Add("descripcion", "Compra en Barcode");
            nvc.Add("idPedido", "10");
            nvc.Add("idCuenta", cuenta);

            byte[] result = wc.UploadValues(urlbase + "Pagar", nvc);
            string JsonResult = Encoding.UTF8.GetString(result);
            RespuestaPagos rp = JsonConvert.DeserializeObject<RespuestaPagos>(JsonResult);
            Session["NCuenta"] = cuenta;
            return Json(rp, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult anularCompra(int id)
        //{
        //    Boleta bol = new Boleta();

        //    bol = db.Boleta.Where(i => i.IdBoleta == id).First();

        //    WebClient wc = new WebClient();
        //    NameValueCollection nvc = new NameValueCollection();
        //    nvc.Add("apikey", apik);
        //    nvc.Add("descripcion", "Anular Compra Barcode");
        //    nvc.Add("idPedido", "10");
        //    nvc.Add("idCuenta", bol.cuentaPago.ToString());
        //    nvc.Add("monto", bol.Total.ToString());

        //    byte[] result = wc.UploadValues(urlbase + "Anular", nvc);
        //    string JsonResult = Encoding.UTF8.GetString(result);
        //    RespuestaAnulacion ra = JsonConvert.DeserializeObject<RespuestaAnulacion>(JsonResult);
        //    return Json(ra, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult anularCompra(int id)
        {
            Boleta bol = new Boleta();

            bol = db.Boleta.Where(i => i.IdBoleta == id).First();

            WebClient wc = new WebClient();
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("apikey", apik);
            nvc.Add("descripcion", "Anular Compra Barcode");
            nvc.Add("idPedido", "10");
            nvc.Add("idCuenta", bol.cuentaPago.ToString());
            nvc.Add("monto", bol.Total.ToString());

            byte[] result = wc.UploadValues(urlbase + "Anular", nvc);
            string JsonResult = Encoding.UTF8.GetString(result);
            RespuestaAnulacion ra = JsonConvert.DeserializeObject<RespuestaAnulacion>(JsonResult);
            //return Json(ra, JsonRequestBehavior.AllowGet);

            ViewBag.respuesta = ra.mensaje.ToString();

            List<Detalle> det = new List<Detalle>();

                foreach (Detalle d in db.Detalle.Where(b => b.idDetalle == id).ToList())
                {
                    Producto pro = db.Producto.Where(i => i.IdProducto == d.IdProducto).FirstOrDefault();
                    pro.Stock = (pro.Stock + d.Cantidad);
                    db.SaveChanges();
                }

                bol.estado = 0;
                db.SaveChanges();       

            ViewBag.venta = id;

            return View(ViewBag);
        }


        public ActionResult PagoOk()
        {

            carro = Session["carro"] as List<Carrito>;


            Cliente cli = new Cliente();
            cli = Session["Usuario"] as Cliente;

       

            int total = 0;
            int idBoleta = 0;
            //Boleta bol = new Boleta();

            foreach (var x in carro)
            {
                total += (x.PrecioUnitario * x.Cantidad);
            }



            using (barCodePruebaEntities Context = new barCodePruebaEntities())
            {

                //Context.Proveedores.Add(entity);
                //Context.SaveChanges();

                Boleta bol = new Boleta ();
               
                bol.IdCliente = cli.IdCliente;
                bol.Total = total;
                bol.Fecha = DateTime.Now.ToString("dd-MM-yyyy");
                bol.estado = 1;
                bol.cuentaPago = int.Parse(Session["NCuenta"].ToString());
                Context.Boleta.Add(bol);
                Context.SaveChanges();
                idBoleta = bol.IdBoleta;
            }



            foreach (var x in carro)
            {
                Producto pro = new Producto();
                pro = db.Producto.Where(i => i.IdProducto == x.IdProducto).FirstOrDefault();
                pro.Stock = pro.Stock - x.Cantidad;
                db.SaveChanges();

                Detalle det = new Detalle();
                det.idDetalle = idBoleta;
                det.Cantidad = x.Cantidad;
                det.Total = (x.PrecioUnitario * x.Cantidad);
                det.IdProducto = x.IdProducto;
                db.Detalle.Add(det);
                db.SaveChanges();

            }


            Session["carro"] = null;
            Session["NCuenta"] = null;


            return View();
        }

        //public ActionResult seleccionarCuenta()
        //{
        //    var cuentas = new List<Seleccionar>();
        //    cuentas.Add(new Seleccionar()
        //    {
        //        id = 1,
        //        Nombre = "Cuenta Corriente",
        //        idCuenta = 154
        //    });
        //    cuentas.Add(new Seleccionar()
        //    {
        //        id = 2,
        //        Nombre = "Cuenta Vista",
        //        idCuenta = 155
        //    });

        //    var list = new SelectList(cuentas, "idCuenta");
        //    ViewData["cuentas"] = list;
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult seleccionarCuenta(int cuenta)
        //{
        //    return View();
        //}

    }
}



