using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIProyectoSC_601.Entities
{
    public class InfoContacto
    {
        public string Nombre_Completo { get; set; }
        public string Correo { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
    }
}