using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Common.Repositories;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;
using System.Data;
using DAL.Mappers;
using Microsoft.Extensions.Configuration;


namespace DAL.Services
{
    public class CocktailService :BaseService,  ICocktailRepository<Cocktail>
    {
        public CocktailService(IConfiguration config): base(config, "Main-DB") { }
        


        public IEnumerable<Cocktail> Get()
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand()) 
                {
                    command.CommandText = "SP_Cocktail_GetAll";
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using(SqlDataReader Reader = command.ExecuteReader()) 
                    {
                        while (Reader.Read()) 
                        {
                           yield return Reader.ToCocktail();
                        }
                    }
                }
            }
        }

        public Cocktail Get(Guid id)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SP_Cocktail_GetById";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(nameof(id), id);
                    connection.Open();
                    using(SqlDataReader reader = command.ExecuteReader()) 
                    {
                        if (reader.Read()) return reader.ToCocktail();

                        else throw new ArgumentOutOfRangeException(nameof(id)); 
                    }
                }
            }
        }
        public Guid Insert(Cocktail cocktail)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString)) 
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SP_Cocktail_Insert";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(nameof(Cocktail.Cocktail_Id), cocktail.Cocktail_Id);
                    command.Parameters.AddWithValue(nameof(Cocktail.Name), cocktail.Name);
                    command.Parameters.AddWithValue(nameof(Cocktail.Description), cocktail.Description);
                    command.Parameters.AddWithValue(nameof(Cocktail.Instructions), cocktail.Instructions);
                    command.Parameters.AddWithValue(nameof(Cocktail.CreatedAt), cocktail.CreatedAt);
                    command.Parameters.AddWithValue("user_id", (object?)cocktail.CreatedBy ?? DBNull.Value);
                    connection.Open();
                    return (Guid)command.ExecuteScalar();
                }
            }
        }

        public void Update(Guid id, Cocktail cocktail)
        {
            using( SqlConnection connection = new SqlConnection(_connectionString)) 
            {
                using(SqlCommand command = connection.CreateCommand()) 
                {
                    command.CommandText = "SP_Cocktail_Update";
                    command.CommandType= CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(nameof(id),id);
                    command.Parameters.AddWithValue(nameof(Cocktail.Name),cocktail.Name);
                    command.Parameters.AddWithValue(nameof(Cocktail.Description), cocktail.Description);
                    command.Parameters.AddWithValue(nameof(Cocktail.Instructions), cocktail.Instructions);
                    connection.Open();
                    command.ExecuteNonQuery();

                }
            }
        }
        public void Delete(Guid id)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString)) 
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SP_Cocktail_Delete";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(nameof(id),id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Cocktail> GetFromUser(Guid user_id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SP_Cocktail_GetByUserId";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(nameof(user_id),user_id);
                    connection.Open();
                    using (SqlDataReader Reader = command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            yield return Reader.ToCocktail();
                        }
                    }
                }
            }
        }
    }
}
