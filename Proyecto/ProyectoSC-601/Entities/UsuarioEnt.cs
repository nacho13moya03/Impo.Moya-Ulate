using ProyectoSC_601.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\\s]+$", ErrorMessage = "No debe contener números ni caracteres especiales.")]
        public string Nombre_Usuario { get; set; }


        [ApellidoRequerido(ErrorMessage = "Este campo es obligatorio.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ]+(?![\s\S]*\s[\s\S]*$)", ErrorMessage = "Debe ingresar solo el primer apellido, sin números ni caracteres especiales")]

        public string Apellido_Usuario { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(70, MinimumLength = 5, ErrorMessage = "El correo electrónico debe tener entre 5 y 70 caracteres.")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Ingresa una dirección de correo electrónico válida.")]

        public string Correo_Usuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,15}$", ErrorMessage = "La contraseña debe tener al menos 6 caracteres y contener números, letras y caracteres especiales ('@#$%!&()')")]

        public string Contrasenna_Usuario { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,15}$", ErrorMessage = "La contraseña debe tener al menos 6 caracteres y contener números, letras y caracteres especiales ('@#$%!&()')")]

        public string NuevaContrasenna_Usuario { get; set; }

        public int ID_Direccion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public int ID_Provincia { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public int ID_Canton { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public int ID_Distrito { get; set; }
        public string Nombre_Provincia { get; set; }
        public string Nombre_Canton { get; set; }
        public string Nombre_Distrito { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(50, MinimumLength = 25, ErrorMessage = "La dirección debe tener entre 25 y 50 caracteres.")]
        public string Direccion_Exacta { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "El teléfono debe contener 8 números.")]
        public string Telefono_Usuario { get; set; }
        public int ID_Estado { get; set; }
        public int ID_Rol { get; set; }
        public int C_esTemporal { get; set; }
        

    }
}

public class CustomIdentificacionAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var usuario = (UsuarioEnt)validationContext.ObjectInstance;

        if (usuario.ID_Identificacion == 1)
        {
            if (value == null)
            {
                return new ValidationResult("Este campo es obligatorio.");
            }
            else if (!Regex.IsMatch(value.ToString(), "^[0-9]{9}$"))
            {
                return new ValidationResult("La identificación debe tener 9 dígitos numéricos.");
            }

        }
        else if (usuario.ID_Identificacion == 2)
        {
            if (value == null)
            {
                return new ValidationResult("Este campo es obligatorio.");
            }
            else if (!Regex.IsMatch(value.ToString(), "^[0-9]{11}$"))
            {
                return new ValidationResult("La identificación debe tener 11 dígitos numéricos.");
            }


        }
        else if (usuario.ID_Identificacion == 3)
        {

            if (value == null)
            {
                return new ValidationResult("El campo identificación es obligatorio.");
            }
            else if (!Regex.IsMatch(value.ToString(), "^[a-zA-Z0-9]{4,15}$"))
            {
                return new ValidationResult("La identificación debe tener entre 4 y 15 caracteres.");
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
