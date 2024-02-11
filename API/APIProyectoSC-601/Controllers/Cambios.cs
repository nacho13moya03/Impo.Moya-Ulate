using System;
using System.IO;

namespace APIProyectoSC_601.Controllers
{
    public class LogExitos
    {
        private string Ruta = "";

        public LogExitos(string Ruta)
        {
            this.Ruta = Ruta;
        }

        public void Add(string procedimiento, string descripcion)
        {
            CreateDirectory();
            string nombre = GetNameFile();
            string cadena = "";

            cadena += $"{DateTime.Now} - Procedimiento: {procedimiento} - Descripción: {descripcion}" + Environment.NewLine;

            StreamWriter sw = new StreamWriter(Ruta + "/" + nombre, true);
            sw.Write(cadena);
            sw.Close();
        }

        #region HELPER
        private string GetNameFile()
        {
            string nombre = "";

            nombre = "log_exitos_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + ".txt";

            return nombre;
        }

        private void CreateDirectory()
        {
            try
            {
                if (!Directory.Exists(Ruta))
                    Directory.CreateDirectory(Ruta);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
