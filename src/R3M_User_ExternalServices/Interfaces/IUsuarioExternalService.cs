using System.Threading.Tasks;
using R3M_User_Domain;

namespace R3M_User_ExternalServices.Interfaces
{
    public interface IUsuarioExternalService
    {
        Task<Usuario> GetById(int idUsuario);
        Task<Usuario> GetByEmail(string email);
        Task<Usuario> Adicionar(Usuario usuario);
    }
}