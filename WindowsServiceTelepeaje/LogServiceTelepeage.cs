using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceTelepeaje
{
    public static class LogServiceTelepeage
    {
        private static readonly string path = @"C:\ExecutedActionSW\Manual\";
        private static readonly string archivo = "LogManual.txt";

        public static void checkFileLog()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                if (!File.Exists(path + archivo))
                {
                    File.CreateText(path + archivo);
                }
            }
        }

        public static void EscribeLog(string newRow)
        {
            try
            {
                StreamReader sr = new StreamReader(path + archivo, true);
                string contenido = sr.ReadToEnd();
                sr.Dispose();
                sr.Close();
                using (StreamWriter file = new StreamWriter(path + "WindowsService_Temporal.txt", true))
                {
                    file.WriteLine(newRow); //se agrega información al documento
                    file.WriteLine(contenido);
                    file.Dispose();
                    file.Close();
                }
                File.Delete(path + archivo);
                File.Move(path + "WindowsService_Temporal.txt", path + archivo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static string MostrarInformacion()
        {
            StreamReader sr = new StreamReader(path + archivo, true);
            string contenido = sr.ReadToEnd();
            return contenido;
        }
    }
}
