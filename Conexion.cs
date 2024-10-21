using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Clave5_Grupo4
{
    public MySqlConnection Connection { get; }

    class Conexion()
    {
        Connection = new MySqlConnection("server=localhost;user=root;password=root;database=cafetin_ues");
        Connection.Open();
    }
}
