using Accounting.Models.PersonCategoryModels;
using Accounting.Models.PersonModels;
using DataLayer.ADO;
using DataLayer.ADO.Services;
using System.Data;
using Utilities.DateUtils;
namespace Ado_First.Classes;
internal partial class ConsoleService
{
    PersonService personService = new PersonService();
    PeopleDatasetService peopleDatasetService = new PeopleDatasetService();
    public void CreatePerson()
      {
        Console.WriteLine("ba formate zir maghadir ro vared kon");
        Console.WriteLine("[full name]-[mobile]-[email]-[tavalod= yyyy:mm:dd]-[person Category Id]");
        var val = Console.ReadLine();
        string[] values = val.Split("-");
        InsertPerson model = new InsertPerson
        {
            PersonCategoryId = Convert.ToInt32(values[4].ToString().TrimStart('[').TrimEnd(']')),
            BirthDate = null,
            Email= string.IsNullOrEmpty(values[2].ToString().TrimStart('[').TrimEnd(']')) ? null : values[2].ToString().TrimStart('[').TrimEnd(']'),
            FullName = values[0].ToString().TrimStart('[').TrimEnd(']'),
            Mobile = values[1].ToString().TrimStart('[').TrimEnd(']'), 
        };
        string date = values[3].ToString().TrimStart('[').TrimEnd(']');
        model.BirthDate = DateConvertor.ToEnglishDateTime(date);
        var res = personService.Insert(model);
        if (res.Success) Console.WriteLine("Success");
        Console.WriteLine(res.Message);
        RunApplication();
    }
    public void EditPerson()
    {
        Console.WriteLine("ba formate zir maghadir ro vared kon");
        Console.WriteLine("[full name]-[mobile]-[email]-[tavalod= yyyy:mm:dd]-[person Category Id]-[Id]");
        var val = Console.ReadLine();
        string[] values = val.Split("-");
        UpdatePerson model = new UpdatePerson
        {
            PersonCategoryId = Convert.ToInt32(values[4].ToString().TrimStart('[').TrimEnd(']')),
            BirthDate = null,
            Email = string.IsNullOrEmpty(values[2].ToString().TrimStart('[').TrimEnd(']')) ? null : values[2].ToString().TrimStart('[').TrimEnd(']'),
            FullName = values[0].ToString().TrimStart('[').TrimEnd(']'),
            Mobile = values[1].ToString().TrimStart('[').TrimEnd(']'),
            Id = Convert.ToInt32(values[5].ToString().TrimStart('[').TrimEnd(']'))
        };
        string date = values[3].ToString().TrimStart('[').TrimEnd(']');
        model.BirthDate = DateConvertor.ToEnglishDateTime(date);
        var res = personService.Update(model);
        if (res.Success) Console.WriteLine("Success");
        Console.WriteLine(res.Message);
        RunApplication();
    }
    public void DeletePerson()
    {
        Console.WriteLine("Id Shakse Morede Nazar Ra Vared Konid"); 
        int id = Convert.ToInt32(Console.ReadLine());
        var res = personService.Delete(id);
        if (res.Success) Console.WriteLine("Success");
        Console.WriteLine(res.Message);
        RunApplication();
    }
    public void GetAllPerson()
    {
        var table = personService.GetAll();
        foreach (DataRow row in table.Rows)
            Console.WriteLine($"{row["Id"]} \t {row["FullName"]} \t {row["Mobile"]} \t {row["Email"]} \t {DateConvertor.ToPersianDate(row["BirthDate"].ToString())} \t {row["CategoryTitle"]} \t {DateConvertor.ToPersianDate(row["CreateDate"].ToString())}");
        RunApplication();
    }
    public void GetAllData()
    {
        var dataSet = peopleDatasetService.GetData(); 
        foreach (DataRow row in dataSet.Tables["People"].Rows)
        {
            Console.WriteLine($"{row["Id"]} \t {row["FullName"]} \t {row["Mobile"]} \t {row["Email"]} \t {DateConvertor.ToPersianDate(row["BirthDate"].ToString())} \t {row["CreateDate"]}");
            if (row.GetParentRow("FK_PersonCategory") is DataRow parentRow)
            {
                Console.WriteLine($"\t Category: {parentRow["Title"]}");
            }
        }
        foreach (DataRow categoryRow in dataSet.Tables["PersonCategories"].Rows)
        {
            Console.WriteLine($"Category: {categoryRow["Id"]} \t {categoryRow["Title"]}");
        }
    }
}
