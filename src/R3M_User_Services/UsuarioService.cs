using System;
using System.Linq;
using System.Security.Cryptography;
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
        private readonly ITokenExternalService _tokenExternalService;

        public UsuarioService(IUsuarioExternalService usuarioExternalService
                                , ITokenExternalService tokenExternalService)
        {
            this._usuarioExternalService = usuarioExternalService;
            this._tokenExternalService = tokenExternalService;
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

        public async Task<Token> GerarToken(int idUsuario)
        {
            if ((await this.Get(new Usuario { IdUsuario = idUsuario })) == null)
                throw new ValidationException("Usuário não existe");

            var tokensValidos = await _tokenExternalService.GetTokens(idUsuario, DateTime.UtcNow);
            if (tokensValidos.Any())
            {
                return tokensValidos.OrderByDescending(i => i.DtExpiracao).First();
            }

            Token token = new Token();

            token.SetTokenValue();
            token.DtInsercao = DateTime.UtcNow;
            token.DtExpiracao = DateTime.UtcNow.AddHours(2);

            await _tokenExternalService.SalvarToken(idUsuario, token);
            return token;
        }

        public async Task AtualizarSenha(string hash, string senha)
        {
            Token token = await _tokenExternalService.GetToken(hash);
            if (token == null)
                throw new ValidationException("Token não existe");

            if (token.DtExpiracao <= DateTime.UtcNow)
                throw new ValidationException("Token expirado");

            Usuario usuario = await _usuarioExternalService.GetById(token.IdUsuario);
            if (usuario == null)
                throw new ValidationException("Usuário não existe");

            usuario.Senha = senha;
            usuario.VerificarCampos(true);

            await _usuarioExternalService.ModificarSenha(usuario);
        }
    }
}