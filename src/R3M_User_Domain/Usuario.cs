
using System;

namespace R3M_User_Domain
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public DateTime Nascimento { get; set; }

    }
}