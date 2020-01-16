using System;

namespace R3M_User_ApiModels.Usuario
{
    public class AtualizarUsuarioRequest
    {
        /// <summary>
        /// Email do novo usuário. Precisa ser único.
        /// </summary>
        /// <value></value>
        public string Email { get; set; }
        /// <summary>
        /// Nome completo do usuário
        /// </summary>
        /// <value></value>
        public string Nome { get; set; }
        /// <summary>
        /// Data de nascimento do novo usuário
        /// </summary>
        /// <value></value>
        public DateTime Nascimento { get; set; }

    }

    public class AtualizarUsuarioResponse
    {
        public int IdUsuario { get; set; }
        /// <summary>
        /// Email do novo usuário. Precisa ser único.
        /// </summary>
        /// <value></value>
        public string Email { get; set; }
        /// <summary>
        /// Nome completo do usuário
        /// </summary>
        /// <value></value>
        public string Nome { get; set; }
        /// <summary>
        /// Data de nascimento do novo usuário
        /// </summary>
        /// <value></value>
        public DateTime Nascimento { get; set; }
    }
}