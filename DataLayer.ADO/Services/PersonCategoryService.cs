using System.Data.SqlClient;

namespace DataLayer.ADO.Services;
public class PersonCategoryService
{

    public bool Insert(string title)
    {
        if (Exist(title))
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
    public bool Exist(string title)
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
    public void GetById(int id)
    {
        try
        {
            SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2);
            connection.Open();
            string query = $"SELECT * FROM PersonCategories WHERE [Id] = {id}";
            SqlCommand command = new SqlCommand(query, connection);
            using(SqlDataReader reader = command.ExecuteReader())
                if (reader.Read())
                    for (int i = 0; i < reader.FieldCount; i++)
                        Console.WriteLine($"{reader.GetName(i)} : {reader[reader.GetName(i)]}");
                else
                    Console.WriteLine($"Person Category With Id : {id} not found");
        }
        catch (Exception x)
        {
            Console.WriteLine(x.Message);
        }
    }
    public void GetAll()
    {
        try
        {
            SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2);
            connection.Open();
            string query = $"SELECT * FROM PersonCategories";
            SqlCommand command = new SqlCommand(query, connection);
            using (SqlDataReader reader = command.ExecuteReader())
                if (reader.HasRows)
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                        Console.Write($"{reader.GetName(i)} \t");
                    Console.WriteLine();
                    while(reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                            Console.Write($"{reader[reader.GetName(i)]} \t");
                        Console.WriteLine();
                    }
                }
                else
                    Console.WriteLine($"No Person Category found");
        }
        catch (Exception x)
        {
            Console.WriteLine(x.Message);
        }
    }
}
