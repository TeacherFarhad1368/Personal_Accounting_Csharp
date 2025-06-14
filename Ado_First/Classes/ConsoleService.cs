using Accounting.Models.PersonCategoryModels;
using DataLayer.ADO;
using DataLayer.ADO.Services;
using System.Reflection;
namespace Ado_First.Classes;
internal class ConsoleService
{
    List<string> Actions = new List<string>()
    {
        "(Get All Person Category : gapc )",
        "(Get By Id Person Category : gipc )",
        "(Create Person Category : cpc )",
        "(Edit Person Category : epc )",
        "(Delete Person Category : dpc )",
        "(Close Application : end)",
        "(Clear Console : clear)",
    };
    public string Action;
    AdoConnection AdoConnection = new();
    PersonCategoryService personCategoryService = new();
    internal void CreatePersonCategory()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Please Insert Person Category Title");
        string title = Console.ReadLine();
        InsertPersonCategory model = new InsertPersonCategory()
        {
            Title = title
        };
        personCategoryService.Insert(model);
        RunApplication();
    }
    internal void GetPersonCategoryById()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Please Insert Person Category Id");
        int id = Convert.ToInt32(Console.ReadLine());
        personCategoryService.GetById(id);
        RunApplication();
    }
    internal void GetAllPersonCategory()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        personCategoryService.GetAll();
        RunApplication();
    }
    internal void EditPersonCategory()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Please Insert Id");
        int id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Please Insert new Title");
        string title = Console.ReadLine();
        personCategoryService.Edit(id, title);
        RunApplication();
    }
    internal void DeletePersonCategory()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Please Insert Id For Delete");
        int id = Convert.ToInt32(Console.ReadLine());
        personCategoryService.Delete(id);
        RunApplication();
    }
    internal void RunApplication()
    {
        Console.ResetColor();
        foreach(var item in Actions)
            Console.WriteLine(item);
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
            default:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("amaliat morede nazar ro dorost vared kon");
                RunApplication();
                break;
        }
    }
}
