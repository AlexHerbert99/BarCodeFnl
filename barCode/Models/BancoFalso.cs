using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace barCode.Models
{
    public class RespuestaLogin
    {
        public string mensaje;
        public bool estado;
        public string observacion;
    }

    //public class RespuestaCuentas
    //{
    //    public string mensaje;
    //    public List<Cuentas> cuentas;
    //}

    public class RespuestaPagos
    {
        public string mensaje;
        public bool estado;
        public long monto;
        public long IdPedido;
    }

    public class RespuestaAnulacion
    {
        public string mensaje;
        public bool estado;
        public bool monto;
        public long IdUsuario;
        public long IdPedido;
        public DateTime fechaAnulacion;
    }

    public class RespuestaConsolidacion
    {
        public string archivo;
    }

    public class CuentasModel
    {
        public int id;
        public string numeroCuenta;
        public int idUsuario;
        public int idTipoCuenta;
    }
    public class CuentasCli
    {
        public List<CuentasModel> cuentas;
    }

}