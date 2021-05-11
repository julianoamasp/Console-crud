using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Estudos.Models;

namespace Estudos
{
    class Program
    {
        static void Main(string[] args)
        {
            usuarioDao usuarioDao = new usuarioDao();
            List<Usuario> usuarios;
            string confirmacao;
            int option = 1;
            while (option != 0)
            {
                Console.Clear();
                Console.WriteLine("1 - ver usuários");
                Console.WriteLine("2 - adicionar usuário");
                Console.WriteLine("3 - remover usuário");
                Console.WriteLine("\n");
                Console.Write("Digite a opção: ");
                string op = Console.ReadLine();
                switch (op)
                {
                    case "1":
                        Console.Clear();
                        usuarios = usuarioDao.buscarTodos();
                        foreach (Usuario usuario in usuarios)
                        {
                            Console.WriteLine("| Id: " + usuario.getId());
                            Console.WriteLine("| Nome: " + usuario.getNome());
                            Console.WriteLine("| Email: " + usuario.getEmail());
                            Console.WriteLine("\n");
                        }

                        Console.WriteLine("0 - para sair");
                        Console.WriteLine("1 - menu anterior");
                        option = Convert.ToInt16(Console.ReadLine());
                        break;
                    case "2":
                        Console.Clear();
                        string nome = "";
                        string email = "";
                        string senha = "";
          
                        while (nome == "" || email == "" || senha == "")
                        {
                            Console.Clear();
                            if (nome == "")
                            {
                                Console.WriteLine("Digite  o nome");
                                nome = Console.ReadLine();
                            }
                            if (email == "")
                            {
                                Console.WriteLine("Digite o email");
                                email = Console.ReadLine();
                            }
                            if (senha == "")
                            {
                                Console.WriteLine("Digite a senha");
                                senha = Console.ReadLine();
                            }
                            Console.Clear();
                            Console.WriteLine("\nDeseja adicionar o usuario?");
                            Console.WriteLine("\n");
                            Console.WriteLine("| Nome: " + nome);
                            Console.WriteLine("| Email: " + email);
                            Console.WriteLine("| Senha: " + senha);
                            Console.WriteLine("\n");
                            Console.WriteLine("Digite \"sim\" ou \"não\" para sair!");
                            confirmacao = Console.ReadLine();
                            if (confirmacao == "sim")
                            {
                                if(usuarioDao.adicionar(new Usuario(0, nome, email, senha)))
                                {
                                    Console.WriteLine("\nUsuário adicionado!");
                                }
                                else
                                {
                                    Console.WriteLine("\nErro ao adicionar!");
                                }
                            }
                        }
                        Console.WriteLine("\n");
                        Console.WriteLine("0 - para sair");
                        Console.WriteLine("1 - menu anterior");
                        option = Convert.ToInt16(Console.ReadLine());
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Digite o Id:");
                        int id = Convert.ToInt16(Console.ReadLine());
                        usuarios = usuarioDao.buscarPorId(new Usuario(id,"","",""));
                        Usuario us = null;
                        foreach (Usuario usuario in usuarios)
                        {
                            us = new Usuario(usuario.getId(), usuario.getNome(), usuario.getEmail(),usuario.getSenha());
                            if (true) { break; }
                        }
                        if (usuarios.Count > 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Deseja excluir o usuario:\n");
                            Console.WriteLine("| id: " + us.getId());
                            Console.WriteLine("| Nome: " + us.getNome());
                            Console.WriteLine("| Email: " + us.getEmail());
                            Console.WriteLine("| Senha: " + us.getSenha());

                            string apagar = "";
                            Console.WriteLine("\nDigite \"sim\" para exclír ou \"não\"!");
                            apagar = Console.ReadLine();
                            if (apagar == "sim")
                            {
                                if (usuarioDao.excluir(us))
                                {
                                    Console.WriteLine("\n");
                                    Console.WriteLine("Ecluído com sucesso!");
                                }
                                else
                                {
                                    Console.WriteLine("\n");
                                    Console.WriteLine("Erro ao excluír!");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n");
                            Console.WriteLine("Usuário não encontrado!");
                        }
                        Console.WriteLine("\n");
                        Console.WriteLine("0 - para sair");
                        Console.WriteLine("1 - menu anterior");
                        option = Convert.ToInt16(Console.ReadLine());
                        break;
                }
            }
        }
    }

    class usuarioDao
    {
        public List<Usuario> buscarPorId(Usuario usuario)
        {
            bool valid = false;
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost;User ID=root;Password=;Database=academia");

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
                MySqlConnection conn = new MySqlConnection("Server=localhost;User ID=root;Password=;Database=academia");

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
                MySqlConnection conn = new MySqlConnection("Server=localhost;User ID=root;Password=;Database=academia");

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
        public List<Usuario> buscarTodos()
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost;User ID=root;Password=;Database=academia");

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
    }
}
