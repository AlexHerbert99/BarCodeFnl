
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------


namespace barCode.Models
{

using System;
    using System.Collections.Generic;
    
public partial class Producto
{

    public int IdProducto { get; set; }

    public string IdCategoria { get; set; }

    public string Foto { get; set; }

    public string NombreProd { get; set; }

    public string Marca { get; set; }

    public Nullable<int> Stock { get; set; }

    public int Precio { get; set; }

    public string Descripcion { get; set; }

    public Nullable<bool> Eliminado { get; set; }

    public string Grado { get; set; }

}

}
