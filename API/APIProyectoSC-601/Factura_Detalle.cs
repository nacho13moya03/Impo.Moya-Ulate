//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APIProyectoSC_601
{
    using System;
    using System.Collections.Generic;
    
    public partial class Factura_Detalle
    {
        public long ID_Detalle { get; set; }
        public long ID_Factura { get; set; }
        public long ID_Producto { get; set; }
        public decimal PrecioPagado { get; set; }
        public int CantidadPagado { get; set; }
        public decimal ImpuestoPagado { get; set; }
    
        public virtual Factura_Encabezado Factura_Encabezado { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
