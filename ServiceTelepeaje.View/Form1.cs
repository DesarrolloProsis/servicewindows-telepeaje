using ServiceTelepeaje.Logic;
using System;
using System.Windows.Forms;

namespace ServiceTelepeaje.View
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Hola");
            ProcesaInformacion procesaInformacion = new ProcesaInformacion();
            procesaInformacion.ExecuteProcess();
        }
    }
}
