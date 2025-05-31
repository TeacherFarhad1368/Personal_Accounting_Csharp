using System.Data.SqlClient;
namespace DataLayer.ADO;
public class AdoConnection
{
    string connectionString = "Server=.;Integrated Security=true;";
    string databaseName = "AccountingDB";

    public void CreateDataBase()
    {
        try
        {
            SqlConnection connection = new SqlConnection(connectionString);
                string createDbQuery = $"CREATE DATABASE {databaseName}";
                SqlCommand command = new SqlCommand(createDbQuery, connection);
                connection.Open();
                command.ExecuteNonQuery();
            connection.Close();
                Console.WriteLine($"Create {databaseName} Is Successfully.");
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Create {databaseName}  Faild : {ex.Message}");
        }
    }
}
