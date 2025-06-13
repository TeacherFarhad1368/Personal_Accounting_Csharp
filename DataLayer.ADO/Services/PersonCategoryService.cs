using System.Data.SqlClient;

namespace DataLayer.ADO.Services;
public class PersonCategoryService
{
    public bool Insert(string title)
    {
        if (ExistForInsert(title))
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
    public bool ExistForInsert(string title)
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
    public bool ExistForEdit(int id,string title)
    {
        try
        {
            SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2);
            connection.Open();
            string query = $"SELECT COUNT(*) FROM PersonCategories WHERE [Title] = '{title}' And [Id] <> {id}";
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
    public bool ExistById(int id)
    {
        try
        {
            SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2);
            connection.Open();
            string query = $"SELECT COUNT(*) FROM PersonCategories WHERE [Id] = {id}";
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
    public bool Edit(int id,string title)
    {
        // چک کردن آی دی
        // چک کردن PersonCategory_Title_Index

        if (ExistById(id))
        {
            if (ExistForEdit(id,title))
            {
                Console.WriteLine($"{title} is Existed");
                return false;
            }
            else
            {

                try
                {
                    SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2);
                    connection.Open();
                    string query = $"UPDATE PersonCategories SET Title = '{title}' Where Id = {id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    Console.WriteLine("Update Data Successfully");
                    return true;
                }
                catch (Exception x)
                {
                    Console.WriteLine(x.Message);
                    return false;
                }
            }
           
        }
        else
        {
            Console.WriteLine($"Person Category By Id :  {id} is Not FOUND");
            return false;
        }
    }
    public bool Delete(int id)
    {
        if (ExistById(id))
            try
            {
                SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2);
                connection.Open();
                string query = $"DELETE FROM PersonCategories Where Id = {id}";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                Console.WriteLine("DELETED Data Successfully");
                return true;
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                return false;
            }
        else
        {
            Console.WriteLine($"Person Category By Id :  {id} is Not FOUND");
            return false;
        }
    }
}
