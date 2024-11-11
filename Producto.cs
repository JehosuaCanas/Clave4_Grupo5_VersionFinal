

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Clave5_Grupo4
{

    ////Jehosua Abdiel Cañas Tijerino
    ////11/11/2024
    // Clase Producto y sus atributos 
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Tipo { get; set; }

        private readonly MySqlConnection connection;

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        // Constructor que recibe directamente la conexión
        public Producto(MySqlConnection connection)
        {
            this.connection = connection;
        }

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        //// Metodo para crear Producto
        public void CrearProducto(string nombre, decimal precio, string tipo, TimeSpan horario_disponible, int cafetinId)
        {
            ////Jehosua Abdiel Cañas Tijerino
            ////11/11/2024
            //// Consulta SQL para insertar un nuevo producto 
            string query = "INSERT INTO productos (nombre, precio, tipo, horario_disponible, cafetin_id) VALUES (@nombre, @precio, @tipo, @horario_disponible, @cafetinId)";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@precio", precio);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@horario_disponible", horario_disponible);
                cmd.Parameters.AddWithValue("@cafetinId", cafetinId);
                cmd.ExecuteNonQuery();
            }
        }

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        ////Metodo para obtener los productos de la base de datos 
        public List<string> ObtenerProductos(int cafetinId)
        {
            List<string> productos = new List<string>();
            string query = @"
            SELECT p.id, COALESCE(p.nombre, 'Sin nombre') AS nombre, 
                   COALESCE(p.precio, 0) AS precio, 
                   COALESCE(p.tipo, 'Sin tipo') AS tipo, 
                   COALESCE(p.horario_disponible, '00:00:00') AS horario_disponible, 
                   COALESCE(l.nombre, 'Sin local') AS nombre_local
            FROM productos p
            JOIN locales l ON p.cafetin_id = l.id
            WHERE p.cafetin_id = @cafetinId";

            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@cafetinId", cafetinId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productos.Add($"{reader["id"]} - {reader["nombre"]} - {reader["precio"]} - {reader["tipo"]} - {reader["horario_disponible"]} - {reader["nombre_local"]}");
                    }
                }
            }
            return productos;
        }

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        ////Metod para obtener una lista de productos segun un cafetin
        public List<Producto> ObtenerProductos2(int cafetinId)
        {
            List<Producto> productos = new List<Producto>();
            string query = @"
            SELECT p.id, p.nombre, p.precio, p.tipo 
            FROM productos p 
            WHERE p.cafetin_id = @cafetinId";

            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@cafetinId", cafetinId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productos.Add(new Producto(connection) // Pasar la conexión a la nueva instancia
                        {
                            Id = reader.GetInt32("id"),
                            Nombre = reader.GetString("nombre"),
                            Precio = reader.GetDecimal("precio"),
                            Tipo = reader.GetString("tipo")
                        });
                    }
                }
            }
            return productos;
        }

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        /////Metodo para modificar los datos un producto segun su id
        public void ModificarProducto(int id, string nombre, decimal precio, string tipo, TimeSpan horario, int cafetinId)
        {
            string query = "UPDATE productos SET nombre = @nombre, precio = @precio, tipo = @tipo, horario_disponible = @horario, cafetin_id = @cafetinId WHERE id = @id";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@precio", precio);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@horario", horario);
                cmd.Parameters.AddWithValue("@cafetinId", cafetinId);
                cmd.ExecuteNonQuery();
            }
        }

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        ////Metodo para eliminar un producto en la base de datos segun su id 
        public void EliminarProducto(int id)
        {
            string query = "DELETE FROM productos WHERE id = @id";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        ////Metodo para  obtener un producto específico por su id, devolviéndolo como un objeto Producto
        private Producto ObtenerProductoPorId(int id)
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            string query = "SELECT id, nombre, precio, tipo FROM productos WHERE id = @id";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Producto(connection) // Pasar la conexión a la nueva instancia
                        {
                            Id = reader.GetInt32("id"),
                            Nombre = reader.GetString("nombre"),
                            Precio = reader.GetDecimal("precio"),
                            Tipo = reader.GetString("tipo")
                        };
                    }
                }
            }
            return null;
        }


        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        ////Este método valida que la cantidad de productos de tipo "Antojito" no exceda de 3 y que solo se vendan entre las 14:00 y las 16:00 horas. Si se cumplen estas restricciones, devuelve true; de lo contrario, false.
        public bool ValidarRestriccionesAntojitos(List<int> productosIds)
        {
            int antojitosContador = 0;

            foreach (var productoId in productosIds)
            {
                var producto = ObtenerProductoPorId(productoId);
                if (producto != null && producto.Tipo == "antojo")
                {
                    antojitosContador++;
                }
            }

            TimeSpan horaActual = DateTime.Now.TimeOfDay;
            bool isWithinTimeFrame = horaActual >= new TimeSpan(14, 0, 0) && horaActual <= new TimeSpan(16, 0, 0);

            if (antojitosContador > 3 || (antojitosContador > 0 && !isWithinTimeFrame))
            {
                return false;
            }

            return true;
        }
    }
}



