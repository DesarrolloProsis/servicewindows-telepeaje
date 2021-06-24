using System;
using System.Linq;
using WindowsServiceTelepeaje.Models;
using WinFormsClientTest.Service;

namespace ServiceTelepeaje.Logic
{
    
    public class CountInfo
    {
        private MetodosGlbRepository MtGlb;
        public CountInfo()
        {
            MtGlb = new MetodosGlbRepository();
        }
        
        static string cadenaConexion = "data source=.;initial catalog=ProsisDBv1_1;user id=SA;password=CAPUFE;MultipleActiveResultSets=True;App=EntityFramework";
        private ApplicationDbContext db = new ApplicationDbContext(cadenaConexion);
        public void ExecuteProcess(DateTime fechaInicio, DateTime fechaFin , out int conteoSql, out Int32 conteoOracle)
        {
            string querySql;
            string queryOracle;
            try
            {
                querySql = @"SELECT * FROM [ProsisDBv1_1].[dbo].[pn_importacion_wsIndra]" +
                       "WHERE (DATE_TRANSACTION between '" + Convert.ToDateTime(fechaInicio).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       "and '" + Convert.ToDateTime(fechaFin).ToString("yyyy-MM-dd HH:mm:ss") + "')";
                queryOracle = @"SELECT COUNT(0) FROM TRANSACTION " +
                                "JOIN TYPE_CLASSE ON TAB_ID_CLASSE = TYPE_CLASSE.ID_CLASSE  " +
                                "LEFT JOIN TYPE_CLASSE   TYPE_CLASSE_ETC  ON ACD_CLASS = TYPE_CLASSE_ETC.ID_CLASSE WHERE" +
                                "(DATE_TRANSACTION BETWEEN TO_DATE('" + Convert.ToDateTime(fechaInicio).ToString("yyyyMMddHHmmss") + "','YYYYMMDDHH24MISS') " +
                                "AND TO_DATE('" + Convert.ToDateTime(fechaFin).ToString("yyyyMMddHHmmss") + "','YYYYMMDDHH24MISS'))" +
                                "AND  ID_PAIEMENT  = 15 " +
                                "AND (TRANSACTION.Id_Voie = '1' " +
                                "OR TRANSACTION.Id_Voie = '2' " +
                                "OR TRANSACTION.Id_Voie = '3' " +
                                "OR TRANSACTION.Id_Voie = '4' " +
                                "OR TRANSACTION.Id_Voie = 'X') ";
                var contadorsql = db.pn_importacion_wsIndra.SqlQuery(querySql).Count();
                conteoSql = contadorsql;
                MtGlb = new MetodosGlbRepository();
                MtGlb.CrearConexionOracle();
                var contadorOracle = Convert.ToInt32( MtGlb.QueryDataCount(queryOracle));
                conteoOracle = contadorOracle;
                MtGlb.ExitConnectionOracle();
            }
            catch (Exception)
            {

                throw;
            }         
        }


        public int CuentaTransaccionesSQl()
        {
            return 0;
        }

        public int cuentaTransaccionesOracle()
        {
            return 0;
        }
    }
}
