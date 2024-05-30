using MTKDotNetCore.Shared;
using MTKDotNetCore.WinFormsApp.Models;
using MTKDotNetCore.WinFormsApp.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MTKDotNetCore.WinFormsApp
{
    public partial class FrmBlogList : Form
    {
        private readonly DapperService _dapperService;

        public FrmBlogList()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            dgvData.AutoGenerateColumns = false; // don't auto generate columns for unspecified columns in the form
        }

        private void FrmBlogList_Load(object sender, EventArgs e)
        {
            BlogList();
        }

        private void BlogList ()
        {
            var lst = _dapperService.Query<BlogModel>(BlogQuery.BlogList);
            dgvData.DataSource = lst;
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int columnIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;

            if (rowIndex < 0) return;

            #region If Case

            int blogId = Convert.ToInt32(dgvData.Rows[rowIndex].Cells["colId"].Value.ToString());

            if (columnIndex == (int) EnumFormControlType.Edit)
            {
                FrmBlog frmBlog = new FrmBlog(blogId);
                frmBlog.ShowDialog();

                BlogList(); // refreshes the data grid after update
            }
            else if (columnIndex == (int) EnumFormControlType.Delete)
            {
                var dialogResult = MessageBox.Show("Are you sure you want to delete this row?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult != DialogResult.Yes) return;

                DeleteBlog(blogId);

                BlogList();
            }

            #endregion

            #region Switch Case

            int index = e.ColumnIndex;

            EnumFormControlType enumFormControlType = (EnumFormControlType)index;

            switch (enumFormControlType)
            {
                case EnumFormControlType.Edit:
                    FrmBlog frmBlog = new FrmBlog(blogId);
                    frmBlog.ShowDialog();

                    BlogList();
                    break;
                case EnumFormControlType.Delete:
                    var dialogResult = MessageBox.Show("Are you sure you want to delete this row?". "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult != DialogResult.Yes) break;

                    DeleteBlog(blogId);

                    BlogList();
                    break;
                case EnumFormControlType.None:
                default:
                    MessageBox.Show("Invalid Case!");
                    break;
            }

            #endregion

        }

        private void DeleteBlog (int id)
        {
            string query = "DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, new { BlogId = id });

            string message = result > 0 ? "Delete successful!" : "Delete failed.";

            MessageBox.Show(message);
        }
    }
}
