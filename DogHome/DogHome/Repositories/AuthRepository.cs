using DogHome.Interface;
using DogHome.Model;
using DogHome.Server;
using Microsoft.Data.SqlClient;
namespace DogHome.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        public async Task CreateAsync(Auth entity)
        {
            try
            {
                using (var sqlConnection = SqlConnectionManager.GetConnection())
                {
                    sqlConnection.Open();
                    string sql = @"INSERT INTO Auth (FullName, Password, Email, Phone, Location, SocialSite,Gender,Shopname, BirthDate)
                           VALUES (@FullName, @Password, @Email, @Phone, @Location, @SocialSite, @Gender, @ShopName, @BirthDate)"
                    ;
                    SqlCommand command = new SqlCommand(sql, sqlConnection);
                    command.Parameters.AddWithValue("@FullName", entity.FullName);
                    command.Parameters.AddWithValue("@Password", entity.Password);
                    command.Parameters.AddWithValue("@Email", entity.Email);
                    command.Parameters.AddWithValue("@Phone", entity.Phone);
                    command.Parameters.AddWithValue("@Location", entity.Location);
                    command.Parameters.AddWithValue("@BirthDate", entity.BirthDate);
                    command.Parameters.AddWithValue("@SocialSite", entity.SocialSite);
                    command.Parameters.AddWithValue("@Gender", entity.Gender);
                    command.Parameters.AddWithValue("@ShopName", entity.ShopName);
                    await command.ExecuteNonQueryAsync();
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
                    string deleteSql = "DELETE FROM Auth WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(deleteSql, sqlConnection);
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting seller: {ex.Message}");
            }
        }

        public async Task<List<Auth>> GetAllAsync()
        {
            List<Auth> authList = new List<Auth>();
            try
            {
                using (var sqlConnection = SqlConnectionManager.GetConnection())
                {
                    sqlConnection.Open();
                    string sql = "SELECT * FROM Auth";
                    SqlCommand command = new SqlCommand(sql, sqlConnection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        Auth auth = new Auth
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FullName = reader["FullName"].ToString(),
                            Password = reader["Password"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Location = reader["Location"].ToString(),
                            SocialSite = reader["SocialSite"].ToString(),
                            ShopName = reader["ShopName"].ToString(),
                            BirthDate = Convert.ToDateTime(reader["BirthDate"])
                        };
                        authList.Add(auth);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting seller: {ex.Message}");
            }
            return authList;
        }

        public async Task<Auth> GetByEmailAsync(string email)
        {
            try
            {
                using (var sqlConnection = SqlConnectionManager.GetConnection())
                {
                    sqlConnection.Open();
                    string sql = "SELECT * FROM Auth WHERE Email = @Email";
                    SqlCommand command = new SqlCommand(sql, sqlConnection);
                    command.Parameters.AddWithValue("@Email", email);

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        return new Auth
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FullName = reader["FullName"].ToString(),
                            Password = reader["Password"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Location = reader["Location"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            SocialSite = reader["SocialSite"].ToString(),
                            ShopName = reader["ShopName"].ToString(),
                            BirthDate = Convert.ToDateTime(reader["BirthDate"])
                        };
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving seller: {ex.Message}");
            }
            return null;
        }

        public async Task UpdateAsync(Auth entity)
        {
            try
            {
                using (var sqlConnection = SqlConnectionManager.GetConnection())
                {
                    sqlConnection.Open();
                    string sql = @"UPDATE Auth SET FullName = @FullName, 
                           Phone = @Phone, Location = @Location, SocialSite = @SocialSite, 
                           ShopName= @ShopName
                           WHERE Id = @Id";

                    SqlCommand command = new SqlCommand(sql, sqlConnection);
                    command.Parameters.AddWithValue("@FullName", entity.FullName);
                    command.Parameters.AddWithValue("@Phone", entity.Phone);
                    command.Parameters.AddWithValue("@Location", entity.Location);
                    command.Parameters.AddWithValue("@SocialSite", entity.SocialSite);
                    command.Parameters.AddWithValue("@ShopName", entity.ShopName);
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating seller: {ex.Message}");
            }
        }
    }
}
