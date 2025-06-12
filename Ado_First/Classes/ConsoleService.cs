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
        personCategoryService.CreatePersonCategory(title);
    }
}
