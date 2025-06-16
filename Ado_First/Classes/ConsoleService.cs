using Accounting.Models.PersonCategoryModels;
using DataLayer.ADO;
using DataLayer.ADO.Services;
using System.Reflection;
using System.Reflection.PortableExecutable;
namespace Ado_First.Classes;
internal class ConsoleService
{
    List<string> Actions = new List<string>()
    {
        "(Get All PC : gapc )",
        "(Get By Id PC : gipc )",
        "(Create PC : cpc )",
        "(Edit PC : epc )",
        "(Delete PC : dpc )",
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
        var res = personCategoryService.Insert(model);
        if(res.Success) Console.WriteLine("Success");
        else Console.WriteLine(res.Message);
            RunApplication();
    }
    internal void GetPersonCategoryById()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Please Insert Person Category Id");
        int id = Convert.ToInt32(Console.ReadLine());
        var model = personCategoryService.GetById(id);
        Console.WriteLine($"{model.Id} \t {model.Title}");
        RunApplication();
    }
    internal void GetAllPersonCategory()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        var model = personCategoryService.GetAll();
        Console.WriteLine($"Id \t Title");
        foreach (var item in model)
            Console.WriteLine($"{item.Id} \t {item.Title}");
        RunApplication();
    }
    internal void EditPersonCategory()
    {
        EditPersonCategoty model = new EditPersonCategoty();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Please Insert Id");
        model.Id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Please Insert new Title");
        model.Title = Console.ReadLine();
         var res =personCategoryService.Edit(model);
        if (res.Success) Console.WriteLine("Success");
        else Console.WriteLine(res.Message);
        RunApplication();
    }
    internal void DeletePersonCategory()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Please Insert Id For Delete");
        int id = Convert.ToInt32(Console.ReadLine());
        var res = personCategoryService.Delete(id);
        if (res.Success) Console.WriteLine("Success");
        else Console.WriteLine(res.Message);
        RunApplication();
    }
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
            default:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("amaliat morede nazar ro dorost vared kon");
                RunApplication();
                break;
        }
    }
}
