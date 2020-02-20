using PersonaPrueba.DataAccess.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonaPrueba.DataAccess.Repository.Repositories
{
    public class DocumentRepository : SqlConnectionRepository, Contracts.IDocumentRepository
    {
        private readonly string spDocumentGetAll;
        private readonly string spDocumentInsert;
        private readonly string spDocumentEdit;
        private readonly string spDocumentDelete;

        public DocumentRepository()
        {
            spDocumentGetAll = "spDocumentGetAll";
            spDocumentInsert = "spDocumentInsert";
            spDocumentEdit = "spDocumentEdit";
            spDocumentDelete = "spDocumentDelete";
        }
        
        public int Add(DocumentEntity entity)
        {
            using (SqlConnection connection=GetSqlConnection())
            {
                connection.Open();

                using (SqlCommand sqlCommand=new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandText = spDocumentInsert;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("@Document", SqlDbType.NVarChar, 100)).Value = entity.Document;

                    return Convert.ToInt32(sqlCommand.ExecuteScalar());
                }
            }
        }

        public void Delete(DocumentEntity entity)
        {
            using (SqlConnection connection = GetSqlConnection())
            {
                connection.Open();

                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.Connection = connection;
                    sqlcommand.CommandText = spDocumentDelete;
                    sqlcommand.CommandType = CommandType.StoredProcedure;

                    sqlcommand.Parameters.Add(new SqlParameter("@DocumentID", SqlDbType.Int)).Value = entity.DocumentID;

                    sqlcommand.ExecuteNonQuery();
                }
            }
        }

        public void Edit(DocumentEntity entity)
        {
            using (SqlConnection connection=GetSqlConnection())
            {
                connection.Open();

                using (SqlCommand sqlcommand=new SqlCommand())
                {
                    sqlcommand.Connection = connection;
                    sqlcommand.CommandText = spDocumentEdit;
                    sqlcommand.CommandType = CommandType.StoredProcedure;

                    sqlcommand.Parameters.Add(new SqlParameter("@DocumentID", SqlDbType.Int)).Value = entity.DocumentID;
                    sqlcommand.Parameters.Add(new SqlParameter("@Document", SqlDbType.NVarChar, 100)).Value = entity.Document;

                    sqlcommand.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<DocumentEntity> GetAll()
        {
            List<DocumentEntity> documentEntities = new List<DocumentEntity>();

            using (SqlConnection connection = GetSqlConnection())
            {
                connection.Open();

                using (SqlCommand sqlCommand=new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandText = spDocumentGetAll;
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        documentEntities.Add(new DocumentEntity
                        {
                            DocumentID = (int)reader["DocumentID"],
                            Document = (string)reader["Document"]
                        });
                    }
                }
            }
            return documentEntities;
        }
    }
}
