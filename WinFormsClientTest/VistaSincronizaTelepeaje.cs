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
        private async void button1_Click(object sender, EventArgs e)
        {
            CuentaInformación cuentaInformación = new CuentaInformación(this.GetPlazaInformation());
            DateTime fechaInicio = DTInicio.Value;
            DateTime fechaFin = DTTermino.Value;

            LogInfo.Text = "Iniciando";
            PlazaEntity plazaSelected = (PlazaEntity)cbPlazas.SelectedItem;
            if (plazaSelected != null)
            {
                try
                {
                    LogInfo.Text = await cuentaInformación.MuestraInformacion(fechaInicio, fechaFin, plazaSelected);
                }
                catch (Exception ex)
                {

                    MessageBox.Show("ERROR:" + ex);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar la Plaza");
            }
        }

        private async void button2_Click(object sender, EventArgs e)
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            
        }
    }
}
