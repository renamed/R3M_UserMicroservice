using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using R3M_User_Domain;
using R3M_User_Domain.Apoio;
using R3M_User_ExternalServices.Interfaces;
using R3M_User_Service.Interfaces;

namespace R3M_User_Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioExternalService _usuarioExternalService;

        public UsuarioService(IUsuarioExternalService usuarioExternalService)
        {
            this._usuarioExternalService = usuarioExternalService;
        }


        public async Task<Usuario> Get(Usuario usuario)
        {
            if (usuario.IdUsuario != default)
                return await _usuarioExternalService.GetById(usuario.IdUsuario);
            else if (usuario.Email != default)
                return await _usuarioExternalService.GetByEmail(usuario.Email);
            throw new ValidationException("Id do usuário ou email são obrigatórios para pesquisar um usuário");
        }
        public async Task<Usuario> AdicionarUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new ValidationException("Usuário não informado");

            usuario.VerificarCampos();

            if ((await this.Get(new Usuario { Email = usuario.Email })) != null)
                throw new ValidationException("Usuário já cadastrado");


            return await _usuarioExternalService.Adicionar(usuario);
        }

        public async Task<int> Delete(int id)
        {
            return await _usuarioExternalService.Delete(id);
        }

        public async Task<Usuario> Atualizar(Usuario usuario)
        {
            if (usuario == null)
                throw new ValidationException("Usuário não informado");

            usuario.VerificarCampos(false);

            if ((await this.Get(new Usuario { IdUsuario = usuario.IdUsuario })) == null)
                throw new ValidationException("Usuário não cadastrado");

            return (await _usuarioExternalService.Atualizar(usuario)) == 0 ? null : usuario;
        }
    }
}