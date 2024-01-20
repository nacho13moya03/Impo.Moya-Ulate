using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIProyectoSC_601.Entities
{
    public class CarritoEnt
    {
        public long ID_Carrito { get; set; }
        public long ID_Usuario { get; set; }
        public long ID_Producto { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaCarrito { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
    }
}