
namespace Clave5_Grupo4
{
    partial class EventosEspecialesForm
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
            this.btnCrearEventosEspecial = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPedidoId = new System.Windows.Forms.TextBox();
            this.txtMontoMaximo = new System.Windows.Forms.TextBox();
            this.txtMontoMinimo = new System.Windows.Forms.TextBox();
            this.txtNombreEvento = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCrearEventosEspecial
            // 
            this.btnCrearEventosEspecial.Location = new System.Drawing.Point(187, 254);
            this.btnCrearEventosEspecial.Name = "btnCrearEventosEspecial";
            this.btnCrearEventosEspecial.Size = new System.Drawing.Size(114, 23);
            this.btnCrearEventosEspecial.TabIndex = 17;
            this.btnCrearEventosEspecial.Text = "Crear Evento";
            this.btnCrearEventosEspecial.UseVisualStyleBackColor = true;
            this.btnCrearEventosEspecial.Click += new System.EventHandler(this.btnCrearEventosEspecial_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(76, 197);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Id Pedido";
            // 
            // txtPedidoId
            // 
            this.txtPedidoId.Location = new System.Drawing.Point(217, 197);
            this.txtPedidoId.Name = "txtPedidoId";
            this.txtPedidoId.Size = new System.Drawing.Size(100, 20);
            this.txtPedidoId.TabIndex = 15;
            // 
            // txtMontoMaximo
            // 
            this.txtMontoMaximo.Location = new System.Drawing.Point(217, 154);
            this.txtMontoMaximo.Name = "txtMontoMaximo";
            this.txtMontoMaximo.Size = new System.Drawing.Size(100, 20);
            this.txtMontoMaximo.TabIndex = 14;
            // 
            // txtMontoMinimo
            // 
            this.txtMontoMinimo.Location = new System.Drawing.Point(217, 111);
            this.txtMontoMinimo.Name = "txtMontoMinimo";
            this.txtMontoMinimo.Size = new System.Drawing.Size(100, 20);
            this.txtMontoMinimo.TabIndex = 13;
            // 
            // txtNombreEvento
            // 
            this.txtNombreEvento.Location = new System.Drawing.Point(217, 67);
            this.txtNombreEvento.Name = "txtNombreEvento";
            this.txtNombreEvento.Size = new System.Drawing.Size(100, 20);
            this.txtNombreEvento.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(76, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Monto Maximo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Monto Minimo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Nombre del Evento";
            // 
            // EventosEspecialesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCrearEventosEspecial);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPedidoId);
            this.Controls.Add(this.txtMontoMaximo);
            this.Controls.Add(this.txtMontoMinimo);
            this.Controls.Add(this.txtNombreEvento);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "EventosEspecialesForm";
            this.Text = "EventosEspeciales";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCrearEventosEspecial;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPedidoId;
        private System.Windows.Forms.TextBox txtMontoMaximo;
        private System.Windows.Forms.TextBox txtMontoMinimo;
        private System.Windows.Forms.TextBox txtNombreEvento;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}