namespace Accounting.Models.PersonModels;
public class InsertPerson
{
    public string FullName { get; set; }
    public string Mobile { get; set; }
    public string? Email { get; set; }
    public DateTime? BirthDate { get; set; }
    public int PersonCategoryId { get; set; }
}
