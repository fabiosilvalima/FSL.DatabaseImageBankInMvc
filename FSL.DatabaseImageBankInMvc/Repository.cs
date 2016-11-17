using System.Configuration;
using System.Data.SqlClient;
using System.Data;

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
                connection.Open();
                var sql = @"SELECT * 
                            FROM    dbo.ImageBankFile 
                            WHERE   FileId = @fileId 
                                    OR ISNULL(AliasId, FileId) = @fileId";

                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@fileId", SqlDbType.VarChar).Value = fileId;
                command.CommandType = CommandType.Text;
                var ada = new SqlDataAdapter(command);
                var dts = new DataSet();
                ada.Fill(dts);

                var model = new Models.ImageFile();
                model.Extension = dts.Tables[0].Rows[0]["Extension"] as string;
                model.ContentType = dts.Tables[0].Rows[0]["ContentType"] as string;
                model.Body = dts.Tables[0].Rows[0]["FileBody"] as byte[];

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