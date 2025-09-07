namespace IMS_Upgrade_To_C_
{
    partial class rptClassReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptClassReport));
            this.rbClassIncome = new System.Windows.Forms.RadioButton();
            this.txtBatch = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGrade = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMonth = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtClass = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rbClassStudentList = new System.Windows.Forms.RadioButton();
            this.rbClassDetails = new System.Windows.Forms.RadioButton();
            this.btnBuild = new System.Windows.Forms.Button();
            this.rbClassPy = new System.Windows.Forms.RadioButton();
            this.rbClassPaidList = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // rbClassIncome
            // 
            this.rbClassIncome.AutoSize = true;
            this.rbClassIncome.BackColor = System.Drawing.Color.LightGreen;
            this.rbClassIncome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbClassIncome.Location = new System.Drawing.Point(264, 66);
            this.rbClassIncome.Name = "rbClassIncome";
            this.rbClassIncome.Size = new System.Drawing.Size(123, 24);
            this.rbClassIncome.TabIndex = 40;
            this.rbClassIncome.Text = "Class Income";
            this.rbClassIncome.UseVisualStyleBackColor = false;
            this.rbClassIncome.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // txtBatch
            // 
            this.txtBatch.Location = new System.Drawing.Point(142, 247);
            this.txtBatch.Name = "txtBatch";
            this.txtBatch.Size = new System.Drawing.Size(245, 20);
            this.txtBatch.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(20, 246);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 21);
            this.label4.TabIndex = 38;
            this.label4.Text = "Batch";
            // 
            // txtGrade
            // 
            this.txtGrade.Location = new System.Drawing.Point(142, 210);
            this.txtGrade.Name = "txtGrade";
            this.txtGrade.Size = new System.Drawing.Size(245, 20);
            this.txtGrade.TabIndex = 37;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 21);
            this.label2.TabIndex = 36;
            this.label2.Text = "Grade";
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
            this.txtMonth.Location = new System.Drawing.Point(142, 172);
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(245, 21);
            this.txtMonth.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 21);
            this.label1.TabIndex = 34;
            this.label1.Text = "Month";
            // 
            // txtClass
            // 
            this.txtClass.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtClass.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtClass.FormattingEnabled = true;
            this.txtClass.Location = new System.Drawing.Point(142, 136);
            this.txtClass.Name = "txtClass";
            this.txtClass.Size = new System.Drawing.Size(245, 21);
            this.txtClass.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 21);
            this.label3.TabIndex = 32;
            this.label3.Text = "Class";
            // 
            // rbClassStudentList
            // 
            this.rbClassStudentList.AutoSize = true;
            this.rbClassStudentList.BackColor = System.Drawing.Color.LightGreen;
            this.rbClassStudentList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbClassStudentList.Location = new System.Drawing.Point(145, 66);
            this.rbClassStudentList.Name = "rbClassStudentList";
            this.rbClassStudentList.Size = new System.Drawing.Size(113, 24);
            this.rbClassStudentList.TabIndex = 31;
            this.rbClassStudentList.Text = "Student List";
            this.rbClassStudentList.UseVisualStyleBackColor = false;
            this.rbClassStudentList.CheckedChanged += new System.EventHandler(this.rbClassStudentList_CheckedChanged);
            // 
            // rbClassDetails
            // 
            this.rbClassDetails.AutoSize = true;
            this.rbClassDetails.BackColor = System.Drawing.Color.LightGreen;
            this.rbClassDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbClassDetails.Location = new System.Drawing.Point(20, 66);
            this.rbClassDetails.Name = "rbClassDetails";
            this.rbClassDetails.Size = new System.Drawing.Size(119, 24);
            this.rbClassDetails.TabIndex = 30;
            this.rbClassDetails.Text = "Class Details";
            this.rbClassDetails.UseVisualStyleBackColor = false;
            this.rbClassDetails.CheckedChanged += new System.EventHandler(this.rbClassDetails_CheckedChanged);
            // 
            // btnBuild
            // 
            this.btnBuild.BackColor = System.Drawing.Color.LightGreen;
            this.btnBuild.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuild.Location = new System.Drawing.Point(406, 202);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(217, 65);
            this.btnBuild.TabIndex = 29;
            this.btnBuild.Text = "Build";
            this.btnBuild.UseVisualStyleBackColor = false;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // rbClassPy
            // 
            this.rbClassPy.AutoSize = true;
            this.rbClassPy.BackColor = System.Drawing.Color.LightGreen;
            this.rbClassPy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbClassPy.Location = new System.Drawing.Point(20, 96);
            this.rbClassPy.Name = "rbClassPy";
            this.rbClassPy.Size = new System.Drawing.Size(229, 24);
            this.rbClassPy.TabIndex = 41;
            this.rbClassPy.Text = "Class Payments by Students";
            this.rbClassPy.UseVisualStyleBackColor = false;
            this.rbClassPy.CheckedChanged += new System.EventHandler(this.rbClassPy_CheckedChanged);
            // 
            // rbClassPaidList
            // 
            this.rbClassPaidList.AutoSize = true;
            this.rbClassPaidList.BackColor = System.Drawing.Color.LightGreen;
            this.rbClassPaidList.Checked = true;
            this.rbClassPaidList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbClassPaidList.Location = new System.Drawing.Point(255, 96);
            this.rbClassPaidList.Name = "rbClassPaidList";
            this.rbClassPaidList.Size = new System.Drawing.Size(87, 24);
            this.rbClassPaidList.TabIndex = 42;
            this.rbClassPaidList.TabStop = true;
            this.rbClassPaidList.Text = "Paid List";
            this.rbClassPaidList.UseVisualStyleBackColor = false;
            this.rbClassPaidList.CheckedChanged += new System.EventHandler(this.rbClassPaidList_CheckedChanged);
            // 
            // rptClassReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(670, 304);
            this.Controls.Add(this.rbClassPaidList);
            this.Controls.Add(this.rbClassPy);
            this.Controls.Add(this.rbClassIncome);
            this.Controls.Add(this.txtBatch);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtGrade);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMonth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtClass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rbClassStudentList);
            this.Controls.Add(this.rbClassDetails);
            this.Controls.Add(this.btnBuild);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "rptClassReport";
            this.Text = "rptClassReport";
            this.Load += new System.EventHandler(this.rptClassReportList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbClassIncome;
        private System.Windows.Forms.TextBox txtBatch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGrade;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox txtMonth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox txtClass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbClassStudentList;
        private System.Windows.Forms.RadioButton rbClassDetails;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.RadioButton rbClassPy;
        private System.Windows.Forms.RadioButton rbClassPaidList;
    }
}