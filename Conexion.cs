using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clave5_Grupo4
{
    public class Conexion
    {
        public MySqlConnection Connection { get; }

        public Conexion()
        {
            Connection = new MySqlConnection("server=localhost;user=root;password=root;database=cafetin_ues");
            Connection.Open();
        }
    }
}
