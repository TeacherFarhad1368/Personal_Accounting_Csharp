namespace Accounting.Models.ExpenseCategoryModels;
public class ExpenseCategoryQueryModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int? ParentId { get; set; }
}
