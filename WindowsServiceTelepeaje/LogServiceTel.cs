using System;
using System.IO;
using System.Text;

namespace WindowsServiceTelepeaje
{
    public class LogServiceTel
    {
        public string GetNombreFile()
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

        public void EscribeLogFile(string PathFile, string Texto)
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
    }
}
