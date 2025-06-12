using DataLayer.ADO;
using DataLayer.ADO.Services;

namespace Ado_First.Classes;
internal class ConsoleService
{
    AdoConnection connection = new();
    PersonCategoryService personCategoryService = new();
    public void First()
    {
        Console.WriteLine("Please Insert Person Category Title");
        string title = Console.ReadLine();
        personCategoryService.Insert(title);
    }
    public void Second()
    {
        Console.WriteLine("Please Insert Person Category Id");
        int id = Convert.ToInt32(Console.ReadLine());
        personCategoryService.GetById(id);  
    }
    public void Third()
    {
        personCategoryService.GetAll();
    }
    public void Forth()
    {
        Console.WriteLine("Please Insert Id");
        int id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Please Insert new Title");
        string title = Console.ReadLine();
        personCategoryService.Edit(id, title);
    }
}
