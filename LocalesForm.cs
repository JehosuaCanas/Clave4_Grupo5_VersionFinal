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
        // Autor: Jaime Anthony Serrano Servellón
        // Fecha: 10 de noviembre de 2024

        // Instancia de la clase Local que se encargará de la lógica de negocios relacionada con los locales
        private Local Local;

        // Instancia de la clase Conexion para manejar la conexión a la base de datos
        private Conexion db;

        // Variable que guarda el ID del local seleccionado en la lista
        private int localIdSeleccionado = -1;

        // Constructor del formulario, donde se inicializan los componentes y objetos necesarios
        public LocalesForm()
        {

            InitializeComponent(); // Inicializa los componentes del formulario (controles, etc.)

            // Crear una instancia de la clase Conexion para interactuar con la base de datos
            db = new Conexion();

            // Crear una instancia de la clase Local y pasarle la conexión a la base de datos
            Local = new Local(db.Connection); // Pasa la conexión a LocalService

            // Asignar el evento de cambio de selección en la lista de locales
            lstLocales.SelectedIndexChanged += lstLocales_SelectedIndexChanged_1;
        }

        // Método que maneja el clic en el botón de crear un nuevo local
        private void btnCrearLocal_Click_1(object sender, EventArgs e)
        {
            // Llamar al método CrearLocal de la clase Local para agregar el nuevo local
            Local.CrearLocal(txtNombreLocal.Text, txtUbicacionLocal.Text);

            // Informar al usuario que el local fue creado exitosamente
            MessageBox.Show("Local creado exitosamente");

            // Limpiar los campos del formulario
            LimpiarCampos();
        }

        // Método que maneja el clic en el botón de consultar los locales existentes
        private void btnConsultarLocales_Click_1(object sender, EventArgs e)
        {
            // Obtener todos los locales utilizando el método ObtenerLocales de la clase Local
            var locales = Local.ObtenerLocales();

            // Limpiar la lista de locales para evitar duplicados
            lstLocales.Items.Clear();

            // Recorrer los locales obtenidos y agregarlos a la lista en el formulario
            foreach (var local in locales)
            {
                lstLocales.Items.Add($"{local.Id} - {local.Nombre} - {local.Ubicacion}");
            }
        }

        // Método que maneja el clic en el botón de modificar un local
        private void btnModificarLocal_Click_1(object sender, EventArgs e)
        {
            // Verificar que un local haya sido seleccionado y que los campos sean válidos
            if (localIdSeleccionado != -1 && ValidarCamposLocal())
            {
                // Llamar al método ModificarLocal de la clase Local para modificar los datos del local seleccionado
                Local.ModificarLocal(localIdSeleccionado, txtNombreLocal.Text, txtUbicacionLocal.Text);

                // Informar al usuario que el local fue modificado exitosamente
                MessageBox.Show("Local modificado exitosamente");

                // Volver a consultar la lista de locales para mostrar los cambios
                btnConsultarLocales_Click_1(null, null);

                // Limpiar los campos del formulario
                LimpiarCampos();
            }
            else
            {
                // Si no se seleccionó un local o los campos son inválidos, mostrar un mensaje de error
                MessageBox.Show("Por favor selecciona un local de la lista para modificar.");
            }
        }

        // Método que maneja el clic en el botón de eliminar un local
        private void btnEliminarLocal_Click_1(object sender, EventArgs e)
        {
            // Verificar que un local haya sido seleccionado
            if (localIdSeleccionado != -1)
            {
                // Llamar al método EliminarLocal de la clase Local para eliminar el local seleccionado
                Local.EliminarLocal(localIdSeleccionado);

                // Informar al usuario que el local fue eliminado exitosamente
                MessageBox.Show("Local eliminado exitosamente");

                // Volver a consultar la lista de locales para reflejar los cambios
                btnConsultarLocales_Click_1(null, null);

                // Limpiar los campos del formulario
                LimpiarCampos();
            }
            else
            {
                // Limpiar los campos del formulario
                MessageBox.Show("Por favor selecciona un local de la lista para eliminar.");
            }
        }

        // Método para obtener el ID del local seleccionado en la lista
        private int ObtenerIdSeleccionado()
        {
            // Verificar que se haya seleccionado un elemento en la lista
            if (lstLocales.SelectedItem != null)
            {
                // Dividir el texto del local seleccionado para obtener el ID
                string[] partes = lstLocales.SelectedItem.ToString().Split('-');

                // Intentar convertir la primera parte (ID) a un número entero
                if (int.TryParse(partes[0].Trim(), out int id))
                {
                    return id; // Retornar el ID del local seleccionado
                }
            }

            // Si no se pudo obtener un ID válido, retornar -1
            return -1;
        }

        // Método que maneja el cambio de selección en la lista de locales
        private void lstLocales_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // Verificar que se haya seleccionado un elemento en la lista
            if (lstLocales.SelectedItem != null)
            {
                // Obtener el texto del local seleccionado
                string selectedItem = lstLocales.SelectedItem.ToString();

                // Dividir el texto para obtener el ID, nombre y ubicación del local
                string[] partes = selectedItem.Split('-');

                // Verificar que el formato del texto sea correcto (al menos 3 partes)
                if (partes.Length >= 3)
                {
                    try
                    {
                        // Asignar el ID del local seleccionado
                        localIdSeleccionado = Convert.ToInt32(partes[0].Trim());

                        // Asignar el nombre y ubicación del local a los campos de texto
                        txtNombreLocal.Text = partes[1].Trim();
                        txtUbicacionLocal.Text = partes[2].Trim();
                    }
                    catch (Exception ex)
                    {
                        // Si ocurre un error durante la conversión, mostrar el mensaje de error
                        MessageBox.Show($"Ocurrió un error: {ex.Message}");
                    }
                }
                else
                {
                    // Si el formato del texto no es válido, mostrar un mensaje de error
                    MessageBox.Show("El formato del local seleccionado es incorrecto.");
                }
            }
        }

        // Método para limpiar los campos del formulario después de una acción
        private void LimpiarCampos()
        {
            // Limpiar los campos de texto
            txtNombreLocal.Clear();
            txtUbicacionLocal.Clear();

            // Establecer el foco en el primer campo (nombre del local)
            txtNombreLocal.Focus();

            // Restablecer el ID del local seleccionado
            localIdSeleccionado = -1;
        }

        // Método para validar que los campos de nombre y ubicación no estén vacíos
        private bool ValidarCamposLocal()
        {
            // Verificar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(txtNombreLocal.Text) || string.IsNullOrWhiteSpace(txtUbicacionLocal.Text))
            {
                // Si los campos están vacíos, mostrar un mensaje de error y retornar falso
                MessageBox.Show("Por favor, completa todos los campos.");
                return false;
            }

            // Si los campos son válidos, retornar verdadero
            return true;
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Close();
        }

        private void usuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsuarioForm usuariosForm = new UsuarioForm();
            usuariosForm.Show();
            this.Close();
        }

        private void localesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalesForm localesForm = new LocalesForm();
            localesForm.Show();
            this.Close();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductosForm productosForm = new ProductosForm();
            productosForm.Show();
            this.Close();
        }

        private void pedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PedidosForm pedidosForm = new PedidosForm();
            pedidosForm.Show();
            this.Close();
        }

        private void eventosEspecialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventosEspecialesForm eventosForm = new EventosEspecialesForm();
            eventosForm.Show();
            this.Close();
        }

        private void cerrarAplicacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
    