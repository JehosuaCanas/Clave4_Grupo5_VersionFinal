using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Clave5_Grupo4
{
    public partial class ProductosForm : Form
    {
        private Producto Producto;
        private Local Local;
        private int productoIdSeleccionado = -1;
        private Conexion db;


        public ProductosForm()
        {
            InitializeComponent();
            db = new Conexion();  // Crea una nueva instancia de Conexion
            Producto = new Producto(db.Connection);
            Local = new Local(db.Connection);


            cmbTipoProducto.Items.AddRange(new string[] { "comida", "bebida", "antojo" });
            lstProductos.SelectedIndexChanged += lstProductos_SelectedIndexChanged;
            CargarLocalesEnComboBox();
        }

        private void CargarLocalesEnComboBox()
        {
            var locales = Local.ObtenerLocalesConId(); // Utiliza LocalService para obtener los locales
            cmbLocales.DataSource = locales;
            cmbLocales.DisplayMember = "Nombre";
            cmbLocales.ValueMember = "Id";
        }

        private bool EsHorarioValido(string horario)
        {
            return TimeSpan.TryParse(horario, out _) || TimeSpan.TryParseExact(horario, "hh\\:mm", null, out _);
        }

        private void btnCrearProducto_Click(object sender, EventArgs e)
        {
            if (!EsHorarioValido(txtHorarioProducto.Text))
            {
                MessageBox.Show("Por favor ingresa un horario válido en formato 24 horas (HH:mm).");
                return;
            }

            int localId = (int)cmbLocales.SelectedValue;
            Producto.CrearProducto(
                txtNombreProducto.Text,
                Convert.ToDecimal(txtPrecioProducto.Text),
                cmbTipoProducto.SelectedItem.ToString(),
                TimeSpan.Parse(txtHorarioProducto.Text),
                localId
            );

            MessageBox.Show("Producto creado exitosamente");
            LimpiarCampos();

        }

        private void btnConsultarProductos_Click(object sender, EventArgs e)
        {
            var productos = Producto.ObtenerProductos((int)cmbLocales.SelectedValue);
            lstProductos.Items.Clear();
            foreach (var producto in productos)
            {
                lstProductos.Items.Add(producto);
            }

        }

        private void btnModificarProducto_Click(object sender, EventArgs e)
        {
            if (productoIdSeleccionado != -1 && ValidarCamposProducto())
            {
                int localId = (int)cmbLocales.SelectedValue;
                try
                {
                    decimal precioProducto = Convert.ToDecimal(txtPrecioProducto.Text);
                    TimeSpan horarioProducto;

                    // Intenta parsear en el formato HH:mm:ss primero, si no funciona, intenta con HH:mm
                    if (!TimeSpan.TryParse(txtHorarioProducto.Text, out horarioProducto))
                    {
                        // Si no se puede parsear, intenta con el formato HH:mm
                        if (!TimeSpan.TryParseExact(txtHorarioProducto.Text, "hh\\:mm", null, out horarioProducto))
                        {
                            MessageBox.Show("Formato de horario no válido. Usa HH:mm o HH:mm:ss.");
                            return;
                        }
                    }

                    Producto.ModificarProducto(
                        productoIdSeleccionado,
                        txtNombreProducto.Text,
                        precioProducto,
                        cmbTipoProducto.SelectedItem.ToString(),
                        horarioProducto,
                        localId
                    );

                    MessageBox.Show("Producto modificado exitosamente");
                    btnConsultarProductos_Click(null, null);
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Error de formato: Asegúrate de que los campos de precio y horario tengan un formato válido.\n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor selecciona un producto de la lista para modificar.");
            }

            LimpiarCampos();

        }


        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            int productoId = ObtenerIdSeleccionado();
            if (productoId != -1)
            {
                Producto.EliminarProducto(productoId);
                MessageBox.Show("Producto eliminado exitosamente");
                btnConsultarProductos_Click(null, null);
            }

        }

        private int ObtenerIdSeleccionado()
        {
            if (lstProductos.SelectedItem != null)
            {
                string[] partes = lstProductos.SelectedItem.ToString().Split('-');
                return Convert.ToInt32(partes[0].Trim());
            }
            return -1;
        }

        private void lstProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstProductos.SelectedItem != null)
            {
                string selectedItem = lstProductos.SelectedItem.ToString();
                string[] partes = selectedItem.Split('-');
                if (partes.Length >= 6)
                {
                    try
                    {
                        productoIdSeleccionado = Convert.ToInt32(partes[0].Trim());
                        txtNombreProducto.Text = partes[1].Trim();
                        txtPrecioProducto.Text = partes[2].Trim();
                        cmbTipoProducto.SelectedItem = partes[3].Trim();
                        txtHorarioProducto.Text = partes[4].Trim();

                        if (int.TryParse(partes[5].Trim(), out int cafetinId))
                        {
                            cmbLocales.SelectedValue = cafetinId;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ocurrió un error: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("El formato del producto seleccionado es incorrecto.");
                }
            }

        }

        private void LimpiarCampos()
        {
            txtNombreProducto.Clear();
            txtPrecioProducto.Clear();
            txtHorarioProducto.Clear();
            txtNombreProducto.Focus();
            productoIdSeleccionado = -1;
        }

        private bool ValidarCamposProducto()
        {
            if (string.IsNullOrWhiteSpace(txtNombreProducto.Text) ||
                string.IsNullOrWhiteSpace(txtPrecioProducto.Text) ||
                cmbTipoProducto.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtHorarioProducto.Text))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return false;
            }

            if (!decimal.TryParse(txtPrecioProducto.Text, out _) || !EsHorarioValido(txtHorarioProducto.Text))
            {
                MessageBox.Show("Por favor, ingresa un precio y horario válido.");
                return false;
            }

            return true;
        }

        private void btnCrearProducto_Click_1(object sender, EventArgs e)
        {

        }
    }
}

   