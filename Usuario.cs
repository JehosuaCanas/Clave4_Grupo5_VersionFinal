using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Clave5_Grupo4
{
    public class Usuario
    {
        // Atributos de usuario
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Email { get; set; }

        // Conexión de base de datos
        private readonly MySqlConnection connection;

        // Constructor que recibe una conexión de base de datos
        public Usuario(MySqlConnection dbConnection)
        {
            this.connection = dbConnection;
        }

        // Métodos para gestionar usuarios
        public Usuario ValidarUsuario(string email, string contrasena)
        {
            string query = "SELECT id, nombre, tipo FROM usuarios WHERE email = @Email AND contraseña = @Contrasena";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Usuario(connection)
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Nombre = reader["nombre"].ToString(),
                            Tipo = reader["tipo"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        public void CrearUsuario(string nombre, string tipo, string email, string contraseña)
        {
            string query = "INSERT INTO usuarios (nombre, tipo, email, contraseña) VALUES (@nombre, @tipo, @email, @contraseña)";
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
                    usuarios.Add(new Usuario(connection)
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
            List<Usuario> usuarios = new List<Usuario>();
            string query = "SELECT id, nombre FROM usuarios"; // Consulta simple para probar

            try
            {
                using (var cmd = new MySqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new Usuario(connection)
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nombre = reader["nombre"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener usuarios: {ex.Message}");
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
