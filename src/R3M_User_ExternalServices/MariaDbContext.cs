using System;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using R3M_User_ExternalServices.Interfaces;

namespace R3M_User_ExternalServices
{
    public class MariaDbContext : IDbContext
    {
        public DbConnection GetConnection()
        {
            DbConnection conexao;

            conexao = new MySqlConnection(Environment.GetEnvironmentVariable("DATABASE_HOST"));
            conexao.Open();

            return conexao;
        }
    }
}