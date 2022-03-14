using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Timers;
using WindowsServiceTelepeaje.Models;
using WindowsServiceTelepeaje.Service;

namespace WindowsServiceTelepeaje
{
    public partial class ServiceTelf3 : ServiceBase
    {
        private ServiceReference1.PortTypeClient Ws = new ServiceReference1.PortTypeClient();

        private ApplicationDbContext db = new ApplicationDbContext();

        private System.Timers.Timer timProcess = null;
        private int i = 0;
        private string path = @"C:\fase3\";
        private string archivo = "WindowsService.txt";
        private bool iniciarCon = true;
        MetodosGlbRepository MtGlb;
        public ServiceTelf3()
        {
            InitializeComponent();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                if (!File.Exists(path + archivo))
                {
                    File.CreateText(path + archivo);
                }
            }
        }

        protected override void OnStart(string[] args)
        {
            timProcess = new System.Timers.Timer
            {
                Interval = 300000
            };
            timProcess.Elapsed += new System.Timers.ElapsedEventHandler(TimProcess_Elapsed);
            timProcess.Enabled = true;
            timProcess.Start();
        }

        public void OnStartTest()
        {
            timProcess = new System.Timers.Timer
            {
                Interval = 100000
            };
            timProcess.Elapsed += new System.Timers.ElapsedEventHandler(TimProcess_Elapsed);
            timProcess.Enabled = true;
            timProcess.Start();
        }

        private void TimProcess_Elapsed(object sender, ElapsedEventArgs e)
        {
            timProcess.Enabled = false;
            ExecuteProcess();
        }

        protected override void OnStop()
        {
            //using (StreamWriter file = new StreamWriter(path + archivo, true))
            //{
            //    file.WriteLine("Termine a las: " + DateTime.Now.ToString()); //se agrega información al documento
            //    file.Dispose();
            //    file.Close();
            //}
            this.EscribeLog("Termine a las: " + DateTime.Now.ToString());
        }

