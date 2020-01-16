
using System;
using System.Threading.Tasks;
using Dapper;
using R3M_User_Domain;
using R3M_User_ExternalServices.Interfaces;

namespace R3M_User_ExternalServices
{
    public class UsuarioExternalService : IUsuarioExternalService
    {
        private readonly IDbContext _context;

        public UsuarioExternalService(IDbContext context)
        {
            this._context = context;
        }

        public async Task<Usuario> Adicionar(Usuario usuario)
        {
            string query = @"INSERT INTO USUARIOS(EMAIL, SENHA, NOME, NASCIMENTO, DTINSERCAO, DTATUALIZACAO)  VALUES (@Email, @Senha, @Nome, @Nascimento, @DataInsercao, @DataAtualizacao); SELECT IdUsuario FROM USUARIOS WHERE Email=@Email;";
            using (var conexao = _context.GetConnection())
            {
                Usuario novoUsuario = await conexao.QuerySingleAsync<Usuario>(query, new
                {
                    Email = usuario.Email,
                    Senha = usuario.Senha,
                    Nome = usuario.Nome,
                    Nascimento = usuario.Nascimento,
                    DataInsercao = DateTime.UtcNow,
                    DataAtualizacao = DateTime.UtcNow
                });
                return novoUsuario;
            }
        }

        public async Task<int> Atualizar(Usuario usuario)
        {
            string query = "UPDATE USUARIOS SET Email=@Email,Nome=@Nome,Nascimento=@Nascimento,DtAtualizacao=@DtAtualizacao WHERE IdUsuario=@IdUsuario";
            using (var conexao = _context.GetConnection())
            {
                return await conexao.ExecuteAsync(query, new
                {
                    IdUsuario = usuario.IdUsuario,
                    Nome = usuario.Nome,
                    Nascimento = usuario.Nascimento,
                    Email = usuario.Email,
                    DtAtualizacao = DateTime.UtcNow
                });
            }
        }

        public async Task<int> Delete(int id)
        {
            string query = @"DELETE FROM USUARIOS WHERE IdUsuario=@Id";
            using (var conexao = _context.GetConnection())
            {
                return await conexao.ExecuteAsync(query, new { Id = id });
            }
        }

        public async Task<Usuario> GetByEmail(string email)
        {
            string query = @"SELECT IdUsuario, Email, Senha, Nome, Nascimento FROM USUARIOS WHERE Email=@Email";
            using (var conexao = _context.GetConnection())
            {
                return await conexao.QueryFirstOrDefaultAsync<Usuario>(query, new { Email = email });
            }
        }

        public async Task<Usuario> GetById(int idUsuario)
        {
            string query = @"SELECT IdUsuario, Email, Senha, Nome, Nascimento FROM USUARIOS WHERE IdUsuario=@IdUsuario";
            using (var conexao = _context.GetConnection())
            {
                return await conexao.QueryFirstOrDefaultAsync<Usuario>(query, new { IdUsuario = idUsuario });
            }
        }

        public async Task ModificarSenha(Usuario usuario)
        {
            string query = "UPDATE USUARIOS SET Senha=@Senha WHERE IdUsuario=@IdUsuario";
            using (var conexao = _context.GetConnection())
            {
                await conexao.ExecuteAsync(query, new
                {
                    IdUsuario = usuario.IdUsuario,
                    Senha = usuario.Senha
                });
            }
        }
    }
}