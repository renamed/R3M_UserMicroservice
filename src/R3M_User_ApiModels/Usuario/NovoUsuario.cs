using System;

namespace R3M_User_ApiModels.Usuario
{
    public class NovoUsuario
    {
        public class Request
        {
            /// <summary>
            /// Email do novo usuário. Precisa ser único.
            /// </summary>
            /// <value></value>
            public string Email { get; set; }
            /// <summary>
            /// A senha de acesso ao sistema do usuário
            /// </summary>
            /// <value></value>
            public string Senha { get; set; }
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

        public class Response
        {
            public int IdUsuario { get; set; }
        }
    }
}