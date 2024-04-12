using DogHome.Interface;
using DogHome.Model;
using DogHome.Server;
using Microsoft.Data.SqlClient;

namespace DogHome.Repositories
{
    public class BreedRepository : IBreedRepository
    {
        public async Task CreateAsync(string name)
        {
            try
            {
                using (var sqlConnection = SqlConnectionManager.GetConnection())
                {
                    sqlConnection.Open();
                    string insertSql = "INSERT INTO Breeds (BreedName) VALUES (@Name)";
                    SqlCommand insertCommand = new SqlCommand(insertSql, sqlConnection);
                    insertCommand.Parameters.Add(new SqlParameter("@Name", name));
                    await insertCommand.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating size: {ex.Message}");
            }
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Breed entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Breed>> GetAllAsync()
        {
            List<Breed> breeds = new List<Breed>();

            try
            {
                using (var sqlConnection = SqlConnectionManager.GetConnection())
                {
                    sqlConnection.Open();
                    string selectSql = "SELECT Id, BreedName FROM Breeds";
                    SqlCommand selectCommand = new SqlCommand(selectSql, sqlConnection);
                    SqlDataReader reader = await selectCommand.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        Breed breed = new Breed
                        {
                            Id = reader.GetInt32(0),
                            BreedName = reader.GetString(1)
                        };
                        breeds.Add(breed);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching breeds: {ex.Message}");
            }

            return breeds;
        }

        public async Task<Breed> GetByBreedAsync(string BreedName)
        {
            try
            {
                using (var sqlConnection = SqlConnectionManager.GetConnection())
                {
                    sqlConnection.Open();
                    string selectSql = "SELECT Id, BreedName FROM Breeds WHERE BreedName = @BreedName";
                    SqlCommand selectCommand = new SqlCommand(selectSql, sqlConnection);
                    selectCommand.Parameters.AddWithValue("@BreedName", BreedName);
                    SqlDataReader reader = await selectCommand.ExecuteReaderAsync();

                    if (reader.Read())
                    {
                        return new Breed
                        {
                            Id = reader.GetInt32(0),
                            BreedName = reader.GetString(1)
                        };
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching breed by name: {ex.Message}");
            }

            return null;
        }

        public async Task<Breed> GetByIdAsync(int id)
        {
            try
            {
                using (var sqlConnection = SqlConnectionManager.GetConnection())
                {
                    sqlConnection.Open();
                    string selectSql = "SELECT Id, BreedName FROM Breeds WHERE Id = @Id";
                    SqlCommand selectCommand = new SqlCommand(selectSql, sqlConnection);
                    selectCommand.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = await selectCommand.ExecuteReaderAsync();

                    if (reader.Read())
                    {
                        return new Breed
                        {
                            Id = reader.GetInt32(0),
                            BreedName = reader.GetString(1)
                        };
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching breed by name: {ex.Message}");
            }
            return null;
        }
    }
}
