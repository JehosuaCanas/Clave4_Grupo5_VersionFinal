using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Clave5_Grupo4
{
    class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Email { get; set; }
    }
    // Servicio para la gestión de usuarios
    public class UsuarioService
    {
        private readonly MySqlConnection connection;

        // Constructor que recibe una instancia de Database
        public UsuarioService(Conexion db)
        {
            this.connection = db.Connection; // Acceso a la propiedad Connection correctamente
        }

        public Usuario ValidarUsuario(string email, string contrasena)
        {
            // Consulta SQL la cual busca el usuario que coincida con los datos ingresados 
            string query = "SELECT id, nombre, tipo FROM usuarios WHERE email = @Email AND contraseña = @Contrasena";
            using (var cmd = new MySqlCommand(query, connection))
            {
                // Se agregan los valores de email y contraseña a la consulta SQL
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                // Ejecuta la consulta y devuelve un objeto que permite leer los resultados de la consulta fila por fila.
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Usuario
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Nombre = reader["nombre"].ToString(),
                            Tipo = reader["tipo"].ToString()
                        };
                    }
                }
            }
            return null; // Usuario no encontrado o credenciales incorrectas
        }

        public void CrearUsuario(string nombre, string tipo, string email, string contraseña)
        {
            string query = "INSERT INTO usuarios (nombre, tipo,email,contraseña) VALUES (@nombre, @tipo,@email,@contraseña)";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@contraseña", contraseña);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            string query = "SELECT * FROM usuarios";
            using (var cmd = new MySqlCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    usuarios.Add(new Usuario
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Nombre = reader["nombre"].ToString(),
                        Tipo = reader["tipo"].ToString(),
                        Email = reader["email"].ToString()
                    });
                }
            }
            return usuarios;
        }

        public List<Usuario> ObtenerUsuariosConId()
        {
            //// Se crea una lista vacia
            List<Usuario> usuarios = new List<Usuario>();
            //Se hace una consulta a la base de datos para obtener los datos que contiene la tabla de usuarios
            string query = "SELECT id, nombre FROM usuarios";
            using (var cmd = new MySqlCommand(query, connection))
            ////se crea un nuevo objeto Usuario con el ID y nombre extraído de la base de datos y se añade a la lista usuarios.
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    usuarios.Add(new Usuario
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Nombre = reader["nombre"].ToString()
                    });
                }
            }
            return usuarios;
        }

        public void ModificarUsuario(int id, string nombre, string tipo, string email)
        {
            string query = "UPDATE usuarios SET nombre = @nombre, tipo = @tipo, email = @email WHERE id = @id";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarUsuario(int id)
        {
            string query = "DELETE FROM usuarios WHERE id = @id";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }


}
