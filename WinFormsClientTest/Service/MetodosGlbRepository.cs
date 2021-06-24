using Oracle.DataAccess.Client;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace WinFormsClientTest.Service
{
    public class MetodosGlbRepository
    {
        public DataSet Ds = new DataSet();
        public DataSet DsSqlServer = new DataSet();
        public DataRow oDataRowSqlServer;
        public DataRow oDataRow;

        private static readonly string ConStrDbContext = ConfigurationManager.ConnectionStrings["ApplicationDbContext"].ConnectionString;
        private static SqlConnection connectionDbContext;

        //private static readonly string ConStrProsis = ConfigurationManager.ConnectionStrings["PROSIS"].ConnectionString;
        //private static SqlConnection connectionProsis;  //Conexion para la migracion...

        private static readonly string ConStrOracle = ConfigurationManager.ConnectionStrings["OracleSql"].ConnectionString;
        private static OracleConnection connectionOracle;
        
        public void CrearConexionOracle()
        {
            connectionOracle = new OracleConnection(ConStrOracle);
            //connectionProsis = new SqlConnection(ConStrProsis);
            connectionDbContext = new SqlConnection(ConStrDbContext);
        }

        /// <summary>
        /// Ejecuta un query y lo agrega a un DataSet (Sql Server)
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="NameTable"></param>
        /// <returns></returns>
        //public bool QueryDataSet_SqlServer(string Query, string NameTable)
        //{
        //    bool Rpt = false;

        //    using (SqlCommand Cmd = new SqlCommand(Query, ConnectionProsis()))
        //    {
        //        using (SqlDataAdapter Da = new SqlDataAdapter(Cmd))
        //        {
        //            if (DsSqlServer.Tables.Count != 0)
        //                DsSqlServer.Clear();

        //            int iPosicionFilaActual = 0;
        //            try
        //            {
        //                Da.Fill(DsSqlServer, NameTable);
        //                if (DsSqlServer.Tables[NameTable].Rows.Count > 0)
        //                {
        //                    oDataRowSqlServer = DsSqlServer.Tables[NameTable].Rows[iPosicionFilaActual];
        //                    Rpt = true;
        //                }
        //                else
        //                    Rpt = false;
        //            }
        //            catch (Exception ex)
        //            {
        //                Rpt = false;
        //            }
        //            finally
        //            {
        //                Cmd.Dispose();
        //            }
        //        }
        //    }

        //    return Rpt;
        //}

        /// <summary>
        /// Ejecuta un query y lo agrega a un DataSet (Sql Server) ProsisDBv1_1
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="NameTable"></param>
        /// <returns></returns>
        public bool QueryDataSet_SqlServerDBv1_1(string Query, string NameTable)
        {
            bool Rpt = false;

            using (SqlCommand Cmd = new SqlCommand(Query, ConnectionDbContext()))
            {
                using (SqlDataAdapter Da = new SqlDataAdapter(Cmd))
                {
                    if (DsSqlServer.Tables.Count != 0)
                        DsSqlServer.Clear();

                    int iPosicionFilaActual = 0;
                    try
                    {
                        Da.Fill(DsSqlServer, NameTable);
                        if (DsSqlServer.Tables[NameTable].Rows.Count > 0)
                        {
                            oDataRowSqlServer = DsSqlServer.Tables[NameTable].Rows[iPosicionFilaActual];
                            Rpt = true;
                        }
                        else
                            Rpt = false;
                    }
                    catch (Exception ex)
                    {
                        Rpt = false;
                    }
                    finally
                    {
                        Cmd.Dispose();
                    }
                }
            }

            return Rpt;
        }

        /// <summary>
        /// Ejecuta un query y lo agrega a un DataSet
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="NameTable"></param>
        /// <returns></returns>
        /// 
        public Task<object> QueryDataCount(string myExecuteQuery)
        {  
                OracleCommand command = new OracleCommand(myExecuteQuery, ConnectionOracle());
            //command.Connection.Open();
            
            return command.ExecuteScalarAsync();
                   
                
        }
        public bool QueryDataSet(string Query, string NameTable)
        {
            if (Ds.Tables.Count != 0)
                Ds.Clear();

            int iPosicionFilaActual = 0;
            bool _return = false;

            OracleCommand Cmd = new OracleCommand(Query, ConnectionOracle());
            Cmd.CommandType = System.Data.CommandType.Text;

            OracleDataAdapter Da = new OracleDataAdapter(Cmd);
            Da.Fill(Ds, NameTable);
            try
            {
                if (Ds.Tables[NameTable].Rows.Count > 0)
                {
                    oDataRow = Ds.Tables[NameTable].Rows[iPosicionFilaActual];
                    _return = true;
                }
                else
                    _return = false;
            }
            catch (Exception ex)
            {
                _return = false;
            }
            finally
            {
                Cmd.Dispose();
            }
            return _return;
        }

        /// <summary>
        /// Inserta en la tabla especificado un query en sql server
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public bool InsertQuerySqlServer(string Query)
        {
            bool rpt = false;

            using (SqlCommand Cmd = new SqlCommand(Query, ConnectionDbContext()))
            {
                try
                {
                    Cmd.ExecuteNonQuery();
                    rpt = true;
                }
                catch (Exception ex)
                {
                    rpt = false;
                }
                finally
                {
                    Cmd.Dispose();
                }
            }

            return rpt;
        }

        /**************************************************************/

        public static SqlConnection ConnectionDbContext()
        {
            if (connectionDbContext.State == ConnectionState.Closed)
                connectionDbContext.Open();

            return connectionDbContext;
        }

        public void ExitConnectionDbContext()
        {
            connectionDbContext.Close();
        }

        //public static SqlConnection ConnectionProsis()
        //{
        //    if (connectionProsis.State == ConnectionState.Closed)
        //        connectionProsis.Open();

        //    return connectionProsis;
        //}

        //public void ExitConnectionProsis()
        //{
        //    connectionProsis.Close();
        //}

        public static OracleConnection ConnectionOracle()
        {
            if (connectionOracle.State == ConnectionState.Closed)
                connectionOracle.Open();

            return connectionOracle;
        }

        public void ExitConnectionOracle()
        {
            connectionOracle.Close();
        }
    }
}
