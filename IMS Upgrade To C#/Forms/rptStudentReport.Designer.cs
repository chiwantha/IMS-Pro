namespace IMS_Upgrade_To_C_
{
    partial class rptStudentReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptStudentReport));
            this.rbID = new System.Windows.Forms.RadioButton();
            this.btnBuild = new System.Windows.Forms.Button();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStudentID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStudent = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbStudentClassList = new System.Windows.Forms.RadioButton();
            this.rbStudentPayments = new System.Windows.Forms.RadioButton();
            this.rbStudentDetails = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // rbID
            // 
            this.rbID.AutoSize = true;
            this.rbID.BackColor = System.Drawing.Color.LightGreen;
            this.rbID.Enabled = false;
            this.rbID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbID.Location = new System.Drawing.Point(332, 101);
            this.rbID.Name = "rbID";
            this.rbID.Size = new System.Drawing.Size(44, 24);
            this.rbID.TabIndex = 47;
            this.rbID.Text = "ID";
            this.rbID.UseVisualStyleBackColor = false;
            this.rbID.CheckedChanged += new System.EventHandler(this.rbID_CheckedChanged);
            // 
            // btnBuild
            // 
            this.btnBuild.BackColor = System.Drawing.Color.LightGreen;
            this.btnBuild.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuild.Location = new System.Drawing.Point(405, 220);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(217, 65);
            this.btnBuild.TabIndex = 46;
            this.btnBuild.Text = "Build";
            this.btnBuild.UseVisualStyleBackColor = false;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // dtpTo
            // 
            this.dtpTo.Enabled = false;
            this.dtpTo.Location = new System.Drawing.Point(160, 243);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(217, 20);
            this.dtpTo.TabIndex = 45;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Enabled = false;
            this.dtpFrom.Location = new System.Drawing.Point(160, 215);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(217, 20);
            this.dtpFrom.TabIndex = 44;
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(37, 215);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 21);
            this.label6.TabIndex = 43;
            this.label6.Text = "Date ( from )";
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(37, 242);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 21);
            this.label7.TabIndex = 42;
            this.label7.Text = "Date ( to )";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.LightGreen;
            this.label5.Location = new System.Drawing.Point(37, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(340, 21);
            this.label5.TabIndex = 41;
            this.label5.Text = "---------------------------------------------------------------------------------" +
    "--";
            // 
            // txtStudentID
            // 
            this.txtStudentID.Location = new System.Drawing.Point(160, 140);
            this.txtStudentID.Name = "txtStudentID";
            this.txtStudentID.Size = new System.Drawing.Size(217, 20);
            this.txtStudentID.TabIndex = 40;
            this.txtStudentID.TextChanged += new System.EventHandler(this.txtStudentID_TextChanged);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(37, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 21);
            this.label2.TabIndex = 39;
            this.label2.Text = "Student Card";
            // 
            // txtStudent
            // 
            this.txtStudent.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtStudent.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtStudent.FormattingEnabled = true;
            this.txtStudent.Location = new System.Drawing.Point(159, 167);
            this.txtStudent.Name = "txtStudent";
            this.txtStudent.Size = new System.Drawing.Size(218, 21);
            this.txtStudent.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 21);
            this.label1.TabIndex = 37;
            this.label1.Text = "Student Name ";
            // 
            // rbStudentClassList
            // 
            this.rbStudentClassList.AutoSize = true;
            this.rbStudentClassList.BackColor = System.Drawing.Color.LightGreen;
            this.rbStudentClassList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbStudentClassList.Location = new System.Drawing.Point(228, 101);
            this.rbStudentClassList.Name = "rbStudentClassList";
            this.rbStudentClassList.Size = new System.Drawing.Size(95, 24);
            this.rbStudentClassList.TabIndex = 36;
            this.rbStudentClassList.Text = "Class List";
            this.rbStudentClassList.UseVisualStyleBackColor = false;
            this.rbStudentClassList.CheckedChanged += new System.EventHandler(this.rbStudentClassList_CheckedChanged);
            // 
            // rbStudentPayments
            // 
            this.rbStudentPayments.AutoSize = true;
            this.rbStudentPayments.BackColor = System.Drawing.Color.LightGreen;
            this.rbStudentPayments.Checked = true;
            this.rbStudentPayments.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbStudentPayments.Location = new System.Drawing.Point(122, 101);
            this.rbStudentPayments.Name = "rbStudentPayments";
            this.rbStudentPayments.Size = new System.Drawing.Size(97, 24);
            this.rbStudentPayments.TabIndex = 35;
            this.rbStudentPayments.TabStop = true;
            this.rbStudentPayments.Text = "Payments";
            this.rbStudentPayments.UseVisualStyleBackColor = false;
            this.rbStudentPayments.CheckedChanged += new System.EventHandler(this.rbStudentPayments_CheckedChanged);
            // 
            // rbStudentDetails
            // 
            this.rbStudentDetails.AutoSize = true;
            this.rbStudentDetails.BackColor = System.Drawing.Color.LightGreen;
            this.rbStudentDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbStudentDetails.Location = new System.Drawing.Point(37, 101);
            this.rbStudentDetails.Name = "rbStudentDetails";
            this.rbStudentDetails.Size = new System.Drawing.Size(76, 24);
            this.rbStudentDetails.TabIndex = 34;
            this.rbStudentDetails.Text = "Details";
            this.rbStudentDetails.UseVisualStyleBackColor = false;
            this.rbStudentDetails.CheckedChanged += new System.EventHandler(this.rbStudentDetails_CheckedChanged);
            // 
            // rptStudentReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(670, 340);
            this.Controls.Add(this.rbID);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtStudentID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtStudent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbStudentClassList);
            this.Controls.Add(this.rbStudentPayments);
            this.Controls.Add(this.rbStudentDetails);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "rptStudentReport";
            this.Text = "rptStudentReport";
            this.Load += new System.EventHandler(this.rptStudentReportList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbID;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStudentID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox txtStudent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbStudentClassList;
        private System.Windows.Forms.RadioButton rbStudentPayments;
        private System.Windows.Forms.RadioButton rbStudentDetails;
    }
}