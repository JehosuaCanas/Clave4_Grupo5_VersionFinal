using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Clave5_Grupo4
{
    public partial class ProductosForm : Form
    {
        // Autor: Jaime Anthony Serrano Servellón
        // Fecha: 10 de noviembre de 2024

        // Instancia de la clase Producto que maneja la lógica de negocio para los productos
        private Producto Producto;

        // Instancia de la clase Local que maneja la lógica de negocio para los locales
        private Local Local;

        // Variable para almacenar el ID del producto seleccionado
        private int productoIdSeleccionado = -1;

        // Instancia de la clase Conexion para la conexión a la base de datos
        private Conexion db;

        // Constructor del formulario
        public ProductosForm()
        {
            InitializeComponent(); // Inicializa los componentes del formulario (controles, etc.)

            // Crear una nueva instancia de Conexion para interactuar con la base de datos
            db = new Conexion();

            // Inicializar las clases Producto y Local pasando la conexión a la base de datos
            Producto = new Producto(db.Connection);
            Local = new Local(db.Connection);

            // Agregar los tipos de productos al ComboBox
            cmbTipoProducto.Items.AddRange(new string[] { "comida", "bebida", "antojo" });

            // Asignar el evento de cambio de selección en la lista de productos
            lstProductos.SelectedIndexChanged += lstProductos_SelectedIndexChanged_1;

            // Cargar los locales en el ComboBox
            CargarLocalesEnComboBox();
        }

        // Método para cargar los locales en el ComboBox de selección
        private void CargarLocalesEnComboBox()
        {
            // Obtener la lista de locales con su ID y nombre
            var locales = Local.ObtenerLocalesConId(); // Utiliza LocalService para obtener los locales
            cmbLocales.DataSource = locales;
            cmbLocales.DisplayMember = "Nombre";
            cmbLocales.ValueMember = "Id";
        }

        // Método para validar que el horario ingresado sea en formato correcto
        private bool EsHorarioValido(string horario)
        {
            // Intentar parsear el horario en los formatos HH:mm:ss o HH:mm
            return TimeSpan.TryParse(horario, out _) || TimeSpan.TryParseExact(horario, "hh\\:mm", null, out _);
        }

        // Evento que maneja el clic en el botón de crear producto
        private void btnCrearProducto_Click_2(object sender, EventArgs e)
        {
            // Validar el formato del horario
            if (!EsHorarioValido(txtHorarioProducto.Text))
            {
                MessageBox.Show("Por favor ingresa un horario válido en formato 24 horas (HH:mm).");
                return;
            }

            // Obtener el ID del local seleccionado en el ComboBox
            int localId = (int)cmbLocales.SelectedValue;

            // Llamar al método para crear el producto en la base de datos
            Producto.CrearProducto(
                txtNombreProducto.Text,
                Convert.ToDecimal(txtPrecioProducto.Text),
                cmbTipoProducto.SelectedItem.ToString(),
                TimeSpan.Parse(txtHorarioProducto.Text),
                localId
            );

            // Informar al usuario que el producto fue creado exitosamente
            MessageBox.Show("Producto creado exitosamente");

            // Limpiar los campos del formulario
            LimpiarCampos();
        }

        // Evento que maneja el clic en el botón de consultar los productos
        private void btnConsultarProductos_Click_1(object sender, EventArgs e)
        {
            // Obtener los productos del local seleccionado
            var productos = Producto.ObtenerProductos((int)cmbLocales.SelectedValue);

            // Limpiar la lista de productos
            lstProductos.Items.Clear();

            // Agregar los productos obtenidos a la lista en el formulario
            foreach (var producto in productos)
            {
                lstProductos.Items.Add(producto);
            }
        }

        // Evento que maneja el clic en el botón de modificar un producto
        private void btnModificarProducto_Click_1(object sender, EventArgs e)
        {
            // Verificar que un producto esté seleccionado y que los campos sean válidos
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

                    // Llamar al método de la clase Producto para modificar el producto en la base de datos
                    Producto.ModificarProducto(
                        productoIdSeleccionado,
                        txtNombreProducto.Text,
                        precioProducto,
                        cmbTipoProducto.SelectedItem.ToString(),
                        horarioProducto,
                        localId
                    );

                    // Informar al usuario que el producto fue modificado exitosamente
                    MessageBox.Show("Producto modificado exitosamente");

                    // Consultar nuevamente la lista de productos para reflejar los cambios
                    btnConsultarProductos_Click_1(null, null);
                }
                catch (FormatException ex)
                {
                    // Manejar cualquier error de formato y mostrar un mensaje
                    MessageBox.Show("Error de formato: Asegúrate de que los campos de precio y horario tengan un formato válido.\n" + ex.Message);
                }
            }
            else
            {
                // Si no se seleccionó un producto o los campos no son válidos, mostrar un mensaje de error
                MessageBox.Show("Por favor selecciona un producto de la lista para modificar.");
            }

            // Limpiar los campos del formulario
            LimpiarCampos();
        }

        // Evento que maneja el clic en el botón de eliminar un producto
        private void btnEliminarProducto_Click_1(object sender, EventArgs e)
        {
            // Obtener el ID del producto seleccionado
            int productoId = ObtenerIdSeleccionado();

            // Verificar que el ID sea válido
            if (productoId != -1)
            {
                // Llamar al método para eliminar el producto
                Producto.EliminarProducto(productoId);

                // Informar al usuario que el producto fue eliminado exitosamente
                MessageBox.Show("Producto eliminado exitosamente");

                // Consultar nuevamente los productos para reflejar los cambios
                btnConsultarProductos_Click_1(null, null);
            }
        }

        // Método para obtener el ID del producto seleccionado en la lista
        private int ObtenerIdSeleccionado()
        {
            // Verificar que un producto haya sido seleccionado
            if (lstProductos.SelectedItem != null)
            {
                string[] partes = lstProductos.SelectedItem.ToString().Split('-');
                return Convert.ToInt32(partes[0].Trim());
            }
            return -1; // Retornar -1 si no se seleccionó ningún producto
        }

        // Evento que maneja el cambio de selección en la lista de productos
        private void lstProductos_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // Verificar que un producto haya sido seleccionado
            if (lstProductos.SelectedItem != null)
            {
                string selectedItem = lstProductos.SelectedItem.ToString();
                string[] partes = selectedItem.Split('-');

                // Verificar que el formato del producto seleccionado sea correcto
                if (partes.Length >= 6)
                {
                    try
                    {
                        // Asignar los valores correspondientes a los campos del formulario
                        productoIdSeleccionado = Convert.ToInt32(partes[0].Trim());
                        txtNombreProducto.Text = partes[1].Trim();
                        txtPrecioProducto.Text = partes[2].Trim();
                        cmbTipoProducto.SelectedItem = partes[3].Trim();
                        txtHorarioProducto.Text = partes[4].Trim();

                        // Establecer el local seleccionado en el ComboBox
                        if (int.TryParse(partes[5].Trim(), out int cafetinId))
                        {
                            cmbLocales.SelectedValue = cafetinId;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar cualquier error durante la asignación de valores
                        MessageBox.Show($"Ocurrió un error: {ex.Message}");
                    }
                }
                else
                {
                    // Si el formato del producto seleccionado es incorrecto, mostrar un mensaje de error
                    MessageBox.Show("El formato del producto seleccionado es incorrecto.");
                }
            }
        }

        // Método para limpiar los campos del formulario después de una acción
        private void LimpiarCampos()
        {
            // Limpiar los campos de texto
            txtNombreProducto.Clear();
            txtPrecioProducto.Clear();
            txtHorarioProducto.Clear();

            // Establecer el foco en el primer campo (nombre del producto)
            txtNombreProducto.Focus();

            // Restablecer el ID del producto seleccionado
            productoIdSeleccionado = -1;
        }

        // Método para validar que todos los campos del producto sean válidos
        private bool ValidarCamposProducto()
        {
            // Verificar que los campos no estén vacíos y que el precio y horario sean válidos
            if (string.IsNullOrWhiteSpace(txtNombreProducto.Text) ||
                string.IsNullOrWhiteSpace(txtPrecioProducto.Text) ||
                cmbTipoProducto.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtHorarioProducto.Text))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return false;
            }

            // Verificar que el precio sea un valor decimal y que el horario sea válido
            if (!decimal.TryParse(txtPrecioProducto.Text, out _) || !EsHorarioValido(txtHorarioProducto.Text))
            {
                MessageBox.Show("Por favor, ingresa un precio y horario válido.");
                return false;
            }

            return true;
        }
    }
}

   