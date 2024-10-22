
namespace Clave5_Grupo4
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crearUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crearLocalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hacerPedidosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eventosEspecialesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eventosEspecialesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeToolStripMenuItem,
            this.crearUsuarioToolStripMenuItem,
            this.crearLocalesToolStripMenuItem,
            this.hacerPedidosToolStripMenuItem,
            this.eventosEspecialesToolStripMenuItem,
            this.eventosEspecialesToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // homeToolStripMenuItem
            // 
            this.homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            this.homeToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.homeToolStripMenuItem.Text = "Home";
            this.homeToolStripMenuItem.Click += new System.EventHandler(this.homeToolStripMenuItem_Click);
            // 
            // crearUsuarioToolStripMenuItem
            // 
            this.crearUsuarioToolStripMenuItem.Name = "crearUsuarioToolStripMenuItem";
            this.crearUsuarioToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.crearUsuarioToolStripMenuItem.Text = "Usuario";
            this.crearUsuarioToolStripMenuItem.Click += new System.EventHandler(this.crearUsuarioToolStripMenuItem_Click);
            // 
            // crearLocalesToolStripMenuItem
            // 
            this.crearLocalesToolStripMenuItem.Name = "crearLocalesToolStripMenuItem";
            this.crearLocalesToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.crearLocalesToolStripMenuItem.Text = "Locales";
            this.crearLocalesToolStripMenuItem.Click += new System.EventHandler(this.crearLocalesToolStripMenuItem_Click);
            // 
            // hacerPedidosToolStripMenuItem
            // 
            this.hacerPedidosToolStripMenuItem.Name = "hacerPedidosToolStripMenuItem";
            this.hacerPedidosToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.hacerPedidosToolStripMenuItem.Text = "Productos";
            this.hacerPedidosToolStripMenuItem.Click += new System.EventHandler(this.hacerPedidosToolStripMenuItem_Click);
            // 
            // eventosEspecialesToolStripMenuItem
            // 
            this.eventosEspecialesToolStripMenuItem.Name = "eventosEspecialesToolStripMenuItem";
            this.eventosEspecialesToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.eventosEspecialesToolStripMenuItem.Text = "Pedidos";
            this.eventosEspecialesToolStripMenuItem.Click += new System.EventHandler(this.eventosEspecialesToolStripMenuItem_Click);
            // 
            // eventosEspecialesToolStripMenuItem1
            // 
            this.eventosEspecialesToolStripMenuItem1.Name = "eventosEspecialesToolStripMenuItem1";
            this.eventosEspecialesToolStripMenuItem1.Size = new System.Drawing.Size(116, 20);
            this.eventosEspecialesToolStripMenuItem1.Text = "Eventos Especiales";
            this.eventosEspecialesToolStripMenuItem1.Click += new System.EventHandler(this.eventosEspecialesToolStripMenuItem1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem homeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem crearUsuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem crearLocalesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hacerPedidosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eventosEspecialesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eventosEspecialesToolStripMenuItem1;
    }
}