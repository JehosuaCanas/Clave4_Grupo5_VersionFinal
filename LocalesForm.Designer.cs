﻿
namespace Clave5_Grupo4
{
    partial class LocalesForm
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
            this.lstLocales = new System.Windows.Forms.ListBox();
            this.btnConsultarLocales = new System.Windows.Forms.Button();
            this.btnEliminarLocal = new System.Windows.Forms.Button();
            this.btnModificarLocal = new System.Windows.Forms.Button();
            this.btnCrearLocal = new System.Windows.Forms.Button();
            this.txtUbicacionLocal = new System.Windows.Forms.TextBox();
            this.txtNombreLocal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstLocales
            // 
            this.lstLocales.FormattingEnabled = true;
            this.lstLocales.Location = new System.Drawing.Point(102, 220);
            this.lstLocales.Name = "lstLocales";
            this.lstLocales.Size = new System.Drawing.Size(443, 160);
            this.lstLocales.TabIndex = 17;
            // 
            // btnConsultarLocales
            // 
            this.btnConsultarLocales.Location = new System.Drawing.Point(453, 166);
            this.btnConsultarLocales.Name = "btnConsultarLocales";
            this.btnConsultarLocales.Size = new System.Drawing.Size(106, 23);
            this.btnConsultarLocales.TabIndex = 16;
            this.btnConsultarLocales.Text = "Consultar Locales";
            this.btnConsultarLocales.UseVisualStyleBackColor = true;
            // 
            // btnEliminarLocal
            // 
            this.btnEliminarLocal.Location = new System.Drawing.Point(335, 166);
            this.btnEliminarLocal.Name = "btnEliminarLocal";
            this.btnEliminarLocal.Size = new System.Drawing.Size(93, 23);
            this.btnEliminarLocal.TabIndex = 15;
            this.btnEliminarLocal.Text = "Eliminar Local";
            this.btnEliminarLocal.UseVisualStyleBackColor = true;
            // 
            // btnModificarLocal
            // 
            this.btnModificarLocal.Location = new System.Drawing.Point(224, 166);
            this.btnModificarLocal.Name = "btnModificarLocal";
            this.btnModificarLocal.Size = new System.Drawing.Size(105, 23);
            this.btnModificarLocal.TabIndex = 14;
            this.btnModificarLocal.Text = "Modificar Local";
            this.btnModificarLocal.UseVisualStyleBackColor = true;
            // 
            // btnCrearLocal
            // 
            this.btnCrearLocal.Location = new System.Drawing.Point(102, 166);
            this.btnCrearLocal.Name = "btnCrearLocal";
            this.btnCrearLocal.Size = new System.Drawing.Size(102, 23);
            this.btnCrearLocal.TabIndex = 13;
            this.btnCrearLocal.Text = "Agregar Local";
            this.btnCrearLocal.UseVisualStyleBackColor = true;
            // 
            // txtUbicacionLocal
            // 
            this.txtUbicacionLocal.Location = new System.Drawing.Point(241, 99);
            this.txtUbicacionLocal.Name = "txtUbicacionLocal";
            this.txtUbicacionLocal.Size = new System.Drawing.Size(100, 20);
            this.txtUbicacionLocal.TabIndex = 12;
            // 
            // txtNombreLocal
            // 
            this.txtNombreLocal.Location = new System.Drawing.Point(241, 57);
            this.txtNombreLocal.Name = "txtNombreLocal";
            this.txtNombreLocal.Size = new System.Drawing.Size(100, 20);
            this.txtNombreLocal.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Ubicacion";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Nombre Local";
            // 
            // LocalesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lstLocales);
            this.Controls.Add(this.btnConsultarLocales);
            this.Controls.Add(this.btnEliminarLocal);
            this.Controls.Add(this.btnModificarLocal);
            this.Controls.Add(this.btnCrearLocal);
            this.Controls.Add(this.txtUbicacionLocal);
            this.Controls.Add(this.txtNombreLocal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "LocalesForm";
            this.Text = "LocalesForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstLocales;
        private System.Windows.Forms.Button btnConsultarLocales;
        private System.Windows.Forms.Button btnEliminarLocal;
        private System.Windows.Forms.Button btnModificarLocal;
        private System.Windows.Forms.Button btnCrearLocal;
        private System.Windows.Forms.TextBox txtUbicacionLocal;
        private System.Windows.Forms.TextBox txtNombreLocal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}