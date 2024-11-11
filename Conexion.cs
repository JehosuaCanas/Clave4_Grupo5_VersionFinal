using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Clave5_Grupo4
{
    public class Conexion
    {
        public MySqlConnection Connection { get; }

        ////Jehosua Abdiel Cañas Tijerino
        ////11/11/2024
        ////Constructor de la clase Conexion
        public Conexion()
        {
            ////Jehosua Abdiel Cañas Tijerino
            ////11/11/2024
            //// Inicializa la propiedad Connection con la instancia de MySqlConnection, con la cadena de conexion con el servidor el usuario y la contraseña
            Connection = new MySqlConnection("server=localhost;user=root;password=root;database=cafetin_ues");

            ////Jehosua Abdiel Cañas Tijerino
            ////11/11/2024
            ////Abre  la conexion a la base de datos inmediatamente al crear un objeto de la clase Conexion 
            Connection.Open();
        }
    }
}
