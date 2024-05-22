namespace MTKDotNetCore.WinFormsApp
{
    partial class FrmBlog
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtContent = new TextBox();
            txtTitle = new TextBox();
            txtAuthor = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // txtContent
            // 
            txtContent.Location = new Point(162, 202);
            txtContent.Multiline = true;
            txtContent.Name = "txtContent";
            txtContent.Size = new Size(281, 75);
            txtContent.TabIndex = 1;
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(162, 46);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(281, 23);
            txtTitle.TabIndex = 2;
            // 
            // txtAuthor
            // 
            txtAuthor.Location = new Point(162, 122);
            txtAuthor.Name = "txtAuthor";
            txtAuthor.Size = new Size(281, 23);
            txtAuthor.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(162, 28);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 4;
            label1.Text = "Title: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(162, 104);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 5;
            label2.Text = "Author: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(162, 184);
            label3.Name = "label3";
            label3.Size = new Size(56, 15);
            label3.TabIndex = 6;
            label3.Text = "Content: ";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.ForestGreen;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = SystemColors.Control;
            btnSave.Location = new Point(266, 295);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 7;
            btnSave.Text = "&Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = SystemColors.ControlDarkDark;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = SystemColors.Control;
            btnCancel.Location = new Point(162, 295);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // FrmBlog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtAuthor);
            Controls.Add(txtTitle);
            Controls.Add(txtContent);
            Name = "FrmBlog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Blog";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtContent;
        private TextBox txtTitle;
        private TextBox txtAuthor;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnSave;
        private Button btnCancel;
    }
}
