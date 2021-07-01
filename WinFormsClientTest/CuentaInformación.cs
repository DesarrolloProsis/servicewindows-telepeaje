using System;
using System.Linq;
using WindowsServiceTelepeaje.Models;
using WinFormsClientTest.Service;

namespace WinFormsClientTest
{
    public class CuentaInformación
    {
        PlazaEntity PlazaEntity;
        public CuentaInformación(PlazaEntity PlazaEntity)
        {
            this.PlazaEntity = PlazaEntity;
        }

        public int CuentaTransaccionesSQLServer(DateTime fechaInicio, DateTime fechaFin)
        {
            //string cadenaConexion = "data source=.;initial catalog=ProsisDBv1_1;user id=SA;password=CAPUFE;MultipleActiveResultSets=True;App=EntityFramework";
            ApplicationDbContext db = new ApplicationDbContext(this.PlazaEntity.SqlCon);
            string querySql;
            try
            {
                querySql = @"SELECT * FROM [ProsisDBv1_1].[dbo].[pn_importacion_wsIndra]" +
                       "WHERE (DATE_TRANSACTION between '" + Convert.ToDateTime(fechaInicio).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       "and '" + Convert.ToDateTime(fechaFin).ToString("yyyy-MM-dd HH:mm:ss") + "')";

                int contadorsql = db.pn_importacion_wsIndra.SqlQuery(querySql).Count();
                return contadorsql;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return -1;
            }
        }

        public Int32 CuentaTransaccionesOracle(DateTime fechaInicio, DateTime fechaFin)
        {
            string queryOracle;
            try
            {
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
                MetodosGlbRepository MtGlb = new MetodosGlbRepository();
                MtGlb.CrearConexionOracle(this.PlazaEntity);
                var contadorOracle = Convert.ToInt32(MtGlb.QueryDataCount(queryOracle).Result.ToString());
                LogServiceTelepeage.EscribeLog("Cuenta oracle" +contadorOracle);             
                MtGlb.ExitConnectionOracle();
                return contadorOracle;
            }
            catch (Exception ex)
            {
                LogServiceTelepeage.checkFileLog();
                LogServiceTelepeage.EscribeLog(ex+ "");
                Console.WriteLine(ex);
                return -1;
            }
        }

        public string MuestraInformacion(DateTime fechaInicio, DateTime fechaFin, PlazaEntity PlazaEntidad)
        {
            int sqlCount = this.CuentaTransaccionesSQLServer(fechaInicio, fechaFin);
            Console.WriteLine("bu");
            int oracleCount = this.CuentaTransaccionesOracle(fechaInicio, fechaFin);
            int diferencia = oracleCount-sqlCount;
            return "SQL Registros: " + sqlCount + "Oracle Registros: " + oracleCount + "Diferencia: " + diferencia;
        }
    }
}
