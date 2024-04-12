using DogHome.Interface;
using DogHome.Model;
using DogHome.Server;
using Microsoft.Data.SqlClient;
namespace DogHome.Repositories
{
    public class FormRepository : IFormRepository
    {
        public async Task CreateAsync(BuyerForm entity)
        {
            try
            {
                using (var sqlConnection = SqlConnectionManager.GetConnection())
                {
                    sqlConnection.Open();
                    string insertSql = "INSERT INTO BuyerForms (VisitTime, BuyerName, BuyerContact, Query, SellerId, InterestedDogs) " +
                                       "VALUES (@VisitTime, @BuyerName, @BuyerContact, @Query,@SellerId, @InterestedDogs)";
                    SqlCommand insertCommand = new SqlCommand(insertSql, sqlConnection);

                    insertCommand.Parameters.Add(new SqlParameter("@VisitTime", entity.VisitTime));
                    insertCommand.Parameters.Add(new SqlParameter("@BuyerName", entity.BuyerName));
                    insertCommand.Parameters.Add(new SqlParameter("@BuyerContact", entity.BuyerContact));
                    insertCommand.Parameters.Add(new SqlParameter("@Query", entity.Query));
                    insertCommand.Parameters.Add(new SqlParameter("@SellerId", entity.SellerId));
                    insertCommand.Parameters.Add(new SqlParameter("@InterestedDogs", entity.InterestedDogs));

                    await insertCommand.ExecuteNonQueryAsync();
                    Console.WriteLine("BuyerForm created successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating BuyerForm: {ex.Message}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                using (var sqlConnection = SqlConnectionManager.GetConnection())
                {
                    sqlConnection.Open();
                    string deleteSql = "DELETE FROM BuyerForms WHERE Id = @Id";
                    SqlCommand deleteCommand = new SqlCommand(deleteSql, sqlConnection);
                    deleteCommand.Parameters.Add(new SqlParameter("@Id", id));
                    await deleteCommand.ExecuteNonQueryAsync();
                    Console.WriteLine($"BuyerForm with Id '{id}' deleted successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting BuyerForm: {ex.Message}");
            }
        }

        public async Task<List<BuyerForm>> GetAllAsync()
        {
            List<BuyerForm> buyerForms = new List<BuyerForm>();

            try
            {
                using (var sqlConnection = SqlConnectionManager.GetConnection())
                {
                    sqlConnection.Open();
                    string selectSql = "SELECT Id, VisitTime, BuyerName, BuyerContact, Query,SellerId, InterestedDogs FROM BuyerForms";
                    SqlCommand selectCommand = new SqlCommand(selectSql, sqlConnection);
                    SqlDataReader reader = await selectCommand.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        BuyerForm buyerForm = new BuyerForm
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            VisitTime = Convert.ToDateTime(reader["VisitTime"]),
                            BuyerName = reader["BuyerName"].ToString(),
                            BuyerContact = reader["BuyerContact"].ToString(),
                            Query = reader["Query"].ToString(),
                            SellerId = Convert.ToInt32(reader["SellerId"]),
                            InterestedDogs = reader["InterestedDogs"].ToString()
                        };
                        buyerForms.Add(buyerForm);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving BuyerForms: {ex.Message}");
            }

            return buyerForms;
        }

        public async Task<BuyerForm> GetBySellerIdAsync(int id)
        {
            BuyerForm buyerForm = null;

            try
            {
                using (var sqlConnection = SqlConnectionManager.GetConnection())
                {
                    sqlConnection.Open();
                    string selectSql = "SELECT Id, VisitTime, BuyerName, BuyerContact, Query,SellerId, InterestedDogs FROM BuyerForms WHERE Id = @Id";
                    SqlCommand selectCommand = new SqlCommand(selectSql, sqlConnection);
                    selectCommand.Parameters.Add(new SqlParameter("@Id", id));
                    SqlDataReader reader = await selectCommand.ExecuteReaderAsync();

                    if (reader.Read())
                    {
                        buyerForm = new BuyerForm
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            VisitTime = Convert.ToDateTime(reader["VisitTime"]),
                            BuyerName = reader["BuyerName"].ToString(),
                            BuyerContact = reader["BuyerContact"].ToString(),
                            Query = reader["Query"].ToString(),
                            SellerId = Convert.ToInt32(reader["SellerId"]),
                            InterestedDogs = reader["InterestedDogs"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving BuyerForm: {ex.Message}");
            }

            return buyerForm;
        }
    }
}
