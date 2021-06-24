﻿using ServiceTelepeaje.Logic;
using System;
using System.Windows.Forms;
using WinFormsClientTest;

namespace WindowsServiceTelepeaje
{
    public partial class VistaSincronizaTelepeaje : Form
    {
        public VistaSincronizaTelepeaje()
        {
            InitializeComponent();
            DTInicio.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            DTTermino.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            var v = new HandlePlazas().getListaPlazas();
            cbPlazas.DataSource = v;

        }

        private void Sincronizar_Click(object sender, EventArgs e)
        {
            ProcessInfo processInfo = new ProcessInfo();
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
                CountInfo countInfo = new CountInfo();
                DateTime fechaInicio = DTInicio.Value;
                DateTime fechaFin = DTTermino.Value;
                
                LogInfo.Text = "Iniciando";
                Count.Enabled = false;
                Sincronizar.Enabled = false;
                countInfo.ExecuteProcess(fechaInicio, fechaFin, out int conteoSql, out int conteoOracle);
                int resta = conteoOracle - conteoSql;
                LogInfo.Text = "Oracle: " + conteoOracle + " " + "SQL: " + conteoSql + " Diferencia: " + resta;
                PlazaEntity plazaSelected = (PlazaEntity)cbPlazas.SelectedItem;
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
    }
}
