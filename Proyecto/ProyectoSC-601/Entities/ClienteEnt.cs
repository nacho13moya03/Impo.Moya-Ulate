using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSC_601.Entities
{
    public class ClienteEnt
    {
        public long ID_Cliente { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "No debe contener espacios ni guiones.")]
        [StringLength(12, MinimumLength = 9, ErrorMessage = "El campo de identificación debe tener entre 9 y 12 digitos.")]

        public string Ced_Cliente { get; set; }
        public string Nombre_Cliente { get; set; }
        public string Apellido_Cliente { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(70, MinimumLength = 5, ErrorMessage = "El correo electrónico debe tener entre 5 y 70 caracteres.")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Ingresa una dirección de correo electrónico válida.")]


        public string Correo_Cliente { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string Contrasenna_Cliente { get; set; }
        public string Direccion_Cliente { get; set; }
        public string Tel_Cliente { get; set; }
        public int Est_Cliente { get; set; }
        public int Rol_Cliente { get; set; }
    }
}