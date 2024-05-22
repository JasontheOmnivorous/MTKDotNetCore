using MTKDotNetCore.Shared;
using MTKDotNetCore.WinFormsApp.Queries;

namespace MTKDotNetCore.WinFormsApp
{
    public partial class FrmBlog : Form
    {
        private readonly DapperService _dapperService;

        public FrmBlog()
        {
            InitializeComponent();

            // we should always add up more code only after InitializeComponent
            // since it controls mostly of the basic form
            _dapperService = new DapperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BlogModel blog = new BlogModel
                {
                    BlogTitle = txtTitle.Text.Trim(),
                    BlogAuthor = txtAuthor.Text.Trim(),
                    BlogContent = txtContent.Text.Trim(),
                };

                int result = _dapperService.Execute(BlogQuery.BlogCreate, blog);
                string message = result > 0 ? "Saving successful!" : "Saving failed.";
                var messageBoxIcon = result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error;
                // text, caption, buttons, icon
                MessageBox.Show(message, "Blog", MessageBoxButtons.OK, messageBoxIcon);

                // if error, leave the texts
                if (result > 0) ClearControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        private void ClearControl ()
        {
            txtTitle.Clear();
            txtAuthor.Clear();
            txtContent.Clear();

            txtTitle.Focus();
        }
    }
}
