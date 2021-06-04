
namespace WindowsServiceTelepeaje
{
    partial class VistaSincronizaTelepeaje
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Sincronizar = new System.Windows.Forms.Button();
            this.DTInicio = new System.Windows.Forms.DateTimePicker();
            this.DTTermino = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.DTFin = new System.Windows.Forms.Label();
            this.LogInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Sincronizar
            // 
            this.Sincronizar.Location = new System.Drawing.Point(82, 185);
            this.Sincronizar.Name = "Sincronizar";
            this.Sincronizar.Size = new System.Drawing.Size(180, 58);
            this.Sincronizar.TabIndex = 0;
            this.Sincronizar.Text = "Sincronizar";
            this.Sincronizar.UseVisualStyleBackColor = true;
            this.Sincronizar.Click += new System.EventHandler(this.Sincronizar_Click);
            // 
            // DTInicio
            // 
            this.DTInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTInicio.Location = new System.Drawing.Point(72, 95);
            this.DTInicio.Name = "DTInicio";
            this.DTInicio.Size = new System.Drawing.Size(200, 20);
            this.DTInicio.TabIndex = 1;
            // 
            // DTTermino
            // 
            this.DTTermino.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTTermino.Location = new System.Drawing.Point(337, 95);
            this.DTTermino.Name = "DTTermino";
            this.DTTermino.Size = new System.Drawing.Size(200, 20);
            this.DTTermino.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fecha Inicio";
            // 
            // DTFin
            // 
            this.DTFin.AutoSize = true;
            this.DTFin.Location = new System.Drawing.Point(342, 56);
            this.DTFin.Name = "DTFin";
            this.DTFin.Size = new System.Drawing.Size(51, 13);
            this.DTFin.TabIndex = 4;
            this.DTFin.Text = "Fecha fin";
            // 
            // LogInfo
            // 
            this.LogInfo.Location = new System.Drawing.Point(82, 264);
            this.LogInfo.Multiline = true;
            this.LogInfo.Name = "LogInfo";
            this.LogInfo.Size = new System.Drawing.Size(455, 234);
            this.LogInfo.TabIndex = 5;
            // 
            // VistaSincronizaTelepeaje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 521);
            this.Controls.Add(this.LogInfo);
            this.Controls.Add(this.DTFin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DTTermino);
            this.Controls.Add(this.DTInicio);
            this.Controls.Add(this.Sincronizar);
            this.Name = "VistaSincronizaTelepeaje";
            this.Text = "VistaSincronizaTelepeaje";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Sincronizar;
        private System.Windows.Forms.DateTimePicker DTInicio;
        private System.Windows.Forms.DateTimePicker DTTermino;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label DTFin;
        public System.Windows.Forms.TextBox LogInfo;
    }
}