using System.Data;
using Microsoft.Data.SqlClient;
using SocialSite.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace SocialSite.Data
{
    public class DataContextDapper
    {
        private IConfiguration _config;
        public DataContextDapper(IConfiguration configuration) {
           _config = configuration;
        }


        public IEnumerable<T> LoadData<T>(String sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            dbConnection.Open();

            IEnumerable<T> data = dbConnection.Query<T>(sql).ToList();

            dbConnection.Close();

            return data;

        }

         public T LoadDataSingle<T>(String sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            dbConnection.Open();

            T data = dbConnection.QuerySingle<T>(sql);

            dbConnection.Close();

            return data;
        }

        public bool ExecuteSQL(string sql) {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            dbConnection.Open();

            int rowsAffected = dbConnection.Execute(sql);

            dbConnection.Close();

            return rowsAffected > 0;
        }

        public int ExecuteSQLWithReturn(string sql) {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            dbConnection.Open();

            int rowsAffected = dbConnection.Execute(sql);

            dbConnection.Close();

            return rowsAffected;
        }
    }
}