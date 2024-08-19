using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ProyectoSC_601.Entities
{
    public class InventarioEnt
    {
        public long ID_Producto { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public int ID_Categoria { get; set; }
        public string Nombre_Categoria { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(300, MinimumLength = 10, ErrorMessage = "La dirección debe tener entre 10 y 50 caracteres.")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Range(1, 99999, ErrorMessage = "La cantidad debe ser superior a 0")]
        public int Cantidad { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string SKU { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Range(1, 999999, ErrorMessage = "El precio debe ser superior a 0")]
        public decimal Precio { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string Imagen { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public HttpPostedFileBase Imagen_Nueva { get; set; }
        public int Estado { get; set; }
    }

}