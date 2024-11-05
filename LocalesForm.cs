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
    public partial class LocalesForm : Form
    {

        private Local Local;
        private Conexion db;
        private int localIdSeleccionado = -1;
        public LocalesForm()
        {

            InitializeComponent();
            db = new Conexion(); // Crea una nueva instancia de Conexion
            Local = new Local(db.Connection); // Pasa la conexión a LocalService
            lstLocales.SelectedIndexChanged += lstLocales_SelectedIndexChanged;
        }

        private void btnCrearLocal_Click(object sender, EventArgs e)
        {
            Local.CrearLocal(txtNombreLocal.Text, txtUbicacionLocal.Text);
            MessageBox.Show("Local creado exitosamente");
            LimpiarCampos();
        }


        private void btnConsultarLocales_Click(object sender, EventArgs e)
        {
            var locales = Local.ObtenerLocales();
            lstLocales.Items.Clear();
            foreach (var local in locales)
            {
                lstLocales.Items.Add($"{local.Id} - {local.Nombre} - {local.Ubicacion}");
            }
        }

        private void btnModificarLocal_Click(object sender, EventArgs e)
        {
            if (localIdSeleccionado != -1 && ValidarCamposLocal())
            {
                Local.ModificarLocal(localIdSeleccionado, txtNombreLocal.Text, txtUbicacionLocal.Text);
                MessageBox.Show("Local modificado exitosamente");
                btnConsultarLocales_Click(null, null);
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Por favor selecciona un local de la lista para modificar.");
            }
        }

        private void btnEliminarLocal_Click(object sender, EventArgs e)
        {
            if (localIdSeleccionado != -1)
            {
                Local.EliminarLocal(localIdSeleccionado);
                MessageBox.Show("Local eliminado exitosamente");
                btnConsultarLocales_Click(null, null);
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Por favor selecciona un local de la lista para eliminar.");
            }
        }

        private int ObtenerIdSeleccionado()
        {
            if (lstLocales.SelectedItem != null)
            {
                string[] partes = lstLocales.SelectedItem.ToString().Split('-');
                if (int.TryParse(partes[0].Trim(), out int id))
                {
                    return id;
                }
            }
            return -1;
        }

        private void lstLocales_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstLocales.SelectedItem != null)
            {
                string selectedItem = lstLocales.SelectedItem.ToString();
                string[] partes = selectedItem.Split('-');
                if (partes.Length >= 3)
                {
                    try
                    {
                        localIdSeleccionado = Convert.ToInt32(partes[0].Trim());
                        txtNombreLocal.Text = partes[1].Trim();
                        txtUbicacionLocal.Text = partes[2].Trim();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ocurrió un error: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("El formato del local seleccionado es incorrecto.");
                }
            }

        }

        private void LimpiarCampos()
        {
            txtNombreLocal.Clear();
            txtUbicacionLocal.Clear();
            txtNombreLocal.Focus();
            localIdSeleccionado = -1;
        }

        private bool ValidarCamposLocal()
        {
            if (string.IsNullOrWhiteSpace(txtNombreLocal.Text) || string.IsNullOrWhiteSpace(txtUbicacionLocal.Text))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return false;
            }
            return true;
        }
    }
}
    