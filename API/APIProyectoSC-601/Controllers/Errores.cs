using System;
using System.IO;

namespace APIProyectoSC_601.Controllers
{
    public class Errores
    {
        private string Ruta = "";
        private int idCounter; 

        public Errores(string Ruta)
        {
            this.Ruta = Ruta;
            idCounter = 1; 
        }

        public void Add(string sLog)
        {
            CreateDirectory();
            string nombre = GetNameFile();
            string cadena = "";

            cadena += "ID: " + idCounter + " - " + DateTime.Now + " - " + sLog + Environment.NewLine;

            StreamWriter sw = new StreamWriter(Ruta + "/" + nombre, true);
            sw.Write(cadena);
            sw.Close();

            idCounter++; // Incrementar el contador de ID para cada instancia
        }

        #region HELPER
        private string GetNameFile()
        {
            string nombre = "";

            nombre = "log_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + ".txt";

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
