using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Estudos.DAO
{
    static class ConnectionFactory
    {
        public static MySqlConnection conexao()
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost;User ID=root;Password=;Database=academia");
            return conn;
        }
    }
}
