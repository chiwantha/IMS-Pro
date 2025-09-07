namespace IMS_Upgrade_To_C_
{
    partial class frmE_Attendance
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmE_Attendance));
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.rbCheck3 = new System.Windows.Forms.RadioButton();
            this.rbCheck2 = new System.Windows.Forms.RadioButton();
            this.rbCheck1 = new System.Windows.Forms.RadioButton();
            this.cbSelector = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.time = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblShow_Stats = new System.Windows.Forms.Label();
            this.lblShow_Student_Name = new System.Windows.Forms.Label();
            this.lblShow_Class_Name = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Back = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.lblShow_Student_ID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Picture = new System.Windows.Forms.PictureBox();
            this.serialPortRfid = new System.IO.Ports.SerialPort(this.components);
            this.panel2.SuspendLayout();
            this.Back.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.PaleGreen;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(257, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(51, 68);
            this.button2.TabIndex = 6;
            this.button2.Text = "Vm";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Green;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(127, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 68);
            this.button1.TabIndex = 5;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rbCheck3
            // 
            this.rbCheck3.AutoSize = true;
            this.rbCheck3.Location = new System.Drawing.Point(32, 58);
            this.rbCheck3.Name = "rbCheck3";
            this.rbCheck3.Size = new System.Drawing.Size(89, 17);
            this.rbCheck3.TabIndex = 4;
            this.rbCheck3.Text = "CheckPoint 3";
            this.rbCheck3.UseVisualStyleBackColor = true;
            this.rbCheck3.CheckedChanged += new System.EventHandler(this.rbCheck3_CheckedChanged);
            // 
            // rbCheck2
            // 
            this.rbCheck2.AutoSize = true;
            this.rbCheck2.Location = new System.Drawing.Point(32, 41);
            this.rbCheck2.Name = "rbCheck2";
            this.rbCheck2.Size = new System.Drawing.Size(89, 17);
            this.rbCheck2.TabIndex = 3;
            this.rbCheck2.Text = "CheckPoint 2";
            this.rbCheck2.UseVisualStyleBackColor = true;
            this.rbCheck2.CheckedChanged += new System.EventHandler(this.rbCheck2_CheckedChanged);
            // 
            // rbCheck1
            // 
            this.rbCheck1.AutoSize = true;
            this.rbCheck1.Checked = true;
            this.rbCheck1.Location = new System.Drawing.Point(32, 24);
            this.rbCheck1.Name = "rbCheck1";
            this.rbCheck1.Size = new System.Drawing.Size(89, 17);
            this.rbCheck1.TabIndex = 2;
            this.rbCheck1.TabStop = true;
            this.rbCheck1.Text = "CheckPoint 1";
            this.rbCheck1.UseVisualStyleBackColor = true;
            this.rbCheck1.CheckedChanged += new System.EventHandler(this.rbCheck1_CheckedChanged);
            // 
            // cbSelector
            // 
            this.cbSelector.AutoSize = true;
            this.cbSelector.Checked = true;
            this.cbSelector.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSelector.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cbSelector.Location = new System.Drawing.Point(5, 3);
            this.cbSelector.Name = "cbSelector";
            this.cbSelector.Size = new System.Drawing.Size(116, 20);
            this.cbSelector.TabIndex = 1;
            this.cbSelector.Text = "Rfid Selector";
            this.cbSelector.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Lavender;
            this.label1.Location = new System.Drawing.Point(518, 295);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 58);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name :";
            // 
            // txtID
            // 
            this.txtID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(523, 474);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(694, 38);
            this.txtID.TabIndex = 2;
            this.txtID.TextChanged += new System.EventHandler(this.txtID_TextChanged);
            // 
            // time
            // 
            this.time.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.time.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.time.ForeColor = System.Drawing.Color.Black;
            this.time.Location = new System.Drawing.Point(393, 17);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(278, 44);
            this.time.TabIndex = 0;
            this.time.Text = "Time";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Lavender;
            this.label4.Location = new System.Drawing.Point(518, 385);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(184, 58);
            this.label4.TabIndex = 6;
            this.label4.Text = "State :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Lavender;
            this.label3.Location = new System.Drawing.Point(518, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 58);
            this.label3.TabIndex = 5;
            this.label3.Text = "ID :";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblShow_Stats
            // 
            this.lblShow_Stats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblShow_Stats.BackColor = System.Drawing.Color.Transparent;
            this.lblShow_Stats.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShow_Stats.ForeColor = System.Drawing.Color.Cyan;
            this.lblShow_Stats.Location = new System.Drawing.Point(709, 385);
            this.lblShow_Stats.Name = "lblShow_Stats";
            this.lblShow_Stats.Size = new System.Drawing.Size(508, 74);
            this.lblShow_Stats.TabIndex = 11;
            this.lblShow_Stats.Text = "ID :";
            // 
            // lblShow_Student_Name
            // 
            this.lblShow_Student_Name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblShow_Student_Name.BackColor = System.Drawing.Color.Transparent;
            this.lblShow_Student_Name.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShow_Student_Name.ForeColor = System.Drawing.Color.Cyan;
            this.lblShow_Student_Name.Location = new System.Drawing.Point(727, 295);
            this.lblShow_Student_Name.Name = "lblShow_Student_Name";
            this.lblShow_Student_Name.Size = new System.Drawing.Size(490, 74);
            this.lblShow_Student_Name.TabIndex = 10;
            this.lblShow_Student_Name.Text = "ID :";
            // 
            // lblShow_Class_Name
            // 
            this.lblShow_Class_Name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblShow_Class_Name.BackColor = System.Drawing.Color.Transparent;
            this.lblShow_Class_Name.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShow_Class_Name.ForeColor = System.Drawing.Color.Cyan;
            this.lblShow_Class_Name.Location = new System.Drawing.Point(710, 205);
            this.lblShow_Class_Name.Name = "lblShow_Class_Name";
            this.lblShow_Class_Name.Size = new System.Drawing.Size(507, 74);
            this.lblShow_Class_Name.TabIndex = 9;
            this.lblShow_Class_Name.Text = "ID :";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.rbCheck3);
            this.panel2.Controls.Add(this.rbCheck2);
            this.panel2.Controls.Add(this.rbCheck1);
            this.panel2.Controls.Add(this.cbSelector);
            this.panel2.Controls.Add(this.time);
            this.panel2.Location = new System.Drawing.Point(523, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(694, 81);
            this.panel2.TabIndex = 7;
            // 
            // Back
            // 
            this.Back.BackColor = System.Drawing.Color.Green;
            this.Back.Controls.Add(this.panel2);
            this.Back.Controls.Add(this.panel1);
            this.Back.Controls.Add(this.lblShow_Stats);
            this.Back.Controls.Add(this.lblShow_Student_Name);
            this.Back.Controls.Add(this.lblShow_Class_Name);
            this.Back.Controls.Add(this.lblShow_Student_ID);
            this.Back.Controls.Add(this.label4);
            this.Back.Controls.Add(this.label3);
            this.Back.Controls.Add(this.label2);
            this.Back.Controls.Add(this.label1);
            this.Back.Controls.Add(this.txtID);
            this.Back.Controls.Add(this.Picture);
            this.Back.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Back.Location = new System.Drawing.Point(0, 0);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(1229, 748);
            this.Back.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.DarkGreen;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 99.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(12, 526);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1205, 210);
            this.panel1.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 80.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.LightGreen;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1205, 210);
            this.label5.TabIndex = 1;
            this.label5.Text = "WELCOME";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblShow_Student_ID
            // 
            this.lblShow_Student_ID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblShow_Student_ID.BackColor = System.Drawing.Color.Transparent;
            this.lblShow_Student_ID.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShow_Student_ID.ForeColor = System.Drawing.Color.Cyan;
            this.lblShow_Student_ID.Location = new System.Drawing.Point(632, 110);
            this.lblShow_Student_ID.Name = "lblShow_Student_ID";
            this.lblShow_Student_ID.Size = new System.Drawing.Size(585, 79);
            this.lblShow_Student_ID.TabIndex = 8;
            this.lblShow_Student_ID.Text = "ID :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Lavender;
            this.label2.Location = new System.Drawing.Point(518, 205);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 58);
            this.label2.TabIndex = 4;
            this.label2.Text = "Class :";
            // 
            // Picture
            // 
            this.Picture.BackColor = System.Drawing.Color.White;
            this.Picture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Picture.BackgroundImage")));
            this.Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Picture.Location = new System.Drawing.Point(12, 12);
            this.Picture.Name = "Picture";
            this.Picture.Size = new System.Drawing.Size(500, 500);
            this.Picture.TabIndex = 0;
            this.Picture.TabStop = false;
            // 
            // serialPortRfid
            // 
            this.serialPortRfid.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortRfid_DataReceived);
            // 
            // frmE_Attendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1229, 748);
            this.Controls.Add(this.Back);
            this.Name = "frmE_Attendance";
            this.Text = "frmE_Attendance";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmE_Attendance_FormClosing);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.Back.ResumeLayout(false);
            this.Back.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox Picture;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rbCheck3;
        private System.Windows.Forms.RadioButton rbCheck2;
        private System.Windows.Forms.RadioButton rbCheck1;
        private System.Windows.Forms.CheckBox cbSelector;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label time;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblShow_Stats;
        private System.Windows.Forms.Label lblShow_Student_Name;
        private System.Windows.Forms.Label lblShow_Class_Name;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel Back;
        private System.Windows.Forms.Label lblShow_Student_ID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.IO.Ports.SerialPort serialPortRfid;
    }
}