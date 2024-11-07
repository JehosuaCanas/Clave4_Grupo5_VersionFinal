using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clave5_Grupo4
{
    public partial class UsuarioForm : Form
    {
        private Conexion db; // Instancia de la clase Conexion
        private Usuario Usuario; // Servicio para la gestión de usuarios
        private int usuarioIdSeleccionado = -1;

        public UsuarioForm()
        {
            InitializeComponent();
            db = new Conexion(); // Inicializa la conexión a la base de datos
            Usuario = new Usuario(db.Connection); // Pasa la propiedad Connection en lugar de db
                                                  // Inicializa el servicio de usuarios

            cmbTipo.Items.AddRange(new string[] { "estudiante", "docente", "administrativo" }); // Opciones del ComboBox
            lstUsuarios.SelectedIndexChanged += lstUsuarios_SelectedIndexChanged; // Evento para cambiar la selección del ListBox
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string tipo = cmbTipo.SelectedItem?.ToString();
            string email = txtEmail.Text;
            string contrasena = txtContraseña.Text;

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(tipo) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            Usuario.CrearUsuario(nombre, tipo, email, contrasena);
            MessageBox.Show("Usuario Creado");
            LimpiarCampos();
            CargarUsuarios(); // Actualiza la lista de usuarios
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CargarUsuarios(); // Carga todos los usuarios
        }

        private void btnModificarUsuario_Click(object sender, EventArgs e)
        {
           
            if (usuarioIdSeleccionado != -1 && cmbTipo.SelectedItem != null)
            {
                string nombre = txtNombre.Text;
                string tipo = cmbTipo.SelectedItem.ToString();
                string email = txtEmail.Text;

                Usuario.ModificarUsuario(usuarioIdSeleccionado, nombre, tipo, email);
                MessageBox.Show("Usuario modificado exitosamente.");
                LimpiarCampos();
                CargarUsuarios(); // Actualiza la lista de usuarios
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario y un tipo de usuario.");
            }
        }

        private void btnEliminarUsuario_Click(object sender, EventArgs e)
        {
            if (usuarioIdSeleccionado != -1)
            {
                Usuario.EliminarUsuario(usuarioIdSeleccionado);
                MessageBox.Show("Usuario eliminado exitosamente.");
                LimpiarCampos();
                CargarUsuarios(); // Actualiza la lista de usuarios
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario para eliminar.");
            }
        }

        private void CargarUsuarios()
        {
            var usuarios = Usuario.ObtenerUsuarios();
            lstUsuarios.Items.Clear();

            foreach (var usuario in usuarios)
            {
                // Mostrar el usuario en el formato id - nombre - tipo - email
                lstUsuarios.Items.Add($"{usuario.Id} - {usuario.Nombre} - {usuario.Tipo} - {usuario.Email}");
            }
        }

        private void lstUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstUsuarios.SelectedItem != null)
            {
                string selectedItem = lstUsuarios.SelectedItem.ToString();
                string[] partes = selectedItem.Split('-');

                if (partes.Length >= 4)
                {
                    usuarioIdSeleccionado = Convert.ToInt32(partes[0].Trim());
                    txtNombre.Text = partes[1].Trim();
                    string tipoUsuario = partes[2].Trim();
                    txtEmail.Text = partes[3].Trim();

                    if (cmbTipo.Items.Contains(tipoUsuario))
                    {
                        cmbTipo.SelectedItem = tipoUsuario;
                    }
                    else
                    {
                        MessageBox.Show("El tipo de usuario no es válido.");
                    }
                }
                else
                {
                    MessageBox.Show("El formato del usuario seleccionado es incorrecto.");
                }
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtContraseña.Clear();
            txtEmail.Clear();
            cmbTipo.SelectedIndex = -1;
            usuarioIdSeleccionado = -1;
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnConsultarUsuario_Load(object sender, EventArgs e)
        {
        }

        private void crearUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }



}























































