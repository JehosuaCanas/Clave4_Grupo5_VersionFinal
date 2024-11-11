
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Clave5_Grupo4
{
    ////Jehosua Abdiel Cañas Tijerino
    ////11/11/2024
    // Clase Local y sus atributos 
    public class Local
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        //// Conexión a la base de datos

        private readonly MySqlConnection connection;

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        // Constructor que recibe la conexión
        public Local(MySqlConnection connection)
        {
            this.connection = connection;
        }

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        // Método para crear un nuevo local
        public void CrearLocal(string nombre, string ubicacion)
        {
            ////Jehosua Abdiel Cañas Tijerino
            ////11/11/2024
            //// Consulta para ingresar un nuevo Local a la base de datos 
            string query = "INSERT INTO locales (nombre, ubicacion) VALUES (@nombre, @ubicacion)";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@ubicacion", ubicacion);
                cmd.ExecuteNonQuery();
            }
        }

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        // Método para obtener todos los locales
        public List<Local> ObtenerLocales()
        {
            List<Local> locales = new List<Local>();

            ////Jehosua Abdiel Cañas Tijerino
            ////11/11/2024
            //// Consulta SQL para obtener todos los locales que hay en la base de datos 
            string query = "SELECT * FROM locales";
            using (var cmd = new MySqlCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    locales.Add(new Local(connection)
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Nombre = reader["nombre"].ToString(),
                        Ubicacion = reader["ubicacion"].ToString()
                    });
                }
            }
            return locales;
        }
        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        // Método para obtener locales solo con id y nombre
        public List<Local> ObtenerLocalesConId()
        {
            List<Local> locales = new List<Local>();

            ////Jehosua Abdiel Cañas Tijerino
            ////11/11/2024
            //// Consulta SQL  para seleccionar locales con sus respectiva id,nombre
            string query = "SELECT id, nombre FROM locales";
            using (var cmd = new MySqlCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    locales.Add(new Local(connection)
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Nombre = reader["nombre"].ToString()
                    });
                }
            }
            return locales;
        }

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        // Método para modificar un local
        public void ModificarLocal(int id, string nombre, string ubicacion)
        {
            ////Jehosua Abdiel Cañas Tijerino
            ////11/11/2024
            //// Consulta SQL para actualizar un local
            string query = "UPDATE locales SET nombre = @nombre, ubicacion = @ubicacion WHERE id = @id";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@ubicacion", ubicacion);
                cmd.ExecuteNonQuery();
            }
        }

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        // Método para eliminar un local
        public void EliminarLocal(int id)
        {
            string query = "DELETE FROM locales WHERE id = @id";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

    
    }
}