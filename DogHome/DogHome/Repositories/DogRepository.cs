using DogHome.Interface;
using DogHome.Model;
using DogHome.Server;
using Microsoft.Data.SqlClient;

namespace DogHome.Repositories
{
    public class DogRepository : IRepository
    {
        public async Task CreateAsync(Dog entity)
        {
            try
            {
                using (var sqlConnection = SqlConnectionManager.GetConnection())
                {
                    sqlConnection.Open();
                    string insertSql = "INSERT INTO Dogs ( Age, Name, ImagePath, Price, Gender, GroomingNeeds, SellerId, Size, BreedId)  VALUES ( @Age,@Name, @ImagePath, @Price, @Gender, @GroomingNeeds, @SellerId, @Size, @BreedId)";
                    SqlCommand insert = new SqlCommand(insertSql, sqlConnection);

                    insert.Parameters.Add(new SqlParameter("@Age", entity.Age));
                    insert.Parameters.Add(new SqlParameter("@Name", entity.Name));
                    insert.Parameters.Add(new SqlParameter("@ImagePath", entity.ImagePath));
                    insert.Parameters.Add(new SqlParameter("@Price", entity.Price));
                    insert.Parameters.Add(new SqlParameter("@Gender", entity.Gender));
                    insert.Parameters.Add(new SqlParameter("@GroomingNeeds", entity.GroomingNeeds));
                    insert.Parameters.Add(new SqlParameter("@SellerId", entity.SellerId));
                    insert.Parameters.Add(new SqlParameter("@Size", entity.Size));
                    insert.Parameters.Add(new SqlParameter("@BreedId", entity.BreedId));
                    await insert.ExecuteNonQueryAsync();
                    Console.WriteLine($"Animal with Id '{entity.Id}' created successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating size: {ex.Message}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                using (var sqlConnection = SqlConnectionManager.GetConnection())
                {
                    sqlConnection.Open();
                    string deleteSql = "DELETE FROM Dogs WHERE Id = @Id";
                    SqlCommand deleteCommand = new SqlCommand(deleteSql, sqlConnection);
                    deleteCommand.Parameters.Add(new SqlParameter("@Id", id));
                    await deleteCommand.ExecuteNonQueryAsync();
                    Console.WriteLine($"Dog with Id '{id}' deleted successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting dog: {ex.Message}");
            }
        }

        public async Task<List<Dog>> GetAllAsync()
        {
            List<Dog> dogs = new List<Dog>();

            try
            {
                using (var sqlConnection = SqlConnectionManager.GetConnection())
                {
                    sqlConnection.Open();
                    string selectSql = "SELECT Id, Age, Name, ImagePath, Price, Gender, GroomingNeeds, SellerId, Size, BreedId FROM Dogs";
                    SqlCommand selectCommand = new SqlCommand(selectSql, sqlConnection);
                    SqlDataReader reader = await selectCommand.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        Dog dog = new Dog
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Age = Convert.ToInt32(reader["Age"]),
                            Name = reader["Name"].ToString(),
                            ImagePath = reader["ImagePath"].ToString(),
                            Price = Convert.ToDouble(reader["Price"]),
                            Gender = reader["Gender"].ToString(),
                            GroomingNeeds = reader["GroomingNeeds"].ToString(),
                            SellerId = Convert.ToInt32(reader["SellerId"]),
                            Size = reader["Size"].ToString(),
                            BreedId = Convert.ToInt32(reader["BreedId"])
                        };
                        dogs.Add(dog);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving dogs: {ex.Message}");
            }

            return dogs;
        }

        public async Task<Dog> GetByIdAsync(int id)
        {
            Dog? dog = null;

            try
            {
                using (var sqlConnection = SqlConnectionManager.GetConnection())
                {
                    sqlConnection.Open();
                    string selectSql = "SELECT Id,Name, Age, ImagePath, Price, Gender, GroomingNeeds, SellerId, Size, BreedId FROM Dogs WHERE Id = @Id";
                    SqlCommand selectCommand = new SqlCommand(selectSql, sqlConnection);
                    selectCommand.Parameters.Add(new SqlParameter("@Id", id));
                    SqlDataReader reader = await selectCommand.ExecuteReaderAsync();

                    if (reader.Read())
                    {
                        dog = new Dog
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Age = Convert.ToInt32(reader["Age"]),
                            Name = reader["Name"].ToString(),
                            ImagePath = reader["ImagePath"].ToString(),
                            Price = Convert.ToDouble(reader["Price"]),
                            Gender = reader["Gender"].ToString(),
                            GroomingNeeds = reader["GroomingNeeds"].ToString(),
                            SellerId = Convert.ToInt32(reader["SellerId"]),
                            Size = reader["Size"].ToString(),
                            BreedId = Convert.ToInt32(reader["BreedId"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving dog: {ex.Message}");
            }

            return dog;
        }

        public async Task UpdateAsync(Dog entity)
        {
            try
            {
                using (var sqlConnection = SqlConnectionManager.GetConnection())
                {
                    sqlConnection.Open();
                    string updateSql = "UPDATE Dogs SET Age = @Age, ImagePath = @ImagePath, Name = @Name, Price = @Price, Gender = @Gender, GroomingNeeds = @GroomingNeeds," +
                        " SellerId = @SellerId, Size = @Size, BreedId = @BreedId WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(updateSql, sqlConnection);
                    command.Parameters.Add(new SqlParameter("@Age", entity.Age));
                    command.Parameters.Add(new SqlParameter("@Name", entity.Name));
                    command.Parameters.Add(new SqlParameter("@ImagePath", entity.ImagePath));
                    command.Parameters.Add(new SqlParameter("@Price", entity.Price));
                    command.Parameters.Add(new SqlParameter("@Gender", entity.Gender));
                    command.Parameters.Add(new SqlParameter("@GroomingNeeds", entity.GroomingNeeds));
                    command.Parameters.Add(new SqlParameter("@SellerId", entity.SellerId));
                    command.Parameters.Add(new SqlParameter("@Size", entity.Size));
                    command.Parameters.Add(new SqlParameter("@BreedId", entity.BreedId));
                    command.Parameters.Add(new SqlParameter("@Id", entity.Id));
                    await command.ExecuteNonQueryAsync();
                    Console.WriteLine($"Dog with Id '{entity.Id}' updated successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating dog: {ex.Message}");
            }
        }
    }
}

