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
        public static string GetNombreFile()
        {
            string Path = @"C:\LogServiceTel\";
            string PathFile;
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            PathFile = Path + "Log" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt";
            return PathFile;
        }
        public static void EscribeLog(string PathFile, string Texto)
        {
            try
            {
                StreamWriter sw = new StreamWriter(PathFile, true, Encoding.ASCII);
                sw.Write(Texto);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        //public static string MostrarInformacion()
        //{
        //    StreamReader sr = new StreamReader(path + archivo, true);
        //    string contenido = sr.ReadToEnd();
        //    return contenido;
        //}
    }
}
