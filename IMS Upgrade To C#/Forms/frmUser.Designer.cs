namespace IMS_Upgrade_To_C_
{
    partial class frmUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUser));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Users");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Attendance View");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Home", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Student");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Teacher");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Subject");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Class");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Holidays");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Sms");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Master", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Study");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Attendance");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Dashboard");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Manage", new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12,
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Invoice");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Teacher Payments");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Pending Payment");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Transaction", new System.Windows.Forms.TreeNode[] {
            treeNode15,
            treeNode16,
            treeNode17});
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Student Report");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Class Report");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Attendance Report");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Report", new System.Windows.Forms.TreeNode[] {
            treeNode19,
            treeNode20,
            treeNode21});
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Devices");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Features");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("System", new System.Windows.Forms.TreeNode[] {
            treeNode23,
            treeNode24});
            this.User = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDesignation = new System.Windows.Forms.ComboBox();
            this.txtMake = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.dgUser = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.access_btn_save = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.accList = new System.Windows.Forms.ComboBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.User.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUser)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // User
            // 
            this.User.Controls.Add(this.tabPage1);
            this.User.Controls.Add(this.tabPage2);
            this.User.Dock = System.Windows.Forms.DockStyle.Fill;
            this.User.Location = new System.Drawing.Point(0, 0);
            this.User.Name = "User";
            this.User.SelectedIndex = 0;
            this.User.Size = new System.Drawing.Size(405, 463);
            this.User.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage1.BackgroundImage")));
            this.tabPage1.Controls.Add(this.txtPassword);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtDesignation);
            this.tabPage1.Controls.Add(this.txtMake);
            this.tabPage1.Controls.Add(this.txtUsername);
            this.tabPage1.Controls.Add(this.txtID);
            this.tabPage1.Controls.Add(this.dgUser);
            this.tabPage1.Controls.Add(this.btnCancel);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.btnDelete);
            this.tabPage1.Controls.Add(this.btnEdit);
            this.tabPage1.Controls.Add(this.btnNew);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(397, 437);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "User";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.SaddleBrown;
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.Color.White;
            this.txtPassword.Location = new System.Drawing.Point(114, 192);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(268, 24);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Candara", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 25);
            this.label1.TabIndex = 52;
            this.label1.Text = "Password";
            // 
            // txtDesignation
            // 
            this.txtDesignation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtDesignation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtDesignation.FormattingEnabled = true;
            this.txtDesignation.Items.AddRange(new object[] {
            "Employee",
            "Management",
            "Accountant"});
            this.txtDesignation.Location = new System.Drawing.Point(18, 155);
            this.txtDesignation.Name = "txtDesignation";
            this.txtDesignation.Size = new System.Drawing.Size(183, 21);
            this.txtDesignation.TabIndex = 2;
            // 
            // txtMake
            // 
            this.txtMake.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMake.Location = new System.Drawing.Point(218, 155);
            this.txtMake.Name = "txtMake";
            this.txtMake.Size = new System.Drawing.Size(164, 22);
            this.txtMake.TabIndex = 3;
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(142, 99);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(241, 22);
            this.txtUsername.TabIndex = 1;
            // 
            // txtID
            // 
            this.txtID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(18, 99);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(108, 22);
            this.txtID.TabIndex = 0;
            // 
            // dgUser
            // 
            this.dgUser.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.username,
            this.designation,
            this.by});
            this.dgUser.Location = new System.Drawing.Point(15, 262);
            this.dgUser.Name = "dgUser";
            this.dgUser.ReadOnly = true;
            this.dgUser.RowHeadersVisible = false;
            this.dgUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgUser.Size = new System.Drawing.Size(368, 160);
            this.dgUser.TabIndex = 47;
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 43;
            // 
            // username
            // 
            this.username.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.username.DataPropertyName = "username";
            this.username.HeaderText = "User";
            this.username.Name = "username";
            this.username.ReadOnly = true;
            this.username.Width = 54;
            // 
            // designation
            // 
            this.designation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.designation.DataPropertyName = "designation";
            this.designation.HeaderText = "Designation";
            this.designation.Name = "designation";
            this.designation.ReadOnly = true;
            this.designation.Width = 88;
            // 
            // by
            // 
            this.by.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.by.DataPropertyName = "make";
            this.by.HeaderText = "Make";
            this.by.Name = "by";
            this.by.ReadOnly = true;
            this.by.Width = 59;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(315, 229);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 22);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(240, 229);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(68, 22);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(165, 229);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(68, 22);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click_1);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.IndianRed;
            this.btnEdit.Enabled = false;
            this.btnEdit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(94)))), ((int)(((byte)(171)))));
            this.btnEdit.Location = new System.Drawing.Point(90, 229);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(68, 22);
            this.btnEdit.TabIndex = 43;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnNew
            // 
            this.btnNew.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(94)))), ((int)(((byte)(171)))));
            this.btnNew.Location = new System.Drawing.Point(15, 229);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(68, 22);
            this.btnNew.TabIndex = 5;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click_1);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tabPage2.Controls.Add(this.access_btn_save);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.accList);
            this.tabPage2.Controls.Add(this.treeView1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(397, 437);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Access Control";
            // 
            // access_btn_save
            // 
            this.access_btn_save.Location = new System.Drawing.Point(8, 395);
            this.access_btn_save.Name = "access_btn_save";
            this.access_btn_save.Size = new System.Drawing.Size(381, 34);
            this.access_btn_save.TabIndex = 57;
            this.access_btn_save.Text = "Save";
            this.access_btn_save.UseVisualStyleBackColor = true;
            this.access_btn_save.Click += new System.EventHandler(this.access_btn_save_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Candara", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 23);
            this.label3.TabIndex = 56;
            this.label3.Text = "Designation";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // accList
            // 
            this.accList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accList.FormattingEnabled = true;
            this.accList.Items.AddRange(new object[] {
            "Employee",
            "Management"});
            this.accList.Location = new System.Drawing.Point(121, 51);
            this.accList.Name = "accList";
            this.accList.Size = new System.Drawing.Size(268, 23);
            this.accList.TabIndex = 55;
            this.accList.Text = "Employee";
            this.accList.SelectedIndexChanged += new System.EventHandler(this.accList_SelectedIndexChanged);
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView1.CheckBoxes = true;
            this.treeView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.treeView1.Indent = 25;
            this.treeView1.ItemHeight = 25;
            this.treeView1.Location = new System.Drawing.Point(8, 83);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "user";
            treeNode1.Text = "Users";
            treeNode2.Name = "attendanceview";
            treeNode2.Text = "Attendance View";
            treeNode3.Checked = true;
            treeNode3.Name = "_home";
            treeNode3.Text = "Home";
            treeNode4.Name = "student";
            treeNode4.Text = "Student";
            treeNode5.Name = "teacher";
            treeNode5.Text = "Teacher";
            treeNode6.Name = "subject";
            treeNode6.Text = "Subject";
            treeNode7.Name = "class";
            treeNode7.Text = "Class";
            treeNode8.Name = "holidays";
            treeNode8.Text = "Holidays";
            treeNode9.Name = "sms";
            treeNode9.Text = "Sms";
            treeNode10.Checked = true;
            treeNode10.Name = "_master";
            treeNode10.Text = "Master";
            treeNode11.Name = "study";
            treeNode11.Text = "Study";
            treeNode12.Name = "attendance";
            treeNode12.Text = "Attendance";
            treeNode13.Name = "dashboard";
            treeNode13.Text = "Dashboard";
            treeNode14.Checked = true;
            treeNode14.Name = "_manage";
            treeNode14.Text = "Manage";
            treeNode15.Name = "invoice";
            treeNode15.Text = "Invoice";
            treeNode16.Name = "teacherpayments";
            treeNode16.Text = "Teacher Payments";
            treeNode17.Name = "pp";
            treeNode17.Text = "Pending Payment";
            treeNode18.Checked = true;
            treeNode18.Name = "_transaction";
            treeNode18.Text = "Transaction";
            treeNode19.Name = "studentreport";
            treeNode19.Text = "Student Report";
            treeNode20.Name = "classreport";
            treeNode20.Text = "Class Report";
            treeNode21.Name = "attendancereport";
            treeNode21.Text = "Attendance Report";
            treeNode22.Checked = true;
            treeNode22.Name = "_report";
            treeNode22.Text = "Report";
            treeNode23.Name = "devices";
            treeNode23.Text = "Devices";
            treeNode24.Name = "feature";
            treeNode24.Text = "Features";
            treeNode25.Checked = true;
            treeNode25.Name = "_system";
            treeNode25.Text = "System";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode10,
            treeNode14,
            treeNode18,
            treeNode22,
            treeNode25});
            this.treeView1.Size = new System.Drawing.Size(381, 306);
            this.treeView1.TabIndex = 54;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Candara", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(381, 32);
            this.label2.TabIndex = 53;
            this.label2.Text = "Access Chain";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 463);
            this.Controls.Add(this.User);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmUser";
            this.Text = "frmUser";
            this.Load += new System.EventHandler(this.frmUser_Load);
            this.User.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUser)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl User;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox txtDesignation;
        private System.Windows.Forms.TextBox txtMake;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.DataGridView dgUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn username;
        private System.Windows.Forms.DataGridViewTextBoxColumn designation;
        private System.Windows.Forms.DataGridViewTextBoxColumn by;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button access_btn_save;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox accList;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label2;
    }
}