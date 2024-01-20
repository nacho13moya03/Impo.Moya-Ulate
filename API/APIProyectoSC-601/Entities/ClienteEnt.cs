using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIProyectoSC_601.Entities
{
    public class ClienteEnt
    {
        public long ID_Cliente { get; set; }
        public string Ced_Cliente { get; set; }
        public string Nombre_Cliente { get; set; }
        public string Apellido_Cliente { get; set; }
        public string Correo_Cliente { get; set; }
        public string Contrasenna_Cliente { get; set; }
        public string Direccion_Cliente { get; set; }
        public string Tel_Cliente { get; set; }
        public int Est_Cliente { get; set; }
        public int Rol_Cliente { get; set; }
    }
}