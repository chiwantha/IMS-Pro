namespace IMS_Upgrade_To_C_
{
    partial class frmDevices
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
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.epcid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtName = new System.Windows.Forms.ComboBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoardRate = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgList = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.ComboBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.rbSerial = new System.Windows.Forms.RadioButton();
            this.rbNetwork = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgList)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort
            // 
            this.serialPort.DtrEnable = true;
            // 
            // epcid
            // 
            this.epcid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.epcid.DataPropertyName = "com";
            this.epcid.HeaderText = "EPCID";
            this.epcid.Name = "epcid";
            this.epcid.Width = 21;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtName.FormattingEnabled = true;
            this.txtName.Items.AddRange(new object[] {
            "Checkpoint1",
            "Checkpoint2",
            "Checkpoint3",
            "SoftwareRfid",
            "ReceptPrinter"});
            this.txtName.Location = new System.Drawing.Point(88, 116);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(159, 23);
            this.txtName.TabIndex = 10;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Location = new System.Drawing.Point(170, 151);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(147, 28);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.Location = new System.Drawing.Point(253, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Use";
            // 
            // txtBoardRate
            // 
            this.txtBoardRate.BackColor = System.Drawing.Color.White;
            this.txtBoardRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtBoardRate.FormattingEnabled = true;
            this.txtBoardRate.Items.AddRange(new object[] {
            "9600",
            "3600",
            "4500"});
            this.txtBoardRate.Location = new System.Drawing.Point(88, 80);
            this.txtBoardRate.Name = "txtBoardRate";
            this.txtBoardRate.Size = new System.Drawing.Size(159, 23);
            this.txtBoardRate.TabIndex = 8;
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.Green;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ForeColor = System.Drawing.Color.White;
            this.btnConnect.Location = new System.Drawing.Point(14, 151);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(150, 28);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "SETUP";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.Location = new System.Drawing.Point(253, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Port";
            // 
            // rate
            // 
            this.rate.DataPropertyName = "rate";
            this.rate.HeaderText = "Board Rate";
            this.rate.Name = "rate";
            this.rate.ReadOnly = true;
            this.rate.Visible = false;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // dgList
            // 
            this.dgList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgList.ColumnHeadersVisible = false;
            this.dgList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.rate,
            this.epcid,
            this.name});
            this.dgList.Location = new System.Drawing.Point(0, 192);
            this.dgList.Name = "dgList";
            this.dgList.RowHeadersVisible = false;
            this.dgList.Size = new System.Drawing.Size(332, 254);
            this.dgList.TabIndex = 4;
            this.dgList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgList_CellClick);
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "Device";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.Location = new System.Drawing.Point(253, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP Address";
            // 
            // txtPort
            // 
            this.txtPort.BackColor = System.Drawing.Color.White;
            this.txtPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtPort.FormattingEnabled = true;
            this.txtPort.Location = new System.Drawing.Point(88, 45);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(159, 23);
            this.txtPort.TabIndex = 3;
            // 
            // txtIP
            // 
            this.txtIP.BackColor = System.Drawing.Color.White;
            this.txtIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIP.Location = new System.Drawing.Point(88, 13);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(159, 21);
            this.txtIP.TabIndex = 2;
            // 
            // rbSerial
            // 
            this.rbSerial.AutoSize = true;
            this.rbSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSerial.Location = new System.Drawing.Point(12, 46);
            this.rbSerial.Name = "rbSerial";
            this.rbSerial.Size = new System.Drawing.Size(57, 19);
            this.rbSerial.TabIndex = 1;
            this.rbSerial.TabStop = true;
            this.rbSerial.Text = "Serial";
            this.rbSerial.UseVisualStyleBackColor = true;
            this.rbSerial.CheckedChanged += new System.EventHandler(this.rbSerial_CheckedChanged);
            // 
            // rbNetwork
            // 
            this.rbNetwork.AutoSize = true;
            this.rbNetwork.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNetwork.Location = new System.Drawing.Point(12, 12);
            this.rbNetwork.Name = "rbNetwork";
            this.rbNetwork.Size = new System.Drawing.Size(70, 19);
            this.rbNetwork.TabIndex = 0;
            this.rbNetwork.TabStop = true;
            this.rbNetwork.Text = "Network";
            this.rbNetwork.UseVisualStyleBackColor = true;
            this.rbNetwork.CheckedChanged += new System.EventHandler(this.rbNetwork_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtBoardRate);
            this.panel1.Controls.Add(this.btnConnect);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtPort);
            this.panel1.Controls.Add(this.txtIP);
            this.panel1.Controls.Add(this.rbSerial);
            this.panel1.Controls.Add(this.rbNetwork);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(332, 196);
            this.panel1.TabIndex = 3;
            // 
            // frmDevices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 446);
            this.Controls.Add(this.dgList);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDevices";
            this.Text = "frmDevices";
            this.Load += new System.EventHandler(this.frmDevices_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn epcid;
        private System.Windows.Forms.ComboBox txtName;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox txtBoardRate;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridView dgList;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox txtPort;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.RadioButton rbSerial;
        private System.Windows.Forms.RadioButton rbNetwork;
        private System.Windows.Forms.Panel panel1;
    }
}