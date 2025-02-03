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


namespace DAL.Services
{
    public class CocktailService : ICocktailRepository<Cocktail>
    {
        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WAD24-DemoASP-DB;Integrated Security=True;";

        public IEnumerable<Cocktail> Get()
        {
            using(SqlConnection connection = new SqlConnection(ConnectionString))
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
            using(SqlConnection connection = new SqlConnection(ConnectionString))
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
            using(SqlConnection connection = new SqlConnection(ConnectionString)) 
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
                    command.Parameters.AddWithValue(nameof(Cocktail.CreatedBy), cocktail.CreatedBy);
                    connection.Open();
                    return (Guid)command.ExecuteScalar();
                }
            }
        }

        public void Update(Guid id, Cocktail cocktail)
        {
            using( SqlConnection connection = new SqlConnection(ConnectionString)) 
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
            using(SqlConnection connection = new SqlConnection(ConnectionString)) 
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

    }
}
