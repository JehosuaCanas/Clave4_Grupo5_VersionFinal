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
    public partial class inicioSesionForm : Form
    {
        // Variable privada para poder tener acceso a los metodos de la base de datos 
        private Conexion db;
        private UsuarioService usuarioService;

        public inicioSesionForm()
        {
            InitializeComponent();
            // Se inicializa la variable db
            db = new Conexion();
            usuarioService = new UsuarioService(db); // Pasamos la instancia de Database directamente
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string contrasena = txtContraseña.Text;

            // Verifica que los campos de email y contraseña tenga datos
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Por favor, ingrese su email y contraseña.");
                return;
            }

            // Llama el método ValidarUsuario desde el servicio de usuario
            var usuario = usuarioService.ValidarUsuario(email, contrasena);

            if (usuario != null)
            {
                MessageBox.Show($"Bienvenido, {usuario.Nombre}!");

                // Aquí puedes abrir la ventana correspondiente según el tipo de usuario
                if (usuario.Tipo == "administrativo")
                {
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                }
                else if (usuario.Tipo == "estudiante" || usuario.Tipo == "docente")
                {
                    AdminForm adminForm = new AdminForm();
                    adminForm.Show();
                }

                this.Hide(); // Ocultar el formulario de inicio de sesión
            }
            else
            {
                MessageBox.Show("Email o contraseña incorrectos.");
            }

        }
    }
}
