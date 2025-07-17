using Accounting.Models.PersonCategoryModels;
using DataLayer.ADO;
using DataLayer.ADO.Services;
namespace Ado_First.Classes;
internal partial class ConsoleService
{
    List<string> Actions = new List<string>()
    {
        "(Get All PC : gapc )",
        "(Get By Id PC : gipc )",
        "(Create PC : cpc )",
        "(Edit PC : epc )",
        "(Delete PC : dpc )",
        "(Create Person : cp )",
        "(Update Person : up )",
        "(Delete Person : dp )",
        "(Get All Person : gap )",
        "(Close Application : end)",
        "(Clear Console : clear)",
    };
    public string Action;
    AdoConnection AdoConnection = new();
    PersonCategoryService personCategoryService = new();
   
    internal void RunApplication()
    {
        Console.ResetColor();
        Console.WriteLine(string.Join(" - ", Actions));
        string action = Console.ReadLine();
        switch (action.ToLower())
        {
            case "gapc":
                GetAllPersonCategory();
                break;
            case "gipc":
                GetPersonCategoryById();
                break;
            case "cpc":
                CreatePersonCategory();
                break;
            case "epc":
                EditPersonCategory();
                break;
            case "dpc":
                DeletePersonCategory();
                break;
            case "end":
                Console.ReadKey();
                break;
            case "clear":
                Console.Clear();
                RunApplication();
                break;
            case "cp":
                CreatePerson();
                break;
            case "up":
                EditPerson();
                break;
            case "dp":
                DeletePerson();
                break;
            case "gap":
                GetAllPerson();
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("amaliat morede nazar ro dorost vared kon");
                RunApplication();
                break;
        }
    }
}
