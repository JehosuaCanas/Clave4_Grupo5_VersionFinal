using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Clave5_Grupo4
{
    public class Usuario
    {
        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        // Atributos de la clase usuario
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Email { get; set; }

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        // Conexión de base de datos
        private readonly MySqlConnection connection;


        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        // Constructor que recibe una conexión de base de datos y la asigna a la variable de conexion
        public Usuario(MySqlConnection dbConnection)
        {
            this.connection = dbConnection;
        }

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        // Métodos para validar el usuario 
        public Usuario ValidarUsuario(string email, string contrasena)
        {
            ////Jehosua Abdiel Cañas Tijerino
            ////11/11/2024
            //// Define la consulta SQL para validar usuario con email y contraseña
            string query = "SELECT id, nombre, tipo FROM usuarios WHERE email = @Email AND contraseña = @Contrasena";
            using (var cmd = new MySqlCommand(query, connection))
            {

                ////Jehosua Abdiel Cañas Tijerino
                ////11/11/2024
                //// Añade parametros para evitar inyeccion SQL
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                using (var reader = cmd.ExecuteReader())
                {
                    ////Jehosua Abdiel Cañas Tijerino
                    ////11/11/2024
                    //// Si encuentra un registro que coincide crea y retorna un nuevo objeto de Usuarip
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


        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        //// Metodo para Crear un nuevo usuario en la base de datos
        public void CrearUsuario(string nombre, string tipo, string email, string contraseña)
        {

            ////Jehosua Abdiel Cañas Tijerino
            ////11/11/2024
            //// Consulta SQL para insertar un nuevo registro 
            string query = "INSERT INTO usuarios (nombre, tipo, email, contraseña) VALUES (@nombre, @tipo, @email, @contraseña)";
            using (var cmd = new MySqlCommand(query, connection))
            {
                ////Jehosua Abdiel Cañas Tijerino
                ////11/11/2024
                //// Añade los valores de los parametros 
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@contraseña", contraseña);
                cmd.ExecuteNonQuery();
            }
        }


        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        //// Metodo para obtener la lista de todos los usuarios en la base de datos 
        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            ////Jehosua Abdiel Cañas Tijerino
            ////11/11/2024
            //// Consulta SQL para obtener todos los usuarios 
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

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        ///// Método para obtener una lista simplificada de usuarios con solo Id y Nombre.
        public List<Usuario> ObtenerUsuariosConId()
        {
            List<Usuario> usuarios = new List<Usuario>();
            ////Jehosua Abdiel Cañas Tijerino
            ////11/11/2024
            ///// Consulta SQL simplificada.
            string query = "SELECT id, nombre FROM usuarios"; 

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


        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        ///// Método para modificar los datos de un usuario existente en la base de datos.
        public void ModificarUsuario(int id, string nombre, string tipo, string email)
        {
            ////Jehosua Abdiel Cañas Tijerino
            ////11/11/2024
            // Consulta SQL para actualizar los datos del usuario.
            string query = "UPDATE usuarios SET nombre = @nombre, tipo = @tipo, email = @email WHERE id = @id";
            using (var cmd = new MySqlCommand(query, connection))
            {
                ////Jehosua Abdiel Cañas Tijerino
                ////11/11/2024
                // Añade los valores de los parámetros.
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.ExecuteNonQuery();
            }
        }

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        // Método para eliminar un usuario de la base de datos.
        public void EliminarUsuario(int id)
        {
            ////Jehosua Abdiel Cañas Tijerino
            ////11/11/2024
            // Consulta SQL para eliminar el usuario con el Id especificado.
            string query = "DELETE FROM usuarios WHERE id = @id";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
