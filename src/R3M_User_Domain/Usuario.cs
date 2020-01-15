
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using R3M_User_Domain.Apoio;

namespace R3M_User_Domain
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }

        private string _senhaInsegura;
        public string Senha { get => GetHashSenha(); set { _senhaInsegura = value; } }
        public string Nome { get; set; }
        public DateTime Nascimento { get; set; }

        public void VerificarCampos()
        {
            if (this.Email == null || !Regex.IsMatch(this.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                throw new ValidationException("Email não é válido");

            if (this.Nome == null)
                throw new ValidationException("Nome não informado");

            if (this.Nascimento == default || this.Nascimento.ToUniversalTime() >= DateTime.UtcNow)
                throw new ValidationException("Data de nascimento não é válida");

            if (this._senhaInsegura == null)
                throw new ValidationException("Senha não informada");

            if (this._senhaInsegura.Length < 4)
                throw new ValidationException("Senha muito fraca");
        }

        public string GetHashSenha()
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(_senhaInsegura));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}