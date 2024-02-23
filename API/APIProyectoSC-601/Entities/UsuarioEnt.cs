namespace APIProyectoSC_601.Entities
{
    public class UsuarioEnt
    {
        public long ID_Usuario { get; set; }

        public int ID_Identificacion { get; set; }

        public string Nombre_Identificacion { get; set; }
        public string Identificacion_Usuario { get; set; }
        public string Nombre_Usuario { get; set; }
        public string Apellido_Usuario { get; set; }
        public string Correo_Usuario { get; set; }
        public string Contrasenna_Usuario { get; set; }
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