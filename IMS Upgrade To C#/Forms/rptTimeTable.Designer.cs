namespace IMS_Upgrade_To_C_.Forms
{
    partial class rptTimeTable
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
            this.dtpPick = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // dtpPick
            // 
            this.dtpPick.BorderRadius = 10;
            this.dtpPick.CheckedState.Parent = this.dtpPick;
            this.dtpPick.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpPick.HoverState.Parent = this.dtpPick;
            this.dtpPick.Location = new System.Drawing.Point(12, 12);
            this.dtpPick.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpPick.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpPick.Name = "dtpPick";
            this.dtpPick.ShadowDecoration.Parent = this.dtpPick;
            this.dtpPick.Size = new System.Drawing.Size(225, 36);
            this.dtpPick.TabIndex = 0;
            this.dtpPick.Value = new System.DateTime(2024, 5, 5, 2, 52, 45, 56);
            // 
            // guna2Button1
            // 
            this.guna2Button1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Button1.BorderRadius = 10;
            this.guna2Button1.CheckedState.Parent = this.guna2Button1;
            this.guna2Button1.CustomImages.Parent = this.guna2Button1;
            this.guna2Button1.FillColor = System.Drawing.Color.Green;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.HoverState.Parent = this.guna2Button1;
            this.guna2Button1.Location = new System.Drawing.Point(243, 12);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.ShadowDecoration.Depth = 15;
            this.guna2Button1.ShadowDecoration.Enabled = true;
            this.guna2Button1.ShadowDecoration.Parent = this.guna2Button1;
            this.guna2Button1.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 5, 5);
            this.guna2Button1.Size = new System.Drawing.Size(145, 36);
            this.guna2Button1.TabIndex = 1;
            this.guna2Button1.Text = "Genarate";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // rptTimeTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(400, 60);
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.dtpPick);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "rptTimeTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "rptTimeTable";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2DateTimePicker dtpPick;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
    }
}