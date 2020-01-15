using System.Data;
using System.Data.Common;

namespace R3M_User_ExternalServices.Interfaces
{
    public interface IDbContext
    {
        DbConnection GetConnection();
    }
}