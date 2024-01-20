using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoSC_601.Entities
{
    public class InventarioEnt
    {
        public long ID_Producto { get; set; }
        public int ID_Categoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        public int Estado { get; set; }
    }
}