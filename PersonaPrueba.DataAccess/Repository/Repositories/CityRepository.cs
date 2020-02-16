using PersonaPrueba.DataAccess.Repository.Contracts;
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
    public class CityRepository : SqlConnectionRepository, ICityRepository
    {
        private readonly string spCityGetAll;
        private readonly string spCityInsert;
        private readonly string spCityEdit;
        private readonly string spCityDelete;

        public CityRepository()
        {
            spCityGetAll = "spCityGetAll";
            spCityInsert = "spCityInsert";
            spCityEdit = "spCityEdit";
            spCityDelete = "spCityDelete";
        }

        public int Add(CityEntity entity)
        {
            using (SqlConnection connection=GetSqlConnection())
            {
                connection.Open();

                using (SqlCommand sqlCommand=new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandText = spCityInsert;
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@RegionID", SqlDbType.Int)).Value = entity.RegionID;
                    sqlCommand.Parameters.Add(new SqlParameter("@CityName", SqlDbType.NVarChar, 100)).Value = entity.CityName;

                    return Convert.ToInt32(sqlCommand.ExecuteScalar());
                }
            }
        }

        public void Delete(CityEntity entity)
        {
            using (SqlConnection connection=GetSqlConnection())
            {
                connection.Open();

                using (SqlCommand sqlCommand=new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandText = spCityDelete;
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@CityID", SqlDbType.Int)).Value = entity.CityID;

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public void Edit(CityEntity entity)
        {
            using (SqlConnection connection=GetSqlConnection())
            {
                connection.Open();

                using (SqlCommand sqlCommand=new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandText = spCityEdit;
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@CityID", SqlDbType.Int)).Value = entity.CityID;
                    sqlCommand.Parameters.Add(new SqlParameter("@RegionID", SqlDbType.Int)).Value = entity.RegionID;
                    sqlCommand.Parameters.Add(new SqlParameter("@CityName", SqlDbType.NVarChar, 100)).Value = entity.CityName;

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<CityEntity> GetAll()
        {
            List<CityEntity> cities = new List<CityEntity>();

            using (var connection = GetSqlConnection())
            {
                connection.Open();

                using (var sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandText = spCityGetAll;
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        var entity = new CityEntity
                        {
                            CityID = (int)reader["CityID"],
                            RegionID = (int)reader["RegionID"],
                            CityName = (string)reader["CityName"]
                        };
                        cities.Add(entity);
                    }
                }
            }
            return cities;
        }
    }
}
