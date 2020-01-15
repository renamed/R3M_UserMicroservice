using System.Threading.Tasks;
using R3M_User_ApiModels.Usuario;
using R3M_User_App.Interfaces;
using R3M_User_Domain;
using R3M_User_Service;
using R3M_User_Service.Interfaces;

namespace R3M_User_App
{
    public class UsuarioApp : IUsuarioApp
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioApp(IUsuarioService usuarioService)
        {
            this._usuarioService = usuarioService;
        }

        public async Task<NovoUsuario.Response> AdicionarUsuario(NovoUsuario.Request request)
        {
            Usuario novoUsuario = await _usuarioService.AdicionarUsuario(new Usuario
            {
                Email = request.Email,
                Nascimento = request.Nascimento,
                Nome = request.Nome,
                Senha = request.Senha
            });

            return new NovoUsuario.Response
            {
                IdUsuario = novoUsuario.IdUsuario
            };

        }

        public async Task<ObtemUsuarioResponse> Get(int id)
        {
            Usuario usuario = await _usuarioService.Get(new Usuario { IdUsuario = id });
            if (usuario == null)
            {
                return null;
            }

            return new ObtemUsuarioResponse
            {
                Email = usuario.Email,
                IdUsuario = usuario.IdUsuario,
                Nascimento = usuario.Nascimento,
                Nome = usuario.Nome
            };
        }
    }
}