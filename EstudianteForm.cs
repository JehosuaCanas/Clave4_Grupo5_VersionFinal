﻿using System;
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
    public partial class EstudianteForm : Form
    {
        public EstudianteForm()
        {
            InitializeComponent();
        }

        private void pedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PedidosEstudiante pedidosEstudiante = new PedidosEstudiante();
            pedidosEstudiante.Show();
        }

        private void cerrarAplicacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
