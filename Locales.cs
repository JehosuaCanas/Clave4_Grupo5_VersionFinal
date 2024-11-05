
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Clave5_Grupo4
{
    // Modelo de Local con métodos de gestión
    public class Local
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }

        private readonly MySqlConnection connection;

        // Constructor que recibe la conexión
        public Local(MySqlConnection connection)
        {
            this.connection = connection;
        }

        // Método para crear un nuevo local
        public void CrearLocal(string nombre, string ubicacion)
        {
            string query = "INSERT INTO locales (nombre, ubicacion) VALUES (@nombre, @ubicacion)";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@ubicacion", ubicacion);
                cmd.ExecuteNonQuery();
            }
        }

        // Método para obtener todos los locales
        public List<Local> ObtenerLocales()
        {
            List<Local> locales = new List<Local>();
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

        // Método para obtener locales solo con id y nombre
        public List<Local> ObtenerLocalesConId()
        {
            List<Local> locales = new List<Local>();
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

        // Método para modificar un local
        public void ModificarLocal(int id, string nombre, string ubicacion)
        {
            string query = "UPDATE locales SET nombre = @nombre, ubicacion = @ubicacion WHERE id = @id";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@ubicacion", ubicacion);
                cmd.ExecuteNonQuery();
            }
        }

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

        public override string ToString()
        {
            return Nombre;
        }
    }
}