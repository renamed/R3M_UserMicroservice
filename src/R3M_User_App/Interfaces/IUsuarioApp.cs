using System.Threading.Tasks;
using R3M_User_ApiModels.Usuario;

namespace R3M_User_App.Interfaces
{
    public interface IUsuarioApp
    {
        Task<NovoUsuario.Response> AdicionarUsuario(NovoUsuario.Request request);
        Task<ObtemUsuarioResponse> Get(int id);
    }
}