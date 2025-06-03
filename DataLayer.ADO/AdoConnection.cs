using System.Data.SqlClient;
namespace DataLayer.ADO;
public class AdoConnection
{
    string connectionString = "Server=.;Integrated Security=true;";
    string databaseName = "AccountingDB";
    string useMyDataBase = $"Use AccountingDB";
    string createPersonCategoryTable = "Create Table PersonCategories ( Id int Not Null Primary Key Identity, Title Nvarchar(250) Not NULL ); Create Unique Index PersonCategory_Title_Index  On PersonCategories(Title);";
    string createPeopleTable = "Create Table People ( Id int Not Null Primary Key Identity, FullName Nvarchar(250) Not NULL, Mobile Nvarchar(11) Not NULL, Email Nvarchar(250) NULL, BirthDate DateTime NULL, CreateDate DateTime DEFAULT GETDATE(), PersonCategoryId int Not Null , Constraint FK_PersonCategory  Foreign Key (PersonCategoryId) References PersonCategories(Id), Constraint Check_Mobile_Length Check(Len(Mobile) = 11) ); Create Unique Index People_Mobile_Index  On People(Mobile); Create Unique Index People_Email_Index  On People(Email) Where Email Is Not NULL;";
    string createSlariesTable = "Create Table Salaries ( Id int Not Null Identity Primary Key, PersonId int Not Null , Amount int Not NULL, Description NVARCHAR(MAX) Not null, DepositDate DateTime2 Not Null, CreateDate DateTime DEFAULT GETDATE(), Constraint FK_Person  Foreign Key (PersonId) References People(Id),);";
    string createExpenseCategoriesTable = "Create Table ExpenseCategories ( Id int Not Null Identity Primary Key, Title NVarchar(250) Not Null , ParentId int Null, Constraint FK_ExpenseCategory_Child Foreign Key (ParentId) References ExpenseCategories(Id), );  Create Unique Index ExpenseCategories_Title_Index  On ExpenseCategories(Title);";
    string createExpensesTable = "Create Table Expenses ( Id int Not Null Identity Primary Key, ExpenseCategoryId int Not Null, Amount int Not NULL, Description NVARCHAR(MAX) Not null, ExpenseDate DateTime2 Not Null, CreateDate DateTime DEFAULT GETDATE(),      Constraint FK_ExpenseCategory  Foreign Key (ExpenseCategoryId) References ExpenseCategories(Id),  );";
    string createChequesTable = "Create Table Cheques ( Id int Not Null Identity Primary Key, PersonId int Not Null , Amount int Not NULL, Description NVARCHAR(MAX) Not null, BankName NVARCHAR(150) Not null, ChequeDate DateTime2 Not Null, CreateDate DateTime DEFAULT GETDATE(), Status int Not Null ,     Constraint FK_Person_Cheque  Foreign Key (PersonId) References People(Id),  \tConstraint Check_Status_Value Check(Status = 0 Or Status = 1) );";
    public AdoConnection()
    {
        CreateDataBase();
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
    public void CreateTables()
    {
        try
        {
            SqlConnection connection = new SqlConnection(connectionString);
           
            SqlCommand command1 = new SqlCommand(useMyDataBase, connection);
            connection.Open();
            command1.ExecuteNonQuery();
            Console.WriteLine("Use DataBase Success");
            SqlCommand command2 = new SqlCommand(createPersonCategoryTable, connection);
            command2.ExecuteNonQuery();
            Console.WriteLine("create PersonCategory Table Success");
            SqlCommand command3 = new SqlCommand(createPeopleTable, connection);
            command3.ExecuteNonQuery();
            Console.WriteLine("create People Table Success");
            SqlCommand command4 = new SqlCommand(createSlariesTable, connection);
            command4.ExecuteNonQuery();
            Console.WriteLine("create Slaries Table");
            SqlCommand command5 = new SqlCommand(createExpenseCategoriesTable, connection);
            command5.ExecuteNonQuery();
            Console.WriteLine("create Expense Categories Table");
            SqlCommand command6 = new SqlCommand(createExpensesTable, connection);
            command6.ExecuteNonQuery();
            Console.WriteLine("create Expenses Table Success");
            SqlCommand command7 = new SqlCommand(createChequesTable, connection);
            command7.ExecuteNonQuery();
            Console.WriteLine("create Cheques Table Success");
            connection.Close();
            Console.WriteLine("All Tables Created Successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message.ToString());   
        }
    }
}
