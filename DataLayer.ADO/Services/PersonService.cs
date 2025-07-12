using Accounting.Models.PersonModels;
using System.Data;
using System.Data.SqlClient;
using Utilities.Opeartions;

namespace DataLayer.ADO.Services;
public class PersonService
{
    public OperationResult Insert(InsertPerson model)
    {
        try
        {
            SqlConnection connection = new SqlConnection(DataBaseConstant.connectionString2);
            connection.Open();
            string query = $"Insert Into People([FullName],[Mobile]";
            if(!string.IsNullOrEmpty(model.Email))
                query = query + $",[Email]";
            if (model.BirthDate != null)
                query = query + $",[BirthDate]";
            query = query + $",[PersonCategoryId]) Values('{model.FullName}', '{model.Mobile}'";
            if (!string.IsNullOrEmpty(model.Email))
                query = query + $", '{model.Email}'";
            if (model.BirthDate != null)
                query = query + $", '{model.BirthDate}'";
            query = query + $", '{model.PersonCategoryId}')";
                    SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
            return OperationResult.Succeded();
        }
        catch (Exception x)
        {
            return OperationResult.Faild(x.Message);
        }
    }
    public DataTable GetAll()
    {
        string command = "Select x.Id,x.FullName,x.Mobile,x.Email,x.BirthDate,s.Title As CategoryTitle,x.CreateDate From PersonCategories AS s Inner JOIN People as x ON x.PersonCategoryId = s.Id";
        using (var adapter = new SqlDataAdapter(command, DataBaseConstant.connectionString2))
        {
            DataTable table = new();
            adapter.Fill(table);
            return table;   
        }
    }
}
