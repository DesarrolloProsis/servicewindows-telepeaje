using ServiceTelepeaje.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsServiceTelepeaje
{
    public partial class VistaSincronizaTelepeaje : Form
    {
        public VistaSincronizaTelepeaje()
        {
            InitializeComponent();
            DTInicio.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            DTTermino.CustomFormat = "yyyy/MM/dd HH:mm:ss";

        }

        private void Sincronizar_Click(object sender, EventArgs e)
        {
            ProcessInfo processInfo = new ProcessInfo();
            DateTime fechaInicio = DTInicio.Value;
            DateTime fechaFin = DTTermino.Value;
            LogInfo.Text = "Iniciando";
            processInfo.EscribeLog("FechaInicio: " + fechaInicio + "FechaFin: " + fechaFin);
            Console.WriteLine(fechaFin + " " + fechaInicio);
            processInfo.ExecuteProcess(fechaInicio, fechaFin);
            LogInfo.Text = processInfo.mostrarInformacion();
        }

        private void Count_Click(object sender, EventArgs e)
        {
            CountInfo countInfo = new CountInfo(); 
            DateTime fechaInicio = DTInicio.Value;
            DateTime fechaFin = DTTermino.Value;     
            LogInfo.Text = "Iniciando";
            countInfo.ExecuteProcess(fechaInicio, fechaFin ,out int conteoSql, out int conteoOracle);
            LogInfo.Text = "Oracle: " + conteoOracle + " " + "SQL: " + conteoSql;
        }
    }
}
