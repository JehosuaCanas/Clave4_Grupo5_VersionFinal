
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Clave5_Grupo4
{
    // Modelo de Local
    public class Local
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }

    // Servicio para la gestión de locales
    public class LocalService
    {
        private readonly MySqlConnection connection;

        public LocalService(Conexion db)
        {
            this.connection = db.Connection;
        }




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

        public List<Local> ObtenerLocales()
        {
            List<Local> locales = new List<Local>();
            string query = "SELECT * FROM locales";
            using (var cmd = new MySqlCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    locales.Add(new Local
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Nombre = reader["nombre"].ToString(),
                        Ubicacion = reader["ubicacion"].ToString()
                    });
                }
            }
            return locales;
        }

        public List<Local> ObtenerLocalesConId()
        {
            List<Local> locales = new List<Local>();
            string query = "SELECT id, nombre FROM locales";
            using (var cmd = new MySqlCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    locales.Add(new Local
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Nombre = reader["nombre"].ToString()
                    });
                }
            }
            return locales;
        }

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