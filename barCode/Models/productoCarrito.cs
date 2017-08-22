using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace barCode.Models
{
    public class productoCarrito
    {
        public int cantidad { set; get; }
        public int montoTotal { set; get; }

        public HttpPostedFile ImgFile { set; get; }
    }
}