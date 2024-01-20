using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIProyectoSC_601.Entities
{
    public class FacturaEnt
    {
        public string CorreoCliente { get; set; }
        public long ID_Factura { get; set; }
        public long ID_Usuario { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal TotalCompra { get; set; }

        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioPagado { get; set; }
        public int CantidadPagado { get; set; }
        public decimal ImpuestoPagado { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Impuesto { get; set; }

        public decimal Total { get; set; }
    }
}