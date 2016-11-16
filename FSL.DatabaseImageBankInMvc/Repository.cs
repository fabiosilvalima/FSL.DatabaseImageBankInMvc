using System;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace FSL.DatabaseImageBankInMvc
{
    public class Repository
    {
        public static Models.ImageFile GetFile(string fileId)
        {
            //Just an example, use you own data repository and/or database
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ImageBankDatabase"].ConnectionString);

            try
            {
                var sql = @"SELECT * 
                            FROM    dbo.ImageBankFile 
                            WHERE   FileId = @fileId 
                                    OR ISNULL(AliasId, FileId) = @fileId";

                var model = connection.QueryFirstOrDefault<Models.ImageFile>(sql, new
                {
                    fileId = fileId
                });

                return model;
            }
            catch 
            {

            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                    connection = null;
                }
            }

            return null;
        }
    }
}