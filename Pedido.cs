using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Clave5_Grupo4
{

    ////Jehosua Abdiel Cañas Tijerino
    ////11/11/2024
    // Clase de Pedido y sus Atributos
    public class Pedido
    {
        public int Id { get; set; }
        public string UsuarioNombre { get; set; }
        public string CafetinNombre { get; set; }
        public decimal Total { get; set; }
        public string MetodoPago { get; set; }
        public DateTime FechaHora { get; set; }
        public List<ProductoPedido> Productos { get; set; } = new List<ProductoPedido>();


        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        ////Conexion a la base de datos 
        private readonly MySqlConnection connection;

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        // Constructor que recibe la conexión
        public Pedido(MySqlConnection connection)
        {
            this.connection = connection;
        }

        // Método para crear un nuevo pedido
        public int CrearPedido(int usuarioId, int cafetinId, decimal total, string metodoPago)
        {
            try
            {
                ////Jehosua Abdiel Cañas Tijerino
                ////11/11/2024
                /////Abre la conexion si no esta abierta
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

                ////Jehosua Abdiel Cañas Tijerino
                ////11/11/2024
                //// Consulta SQL para insertar un  nuevo pedido 
                string query = "INSERT INTO pedidos (usuario_id, cafetin_id, total, metodo_pago, fecha_hora) VALUES (@usuarioId, @cafetinId, @total, @metodoPago, @fechaHora)";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@usuarioId", usuarioId);
                    cmd.Parameters.AddWithValue("@cafetinId", cafetinId);
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.Parameters.AddWithValue("@metodoPago", metodoPago);
                    cmd.Parameters.AddWithValue("@fechaHora", DateTime.Now); // Fecha actual
                    cmd.ExecuteNonQuery();
                    return (int)cmd.LastInsertedId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el pedido: " + ex.Message);
                return -1; // Indica que ocurrió un error
            }
        }

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        // Método para agregar un producto a un pedido
        public bool AgregarProductoAPedido(int pedidoId, int productoId, int cantidad)
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

                ////Jehosua Abdiel Cañas Tijerino
                ////11/11/2024
                /////Consulta SQL para insertar un nuevo producto al pedido 
                string query = "INSERT INTO pedidos_productos (pedido_id, producto_id, cantidad) VALUES (@pedidoId, @productoId, @cantidad)";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@pedidoId", pedidoId);
                    cmd.Parameters.AddWithValue("@productoId", productoId);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.ExecuteNonQuery();
                }
                return true; // Indica que la inserción fue exitosa
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el producto al pedido: " + ex.Message);
                return false; // Indica que ocurrió un error
            }
        }

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        // Método para obtener una lista de pedidos junto con sus productos
        public List<Pedido> ObtenerPedidosConProductos()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            List<Pedido> pedidos = new List<Pedido>();

            ////Jehosua Abdiel Cañas Tijerino
            ////11/11/2024
            //// Consulta SQL para obtener los datos de pedidos y productos relacionados
            string query = @"
                SELECT p.id, u.nombre AS UsuarioNombre, l.nombre AS CafetinNombre, 
                       p.total, p.metodo_pago AS MetodoPago, p.fecha_hora AS FechaHora,
                       pp.producto_id, pp.cantidad, pr.nombre AS ProductoNombre
                FROM pedidos p
                JOIN usuarios u ON p.usuario_id = u.id
                JOIN locales l ON p.cafetin_id = l.id
                LEFT JOIN pedidos_productos pp ON p.id = pp.pedido_id
                LEFT JOIN productos pr ON pp.producto_id = pr.id";

            using (var cmd = new MySqlCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int pedidoId = reader.GetInt32("id");

                    // Buscar el pedido existente o crear uno nuevo
                    Pedido pedido = pedidos.FirstOrDefault(p => p.Id == pedidoId);
                    if (pedido == null)
                    {
                        pedido = new Pedido(connection)
                        {
                            Id = pedidoId,
                            UsuarioNombre = reader.GetString("UsuarioNombre"),
                            CafetinNombre = reader.GetString("CafetinNombre"),
                            Total = reader.GetDecimal("total"),
                            MetodoPago = reader.GetString("MetodoPago"),
                            FechaHora = reader.GetDateTime("FechaHora"),
                            Productos = new List<ProductoPedido>()
                        };
                        pedidos.Add(pedido);
                    }

                    // Agregar producto solo si existe
                    if (!reader.IsDBNull(reader.GetOrdinal("ProductoNombre")))
                    {
                        pedido.Productos.Add(new ProductoPedido
                        {
                            Nombre = reader.GetString("ProductoNombre"),
                            Cantidad = reader.GetInt32("cantidad")
                        });
                    }
                }
            }

            return pedidos;
        }

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        // Método para eliminar un pedido
        public void EliminarPedido(int id)
        {
            string query = "DELETE FROM pedidos WHERE id = @id";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }

    ////Jehosua Abdiel Cañas Tijerino
    ////11/11/2024
    // Modelo para Producto en un Pedido
    public class ProductoPedido
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
    }
}

