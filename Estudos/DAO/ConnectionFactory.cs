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
