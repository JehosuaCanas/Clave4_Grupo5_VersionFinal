using System;
using System.Windows.Forms;

namespace Clave5_Grupo4
{
    public partial class EventosEspecialesForm : Form
    {
        private readonly EventosEspeciales eventosEspeciales; // Instancia de la clase EventosEspeciales
        public EventosEspecialesForm()
        {
            InitializeComponent();
            var db = new Conexion(); // Crear la instancia de conexión
            eventosEspeciales = new EventosEspeciales(db); // Inicializar EventosEspeciales con la conexión
        }

        private void btnCrearEventosEspecial_Click_1(object sender, EventArgs e)
        {
            try
            {
                int pedidoId = Convert.ToInt32(txtPedidoId.Text);
                string nombreEvento = txtNombreEvento.Text;
                decimal montoMinimo = Convert.ToDecimal(txtMontoMinimo.Text);
                decimal montoMaximo = Convert.ToDecimal(txtMontoMaximo.Text);

                // Llamar al método para crear el evento especial
                eventosEspeciales.CrearPedidoEventoEspecial(pedidoId, nombreEvento, montoMinimo, montoMaximo);
                MessageBox.Show("Evento especial creado exitosamente");
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error de formato: Asegúrate de que los campos sean válidos.\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}");
            }

            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtPedidoId.Clear();
            txtNombreEvento.Clear();
            txtMontoMaximo.Clear();
            txtMontoMinimo.Clear();
            txtNombreEvento.Focus();
        }
    }
}
    