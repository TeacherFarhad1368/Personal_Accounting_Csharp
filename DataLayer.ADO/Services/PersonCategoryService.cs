using System.Data.SqlClient;

namespace DataLayer.ADO.Services;
public class PersonCategoryService
{

    public bool CreatePersonCategory(string title)
    {
        if (ExistPersonCategory(title))
        {
            Console.WriteLine($"{title} is Existed");
            return false;
        }
        else
            try
            {
                SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2);
                connection.Open();
                string query = $"Insert Into PersonCategories(Title) Values ('{title}')";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                Console.WriteLine("Success");
                return true;
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                return false;
            }
    }
    public bool ExistPersonCategory(string title)
    {
        try
        {
            SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2);
            connection.Open();
            string query = $"SELECT COUNT(*) FROM PersonCategories WHERE [Title] = '{title}'";
            SqlCommand command = new SqlCommand(query, connection);
            int count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }
        catch (Exception x)
        {
            Console.WriteLine(x.Message);
            return true;
        }
    }
}
