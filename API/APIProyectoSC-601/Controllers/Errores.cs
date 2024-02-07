using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace APIProyectoSC_601.Controllers
{
    public class Errores
    {
            private string Ruta = "";


            public Errores(string Ruta)
            {
                this.Ruta = Ruta;
            }

            public void Add(string sLog)
            {
                CreateDirectory();
                string nombre = GetNameFile();
                string cadena = "";

                cadena += DateTime.Now + " - " + sLog + Environment.NewLine;

                StreamWriter sw = new StreamWriter(Ruta + "/" + nombre, true);
                sw.Write(cadena);
                sw.Close();

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