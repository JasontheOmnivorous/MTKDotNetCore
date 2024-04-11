using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
stringBuilder.DataSource = "DESKTOP-ROVEV23";
stringBuilder.InitialCatalog = "MTKDotNetCore";
stringBuilder.UserID = "sa";
stringBuilder.Password = "sa@123";    
SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);

connection.Open();
Console.WriteLine("Connection opened!");

// Ado.net Read
string query = "select * from Tbl_Blog";
// make a command out of sql query string
SqlCommand cmd = new SqlCommand(query, connection);
// make a new sql adapter and run the command on it
SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
// make data table to show the result returned from running the command
DataTable dt = new DataTable();
// fill the result in the data table
sqlDataAdapter.Fill(dt);

connection.Close();
Console.WriteLine("Connection closed!");

// loop through each data row and visualize on the console
foreach (DataRow dr in dt.Rows)
{
    Console.WriteLine("Blog Id => " + dr["BlogId"]);
    Console.WriteLine("Blog Title => " + dr["BlogTitle"]);
    Console.WriteLine("Blog Author => " + dr["BlogAuthor"]);
    Console.WriteLine("Blog Content => " + dr["BlogContent"]);
    Console.WriteLine("------------------------------------");
}

Console.ReadKey();