        private void ExecuteProcess()
        {
            try
            {

                //using (StreamWriter file = new StreamWriter(path + archivo, true))
                //{
                Int16 Rodada = 0;
                i++;
                //    file.WriteLine("Se ejecuto el proceso ServicioWinProsis: " + i.ToString() + " a las " + DateTime.Now.ToString()); //se agrega información al documento
                //    file.Dispose();
                //    file.Close();
                //}
                this.EscribeLog("Se ejecuto el proceso ServicioWinProsis: " + i.ToString() + " a las " + DateTime.Now.ToString());

                /***********************************************************************************************************/


                if (iniciarCon)
                {
                    MtGlb = new MetodosGlbRepository();
                    MtGlb.CrearConexionOracle();
                    iniciarCon = false;
                }
                var IdPlazaCobro = ConfigurationManager.AppSettings["plazacobro"];

                string StrQuerys;

                string H_inicio_turno = string.Empty;

                double Dbl_registros = 0.0d;
                double _count = 0.0d;
                //---------------
                int Secuencial;
                int Carril = 0;
                string Fecha;
                string Hora;
                string Tarjeta;
                Int16 Status;
                int Clase = 0;
                int Ejes = 0;
                int Sec_piso;
                Int16 Turno = 0;

                int SecuencialTC;
                int AutorizacionTC;
                string TarjetaC;
                int medioTC;
                int StatusTC;
                DateTime Utctime;
                DateTime LocalTime;
                int TipoVehiculo = 0;
                string Cuerpo;

                int resultado = 0;
                bool CarrilInex = false;
                bool event_numbool = false;
                bool monto_detec = false;
                bool tagempty = false;

                //ULTIMA TRANSACTION DE LA DB SQL SERVER PROSIS
                StrQuerys = @"SELECT TOP (1) [id_exp]
                                  ,[DATE_TRANSACTION]
                                  ,[VOIE]
                                  ,[EVENT_NUMBER]
                                  ,[FOLIO_ECT]
                                  ,[Version_Tarif]
                                  ,[ID_PAIEMENT]
                                  ,[TAB_ID_CLASSE]
                                  ,[CLASE_MARCADA]
                                  ,[MONTO_MARCADO]
                                  ,[ACD_CLASS]
                                  ,[CLASE_DETECTADA]
                                  ,[MONTO_DETECTADO]
                                  ,[CONTENU_ISO]
                                  ,[CODE_GRILLE_TARIF]
                                  ,[ID_OBS_MP]
                                  ,[fecha_ext]
                                  ,[Shift_number]
                                  ,[PLAZA]
                          FROM [pn_importacion_wsIndra]
                          ORDER BY DATE_TRANSACTION DESC";

                var query = db.pn_importacion_wsIndra.SqlQuery(StrQuerys).FirstOrDefault();

                if (query != null)
                    H_inicio_turno = query.DATE_TRANSACTION.Value.ToString("yyyy/MM/dd HH:mm:ss");
                //else if (MtGlb.QueryDataSet_SqlServer(StrQuerys, "DATE_TRANSACTION"))
                //{
                //    if (MtGlb.DsSqlServer.Tables["DATE_TRANSACTION"].Rows.Count > 0)
                //        H_inicio_turno = Convert.ToDateTime(MtGlb.oDataRowSqlServer["DATE_TRANSACTION"]).ToString("yyyy/MM/dd HH:mm:ss");
                //}
                else
                    H_inicio_turno = Convert.ToDateTime("2021/05/26 11:00:00").ToString("yyyy/MM/dd HH:mm:ss");

                H_inicio_turno = Convert.ToDateTime(H_inicio_turno).AddMinutes(-10).ToString("yyyy/MM/dd HH:mm:ss");
                //ORACLE
                DateTime fechaInicio = Convert.ToDateTime(ConfigurationManager.AppSettings["fechaInicio"]);
                DateTime fechaFin = Convert.ToDateTime(ConfigurationManager.AppSettings["fechaFin"]);
                //Cambios Richi
                if (ConfigurationManager.AppSettings["carril"] != "")
                {
                    //H_inicio_turno = Convert.ToDateTime(H_inicio_turno).AddDays(-3).ToString("yyyy/MM/dd HH:mm:ss");
                    StrQuerys = "SELECT DATE_TRANSACTION, VOIE,  EVENT_NUMBER, FOLIO_ECT, Version_Tarif, ID_PAIEMENT, " +
                          "TAB_ID_CLASSE, TYPE_CLASSE.LIBELLE_COURT1 AS CLASE_MARCADA,  NVL(TRANSACTION.Prix_Total,0) as MONTO_MARCADO, " +
                          "ACD_CLASS, TYPE_CLASSE_ETC.LIBELLE_COURT1 AS CLASE_DETECTADA, NVL(TRANSACTION.transaction_CPT1 / 100, 0) as MONTO_DETECTADO, " +
                          "CONTENU_ISO, CODE_GRILLE_TARIF, ID_OBS_MP, Shift_number, TRANSACTION_CPT1 " +
                          "FROM TRANSACTION " +

                          "JOIN TYPE_CLASSE ON TAB_ID_CLASSE = TYPE_CLASSE.ID_CLASSE  " +
                          "LEFT JOIN TYPE_CLASSE   TYPE_CLASSE_ETC  ON ACD_CLASS = TYPE_CLASSE_ETC.ID_CLASSE " +
                          "WHERE"
                          +
                          "(DATE_TRANSACTION BETWEEN TO_DATE('" + Convert.ToDateTime(fechaInicio).ToString("yyyyMMddHHmmss") + "','YYYYMMDDHH24MISS') AND TO_DATE('" + Convert.ToDateTime(fechaFin).ToString("yyyyMMddHHmmss") + "','YYYYMMDDHH24MISS'))"
                          +
                          "AND  ID_PAIEMENT  = 15 " +
                          "AND (TRANSACTION.Id_Voie = '1' " +
                          "OR TRANSACTION.Id_Voie = '2' " +
                          "OR TRANSACTION.Id_Voie = '3' " +
                          "OR TRANSACTION.Id_Voie = '4' " +
                          "OR TRANSACTION.Id_Voie = 'X') " +
                          "AND regexp_like (TRANSACTION.voie , '^(" + ConfigurationManager.AppSettings["carril"] + ")')" +
                          "ORDER BY DATE_TRANSACTION ";
                }
                else
                {
                    StrQuerys = "SELECT DATE_TRANSACTION, VOIE,  EVENT_NUMBER, FOLIO_ECT, Version_Tarif, ID_PAIEMENT, " +
                             "TAB_ID_CLASSE, TYPE_CLASSE.LIBELLE_COURT1 AS CLASE_MARCADA,  NVL(TRANSACTION.Prix_Total,0) as MONTO_MARCADO, " +
                             "ACD_CLASS, TYPE_CLASSE_ETC.LIBELLE_COURT1 AS CLASE_DETECTADA, NVL(TRANSACTION.transaction_CPT1 / 100, 0) as MONTO_DETECTADO, " +
                             "CONTENU_ISO, CODE_GRILLE_TARIF, ID_OBS_MP, Shift_number, TRANSACTION_CPT1 " +
                             "FROM TRANSACTION " +

                             "JOIN TYPE_CLASSE ON TAB_ID_CLASSE = TYPE_CLASSE.ID_CLASSE  " +
                             "LEFT JOIN TYPE_CLASSE   TYPE_CLASSE_ETC  ON ACD_CLASS = TYPE_CLASSE_ETC.ID_CLASSE " +
                             "WHERE" +
                             "(DATE_TRANSACTION > TO_DATE('" + Convert.ToDateTime(H_inicio_turno).ToString("yyyyMMddHHmmss") + "','YYYYMMDDHH24MISS'))  " +
                             "AND  ID_PAIEMENT  = 15 " +
                 
                             "AND (TRANSACTION.Id_Voie = '1' " +
                             "OR TRANSACTION.Id_Voie = '2' " +
                             "OR TRANSACTION.Id_Voie = '3' " +
                             "OR TRANSACTION.Id_Voie = '4' " +
                             "OR TRANSACTION.Id_Voie = 'X') " +
                             "ORDER BY DATE_TRANSACTION ";

                }

                if (MtGlb.QueryDataSet(StrQuerys, "TRANSACTION"))
                {
                    Dbl_registros = MtGlb.Ds.Tables["TRANSACTION"].Rows.Count;

                    var Carriles = db.Type_Plaza.GroupJoin(db.Type_Carril,
                                    pla => pla.Id_Plaza,
                                    car => car.Plaza_Id,
                                    (pla, car) => new { pla, car })
                                    .ToList()
                                    .FirstOrDefault(x => x.pla.Num_Plaza == IdPlazaCobro);

                    if (Carriles != null)
                    {
                        for (int i = 0; i < MtGlb.Ds.Tables["TRANSACTION"].Rows.Count; i++)
                        {
                            MtGlb.oDataRow = MtGlb.Ds.Tables["TRANSACTION"].Rows[i];

                            var n1 = MtGlb.oDataRow["TRANSACTION_CPT1"].ToString();
                            var n2 = Convert.ToInt16(MtGlb.oDataRow["ACD_CLASS"].ToString());

                            if (MtGlb.oDataRow["TRANSACTION_CPT1"].ToString() == "000000" && Convert.ToInt16(MtGlb.oDataRow["ACD_CLASS"].ToString()) >= 1)
                            {
                                monto_detec = true;
                                break;
                            }

                            var duplicate = (from h in MtGlb.Ds.Tables["TRANSACTION"].AsEnumerable()
                                            .Where(n => n["VOIE"].ToString() == MtGlb.oDataRow["VOIE"].ToString() &&
                                                        n["EVENT_NUMBER"].ToString() == MtGlb.oDataRow["EVENT_NUMBER"].ToString())
                                             group h by new { voie = h["VOIE"], event_number = h["EVENT_NUMBER"] } into g
                                             where g.Count() > 1
                                             select g).Count();

                            if (duplicate == 0)
                            {
                                StrQuerys = "SELECT count(EVENT_NUMBER) " +
                                            "FROM [ProsisDBv1_1].[dbo].[pn_importacion_wsIndra]" +
                                            "where DATE_TRANSACTION = '" + Convert.ToDateTime(MtGlb.oDataRow["DATE_TRANSACTION"]).ToString("yyyy-dd-MM HH:mm:ss") + "' " +
                                            "and VOIE = '" + MtGlb.oDataRow["VOIE"].ToString() + "' " +
                                            "and EVENT_NUMBER = " + MtGlb.oDataRow["EVENT_NUMBER"].ToString() + " " +
                                            " and FOLIO_ECT = " + MtGlb.oDataRow["FOLIO_ECT"].ToString() +
                                            " and TAB_ID_CLASSE = " + MtGlb.oDataRow["TAB_ID_CLASSE"].ToString() + " " +
                                            " and ACD_CLASS = " + MtGlb.oDataRow["ACD_CLASS"].ToString() + " ";


                                if (MtGlb.QueryDataSet_SqlServerDBv1_1(StrQuerys, "pn_importacion_wsIndra"))
                                {
                                         
                                    if (MtGlb.oDataRowSqlServer.HasErrors == false)
                                    {
                                        if (Convert.ToInt32(MtGlb.oDataRowSqlServer[0]) < 1)
                                        {
                                            _count++;

                                            //INICIO
                                            Secuencial = Convert.ToInt32(MtGlb.oDataRow["EVENT_NUMBER"]);

                                            var CarrilFound = from myRow in Carriles.car
                                                              where myRow.Num_Gea == MtGlb.oDataRow["Voie"].ToString().Substring(1, 2)
                                                              select myRow;

                                            if (CarrilFound.Count() > 0)
                                                Carril = Convert.ToInt32(CarrilFound.FirstOrDefault().Num_Capufe);
                                            else
                                            {
                                                CarrilInex = true;
                                                break;
                                            }


                                            Fecha = Convert.ToDateTime(MtGlb.oDataRow["DATE_TRANSACTION"]).ToString("dd-MM-yyyy");
                                            //'<!--21-09-2007-->
                                            //'<element name="Hora" type="xsd:string"/>
                                            Hora = Convert.ToDateTime(MtGlb.oDataRow["DATE_TRANSACTION"]).ToString("HH:mm:ss");
                                            //'<!--11:59:59-->
                                            //'<element name="tarjeta" type="xsd:string"/>
                                            Tarjeta = MtGlb.oDataRow["CONTENU_ISO"].ToString().Substring(0, 15).Trim();

                                            if (string.IsNullOrEmpty(Tarjeta))
                                            {
                                                tagempty = true;
                                                break;
                                            }

                                            //isoc
                                            if (Tarjeta.Length == 13 && Tarjeta.Substring(0, 3) == "009")
                                                Tarjeta = Tarjeta.Substring(0, 3) + Tarjeta.Substring(5, 8).Trim();

                                            Status = 1;

                                            switch (Convert.ToInt32(MtGlb.oDataRow["TAB_ID_CLASSE"]))
                                            {
                                                case 1:
                                                    Clase = 1;
                                                    Ejes = 2;
                                                    Rodada = 0;
                                                    TipoVehiculo = 0;
                                                    break;
                                                case 2:
                                                    Clase = 12;
                                                    Ejes = 2;
                                                    Rodada = 1;
                                                    TipoVehiculo = 0;
                                                    break;
                                                case 3:
                                                    Clase = 13;
                                                    Ejes = 3;
                                                    Rodada = 1;
                                                    TipoVehiculo = 0;
                                                    break;
                                                case 4:
                                                    Clase = 14;
                                                    Ejes = 4;
                                                    Rodada = 1;
                                                    TipoVehiculo = 0;
                                                    break;
                                                case 5:
                                                    Clase = 5;
                                                    Ejes = 5;
                                                    Rodada = 1;
                                                    TipoVehiculo = 0;
                                                    break;
                                                case 6:
                                                    Clase = 6;
                                                    Ejes = 6;
                                                    Rodada = 1;
                                                    TipoVehiculo = 0;
                                                    break;
                                                case 7:
                                                    Clase = 7;
                                                    Ejes = 7;
                                                    Rodada = 1;
                                                    TipoVehiculo = 0;
                                                    break;
                                                case 8:
                                                    Clase = 8;
                                                    Ejes = 8;
                                                    Rodada = 1;
                                                    TipoVehiculo = 0;
                                                    break;
                                                case 9:
                                                    Clase = 9;
                                                    Ejes = 9;
                                                    Rodada = 1;
                                                    TipoVehiculo = 0;
                                                    break;
                                                case 10:
                                                    Clase = 1;
                                                    Ejes = 3;
                                                    Rodada = 0;
                                                    TipoVehiculo = 0;
                                                    break;
                                                case 11:
                                                    Clase = 1;
                                                    Ejes = 4;
                                                    Rodada = 0;
                                                    TipoVehiculo = 0;
                                                    break;
                                                case 12:
                                                    Clase = 2;
                                                    Ejes = 2;
                                                    Rodada = 1;
                                                    TipoVehiculo = 1;
                                                    break;
                                                case 13:
                                                    Clase = 3;
                                                    Ejes = 3;
                                                    Rodada = 1;
                                                    TipoVehiculo = 1;
                                                    break;
                                                case 14:
                                                    Clase = 4;
                                                    Ejes = 4;
                                                    Rodada = 1;
                                                    TipoVehiculo = 1;
                                                    break;
                                                case 15:
                                                    Clase = 10;
                                                    Ejes = 2;
                                                    Rodada = 1;
                                                    TipoVehiculo = 0;
                                                    break;
                                                case 16:
                                                    Clase = 9;
                                                    Ejes = 9 + Convert.ToInt32(MtGlb.oDataRow["CODE_GRILLE_TARIF"]); //n
                                                    Rodada = 1;
                                                    TipoVehiculo = 0;
                                                    break;
                                                case 17:
                                                    Clase = 1;
                                                    if (MtGlb.oDataRow["CODE_GRILLE_TARIF"].ToString() == "@" || MtGlb.oDataRow["CODE_GRILLE_TARIF"].ToString() == ":")
                                                        Ejes = 2 + 8; //n
                                                    else
                                                        Ejes = 2 + Convert.ToInt32(MtGlb.oDataRow["CODE_GRILLE_TARIF"]); //n
                                                    Rodada = 0;
                                                    TipoVehiculo = 0;
                                                    break;
                                                case 18:
                                                    Clase = 9;
                                                    TipoVehiculo = 0;
                                                    Ejes = 10;
                                                    Rodada = 1;
                                                    TipoVehiculo = 0;
                                                    break;
                                                case 19:
                                                    Clase = 11;
                                                    Ejes = 2 + Convert.ToInt32(MtGlb.oDataRow["CODE_GRILLE_TARIF"]); //n
                                                    Rodada = 0;
                                                    TipoVehiculo = 0;
                                                    break;
                                                case 20:
                                                    Clase = 0;
                                                    Ejes = 0;
                                                    Rodada = 0;
                                                    TipoVehiculo = 0;
                                                    break;
                                            }

                                            Sec_piso = Convert.ToInt32(MtGlb.oDataRow["EVENT_NUMBER"]);

                                            if (MtGlb.oDataRow["Shift_number"].ToString() == "1")
                                                Turno = 4;
                                            else if (MtGlb.oDataRow["Shift_number"].ToString() == "2")
                                                Turno = 5;
                                            else if (MtGlb.oDataRow["Shift_number"].ToString() == "3")
                                                Turno = 6;

                                            //<element name="secuencialTC" type="xsd:int" minOccurs="0" maxOccurs="1"/>
                                            SecuencialTC = 0;
                                            //<element name="autorizacionTC" type="xsd:int" minOccurs="0" maxOccurs="1"/>
                                            AutorizacionTC = 0;
                                            //<element name="tarjetaC" type="xsd:string" minOccurs="0" maxOccurs="1"/>
                                            TarjetaC = string.Empty;
                                            //<element name="medioTC" type="xsd:int">
                                            medioTC = 1;
                                            //<!-- 1. IAVE card-->
                                            //<!-- 2. Credit card-->
                                            //</element>
                                            //<element name="statusTC" type="xsd:int" minOccurs="0" maxOccurs="1"/>
                                            StatusTC = 0;
                                            //<element name="UtcTime" type="xsd:dateTime" minOccurs="0" maxOccurs="1"/>
                                            Utctime = Convert.ToDateTime(MtGlb.oDataRow["DATE_TRANSACTION"]).AddHours(5);
                                            //<element name="LocalTime" type="xsd:dateTime" minOccurs="0" maxOccurs="1"/>
                                            LocalTime = Convert.ToDateTime(MtGlb.oDataRow["DATE_TRANSACTION"]);
                                            //<element name="tipoVehiculo" minOccurs="0" maxOccurs="1" type="xsd:unsignedByte"/>
                                            //1 autobus
                                            //0 camion
                                            //tipoVehiculo = 0
                                            //element name="Cuerpo" type="xsd:string" minOccurs="0" maxOccurs="1"/>
                                            //Cuerpo = "A"
                                            Cuerpo = MtGlb.oDataRow["Voie"].ToString().Substring(0, 1);
                                            //fin

                                            resultado = Ws.MoveTransactionsUp(Convert.ToInt32(Secuencial),
                                                                                Convert.ToInt32(Carril),
                                                                                Convert.ToString(Fecha),
                                                                                Convert.ToString(Hora),
                                                                                Convert.ToString(Tarjeta),
                                                                                Convert.ToByte(Status),
                                                                                Convert.ToByte(Clase),
                                                                                Convert.ToByte(Ejes),
                                                                                Convert.ToByte(Rodada),
                                                                                Convert.ToInt32(Sec_piso),
                                                                                Convert.ToByte(Turno),
                                                                                Convert.ToInt32(SecuencialTC),
                                                                                Convert.ToInt32(AutorizacionTC),
                                                                                Convert.ToString(TarjetaC),
                                                                                Convert.ToInt32(medioTC),
                                                                                Convert.ToInt32(StatusTC),
                                                                                Convert.ToDateTime(Utctime.ToString("yyyy-MM-ddTHH:mm:ss")),
                                                                                Convert.ToDateTime(LocalTime.ToString("yyyy-MM-ddTHH:mm:ss")),
                                                                                Convert.ToByte(TipoVehiculo),
                                                                                Convert.ToString(Cuerpo));
                                            //resultado = 1;//comentar o borrar esto y descomentar llamada del servicio
                                            if (resultado == 1)
                                            {
                                                if (MtGlb.oDataRow["CODE_GRILLE_TARIF"].ToString() == "@" || MtGlb.oDataRow["CODE_GRILLE_TARIF"].ToString() == ":")
                                                {
                                                    StrQuerys = "insert into pn_importacion_wsIndra(" +
                                                                "DATE_TRANSACTION, VOIE, EVENT_NUMBER, FOLIO_ECT, Version_Tarif, ID_PAIEMENT, TAB_ID_CLASSE, CLASE_MARCADA, MONTO_MARCADO, ACD_CLASS, CLASE_DETECTADA, MONTO_DETECTADO, CONTENU_ISO, CODE_GRILLE_TARIF, ID_OBS_MP, Shift_number, fecha_ext, PLAZA)values(" +
                                                                "'" + Convert.ToDateTime(MtGlb.oDataRow["DATE_TRANSACTION"]).ToString("yyyyMMdd HH:mm:ss") +
                                                                "','" + MtGlb.oDataRow["VOIE"] + "'," + MtGlb.oDataRow["EVENT_NUMBER"] + "," +
                                                                MtGlb.oDataRow["FOLIO_ECT"] + "," + MtGlb.oDataRow["Version_Tarif"] + "," + MtGlb.oDataRow["ID_PAIEMENT"] +
                                                                "," + MtGlb.oDataRow["TAB_ID_CLASSE"] + ",'" + MtGlb.oDataRow["CLASE_MARCADA"] + "'," + MtGlb.oDataRow["MONTO_MARCADO"] +
                                                                "," + MtGlb.oDataRow["ACD_CLASS"] + ",'" + MtGlb.oDataRow["CLASE_DETECTADA"] + "'," + MtGlb.oDataRow["MONTO_DETECTADO"] +
                                                                ",'" + Tarjeta + "',8,'" + MtGlb.oDataRow["ID_OBS_MP"] + "'," + MtGlb.oDataRow["Shift_number"] + ",'" +
                                                                DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "'," + IdPlazaCobro + ")";
                                                }
                                                else
                                                {
                                                    StrQuerys = "insert into pn_importacion_wsIndra(" +
                                                                "DATE_TRANSACTION, VOIE, EVENT_NUMBER, FOLIO_ECT, Version_Tarif, ID_PAIEMENT, TAB_ID_CLASSE, CLASE_MARCADA, MONTO_MARCADO, ACD_CLASS, CLASE_DETECTADA, MONTO_DETECTADO, CONTENU_ISO, CODE_GRILLE_TARIF, ID_OBS_MP, Shift_number, fecha_ext, PLAZA)values(" +
                                                                "'" + Convert.ToDateTime(MtGlb.oDataRow["DATE_TRANSACTION"]).ToString("yyyyMMdd HH:mm:ss") + "','" +
                                                                MtGlb.oDataRow["VOIE"] + "'," + MtGlb.oDataRow["EVENT_NUMBER"] + "," + MtGlb.oDataRow["FOLIO_ECT"] + "," +
                                                                MtGlb.oDataRow["Version_Tarif"] + "," + MtGlb.oDataRow["ID_PAIEMENT"] + "," + MtGlb.oDataRow["TAB_ID_CLASSE"] +
                                                                ",'" + MtGlb.oDataRow["CLASE_MARCADA"] + "'," + MtGlb.oDataRow["MONTO_MARCADO"] + "," + MtGlb.oDataRow["ACD_CLASS"] +
                                                                ",'" + MtGlb.oDataRow["CLASE_DETECTADA"] + "'," + MtGlb.oDataRow["MONTO_DETECTADO"] + ",'" + Tarjeta + "'," +
                                                                MtGlb.oDataRow["CODE_GRILLE_TARIF"] + ",'" + MtGlb.oDataRow["ID_OBS_MP"] + "'," + MtGlb.oDataRow["Shift_number"] + ",'" +
                                                                DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "'," + IdPlazaCobro + ")";
                                                }
                                            }

                                            MtGlb.InsertQuerySqlServer(StrQuerys);

                                            /*******************************************************************************************/
                                        }
                                    }
                                }
                            }
                            else
                            {
                                event_numbool = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        //using (StreamWriter file = new StreamWriter(path + archivo, true))
                        //{
                        //    file.WriteLine("Error en el proceso ServicioWinProsis: " + i.ToString() + " a las " + DateTime.Now.ToString() + " no existen carriles."); //se agrega información al documento
                        //    file.Dispose();
                        //    file.Close();
                        //}
                        this.EscribeLog("Error en el proceso ServicioWinProsis: " + i.ToString() + " a las " + DateTime.Now.ToString() + " no existen carriles.");
                        timProcess.Enabled = false;
                    }
                }

                // VALIDACIONES
                if (tagempty)
                {
                    //using (StreamWriter file = new StreamWriter(path + archivo, true))
                    //{
                    //    file.WriteLine("Tag vacio: " + i.ToString() + " a las " + DateTime.Now.ToString()); //se agrega información al documento
                    //    file.Dispose();
                    //    file.Close();
                    //}
                    this.EscribeLog("Tag vacio: " + i.ToString() + " a las " + DateTime.Now.ToString());
                    timProcess.Enabled = false;
                }
                else if (monto_detec)
                {
                    //using (StreamWriter file = new StreamWriter(path + archivo, true))
                    //{
                    //    file.WriteLine("Cruce sin tarifa en pos: " + i.ToString() + " a las " + DateTime.Now.ToString()); //se agrega información al documento
                    //    file.Dispose();
                    //    file.Close();
                    //}
                    this.EscribeLog("Cruce sin tarifa en pos: " + i.ToString() + " a las " + DateTime.Now.ToString());
                    timProcess.Enabled = false;
                }
                else if (CarrilInex)
                {
                    //using (StreamWriter file = new StreamWriter(path + archivo, true))
                    //{
                    //    file.WriteLine(); //se agrega información al documento
                    //    file.Dispose();
                    //    file.Close();
                    //}
                    this.EscribeLog("Error en el proceso ServicioWinProsis: " + i.ToString() + " a las " + DateTime.Now.ToString() + " no existe carril.");
                    timProcess.Enabled = false;
                }
                else if (event_numbool)
                {
                    //using (StreamWriter file = new StreamWriter(path + archivo, true))
                    //{
                    //    file.WriteLine("EVENT_NUMBER duplicado: " + i.ToString() + " a las " + DateTime.Now.ToString() + " VOIE: " + MtGlb.oDataRow["VOIE"] + " EVENT_NUMBER: " + MtGlb.oDataRow["EVENT_NUMBER"]); //se agrega información al documento
                    //    file.Dispose();
                    //    file.Close();
                    //}
                    this.EscribeLog("EVENT_NUMBER duplicado: " + i.ToString() + " a las " + DateTime.Now.ToString() + " VOIE: " + MtGlb.oDataRow["VOIE"] + " EVENT_NUMBER: " + MtGlb.oDataRow["EVENT_NUMBER"]);
                    timProcess.Enabled = false;
                }
                else
                {
                    //using (StreamWriter file = new StreamWriter(path + archivo, true))
                    //{
                    //    file.WriteLine("Proceso terminado con exito ServicioWinProsis: " + i.ToString() + " a las " + DateTime.Now.ToString() + " " + "con " + _count.ToString() + " registros."); //se agrega información al documento
                    //    file.Dispose();
                    //    file.Close();
                    //}
                    this.EscribeLog("Proceso terminado con exito ServicioWinProsis: " + i.ToString() + " a las " + DateTime.Now.ToString() + " " + "con " + _count.ToString() + " registros.");
                    timProcess.Enabled = true;
                }


            }
            catch (Exception ex)
            {
                //using (StreamWriter file = new StreamWriter(path + archivo, true))
                //{
                //    file.WriteLine("Error en el proceso ServicioWinProsis: " + i.ToString() + " a las " + DateTime.Now.ToString() + " " + ex.Message + " " + ex.StackTrace); //se agrega información al documento
                //    file.Dispose();
                //    file.Close();
                //}
                this.EscribeLog("Error en el proceso ServicioWinProsis: " + i.ToString() + " a las " + DateTime.Now.ToString() + " " + ex.Message + " " + ex.StackTrace);
                timProcess.Enabled = false;
            }
            finally
            {
                this.EscribeLog("==========FIN DEL PROCESO PROSIS" + i.ToString() + " a las " + DateTime.Now.ToString());

                MtGlb.ExitConnectionDbContext();
                MtGlb.ExitConnectionOracle();
                //MtGlb.ExitConnectionProsis();
            }
        }


        //Metodo para escribir log con el ultimo registro al inicio
        private void EscribeLog(string newRow)
        {
            string contenido = "";
            try
            {
                StreamReader sr = new StreamReader(path + archivo, true);
                contenido = sr.ReadToEnd();
                sr.Dispose();
                sr.Close();
                using (StreamWriter file = new StreamWriter(path + "WindowsService_Temporal.txt", true))
                {
                    file.WriteLine(newRow); //se agrega información al documento
                    file.WriteLine(contenido);
                    file.Dispose();
                    file.Close();
                }
                File.Delete(path + archivo);
                //Renombrar archivo
                File.Move(path + "WindowsService_Temporal.txt", path + archivo);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }

        }
    }
}

#region Comentarios
//if (MtGlb.QueryDataSet_SqlServer(StrQuerys, "DATE_TRANSACTION"))
//{
//    if (MtGlb.DsSqlServer.Tables["DATE_TRANSACTION"].Rows.Count > 0)
//    {
//        foreach (DataRow item in MtGlb.DsSqlServer.Tables["DATE_TRANSACTION"].Rows)
//        {
//            H_inicio_turno = Convert.ToDateTime(item[0]).ToString("yyyy/MM/dd HH:mm:ss");
//        }
//    }
//}
//else if (MtGlb.QueryDataSet_SqlServer_prosis(StrQuerys, "DATE_TRANSACTION"))
//{
//    if (MtGlb.DsSqlServer.Tables["DATE_TRANSACTION"].Rows.Count > 0)
//    {
//        foreach (DataRow item in MtGlb.DsSqlServer.Tables["DATE_TRANSACTION"].Rows)
//        {
//            H_inicio_turno = Convert.ToDateTime(item[0]).ToString("yyyy/MM/dd HH:mm:ss");
//        }
//    }
//}
//else
//{
//    H_inicio_turno = Convert.ToDateTime("2018/10/28 00:00:00").ToString("yyyy/MM/dd HH:mm:ss");
//}
#endregion
