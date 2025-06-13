using DataLayer.ADO;
using DataLayer.ADO.Services;
namespace Ado_First.Classes;
internal class ConsoleService
{
    List<string> Actions = new List<string>()
    {
        "(Get All PersonCategory : gapc )",
        "(Get By Id PersonCategory : gipc )",
        "(Create PersonCategory : cpc )",
        "(Edit PersonCategory : epc )",
        "(Delete PersonCategory : dpc )",
        "Close Application : end"
    };
    public string Action;
    AdoConnection AdoConnection = new();
    PersonCategoryService personCategoryService = new();
    public void CreatePersonCategory()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Please Insert Person Category Title");
        string title = Console.ReadLine();
        personCategoryService.Insert(title);
        RunApplication();
    }
    public void GetPersonCategoryById()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Please Insert Person Category Id");
        int id = Convert.ToInt32(Console.ReadLine());
        personCategoryService.GetById(id);
        RunApplication();
    }
    public void GetAllPersonCategory()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        personCategoryService.GetAll();
        RunApplication();
    }
    public void EditPersonCategory()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Please Insert Id");
        int id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Please Insert new Title");
        string title = Console.ReadLine();
        personCategoryService.Edit(id, title);
        RunApplication();
    }
    public void DeletePersonCategory()
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
        Console.WriteLine("Chi Kar Mikhay Bokoni ?");
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
            default:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("amaliat morede nazar ro dorost vared kon");
                RunApplication();
                break;
        }
    }
}
