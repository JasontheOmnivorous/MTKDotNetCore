using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTKDotNetCore.ConsoleApp
{
    internal class AdoDotNetExamples
    {
        // explicitly define properties right after creating instance, so that we don't need to add them again and again
        // use underscore when defining global variables
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-ROVEV23",
            InitialCatalog = "MTKDotNetCore",
            UserID = "sa",
            Password = "sa@123",
        };

        // Ado.net Read
        public void Read()
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            connection.Open();
            Console.WriteLine("Connection opened!");

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
        }


    }
}
