using MTKDotNetCore.Shared;
using System.Configuration;

namespace MTKDotNetCore.WinFormsAppSqlInjection
{
    public partial class Form1 : Form
    {
        private readonly DapperService _dapperService;

        public Form1()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // sql injection usage => random bullshits' or 1=1 +' in an input field
            // we can inject malefic codes in the place of a parameter and attack our DB
            // that's why using params is the best practice
            string query = $"select * from Tbl_User where Email = '{txtEmail.Text.Trim()}' and Password = '{txtPassword.Text.Trim()}'";
            var model = _dapperService.QueryFirstOrDefault<UserModel>(query);

            if (model is null)
            {
                MessageBox.Show("User doesn't exist!");
                return;
            }

            MessageBox.Show("Username is: " + model.Name);
        }
    }

    public class UserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string IsAdmin { get; set; }
    }
}
