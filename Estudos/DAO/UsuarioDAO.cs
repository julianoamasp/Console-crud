using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estudos.Models;
using MySql.Data.MySqlClient;

namespace Estudos.DAO
{
    class UsuarioDAO : Usuario
    {
        public MySqlCommand conn;
    }
}
