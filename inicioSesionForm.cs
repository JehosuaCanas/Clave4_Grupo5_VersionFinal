
using System;
using System.Windows.Forms;


namespace Clave5_Grupo4
{
    public partial class InicioSesioForm : Form
    {
        // Variable privada para poder tener acceso a los metodos de la base de datos 
        private Conexion db;
        private Usuario Usuario;

        public InicioSesioForm()
        {
            InitializeComponent();
            // Se inicializa la variable db
            db = new Conexion();
            Usuario = new Usuario(db.Connection); // Pasamos la instancia de Database directamente
        }

        private void InicioSesioForm_Load(object sender, EventArgs e)
        {
            // Puedes agregar código aquí si es necesario al cargar el formulario
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
            var usuario = Usuario.ValidarUsuario(email, contrasena);

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
                    EstudianteForm estudianteForm = new EstudianteForm();
                    estudianteForm.Show();
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
