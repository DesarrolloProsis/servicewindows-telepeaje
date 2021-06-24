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
