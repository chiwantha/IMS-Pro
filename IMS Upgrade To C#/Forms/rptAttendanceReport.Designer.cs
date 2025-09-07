namespace IMS_Upgrade_To_C_
{
    partial class rptAttendanceReport
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
            this.txtExtra = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtyear = new System.Windows.Forms.DateTimePicker();
            this.txtMonth = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnBuild = new System.Windows.Forms.Button();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtClass = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStudentID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStudent = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbClass = new System.Windows.Forms.RadioButton();
            this.rbStudent = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // txtExtra
            // 
            this.txtExtra.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtExtra.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtExtra.FormattingEnabled = true;
            this.txtExtra.Location = new System.Drawing.Point(152, 197);
            this.txtExtra.Name = "txtExtra";
            this.txtExtra.Size = new System.Drawing.Size(218, 21);
            this.txtExtra.TabIndex = 42;
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(30, 197);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 21);
            this.label9.TabIndex = 41;
            this.label9.Text = "Extra Class";
            // 
            // txtyear
            // 
            this.txtyear.CustomFormat = "yyyy";
            this.txtyear.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtyear.Location = new System.Drawing.Point(303, 246);
            this.txtyear.Name = "txtyear";
            this.txtyear.ShowUpDown = true;
            this.txtyear.Size = new System.Drawing.Size(67, 20);
            this.txtyear.TabIndex = 40;
            // 
            // txtMonth
            // 
            this.txtMonth.FormattingEnabled = true;
            this.txtMonth.Items.AddRange(new object[] {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.txtMonth.Location = new System.Drawing.Point(152, 245);
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(145, 21);
            this.txtMonth.TabIndex = 39;
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(30, 245);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 21);
            this.label8.TabIndex = 38;
            this.label8.Text = "Month";
            // 
            // btnBuild
            // 
            this.btnBuild.BackColor = System.Drawing.Color.LightGreen;
            this.btnBuild.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuild.Location = new System.Drawing.Point(395, 251);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(217, 65);
            this.btnBuild.TabIndex = 37;
            this.btnBuild.Text = "Build";
            this.btnBuild.UseVisualStyleBackColor = false;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // dtpTo
            // 
            this.dtpTo.Location = new System.Drawing.Point(153, 300);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(217, 20);
            this.dtpTo.TabIndex = 36;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(153, 272);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(217, 20);
            this.dtpFrom.TabIndex = 35;
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(30, 272);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 21);
            this.label6.TabIndex = 34;
            this.label6.Text = "Date ( from )";
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(30, 299);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 21);
            this.label7.TabIndex = 33;
            this.label7.Text = "Date ( to )";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.LightGreen;
            this.label5.Location = new System.Drawing.Point(30, 223);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(340, 21);
            this.label5.TabIndex = 32;
            this.label5.Text = "---------------------------------------------------------------------------------" +
    "--";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.LightGreen;
            this.label4.Location = new System.Drawing.Point(30, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(340, 21);
            this.label4.TabIndex = 31;
            this.label4.Text = "---------------------------------------------------------------------------------" +
    "--";
            // 
            // txtClass
            // 
            this.txtClass.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtClass.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtClass.FormattingEnabled = true;
            this.txtClass.Location = new System.Drawing.Point(152, 171);
            this.txtClass.Name = "txtClass";
            this.txtClass.Size = new System.Drawing.Size(218, 21);
            this.txtClass.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 21);
            this.label3.TabIndex = 29;
            this.label3.Text = "Class";
            // 
            // txtStudentID
            // 
            this.txtStudentID.Location = new System.Drawing.Point(153, 95);
            this.txtStudentID.Name = "txtStudentID";
            this.txtStudentID.Size = new System.Drawing.Size(217, 20);
            this.txtStudentID.TabIndex = 28;
            this.txtStudentID.TextChanged += new System.EventHandler(this.txtStudentID_TextChanged);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 21);
            this.label2.TabIndex = 27;
            this.label2.Text = "Student Card";
            // 
            // txtStudent
            // 
            this.txtStudent.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtStudent.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtStudent.FormattingEnabled = true;
            this.txtStudent.Location = new System.Drawing.Point(152, 122);
            this.txtStudent.Name = "txtStudent";
            this.txtStudent.Size = new System.Drawing.Size(218, 21);
            this.txtStudent.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 21);
            this.label1.TabIndex = 25;
            this.label1.Text = "Student Name ";
            // 
            // rbClass
            // 
            this.rbClass.AutoSize = true;
            this.rbClass.BackColor = System.Drawing.Color.LightGreen;
            this.rbClass.Checked = true;
            this.rbClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbClass.Location = new System.Drawing.Point(275, 59);
            this.rbClass.Name = "rbClass";
            this.rbClass.Size = new System.Drawing.Size(95, 24);
            this.rbClass.TabIndex = 24;
            this.rbClass.TabStop = true;
            this.rbClass.Text = "Class Att.";
            this.rbClass.UseVisualStyleBackColor = false;
            this.rbClass.CheckedChanged += new System.EventHandler(this.rbClass_CheckedChanged);
            // 
            // rbStudent
            // 
            this.rbStudent.AutoSize = true;
            this.rbStudent.BackColor = System.Drawing.Color.LightGreen;
            this.rbStudent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbStudent.Location = new System.Drawing.Point(149, 59);
            this.rbStudent.Name = "rbStudent";
            this.rbStudent.Size = new System.Drawing.Size(113, 24);
            this.rbStudent.TabIndex = 23;
            this.rbStudent.Text = "Student Att.";
            this.rbStudent.UseVisualStyleBackColor = false;
            this.rbStudent.CheckedChanged += new System.EventHandler(this.rbStudent_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.BackColor = System.Drawing.Color.LightGreen;
            this.rbAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAll.Location = new System.Drawing.Point(30, 59);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(105, 24);
            this.rbAll.TabIndex = 22;
            this.rbAll.Text = "All Capture";
            this.rbAll.UseVisualStyleBackColor = false;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // rptAttendanceReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IMS_Upgrade_To_C_.Properties.Resources.Artboard_1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(670, 340);
            this.Controls.Add(this.txtExtra);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtyear);
            this.Controls.Add(this.txtMonth);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtClass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtStudentID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtStudent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbClass);
            this.Controls.Add(this.rbStudent);
            this.Controls.Add(this.rbAll);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "rptAttendanceReport";
            this.Text = "rptAttendanceReport";
            this.Load += new System.EventHandler(this.rptAttendanceReport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox txtExtra;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker txtyear;
        private System.Windows.Forms.ComboBox txtMonth;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox txtClass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStudentID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox txtStudent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbClass;
        private System.Windows.Forms.RadioButton rbStudent;
        private System.Windows.Forms.RadioButton rbAll;
    }
}