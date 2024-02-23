using System.ComponentModel.DataAnnotations;

namespace ProyectoSC_601.Entities
{
    public class CategoriaEnt
    {
        public int ID_Categoria { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string Nombre_Categoria { get; set; }
        public int Estado_Categoria { get; set; }
    }

}