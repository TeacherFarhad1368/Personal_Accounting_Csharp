using Accounting.Models.PersonCategoryModels;
using Accounting.Models.PersonModels;
using DataLayer.ADO;
using DataLayer.ADO.Services;
namespace Ado_First.Classes;
internal partial class ConsoleService
{
    PersonService PersonService = new PersonService();
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
            Email= values[2].ToString().TrimStart('[').TrimEnd(']'),
            FullName = values[0].ToString().TrimStart('[').TrimEnd(']'),
            Mobile = values[1].ToString().TrimStart('[').TrimEnd(']'), 
        };
        string date = values[3].ToString().TrimStart('[').TrimEnd(']');
        if (!string.IsNullOrEmpty(date))
        {
            string[] strings = date.Split(':');
            if (strings.Length == 3)
                model.BirthDate = 
                    new DateTime(
                        Convert.ToInt32(strings[0]), 
                        Convert.ToInt32(strings[1]), 
                        Convert.ToInt32(strings[2])
                        );
            var res = PersonService.Insert(model);
            if(res.Success) Console.WriteLine("Success");
            Console.WriteLine(res.Message);
        } 
      }
}
