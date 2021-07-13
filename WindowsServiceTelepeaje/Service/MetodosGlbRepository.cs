using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceTelepeaje.Service
{
    public class MetodosGlbRepository
    {
        public readonly DataSet Ds = new DataSet();
        public readonly DataSet DsSqlServer = new DataSet();
        public DataRow oDataRowSqlServer { get; set; }
        public DataRow oDataRow { get; set; }

        private static readonly string ConStrDbContext = ConfigurationManager.ConnectionStrings["ApplicationDbContext"].ConnectionString;
        private SqlConnection connectionDbContext;

        private readonly string ConStrOracle = ConfigurationManager.ConnectionStrings["OracleSql"].ConnectionString;
        private OracleConnection connectionOracle;

        public void CrearConexionOracle()
        {
            connectionOracle = new OracleConnection(ConStrOracle);
            connectionDbContext = new SqlConnection(ConStrDbContext);
        }

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

        public SqlConnection ConnectionDbContext()
        {
            if (connectionDbContext.State == ConnectionState.Closed)
                connectionDbContext.Open();

            return connectionDbContext;
        }

        public void ExitConnectionDbContext()
        {
            connectionDbContext.Close();
        }

        public OracleConnection ConnectionOracle()
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