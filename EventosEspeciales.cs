

using MySql.Data.MySqlClient;

namespace Clave5_Grupo4
{
    public class EventosEspeciales
    {
        private readonly MySqlConnection connection;

        public EventosEspeciales(Conexion dbConnection)
        {
            connection = dbConnection.Connection;
        }

        public void CrearPedidoEventoEspecial(int pedidoId, string nombre, decimal montoMinimo, decimal montoMaximo)
        {
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
