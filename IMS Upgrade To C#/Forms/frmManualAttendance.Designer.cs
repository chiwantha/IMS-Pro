namespace IMS_Upgrade_To_C_
{
    partial class frmManualAttendance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManualAttendance));
            this.lblShow_Student_Name = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.time = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtyear = new System.Windows.Forms.DateTimePicker();
            this.lblShow_Student_ID = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMonth = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblShow_Class_Name = new System.Windows.Forms.Label();
            this.txtStudentID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Back = new System.Windows.Forms.Panel();
            this.lblShow_Stats = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtStudent = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Picture = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Back.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblShow_Student_Name
            // 
            this.lblShow_Student_Name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblShow_Student_Name.BackColor = System.Drawing.Color.Transparent;
            this.lblShow_Student_Name.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShow_Student_Name.ForeColor = System.Drawing.Color.Cyan;
            this.lblShow_Student_Name.Location = new System.Drawing.Point(510, 242);
            this.lblShow_Student_Name.Name = "lblShow_Student_Name";
            this.lblShow_Student_Name.Size = new System.Drawing.Size(519, 39);
            this.lblShow_Student_Name.TabIndex = 18;
            this.lblShow_Student_Name.Text = "ID :";
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(48, 126);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(304, 22);
            this.label11.TabIndex = 34;
            this.label11.Text = "Please Double Check ! , * can\'t be undo *";
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(12, 85);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(502, 74);
            this.label10.TabIndex = 33;
            this.label10.Text = "Warning";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightGreen;
            this.button1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(520, 84);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(509, 76);
            this.button1.TabIndex = 32;
            this.button1.Text = "SUBMIT";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // time
            // 
            this.time.CustomFormat = "HH:mm:ss tt";
            this.time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.time.Location = new System.Drawing.Point(735, 50);
            this.time.Name = "time";
            this.time.ShowUpDown = true;
            this.time.Size = new System.Drawing.Size(294, 20);
            this.time.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(581, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 21);
            this.label4.TabIndex = 30;
            this.label4.Text = "Time Configuration";
            // 
            // txtyear
            // 
            this.txtyear.CustomFormat = "yyyy";
            this.txtyear.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtyear.Location = new System.Drawing.Point(298, 49);
            this.txtyear.Name = "txtyear";
            this.txtyear.ShowUpDown = true;
            this.txtyear.Size = new System.Drawing.Size(67, 20);
            this.txtyear.TabIndex = 29;
            // 
            // lblShow_Student_ID
            // 
            this.lblShow_Student_ID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblShow_Student_ID.BackColor = System.Drawing.Color.Transparent;
            this.lblShow_Student_ID.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShow_Student_ID.ForeColor = System.Drawing.Color.Cyan;
            this.lblShow_Student_ID.Location = new System.Drawing.Point(455, 107);
            this.lblShow_Student_ID.Name = "lblShow_Student_ID";
            this.lblShow_Student_ID.Size = new System.Drawing.Size(574, 39);
            this.lblShow_Student_ID.TabIndex = 16;
            this.lblShow_Student_ID.Text = "ID :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Lavender;
            this.label5.Location = new System.Drawing.Point(355, 308);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 39);
            this.label5.TabIndex = 15;
            this.label5.Text = "State :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Lavender;
            this.label6.Location = new System.Drawing.Point(355, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 39);
            this.label6.TabIndex = 14;
            this.label6.Text = "ID :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Lavender;
            this.label7.Location = new System.Drawing.Point(355, 174);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 39);
            this.label7.TabIndex = 13;
            this.label7.Text = "Class :";
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
            this.txtMonth.Location = new System.Drawing.Point(166, 48);
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(126, 21);
            this.txtMonth.TabIndex = 28;
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(148, 21);
            this.label8.TabIndex = 27;
            this.label8.Text = "Date Configuration";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "yyyy-MM-dd";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(371, 49);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(204, 20);
            this.dtpDate.TabIndex = 26;
            // 
            // lblShow_Class_Name
            // 
            this.lblShow_Class_Name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblShow_Class_Name.BackColor = System.Drawing.Color.Transparent;
            this.lblShow_Class_Name.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShow_Class_Name.ForeColor = System.Drawing.Color.Cyan;
            this.lblShow_Class_Name.Location = new System.Drawing.Point(498, 174);
            this.lblShow_Class_Name.Name = "lblShow_Class_Name";
            this.lblShow_Class_Name.Size = new System.Drawing.Size(531, 39);
            this.lblShow_Class_Name.TabIndex = 17;
            this.lblShow_Class_Name.Text = "ID :";
            // 
            // txtStudentID
            // 
            this.txtStudentID.Location = new System.Drawing.Point(166, 14);
            this.txtStudentID.Name = "txtStudentID";
            this.txtStudentID.Size = new System.Drawing.Size(186, 20);
            this.txtStudentID.TabIndex = 23;
            this.txtStudentID.TextChanged += new System.EventHandler(this.txtStudentID_TextChanged);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 21);
            this.label2.TabIndex = 22;
            this.label2.Text = "Student Card or ID";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.ForestGreen;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Location = new System.Drawing.Point(362, 11);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(667, 82);
            this.panel2.TabIndex = 20;
            // 
            // Back
            // 
            this.Back.BackColor = System.Drawing.Color.Green;
            this.Back.Controls.Add(this.panel2);
            this.Back.Controls.Add(this.lblShow_Stats);
            this.Back.Controls.Add(this.lblShow_Student_Name);
            this.Back.Controls.Add(this.lblShow_Class_Name);
            this.Back.Controls.Add(this.lblShow_Student_ID);
            this.Back.Controls.Add(this.label5);
            this.Back.Controls.Add(this.label6);
            this.Back.Controls.Add(this.label7);
            this.Back.Controls.Add(this.label9);
            this.Back.Controls.Add(this.Picture);
            this.Back.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Back.Location = new System.Drawing.Point(0, 173);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(1041, 363);
            this.Back.TabIndex = 3;
            // 
            // lblShow_Stats
            // 
            this.lblShow_Stats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblShow_Stats.BackColor = System.Drawing.Color.Transparent;
            this.lblShow_Stats.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShow_Stats.ForeColor = System.Drawing.Color.Cyan;
            this.lblShow_Stats.Location = new System.Drawing.Point(500, 308);
            this.lblShow_Stats.Name = "lblShow_Stats";
            this.lblShow_Stats.Size = new System.Drawing.Size(529, 39);
            this.lblShow_Stats.TabIndex = 19;
            this.lblShow_Stats.Text = "ID :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Lavender;
            this.label9.Location = new System.Drawing.Point(355, 242);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 39);
            this.label9.TabIndex = 12;
            this.label9.Text = "Name :";
            // 
            // txtStudent
            // 
            this.txtStudent.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtStudent.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtStudent.FormattingEnabled = true;
            this.txtStudent.Location = new System.Drawing.Point(480, 13);
            this.txtStudent.Name = "txtStudent";
            this.txtStudent.Size = new System.Drawing.Size(549, 21);
            this.txtStudent.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(358, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 21);
            this.label1.TabIndex = 20;
            this.label1.Text = "Student Name ";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.time);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtyear);
            this.panel1.Controls.Add(this.txtMonth);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.dtpDate);
            this.panel1.Controls.Add(this.txtStudentID);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtStudent);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1041, 173);
            this.panel1.TabIndex = 2;
            // 
            // Picture
            // 
            this.Picture.BackColor = System.Drawing.SystemColors.Control;
            this.Picture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Picture.BackgroundImage")));
            this.Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Picture.Location = new System.Drawing.Point(12, 11);
            this.Picture.Name = "Picture";
            this.Picture.Size = new System.Drawing.Size(339, 339);
            this.Picture.TabIndex = 0;
            this.Picture.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(442, 91);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 62);
            this.pictureBox1.TabIndex = 35;
            this.pictureBox1.TabStop = false;
            // 
            // frmManualAttendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 536);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManualAttendance";
            this.Text = "frmManualAttendance";
            this.Load += new System.EventHandler(this.pgManualAttendance_Load);
            this.Back.ResumeLayout(false);
            this.Back.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblShow_Student_Name;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker time;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker txtyear;
        private System.Windows.Forms.Label lblShow_Student_ID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox txtMonth;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblShow_Class_Name;
        private System.Windows.Forms.TextBox txtStudentID;
        private System.Windows.Forms.PictureBox Picture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel Back;
        private System.Windows.Forms.Label lblShow_Stats;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox txtStudent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
    }
}