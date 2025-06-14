using Accounting.Models.PersonCategoryModels;
using System.Data.SqlClient;

namespace DataLayer.ADO.Services;
public class PersonCategoryService
{
    public bool Insert(InsertPersonCategory model)
    {
        if (string.IsNullOrEmpty(model.Title))
        {
            Console.WriteLine("Title Nemitoone Khali Bashe");
            return false;
        }
        else if (model.Title.Length > 250)
        {
            Console.WriteLine("Maximom Length For Title is 255 charecter");
            return false;
        }
        else
        if (ExistForInsert(model.Title))
        {
            Console.WriteLine($"{model.Title} is Existed");
            return false;
        }
        else
            try
            {
                SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2);
                connection.Open();
                string query = $"Insert Into PersonCategories(Title) Values ('{model.Title}')";
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
    public bool ExistForEdit(EditPersonCategoty model)
    {
        try
        {
            SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2);
            connection.Open();
            string query = $"SELECT COUNT(*) FROM PersonCategories WHERE [Title] = '{model.Title}' And [Id] <> {model.Id}";
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
    public PersonCategoryQueryModel GetById(int id)
    {
        try
        {
            PersonCategoryQueryModel model = new();
            SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2);
            connection.Open();
            string query = $"SELECT * FROM PersonCategories WHERE [Id] = {id}";
            SqlCommand command = new SqlCommand(query, connection);
            using(SqlDataReader reader = command.ExecuteReader())
                if (reader.Read())
                {
                    model.Id =Convert.ToInt32(reader["Id"]);
                    model.Title = reader[nameof(model.Title)].ToString();
                    return model;
                }
            return null;
                        //if (reader.Read())
                        //    for (int i = 0; i < reader.FieldCount; i++)
                        //        Console.WriteLine($"{reader.GetName(i)} : {reader[reader.GetName(i)]}");
                        //else
                        //    Console.WriteLine($"Person Category With Id : {id} not found");
        }
        catch (Exception x)
        {
            //Console.WriteLine(x.Message);
            return null;
        }
    }
    public List<PersonCategoryQueryModel> GetAll()
    {
        try
        {
            List<PersonCategoryQueryModel> model = new();
            SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2);
            connection.Open();
            string query = $"SELECT * FROM PersonCategories";
            SqlCommand command = new SqlCommand(query, connection);
            using (SqlDataReader reader = command.ExecuteReader())
                if (reader.HasRows)
                {
                    Console.WriteLine();
                    while(reader.Read())
                    {
                        PersonCategoryQueryModel personModel = new()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = reader["Title"].ToString()
                        };
                        model.Add(personModel);
                        //for (int i = 0; i < reader.FieldCount; i++)
                        //    Console.Write($"{reader[reader.GetName(i)]} \t");
                        //Console.WriteLine();
                    }
                }
            //Console.WriteLine($"No Person Category found");
            return model;
        }
        catch (Exception x)
        {
            return null;
            //Console.WriteLine(x.Message);
        }
    }
    public bool Edit(EditPersonCategoty model)
    {
        if (string.IsNullOrEmpty(model.Title))
        {
            Console.WriteLine("Title Nemitoone Khali Bashe");
            return false;
        }
        else if (model.Title.Length > 250)
        {
            Console.WriteLine("Maximom Length For Title is 255 charecter");
            return false;
        }
        else
          if (ExistById(model.Id))
          {
            if (ExistForEdit(model))
            {
                Console.WriteLine($"{model.Title} is Existed");
                return false;
            }
            else
            {

                try
                {
                    SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2);
                    connection.Open();
                    string query = $"UPDATE PersonCategories SET Title = '{model.Title}' Where Id = {model.Id}";
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
            Console.WriteLine($"Person Category By Id :  {model.Id} is Not FOUND");
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
