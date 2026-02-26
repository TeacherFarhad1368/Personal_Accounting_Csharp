using Accounting.Models.ExpenseCategoryModels;
using Accounting.Models.PersonCategoryModels;
using System.Data.SqlClient;
using Utilities.Opeartions;

namespace DataLayer.ADO.Services;
public class PersonCategoryService
{
    public OperationResult Insert(InsertPersonCategory model)
    {
        if (string.IsNullOrEmpty(model.Title)) return OperationResult.Faild("Title Nemitoone Khali Bashe");
        else if (model.Title.Length > 250) return OperationResult.Faild("Maximom Length For Title is 255 charecter");
        else
        if (ExistForInsert(model.Title)) return OperationResult.Faild($"{model.Title} is Existed");
        else
            try
            {
                SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2);
                connection.Open();
                string query = $"Insert Into PersonCategories(Title) Values ('{model.Title}')";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                return OperationResult.Succeded();
            }
            catch (Exception x)
            {
                return OperationResult.Faild(x.Message);
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
        }
        catch (Exception x)
        {
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
                    while(reader.Read())
                    {
                        PersonCategoryQueryModel personModel = new()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = reader["Title"].ToString()
                        };
                        model.Add(personModel);
                    }
                }
            return model;
        }
        catch (Exception x)
        {
            return null;
        }
    }
    public OperationResult Edit(EditPersonCategoty model)
    {
        if (string.IsNullOrEmpty(model.Title)) return OperationResult.Faild("Title Nemitoone Khali Bashe");
        else if (model.Title.Length > 250) return OperationResult.Faild("Maximom Length For Title is 255 charecter");
        else
          if (ExistById(model.Id))
          {
            if (ExistForEdit(model)) return OperationResult.Faild($"{model.Title} is Existed");
            else
            {

                try
                {
                    SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2);
                    connection.Open();
                    string query = $"UPDATE PersonCategories SET Title = '{model.Title}' Where Id = {model.Id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    return OperationResult.Succeded();
                }
                catch (Exception x)
                {
                    return OperationResult.Faild(x.Message);
                }
            }
           
        }
        else
        {
            return OperationResult.Faild($"Person Category By Id :  {model.Id} is Not FOUND");
        }
    }
    public OperationResult Delete(int id)
    {
        if (ExistById(id))
            try
            {
                SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2);
                connection.Open();
                string query = $"DELETE FROM PersonCategories Where Id = {id}";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                return OperationResult.Succeded();
            }
            catch (Exception x)
            {
                return OperationResult.Faild(x.Message);
            }
        else
        {
            return OperationResult.Faild($"Person Category By Id :  {id} is Not FOUND");
        }
    }
}
public class ExpenseCategoryService
{
    public OperationResult Insert(InsertExpenseCategory model)
    {
        if (string.IsNullOrEmpty(model.Title)) return OperationResult.Faild("Title Nemitoone Khali Bashe");
        else if (model.Title.Length > 250) return OperationResult.Faild("Maximom Length For Title is 255 charecter");
        else
            try
            {
                SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2);
                connection.Open();
                string query = $"Exec CreateExpenseCategory @Title = '{model.Title}' ";
                if(model.ParentId != null) query += $", @ParentId = {model.ParentId}";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                return OperationResult.Succeded();
            }
            catch (Exception x)
            {
                return OperationResult.Faild(x.Message);
            }
    }
}