using System.Data.SqlClient;
namespace DataLayer.ADO;
public class AdoConnection
{
    string connectionString = "Server=.;Integrated Security=true;";
    string connectionString2 = "Server=.;Database=AccountingDB;Integrated Security=true;";
    string databaseName = "AccountingDB";
    string useMyDataBase = $"Use AccountingDB";
    string createPersonCategoryTable = "IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PersonCategories' AND type = 'U') BEGIN Create Table PersonCategories ( Id int Not Null Primary Key Identity, Title Nvarchar(250) Not NULL ); Create Unique Index PersonCategory_Title_Index  On PersonCategories(Title); END";
    string createPeopleTable = "IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'People' AND type = 'U') BEGIN Create Table People ( Id int Not Null Primary Key Identity, FullName Nvarchar(250) Not NULL, Mobile Nvarchar(11) Not NULL, Email Nvarchar(250) NULL, BirthDate DateTime NULL, CreateDate DateTime DEFAULT GETDATE(), PersonCategoryId int Not Null , Constraint FK_PersonCategory  Foreign Key (PersonCategoryId) References PersonCategories(Id), Constraint Check_Mobile_Length Check(Len(Mobile) = 11) ); Create Unique Index People_Mobile_Index  On People(Mobile); Create Unique Index People_Email_Index  On People(Email) Where Email Is Not NULL; END";
    string createSlariesTable = "IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Salaries' AND type = 'U') BEGIN Create Table Salaries ( Id int Not Null Identity Primary Key, PersonId int Not Null , Amount int Not NULL, Description NVARCHAR(MAX) Not null, DepositDate DateTime2 Not Null, CreateDate DateTime DEFAULT GETDATE(), Constraint FK_Person  Foreign Key (PersonId) References People(Id),); END";
    string createExpenseCategoriesTable = "IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ExpenseCategories' AND type = 'U') BEGIN Create Table ExpenseCategories ( Id int Not Null Identity Primary Key, Title NVarchar(250) Not Null , ParentId int Null, Constraint FK_ExpenseCategory_Child Foreign Key (ParentId) References ExpenseCategories(Id), );  Create Unique Index ExpenseCategories_Title_Index  On ExpenseCategories(Title); END";
    string createExpensesTable = "IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Expenses' AND type = 'U') BEGIN Create Table Expenses ( Id int Not Null Identity Primary Key, ExpenseCategoryId int Not Null, Amount int Not NULL, Description NVARCHAR(MAX) Not null, ExpenseDate DateTime2 Not Null, CreateDate DateTime DEFAULT GETDATE(),      Constraint FK_ExpenseCategory  Foreign Key (ExpenseCategoryId) References ExpenseCategories(Id),); END";
    string createChequesTable = "IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Cheques' AND type = 'U') BEGIN Create Table Cheques ( Id int Not Null Identity Primary Key, PersonId int Not Null , Amount int Not NULL, Description NVARCHAR(MAX) Not null, BankName NVARCHAR(150) Not null, ChequeDate DateTime2 Not Null, CreateDate DateTime DEFAULT GETDATE(), Status int Not Null ,     Constraint FK_Person_Cheque  Foreign Key (PersonId) References People(Id),  \tConstraint Check_Status_Value Check(Status = 0 Or Status = 1) ); END";
    public AdoConnection()
    {
        CreateDataBase();
        CreateTables();
    }
    public bool CreateDataBase()
    {
        try
        {
            SqlConnection connection = new SqlConnection(connectionString);
                string createDbQuery = $"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = '{databaseName}') " +
                $"CREATE DATABASE {databaseName}";
                SqlCommand command = new SqlCommand(createDbQuery, connection);
                connection.Open();
                command.ExecuteNonQuery();
            connection.Close();
            return true;
            
        }
        catch 
        {
            return false;
        }
    }
    public bool CreateTables()
    {
        List<string> Queries = new List<string>()
        {
            useMyDataBase,
            createPersonCategoryTable,
            createPeopleTable,
            createSlariesTable,
            createExpenseCategoriesTable,
            createExpensesTable,
            createChequesTable
        };
        try
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            foreach(var  query in Queries)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
            connection.Close();
            return true;
        }
        catch 
        {
            return false;
        }
    }
    public bool CreatePersonCategory(string title)
    {
        try
        {
            SqlConnection connection = new SqlConnection(connectionString2);
            connection.Open();
            string query = $"Insert Into PersonCategories(Title) Values ('{title}')";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            Console.WriteLine("Success");
            return true;
        }
        catch (Exception x)
        {
            Console.WriteLine(x.Message);
            return false;
        }
    }
}
