using Accounting.Models.ExpenseCategoryModels;
using Accounting.Models.PersonCategoryModels;
using DataLayer.ADO.Services;

namespace Ado_First.Classes;
internal partial class ConsoleService
{
    ExpenseCategoryService expenseCategoryService = new ExpenseCategoryService();
    internal void CreateExpenseCategory()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Please Insert Expense Category Title");
        string title = Console.ReadLine();
        InsertExpenseCategory model = new InsertExpenseCategory()
        {
            Title = title,
            ParentId = null
        };
        var res = expenseCategoryService.Insert(model);
        if (res.Success) Console.WriteLine("Success");
        else Console.WriteLine(res.Message);
        RunApplication();
    }
}
