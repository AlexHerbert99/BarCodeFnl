using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using barCode.Models;

namespace barCode.Models
{

    public class Carrito
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string ImagenProducto { get; set; }
        public string Marca { get; set; }
        public string Categoria { get; set; }
        public int Cantidad { get; set; }
        public int PrecioUnitario { get; set; }
    }

    /*
    public class Carrito : productoCarrito
    {
        List<Producto> miCarrito = new List<Producto>();
        int precioTotal;
        int cantidadProductos;


        public void Agregar(Producto pro)
        {
            if (pro.cantidad > 0)
            {
                if (pro.cantidad > 0 && pro.Precio > 0)
                {
                    if (Buscar(pro.IdProducto))
                    {
                        IncrementarCantidad(pro.IdProducto, pro.cantidad);
                    }
                    else
                    {
                        miCarrito.Add(pro);
                    }
                }
                else
                {
                    Eliminar(pro.IdProducto);
                }
            }
            Total();
        }


        public void Eliminar(int id)
        {
            Producto pro = miCarrito.SingleOrDefault(x => x.IdProducto == id);
            miCarrito.Remove(pro);
            Total();
        }


        public void Total()
        {
            foreach (var x in miCarrito)
            {
                x.montoTotal = x.Precio * x.cantidad;
            }
            precioTotal = miCarrito.Sum(x => x.montoTotal);
            Contar();
        }


        public void Contar()
        {
            cantidadProductos = miCarrito.Count();
        }


        public void Vaciar()
        {
            miCarrito = null;
        }


        public void Actualizar(int id, int cantidad)
        {
            if (cantidad > 0)
            {
                miCarrito.SingleOrDefault(x => x.IdProducto == id).cantidad = cantidad;
                Total();
            }
            else
            {
                Eliminar(id);
            }
        }


        public void IncrementarCantidad(int id, int cantidad)
        {
            if (cantidad > 0)
                miCarrito.SingleOrDefault(x => x.IdProducto == id).cantidad += cantidad;
            Total();
        }


        public bool Buscar(int id)
        {
            Producto pro = miCarrito.SingleOrDefault(x => x.IdProducto == id);
            if (pro != null)
                return true;
            else
                return false;
        }

    }
    */
}