using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WinFormsClientTest;

namespace WindowsServiceTelepeaje
{
    public partial class VistaSincronizaTelepeaje : Form
    {
        List<PlazaEntity> listaPlazas;
        public VistaSincronizaTelepeaje()
        {
            InitializeComponent();
            DTInicio.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            DTTermino.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            listaPlazas = new HandlePlazas().getListaPlazas();
            cbPlazas.DataSource = listaPlazas;

        }

        private void Sincronizar_Click(object sender, EventArgs e)
        {
            ProcessInfo processInfo = new ProcessInfo(GetPlazaInformation());
            DateTime fechaInicio = DTInicio.Value;
            DateTime fechaFin = DTTermino.Value;
            LogInfo.Text = "Iniciando";
            LogServiceTelepeage.EscribeLog("FechaInicio: " + fechaInicio + "FechaFin: " + fechaFin);
            Console.WriteLine(fechaFin + " " + fechaInicio);
            processInfo.ExecuteProcess(fechaInicio, fechaFin);
            LogInfo.Text = LogServiceTelepeage.MostrarInformacion();
        }

        private void Count_Click(object sender, EventArgs e)
        {
            try
            {
                //CountInfo countInfo = new CountInfo();
                DateTime fechaInicio = DTInicio.Value;
                DateTime fechaFin = DTTermino.Value;
                
                LogInfo.Text = "Iniciando";
                Count.Enabled = false;
                Sincronizar.Enabled = false;
                //countInfo.ExecuteProcess(fechaInicio, fechaFin, out int conteoSql, out int conteoOracle);
                //int resta = conteoOracle - conteoSql;
                //LogInfo.Text = "Oracle: " + conteoOracle + " " + "SQL: " + conteoSql + " Diferencia: " + resta;
                PlazaEntity plazaSelected = GetPlazaInformation();
                LogInfo.Text = plazaSelected.IPService + " Con oracle: " + plazaSelected.OracleCon;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            finally
            {
                Count.Enabled = true;
                Sincronizar.Enabled = true;
            }
        }


        /*
           PlazaEntity plazaSelected = (PlazaEntity)cbPlazas.SelectedItem;
            LogInfo.Text = plazaSelected.IPService + " Con oracle: " +plazaSelected.OracleCon;
         */
        private void DTTermino_ValueChanged(object sender, EventArgs e)
        {

        }
        public PlazaEntity GetPlazaInformation()
        {
            PlazaEntity plazaSelected = (PlazaEntity)cbPlazas.SelectedItem;
            try
            {
                if (plazaSelected != null)
                {
                    return plazaSelected;
                }
                else
                {
                    throw new Exception("No tenemos datos para la conexion");
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CuentaInformación cuentaInformación = new CuentaInformación(this.GetPlazaInformation());
            DateTime fechaInicio = DTInicio.Value;
            DateTime fechaFin = DTTermino.Value;

            LogInfo.Text = "Iniciando";
            PlazaEntity plazaSelected = (PlazaEntity)cbPlazas.SelectedItem;
            if (plazaSelected != null)
            {
                LogInfo.Text = cuentaInformación.MuestraInformacion(fechaInicio, fechaFin, plazaSelected);
            }
            else
            {
                MessageBox.Show("Debe seleccionar la Plaza");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CuentaInformación cuentaInformación = new CuentaInformación(this.GetPlazaInformation());
            DateTime fechaInicio = DTInicio.Value;
            DateTime fechaFin = DTTermino.Value;

            //LogInfo.Text = "Iniciando";
            PlazaEntity plazaSelected = (PlazaEntity)cbPlazas.SelectedItem;
            if (plazaSelected != null)
            {
                LogInfo.Text = plazaSelected.OracleCon;
                LogInfo.Text = cuentaInformación.CuentaTransaccionesOracle(fechaInicio, fechaFin) + "oracle";
            }
            else
            {
                MessageBox.Show("Debe seleccionar la Plaza");
            }
           
        }
    }
}
