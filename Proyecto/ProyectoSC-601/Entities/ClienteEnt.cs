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
        [StringLength(9, MinimumLength = 9, ErrorMessage = "El campo de identificación debe tener 9 digitos.")]

        public string Ced_Cliente { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "No debe contener números ni caracteres especiales.")]
        public string Nombre_Cliente { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [RegularExpression(@"^[[a-zA-Z]+$", ErrorMessage = "No debe contener números ni caracteres especiales.")]

        public string Apellido_Cliente { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(70, MinimumLength = 5, ErrorMessage = "El correo electrónico debe tener entre 5 y 70 caracteres.")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Ingresa una dirección de correo electrónico válida.")]

        public string Correo_Cliente { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 15 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,15}$", ErrorMessage = "La contraseña debe tener entre 8 y 15 caracteres y contener números, letras y caracteres especiales.")]

        public string Contrasenna_Cliente { get; set; }
        public string Direccion_Cliente { get; set; }
        public string Tel_Cliente { get; set; }
        public int Est_Cliente { get; set; }
        public int Rol_Cliente { get; set; }
    }
}