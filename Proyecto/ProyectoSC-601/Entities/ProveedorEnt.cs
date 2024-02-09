using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ProyectoSC_601.Entities
{
    public class ProveedorEnt
    {
        public long ID_Proveedor { get; set; }
        public int ID_Identificacion { get; set; }
        public string Nombre_Identificacion { get; set; }
        public string Nombre_Proveedor { get; set; }
        public string Apellido_Proveedor { get; set; }
        public string Cedula_Proveedor { get; set; }
        public string Direccion_Exacta { get; set; }
        public int Estado_Proveedor { get; set; }
        public long Empresa { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
    }
}