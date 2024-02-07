using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoSC_601.Entities
{
    public class EmpresaEnt
    {
        public long ID_Empresa { get; set; }
        public string Nombre_empresa { get; set; }
        public string Descripcion { get; set; }
        public string Ubicacion { get; set; }
    }
}