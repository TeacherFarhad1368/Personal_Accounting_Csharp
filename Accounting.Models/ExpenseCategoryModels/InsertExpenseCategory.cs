namespace Accounting.Models.ExpenseCategoryModels;

public class InsertExpenseCategory
{
    public string Title { get; set; }
    public int? ParentId { get; set; }
}
