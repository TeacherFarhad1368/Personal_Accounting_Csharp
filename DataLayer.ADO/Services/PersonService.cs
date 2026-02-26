using Accounting.Models.PersonModels;
using System.Data;
using System.Data.SqlClient;
using Utilities.Opeartions;

namespace DataLayer.ADO.Services;
public class PersonService
{
    public DataTable Persons { get; set; } = new();
    public PersonService()
    {
        GetDataTable();
    }
    public OperationResult Insert(InsertPerson model)
    {
        try
        {
            string command = $"Insert Into People([FullName],[Mobile]";
            if(!string.IsNullOrEmpty(model.Email))
                command = command + $",[Email]";
            if (model.BirthDate != null)
                command = command + $",[BirthDate]";
            command = command + $",[PersonCategoryId]) Values('{model.FullName}', '{model.Mobile}'";
            if (!string.IsNullOrEmpty(model.Email))
                command = command + $", '{model.Email}'";
            if (model.BirthDate != null)
                command = command + $", '{model.BirthDate}'";
            command = command + $", '{model.PersonCategoryId}')";
            using(SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2))
            using (var adapter = new SqlDataAdapter(command, connection))
            {
                connection.Open();
                adapter.InsertCommand = new SqlCommand(command, connection);
                int x = Convert.ToInt32(adapter.InsertCommand.ExecuteScalar());
            }
            GetDataTable();
            return OperationResult.Succeded();
        }
        catch (Exception x)
        {
            return OperationResult.Faild(x.Message);
        }
    }
    public OperationResult Update(UpdatePerson model)
    {
        try
        {
            string command = $"Update People SET FullName = '{model.FullName}' ,Mobile = '{model.Mobile}' ,";
            if (!string.IsNullOrEmpty(model.Email))
                command = command + $"Email = '{model.Email}',";
            if (model.BirthDate != null)
                command = command + $"BirthDate = '{model.BirthDate}' ,";
            command = command + $"PersonCategoryId = {model.PersonCategoryId} WHERE (Id = {model.Id})";
            
            using (SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2))
            using (var adapter = new SqlDataAdapter(command, connection))
            {
                connection.Open();
                adapter.UpdateCommand = new SqlCommand(command, connection);
                int x = adapter.UpdateCommand.ExecuteNonQuery();
            }
            GetDataTable();
            return OperationResult.Succeded();
        }
        catch (Exception x)
        {
            return OperationResult.Faild(x.Message);
        }
    }
    public OperationResult Delete(int id)
    {
        try
        {
            string command = $"DELETE FROM People WHERE Id = {id}";
            using (SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2))
            using (var adapter = new SqlDataAdapter(command, connection))
            {
                connection.Open();
                adapter.DeleteCommand = new SqlCommand(command, connection);
                adapter.DeleteCommand.ExecuteNonQuery();
            }
            GetDataTable();
            return OperationResult.Succeded();
        }
        catch (Exception x)
        {
            return OperationResult.Faild(x.Message);
        }
    }
    public DataTable GetAll() => Persons;    
    public void GetDataTable()
    {
        string command = "Select x.Id,x.FullName,x.Mobile,x.Email,x.BirthDate,s.Title As CategoryTitle,x.CreateDate From PersonCategories AS s Inner JOIN People as x ON x.PersonCategoryId = s.Id";
        using (var adapter = new SqlDataAdapter(command, DataBaseConstant.connectionString2))
        {
            DataTable data = new();
            adapter.Fill(data);
            Persons = data; 
        }
    }
}
