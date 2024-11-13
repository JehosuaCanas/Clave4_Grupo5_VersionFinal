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
    public partial class PedidosForm : Form
    {
        private Producto Producto;
        private Local local; // Usamos la clase Local directamente
        private Conexion db;
        private Usuario usuario; // Usamos la clase Usuario directamente
        private Pedido Pedido;
        private decimal total = 0; // Total price

        public PedidosForm()
        {
            InitializeComponent();
            db = new Conexion(); // Inicializa la conexión a la base de datos
            usuario = new Usuario(db.Connection); // Inicializa la clase Usuario
            Producto = new Producto(db.Connection);
            local = new Local(db.Connection); // Inicializa la clase Local
            Pedido = new Pedido(db.Connection);
            CargarUsuarios();
            CargarCafetines();
        }

        private void CargarUsuarios()
        {
            List<Usuario> usuarios = usuario.ObtenerUsuariosConId();
            comboBoxUsuarios.DataSource = usuarios;
            comboBoxUsuarios.DisplayMember = "Nombre";
        }

        private void CargarCafetines()
        {
            List<Local> locales = local.ObtenerLocalesConId();
            comboBoxCafetines.DataSource = locales;
            comboBoxCafetines.DisplayMember = "Nombre";
            comboBoxCafetines.SelectedIndexChanged += new EventHandler(comboBoxCafetines_SelectedIndexChanged);
        }

        private void CargarProductos(int cafetinId)
        {
            List<Producto> productos = Producto.ObtenerProductos2(cafetinId);
            comboBoxProductos.DataSource = productos;
            comboBoxProductos.DisplayMember = "Nombre";
        }
        private void btnCrearPedido_Click(object sender, EventArgs e)
        {
            if (comboBoxUsuarios.SelectedItem == null || comboBoxCafetines.SelectedItem == null || productosTemp.Count == 0)
            {
                MessageBox.Show("Debes seleccionar un usuario, un cafetín y agregar al menos un producto.");
                return;
            }

            // Obtener IDs seleccionados y método de pago
            int usuarioId = ((Usuario)comboBoxUsuarios.SelectedItem).Id;
            int cafetinId = ((Local)comboBoxCafetines.SelectedItem).Id;
            string metodoPago = cmbMetodoPago.SelectedItem.ToString();

            // Validar restricciones de "Antojitos"
            var productosIds = productosTemp.Select(p => p.Producto.Id).ToList();
            if (!Producto.ValidarRestriccionesAntojitos(productosIds))
            {
                MessageBox.Show("El pedido no cumple con las restricciones para productos de tipo 'Antojito'. Solo puedes incluir hasta 3 antojitos y en el horario permitido (14:00 - 16:00).");
                return;
            }

            // Crear el pedido
            int pedidoId = Pedido.CrearPedido(usuarioId, cafetinId, total, metodoPago);
            if (pedidoId <= 0)
            {
                MessageBox.Show("No se pudo crear el pedido. Verifica los datos y vuelve a intentarlo.");
                return;
            }

            // Agregar los productos al pedido desde la lista temporal
            foreach (var item in productosTemp)
            {
                bool resultado = Pedido.AgregarProductoAPedido(pedidoId, item.Producto.Id, item.Cantidad);
                if (!resultado)
                {
                    MessageBox.Show("No se pudo agregar el producto al pedido.");
                }
            }

            MessageBox.Show("Pedido creado y productos agregados exitosamente.");
            LimpiarFormulario();
        }

        // Modificar el método LimpiarFormulario para limpiar la lista temporal
        private void LimpiarFormulario()
        {
            comboBoxUsuarios.SelectedIndex = -1;
            comboBoxCafetines.SelectedIndex = -1;
            txtTotal.Clear();
            cmbMetodoPago.SelectedIndex = -1;
            productosTemp.Clear(); // Limpiar la lista temporal
            total = 0;
            txtCantidadProducto.Clear();
        }







        private void comboBoxCafetines_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cafetinSeleccionado = (Local)comboBoxCafetines.SelectedItem;
            if (cafetinSeleccionado != null)
            {
                CargarProductos(cafetinSeleccionado.Id);
            }
        }

        // Lista temporal para almacenar productos seleccionados y sus cantidades
        private List<(Producto Producto, int Cantidad)> productosTemp = new List<(Producto, int)>();

        private void button1_Click(object sender, EventArgs e)
        {
            // Capturar el producto seleccionado
            var productoSeleccionado = (Producto)comboBoxProductos.SelectedItem;

            // Comprobar si la cantidad ingresada es válida
            if (int.TryParse(txtCantidadProducto.Text, out int cantidad) && productoSeleccionado != null && cantidad > 0)
            {
                // Comprobar si el producto ya está en la lista temporal
                var productoExistenteIndex = productosTemp.FindIndex(p => p.Producto.Id == productoSeleccionado.Id);

                if (productoExistenteIndex == -1)
                {
                    // Agregar el producto a la lista temporal
                    productosTemp.Add((productoSeleccionado, cantidad));
                    MessageBox.Show($"{cantidad} unidades de {productoSeleccionado.Nombre} han sido agregadas al pedido.");
                }
                else
                {
                    // Crear una nueva tupla con la cantidad actualizada
                    var nuevoProducto = (productosTemp[productoExistenteIndex].Producto, productosTemp[productoExistenteIndex].Cantidad + cantidad);
                    productosTemp[productoExistenteIndex] = nuevoProducto; // Reemplazar la tupla existente
                    MessageBox.Show($"Se ha actualizado la cantidad de {productoSeleccionado.Nombre} a {productosTemp[productoExistenteIndex].Cantidad}.");
                }

                // Actualizar el total al agregar o actualizar el producto
                ActualizarTotal();
            }
            else
            {
                string errorMessage = "Por favor, introduce una cantidad válida";
                if (productoSeleccionado == null)
                {
                    errorMessage += " y selecciona un producto.";
                }
                MessageBox.Show(errorMessage);
            }
        }



        // Método para actualizar el total basado en los productos temporales
        private void ActualizarTotal()
        {
            total = productosTemp.Sum(p => p.Producto.Precio * p.Cantidad); // Calcular el total
            txtTotal.Text = total.ToString("C2"); // Mostrar el total en formato moneda
        }







        private void btnConsultarPedido_Click(object sender, EventArgs e)
        {
            // Limpiar el DataGridView antes de cargar nuevos datos
            dgvPedidos.Rows.Clear();

            var pedidos = Pedido.ObtenerPedidosConProductos();

            // Iterar sobre cada pedido
            foreach (var pedido in pedidos)
            {
                // Concatenar los productos en el formato "Producto(xCantidad)"
                string productosConcat = string.Join(", ", pedido.Productos.Select(p => $"{p.Nombre}({p.Cantidad})"));

                // Agregar la fila al DataGridView
                dgvPedidos.Rows.Add(
                    pedido.Id,                   // Columna 1: ID Pedido
                    pedido.UsuarioNombre,        // Columna 2: Nombre Usuario
                    pedido.CafetinNombre,        // Columna 3: Nombre Cafetín
                    pedido.Total.ToString("C"),  // Columna 4: Total (formato moneda)
                    pedido.MetodoPago,           // Columna 5: Método Pago
                    pedido.FechaHora.ToString("g"), // Columna 6: Fecha
                    productosConcat               // Columna 7: Productos
                );
            }

            if (pedidos.Count == 0)
            {
                MessageBox.Show("No se encontraron pedidos.");
            }
        }








        ////Esto no va

        private void dgvPedidos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void cerrarAplicacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
