using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estudos.Models
{
    class Usuario
    {
        private int id;
        private string nome;
        private string email;
        private string senha;

        public Usuario()
        {
        }
        public Usuario(int id, string nome, string email, string senha)
        {
            this.id = id;
            this.nome = nome;
            this.email = email;
            this.senha = senha;
        }
        public int getId()
        {
            return this.id;
        }
        public string getNome()
        {
            return this.nome;
        }
        public string getEmail()
        {
            return this.email;
        }
        public string getSenha()
        {
            return this.senha;
        }

    }
}
