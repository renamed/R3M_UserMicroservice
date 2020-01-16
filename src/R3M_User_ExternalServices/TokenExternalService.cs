using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using R3M_User_Domain;
using R3M_User_ExternalServices.Interfaces;

namespace R3M_User_ExternalServices
{
    public class TokenExternalService : ITokenExternalService
    {
        private readonly IDbContext _context;

        public TokenExternalService(IDbContext context)
        {
            this._context = context;
        }

        public async Task<Token> GetToken(string token)
        {
            string query = @"SELECT IdUsuario, Token as TokenValue, DtInsercao, DtExpiracao FROM TOKENS WHERE Token=@Token";
            using (var conexao = _context.GetConnection())
            {
                return await conexao.QueryFirstOrDefaultAsync<Token>(query, new
                {
                    Token = token
                });
            }
        }

        public async Task<IEnumerable<Token>> GetTokens(int idUsuario, DateTime dtExpiracao)
        {
            string query = @"SELECT IdUsuario, Token as TokenValue, DtInsercao, DtExpiracao FROM TOKENS WHERE IdUsuario=@IdUsuario AND DtExpiracao > @DtExpiracao";
            using (var conexao = _context.GetConnection())
            {
                return await conexao.QueryAsync<Token>(query, new
                {
                    IdUsuario = idUsuario,
                    DtExpiracao = dtExpiracao
                });
            }
        }

        public async Task SalvarToken(int idUsuario, Token token)
        {
            string query = "INSERT INTO TOKENS (IdUsuario, Token, DtInsercao, DtExpiracao) VALUES (@IdUsuario, @Token, @DtInsercao, @DtExpiracao)";

            using (var conexao = _context.GetConnection())
            {
                await conexao.ExecuteAsync(query, new
                {
                    IdUsuario = idUsuario,
                    Token = token.TokenValue,
                    DtInsercao = token.DtInsercao,
                    DtExpiracao = token.DtExpiracao
                });
            }
        }
    }
}