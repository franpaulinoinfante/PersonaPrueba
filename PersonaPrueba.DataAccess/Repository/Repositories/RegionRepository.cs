using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PersonaPrueba.DataAccess.Repository.Entities;

namespace PersonaPrueba.DataAccess.Repository.Repositories
{
    public class RegionRepository : SqlConnectionRepository, Contracts.IRegionRepository
    {
        private readonly string spRegionGetAll;
        private readonly string spRegionInsert;
        private readonly string spRegionEdit;
        private readonly string spRegionDelete;

        public RegionRepository()
        {
            spRegionGetAll = "spRegionGetAll";
            spRegionInsert = "spRegionInsert";
            spRegionEdit = "spRegionEdit";
            spRegionDelete = "spRegionDelete";
        }

        public int Add(RegionEntity entity)
        {
            using (SqlConnection connection = GetSqlConnection())
            {
                connection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandText = spRegionInsert;
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@RegionName", SqlDbType.NVarChar, 100)).Value = entity.RegionName;

                    return Convert.ToInt32(sqlCommand.ExecuteScalar());
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = GetSqlConnection())
            {
                connection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandText = spRegionDelete;
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@RegionID", SqlDbType.Int)).Value = id;

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public void Edit(RegionEntity entity)
        {
            using (SqlConnection connection = GetSqlConnection())
            {
                connection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandText = spRegionEdit;

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("@RegionID", SqlDbType.Int)).Value = entity.RegionID;
                    sqlCommand.Parameters.Add(new SqlParameter("@RegionName", SqlDbType.NVarChar, 100)).Value = entity.RegionName;

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<RegionEntity> GetAll()
        {
            List<RegionEntity> entities = new List<RegionEntity>();

            using (SqlConnection connection = GetSqlConnection())
            {
                connection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandText = spRegionGetAll;
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        var entity = new RegionEntity
                        {
                            RegionID = (int)reader["RegionID"],
                            RegionName = (string)reader["RegionName"]
                        };
                        entities.Add(entity);
                    }
                }
            }
            return entities;
        }
    }
}
