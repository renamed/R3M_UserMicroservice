namespace R3M_User_ApiModels.Usuario
{
    public class AtualizacaoSenha
    {
        public class AtualizacaoSenhaRequest
        {
            public string Hash { get; set; }
            public string Senha { get; set; }
        }

    }
}