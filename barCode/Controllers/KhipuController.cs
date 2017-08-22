//using Khipu.Api;
//using Khipu.Client;
//using Khipu.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace barCode.Controllers
//{
//    public class KhipuController : Controller
//    {
//        // GET: Khipu
//        public ActionResult Index()
//        {
//            return View();
//        }

//        string secret = "5a042003cc116dd72b6e334ede10f95810abc195";
//        int idReceiver = 137704;

//        public JsonResult crearPago()
//        {
//            Configuration.ReceiverId = idReceiver;
//            Configuration.Secret = secret;
//            PaymentsApi a = new PaymentsApi();

//            try
//            {
//                DateTime dt = DateTime.Now;
//                dt = dt.AddDays(5);
//                PaymentsCreateResponse response = a.PaymentsPost("Compra de prueba de la API", "CLP", 100.0
//                                                , transactionId: "FACT2001"
//                                                , expiresDate: dt
//                                                , body: "Descripción de la compra"
//                                                , pictureUrl: "http://mi-ecomerce.com/pictures/foto-producto.jpg"
//                                                , returnUrl: "http://mi-ecomerce.com/backend/return"
//                                                , cancelUrl: "http://mi-ecomerce.com/backend/cancel"
//                                                , notifyUrl: "http://mi-ecomerce.com/backend/notify"
//                                                , notifyApiVersion: "1.3");
//                return Json(response);
//            }
//            catch (ApiException e)
//            {
//                return Json(e, JsonRequestBehavior.AllowGet);
//            }
//        }

//        //public ActionResult EnviarMail()
//        //{
//        //    Configuration.ReceiverId = idReceiver;
//        //    Configuration.Secret = secret;
//        //    PaymentsApi a = new PaymentsApi();

//        //    try
//        //    {
//        //        PaymentsCreateResponse response = a.PaymentsPost("Compra de prueba de la API", "CLP", 100.0
//        //                            , sendEmail: true
//        //                            , sendReminders: true
//        //                            , payerEmail: "juan.pagador@correo.com"
//        //                            , payerName: "Juan Pagador");
//        //        System.Console.WriteLine(response);
//        //    }
//        //    catch (ApiException e)
//        //    {
//        //        return Json(e, JsonRequestBehavior.AllowGet);
//        //    }
        
//    }
//}