using System.Threading.Tasks;
using R3M_User_Domain;

namespace R3M_User_Service.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> AdicionarUsuario(Usuario usuario);

        Task<Usuario> Get(Usuario usuario);
        Task<int> Delete(int id);
        Task<Usuario> Atualizar(Usuario usuario);
    }
}