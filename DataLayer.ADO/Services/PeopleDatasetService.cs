using System.Data;
using System.Data.SqlClient;

namespace DataLayer.ADO.Services;
public class PeopleDatasetService
{
    public DataSet MyData { get; set; } = new DataSet();
    public PeopleDatasetService()
    {
        SetData();
    }
    public DataSet GetData()
    {
        SetData(); 
        return MyData;
    }
    public void SetData()
    {
         using(var connection = new SqlConnection(DataBaseConstant.connectionString2))
        {
            DataSet dataset = new DataSet();
            connection.Open();
            var categoryQuery = "SELECT * FROM PersonCategories";
            var personQuery = "SELECT * FROM People";
            SqlDataAdapter categoryAdapter = new SqlDataAdapter(categoryQuery, connection);
            categoryAdapter.Fill(dataset, "PersonCategories");
            SqlDataAdapter personAdapter = new SqlDataAdapter(personQuery, connection);
            personAdapter.Fill(dataset, "People");

            DataColumn parent = dataset.Tables["PersonCategories"].Columns["Id"];
            DataColumn child = dataset.Tables["People"].Columns["PersonCategoryId"];
            DataRelation myRelation = new DataRelation("FK_PersonCategory", parent,child);
            dataset.Relations.Add(myRelation);
            MyData = dataset;
        }
    }
}
