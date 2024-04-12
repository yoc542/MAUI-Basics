using Microsoft.Data.SqlClient;

namespace DogHome.Server
{
    public static class SqlConnectionManager
    {
        private static string sqlconn = $"Data Source=localhost;Initial Catalog=DogHomeApp;User ID=Swechha;Password=Swechha;TrustServerCertificate=True;";
        public static SqlConnection GetConnection()
        {
            try
            {
                return new SqlConnection(sqlconn);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create SQL connection: " + ex.Message);
            }
        }
    }
}
