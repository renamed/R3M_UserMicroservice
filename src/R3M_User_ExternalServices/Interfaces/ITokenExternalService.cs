using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using R3M_User_Domain;

namespace R3M_User_ExternalServices.Interfaces
{
    public interface ITokenExternalService
    {
        Task SalvarToken(int idUsuario, Token token);
        Task<IEnumerable<Token>> GetTokens(int idUsuario, DateTime dtExpiracao);
        Task<Token> GetToken(string token);
    }
}