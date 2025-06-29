﻿using Accounting.Models.PersonModels;
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
            string query = $"Insert Into People([FullName],[Mobile],[Email],[BirthDate],[PersonCategoryId]) " +
                $"Values ('{model.FullName}','{model.Mobile}','{model.Email}','{model.BirthDate}','{model.PersonCategoryId}')";
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
        using (var adapter = new SqlDataAdapter("SELECT * FROM PEOPLE", DataBaseConstant.connectionString2))
        {
            DataTable table = new();
            adapter.Fill(table);
            return table;   
        }
    }
}
