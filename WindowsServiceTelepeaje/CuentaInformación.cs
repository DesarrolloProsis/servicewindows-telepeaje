using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsServiceTelepeaje.Models;
using WindowsServiceTelepeaje.Service;

namespace WindowsServiceTelepeaje
{
    public class CuentaInformación
    {
        public CuentaInformación()
        {
        }

        public Task<int> CuentaTransaccionesSQLServer(DateTime fechaInicio, DateTime fechaFin)
        {
            ApplicationDbContext db = new ApplicationDbContext();//Check connection
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

        public int CuentaTransaccionesOracle(DateTime fechaInicio, DateTime fechaFin)
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
                MtGlb.CrearConexionOracle();
                var contadorOracle = Convert.ToInt32(MtGlb.QueryDataCount(queryOracle));
                MtGlb.ExitConnectionOracle();
                return contadorOracle;
            }
            catch (Exception ex)
            {
                throw new Exception("Error ORACLE: " + ex.Message);
            }
        }

        public async Task<string> MuestraInformacion(DateTime fechaInicio, DateTime fechaFin)
        {
            
            int sqlCount = await this.CuentaTransaccionesSQLServer(fechaInicio, fechaFin);
            //var oracleCountTask = await this.CuentaTransaccionesOracle(fechaInicio, fechaFin);
            int oracleCount = this.CuentaTransaccionesOracle(fechaInicio, fechaFin);
            int diferencia = oracleCount - sqlCount;
            var variable = "SQL Registros: " + sqlCount + "Oracle Registros: " + oracleCount + "Diferencia: " + diferencia;
            return variable;
        }

        public int CuentaDiferenciaRegistros(string ruta, DateTime fechaInicio, DateTime fechaFin)
        {
            int diferencia = 0;
            LogServiceTel log = new LogServiceTel();
            //string ruta = log.GetNombreFile();
            try
            {
                int sqlCount = this.CuentaTransaccionesSQLServer(fechaInicio, fechaFin).Result;
                int oracleCount = 1 /*this.CuentaTransaccionesOracle(fechaInicio, fechaFin)*/;
                diferencia = oracleCount - sqlCount;
            }
            catch (Exception ex)
            {
                diferencia = -1;
                log.EscribeLogFile(ruta, ex + "");
            }
            return diferencia;
        }
    }
}
