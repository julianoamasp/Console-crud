using System;
using System.Collections.Generic;
using Estudos.Models;
using MySql.Data.MySqlClient;

namespace Estudos.DAO
{
    class UsuarioDAO : Usuario
    {
        public List<Usuario> buscarTodos()
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                MySqlConnection conn = ConnectionFactory.conexao();

                string query = "SELECT * FROM administradores";
                MySqlCommand comando = new MySqlCommand(query, conn);
                conn.Open();

                MySqlDataReader rd = comando.ExecuteReader();

                while (rd.Read())
                {
                    Usuario u = new Usuario(
                        Convert.ToInt16(rd["AdministradoresId"]),
                        Convert.ToString(rd["AdministradoresNome"]),
                        Convert.ToString(rd["AdministradoresEmail"]),
                        Convert.ToString(rd["AdministradoresSenha"])
                        );
                    usuarios.Add(u);
                }
                conn.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            return usuarios;
        }
        public List<Usuario> buscarPorId(Usuario usuario)
        {
            bool valid = false;
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                MySqlConnection conn = ConnectionFactory.conexao();

                string query = "SELECT * FROM `administradores` WHERE `AdministradoresId` = @id";
                MySqlCommand comando = new MySqlCommand(query, conn);
                comando.Parameters.AddWithValue("@id", usuario.getId());

                conn.Open();

                MySqlDataReader rd = comando.ExecuteReader();

                while (rd.Read())
                {
                    Usuario u = new Usuario(
                        Convert.ToInt16(rd["AdministradoresId"]),
                        Convert.ToString(rd["AdministradoresNome"]),
                        Convert.ToString(rd["AdministradoresEmail"]),
                        Convert.ToString(rd["AdministradoresSenha"])
                        );
                    usuarios.Add(u);
                }
                conn.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            return usuarios;
        }
        public bool adicionar(Usuario usuario)
        {
            bool valid = false;
            try
            {
                MySqlConnection conn = ConnectionFactory.conexao();

                string query = "INSERT INTO `administradores` (`AdministradoresId`, `AdministradoresNome`, `AdministradoresEmail`, `AdministradoresSenha`) VALUES (NULL, @nome, @email, @senha);";
                MySqlCommand comando = new MySqlCommand(query, conn);
                comando.Parameters.AddWithValue("@nome", usuario.getNome());
                comando.Parameters.AddWithValue("@email", usuario.getEmail());
                comando.Parameters.AddWithValue("@senha", usuario.getSenha());

                conn.Open();
                comando.ExecuteNonQuery();
                conn.Close();
                valid = true;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            return valid;
        }
        public bool excluir(Usuario usuario)
        {
            bool valid = false;
            try
            {
                MySqlConnection conn = ConnectionFactory.conexao();

                string query = "DELETE FROM `administradores` WHERE `administradores`.`AdministradoresId` = @id";
                MySqlCommand comando = new MySqlCommand(query, conn);
                comando.Parameters.AddWithValue("@id", usuario.getId());

                conn.Open();
                comando.ExecuteNonQuery();
                conn.Close();
                valid = true;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            return valid;
        }
    }
}
