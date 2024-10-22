using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;


namespace Clave5_Grupo4
{
    // Modelo de Producto
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Tipo { get; set; }
    }

    // Servicio para la gestión de productos
    public class ProductoService
    {
        private readonly MySqlConnection connection;

        public ProductoService(Conexion db)
        {
            this.connection = db.Connection;
        }



        public void CrearProducto(string nombre, decimal precio, string tipo, TimeSpan horario_disponible, int cafetinId)
        {
            /////Se define una consulta SQL para insertar un nuevo producto.
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


        ////Este método obtiene una lista de productos pertenecientes a un cafetín específico, y devuelve los resultados en forma de cadenas que incluyen el ID, nombre, precio, tipo, horario disponible, y el nombre del local.

        public List<string> ObtenerProductos(int cafetinId)
        {
            List<string> productos = new List<string>();
            ////Se hace la consulta a la base de datos para selecciona datos de la tabla productos y se une a la tabla locales para obtener el nombre del local.
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
                        productos.Add(new Producto
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


        public void EliminarProducto(int id)
        {
            string query = "DELETE FROM productos WHERE id = @id";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        private Producto ObtenerProductoPorId(int id)
        {
            // Usar la conexión existente de la clase ProductoService
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open(); // Asegurarse de que la conexión esté abierta
            }

            string query = "SELECT id, nombre, precio, tipo FROM productos WHERE id = @id";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Producto
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





        public Producto ObtenerProductoPorNombre(string nombreProducto)
        {
            Producto producto = null;
            MySqlConnection conn = connection;
            // Asegúrse de que la conexión está abierta
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }

            string query = "SELECT Id, Nombre, Precio FROM productos WHERE Nombre = @nombre";
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@nombre", nombreProducto);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        producto = new Producto
                        {
                            Id = reader.GetInt32("Id"),
                            Nombre = reader.GetString("Nombre"),
                            Precio = reader.GetDecimal("Precio")
                        };
                    }
                }
            }

            return producto;
        }


        public bool ValidarRestriccionesAntojitos(List<int> productosIds)
        {
            int antojitosContador = 0;

            // Recorrer los IDs de productos y contar cuántos son antojitos
            foreach (var productoId in productosIds)
            {
                var producto = ObtenerProductoPorId(productoId); // Obtener el producto por su ID
                if (producto != null && producto.Tipo == "Antojito")
                {
                    antojitosContador++;
                }
            }

            // Validar el horario actual
            TimeSpan horaActual = DateTime.Now.TimeOfDay;
            bool isWithinTimeFrame = horaActual >= new TimeSpan(14, 0, 0) && horaActual <= new TimeSpan(16, 0, 0);

            // Verificar si el número de antojitos excede 3 o si están fuera del horario permitido
            if (antojitosContador > 3 || (antojitosContador > 0 && !isWithinTimeFrame))
            {
                return false; // La validación falló
            }

            return true; // La validación pasó
        }

    }
}