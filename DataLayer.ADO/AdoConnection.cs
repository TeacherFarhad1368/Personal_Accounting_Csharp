using System.Data.SqlClient;
namespace DataLayer.ADO;
public class AdoConnection
{
  
    public AdoConnection()
    {
        CreateDataBase();
        CreateTables();
    }
    public bool CreateDataBase()
    {
        try
        {
            SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString);
                string createDbQuery = $"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = '{DataBaseConstant.databaseName}') " +
                $"CREATE DATABASE {DataBaseConstant.databaseName}";
                SqlCommand command = new SqlCommand(createDbQuery, connection);
                connection.Open();
                command.ExecuteNonQuery();
            connection.Close();
            return true;
            
        }
        catch 
        {
            return false;
        }
    }
    public bool CreateTables()
    {
        List<string> Queries = new List<string>()
        {
            DataBaseConstant.useMyDataBase,
            DataBaseConstant.createPersonCategoryTable,
            DataBaseConstant.createPeopleTable,
            DataBaseConstant.createSlariesTable,
            DataBaseConstant.createExpenseCategoriesTable,
            DataBaseConstant.createExpensesTable,
            DataBaseConstant.createChequesTable
        };
        try
        {
            SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString);
            connection.Open();
            foreach(var  query in Queries)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
            connection.Close();
            return true;
        }
        catch 
        {
            return false;
        }
    }
}
