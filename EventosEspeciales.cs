

using MySql.Data.MySqlClient;

namespace Clave5_Grupo4
{
    ////Jehosua Abdiel Cañas Tijerino
    ////11/11/2024
    //// Clase que representa los eventos especiales asociados a pedidos
    public class EventosEspeciales
    {
        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        ///// Conexión a la base de datos
        private readonly MySqlConnection connection;

        public EventosEspeciales(Conexion dbConnection)
        {
            connection = dbConnection.Connection;
        }

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        ///// Método para crear un nuevo registro de evento especial asociado a un pedido específico
        public void CrearPedidoEventoEspecial(int pedidoId, string nombre, decimal montoMinimo, decimal montoMaximo)
        {
            ////Jehosua Abdiel Cañas Tijerino
            ////11/11/2024
            // Consulta SQL para insertar el evento especial en la base de datos
            string query = "INSERT INTO eventos_especiales (pedido_id, nombre, monto_minimo, monto_maximo) VALUES (@pedidoId, @nombre, @montoMinimo, @montoMaximo)";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@pedidoId", pedidoId);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@montoMinimo", montoMinimo);
                cmd.Parameters.AddWithValue("@montoMaximo", montoMaximo);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
