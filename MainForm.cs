using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clave5_Grupo4
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Close();
        }

        private void crearUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsuarioForm usuariosForm = new UsuarioForm();
            usuariosForm.Show();
            this.Close();
        }

        private void crearLocalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalesForm localesForm = new LocalesForm();
            localesForm.Show();
            this.Close();
        }

        private void hacerPedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductosForm productosForm = new ProductosForm();
            productosForm.Show();
            this.Close();
        }

        private void eventosEspecialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PedidosForm pedidosForm = new PedidosForm();
            pedidosForm.Show();
            this.Close();
        }

        private void eventosEspecialesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EventosEspecialesForm eventosForm = new EventosEspecialesForm();
            eventosForm.Show();
            this.Close();
        }
    }
}
