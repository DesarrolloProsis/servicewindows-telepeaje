using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace WinFormsClientTest
{
    public class HandlePlazas
    {

        public List<PlazaEntity> getListaPlazas()
        {
            List<PlazaEntity> lista;
            using (StreamReader r = new StreamReader("PlazasList.json"))
            {
                string json = r.ReadToEnd();
                lista = JsonConvert.DeserializeObject<List<PlazaEntity>>(json);
                Console.WriteLine(lista);
            }
            return lista;
        }
    }
}
