using System;
using System.Windows.Forms;

namespace Clave5_Grupo4
{
    public partial class EventosEspecialesForm : Form
    {
        // Instancia de la clase EventosEspeciales que se encargará de la lógica del negocio
        private readonly EventosEspeciales eventosEspeciales; // Instancia de la clase EventosEspeciales

        // Nombre del autor y la fecha actual
        // Autor: Jaime Anthony Serrano Servellón
        // Fecha: 10 de noviembre de 2024

        public EventosEspecialesForm()
        {
            InitializeComponent(); // Inicializa los componentes del formulario (controles, etc.)
            var db = new Conexion();  // Crear una instancia de la clase Conexion, que se utilizará para realizar operaciones con la base de datos
            eventosEspeciales = new EventosEspeciales(db); // Inicializar la clase EventosEspeciales con la conexión a la base de datos
        }
        // Método manejador del evento click del botón para crear el evento especial
        private void btnCrearEventosEspecial_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Intentamos leer los valores introducidos en los campos de texto del formulario
                // Convertimos el texto a los tipos de datos correspondientes
                int pedidoId = Convert.ToInt32(txtPedidoId.Text); // Convertimos el texto del PedidoId a entero
                string nombreEvento = txtNombreEvento.Text; // Tomamos el nombre del evento como texto
                decimal montoMinimo = Convert.ToDecimal(txtMontoMinimo.Text); // Convertimos el monto mínimo a decimal
                decimal montoMaximo = Convert.ToDecimal(txtMontoMaximo.Text); // Convertimos el monto máximo a decimal

                // Llamamos al método CrearPedidoEventoEspecial de la clase EventosEspeciales para crear el evento
                // Pasamos los parámetros obtenidos del formulario
                eventosEspeciales.CrearPedidoEventoEspecial(pedidoId, nombreEvento, montoMinimo, montoMaximo);

                // Si todo va bien, mostramos un mensaje al usuario indicando que el evento fue creado con éxito
                MessageBox.Show("Evento especial creado exitosamente");
            }
            catch (FormatException ex)
            {
                // Si ocurre un error de formato (por ejemplo, si el texto no puede convertirse a un número), mostramos un mensaje de error
                MessageBox.Show("Error de formato: Asegúrate de que los campos sean válidos.\n" + ex.Message);
            }
            catch (Exception ex)
            {
                // Si ocurre cualquier otro tipo de error, mostramos un mensaje con el detalle del error
                MessageBox.Show($"Ocurrió un error: {ex.Message}");
            }
            // Independientemente de si hubo un error o no, limpiamos los campos del formulario
            LimpiarFormulario();
        }

        // Método para limpiar los campos del formulario después de intentar crear el evento
        private void LimpiarFormulario()
        {
            // Limpiamos el contenido de los campos de texto
            txtPedidoId.Clear();
            txtNombreEvento.Clear();
            txtMontoMaximo.Clear();
            txtMontoMinimo.Clear();
            txtNombreEvento.Focus();
        }



        ////Codigo del Menu
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
    }
}
    