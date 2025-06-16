using Accounting.Models.PersonCategoryModels;
using DataLayer.ADO;
using DataLayer.ADO.Services;
namespace Ado_First.Classes;
internal partial class ConsoleService
{
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
        if (res.Success) Console.WriteLine("Success");
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
        var res = personCategoryService.Edit(model);
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
}
