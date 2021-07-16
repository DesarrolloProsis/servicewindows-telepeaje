using System;
using System.Linq;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
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

        public Task<int> CuentaTransaccionesSQLServer(DateTime fechaInicio, DateTime fechaFin)
        {
            //string cadenaConexion = "data source=.;initial catalog=ProsisDBv1_1;user id=SA;password=CAPUFE;MultipleActiveResultSets=True;App=EntityFramework";
            ApplicationDbContext db = new ApplicationDbContext(this.PlazaEntity.SqlCon);
            string querySql;
            try
            {
                querySql = @"SELECT * FROM [ProsisDBv1_1].[dbo].[pn_importacion_wsIndra]" +
                       "WHERE (DATE_TRANSACTION between '" + Convert.ToDateTime(fechaInicio).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       "and '" + Convert.ToDateTime(fechaFin).ToString("yyyy-MM-dd HH:mm:ss") + "')";

                var contadorsql = db.pn_importacion_wsIndra.SqlQuery(querySql).CountAsync();
                return contadorsql;
            }
            catch (Exception ex)
            {
                throw new Exception("Error SQL SERVER: " + ex.Message);
            }
        }

        public Task<object> CuentaTransaccionesOracle(DateTime fechaInicio, DateTime fechaFin)
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
                //var resultado = await MtGlb.QueryDataCount(queryOracle);
                var contadorOracle = MtGlb.QueryDataCount(queryOracle);
                //LogServiceTelepeage.EscribeLog("Cuenta oracle" +contadorOracle);             
                MtGlb.ExitConnectionOracle();
                return contadorOracle;
            }
            catch (Exception ex)
            {
                LogServiceTelepeage.checkFileLog();
                LogServiceTelepeage.EscribeLog(ex+ "");
                throw new Exception("Error SQL SERVER: " + ex.Message);
            }
        }

        public async Task<string> MuestraInformacion(DateTime fechaInicio, DateTime fechaFin, PlazaEntity PlazaEntidad)
        {
            int sqlCount =  await this.CuentaTransaccionesSQLServer(fechaInicio, fechaFin);
            Console.WriteLine("bu");
            var oracleCountTask = await this.CuentaTransaccionesOracle(fechaInicio, fechaFin);
            int oracleCount =  Convert.ToInt32(oracleCountTask);
            int diferencia = oracleCount-sqlCount;
            var variable= "SQL Registros: " + sqlCount + "Oracle Registros: " + oracleCount + "Diferencia: " + diferencia;
            return variable;
        }
    }
}
