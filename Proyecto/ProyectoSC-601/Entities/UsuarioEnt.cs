using ProyectoSC_601.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ProyectoSC_601.Entities
{
    public class UsuarioEnt
    {

        public long ID_Usuario { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una opción.")]
        public int ID_Identificacion { get; set; }
        public string Nombre_Identificacion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [CustomIdentificacion(ErrorMessage = "La identificación no cumple con los requisitos según el tipo seleccionado.")]

        public string Identificacion_Usuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "No debe contener números ni caracteres especiales.")]
        public string Nombre_Usuario { get; set; }

        [ApellidoRequerido(ErrorMessage = "Este campo es obligatorio.")]
        [RegularExpression(@"^[[a-zA-Z]+$", ErrorMessage = "No debe contener números ni caracteres especiales.")]

        public string Apellido_Usuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(70, MinimumLength = 5, ErrorMessage = "El correo electrónico debe tener entre 5 y 70 caracteres.")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Ingresa una dirección de correo electrónico válida.")]

        public string Correo_Usuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 15 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,15}$", ErrorMessage = "La contraseña debe tener entre 8 y 15 caracteres y contener números, letras y caracteres especiales.")]

        public string Contrasenna_Usuario { get; set; }

        [StringLength(15, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 15 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,15}$", ErrorMessage = "La contraseña debe tener entre 8 y 15 caracteres y contener números, letras y caracteres especiales.")]

        public string NuevaContrasenna_Usuario { get; set; }
        
        public int ID_Direccion { get; set; }

        public int ID_Provincia { get; set; }
        public int ID_Canton { get; set; }
        public int ID_Distrito { get; set; }
        public string Nombre_Provincia { get; set; }
        public string Nombre_Canton { get; set; }
        public string Nombre_Distrito { get; set; }
        public string Direccion_Exacta { get; set; }
        public string Telefono_Usuario { get; set; }
        public int ID_Estado { get; set; }
        public int ID_Rol { get; set; }

    }
}

public class CustomIdentificacionAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var usuario = (UsuarioEnt)validationContext.ObjectInstance;

        if (usuario.ID_Identificacion == 1)
        {
            // Validación para el tipo 1 (ejemplo: 9 dígitos y solo números)
            if (!Regex.IsMatch(value.ToString(), "^[0-9]{9}$"))
            {
                return new ValidationResult("La identificación debe tener 9 dígitos numéricos.");
            }
        }
        else if (usuario.ID_Identificacion == 2)
        {
            // Validación para el tipo 2 (ejemplo: 11 dígitos y solo números)
            if (!Regex.IsMatch(value.ToString(), "^[0-9]{11}$"))
            {
                return new ValidationResult("La identificación debe tener 11 dígitos numéricos.");
            }
        }
        else if (usuario.ID_Identificacion == 3)
        {
            // Validación para el tipo 3 (ejemplo: 15 caracteres alfanuméricos)
            if (!Regex.IsMatch(value.ToString(), "^[a-zA-Z0-9]{15}$"))
            {
                return new ValidationResult("La identificación debe tener 15 caracteres alfanuméricos.");
            }
        }

        return ValidationResult.Success;
    }
}

public class ApellidoRequeridoAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var instance = validationContext.ObjectInstance as UsuarioEnt;

        if (instance != null && instance.ID_Identificacion == 2)
        {
            // Si ID_Identificacion es 2, permitimos que el campo de apellido esté vacío
            return ValidationResult.Success;
        }

        // Para otras opciones, se aplica la validación estándar de Required
        if (string.IsNullOrWhiteSpace(value?.ToString()))
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}
