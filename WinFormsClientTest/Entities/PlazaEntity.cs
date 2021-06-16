using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsClientTest
{
    public class PlazaEntity
    {
        public string plazacobro { get; set; }
        public string Nombre { get; set; }
        public string IPService { get; set; }
        public string OracleCon { get; set; }
        public string SqlCon { get; set; }

        override
        public string ToString()
        {
            return Nombre;
        }

    }
    
}
