namespace IMS_Upgrade_To_C_.Custom_Controls
{
    partial class At_View
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblClassName = new System.Windows.Forms.Label();
            this.guna2CustomGradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2CustomGradientPanel1
            // 
            this.guna2CustomGradientPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2CustomGradientPanel1.BorderRadius = 8;
            this.guna2CustomGradientPanel1.Controls.Add(this.lblCount);
            this.guna2CustomGradientPanel1.Controls.Add(this.lblClassName);
            this.guna2CustomGradientPanel1.FillColor = System.Drawing.Color.Indigo;
            this.guna2CustomGradientPanel1.FillColor2 = System.Drawing.Color.MediumPurple;
            this.guna2CustomGradientPanel1.FillColor3 = System.Drawing.Color.RoyalBlue;
            this.guna2CustomGradientPanel1.FillColor4 = System.Drawing.Color.Navy;
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.Quality = 15;
            this.guna2CustomGradientPanel1.ShadowDecoration.BorderRadius = 8;
            this.guna2CustomGradientPanel1.ShadowDecoration.Depth = 15;
            this.guna2CustomGradientPanel1.ShadowDecoration.Enabled = true;
            this.guna2CustomGradientPanel1.ShadowDecoration.Parent = this.guna2CustomGradientPanel1;
            this.guna2CustomGradientPanel1.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 5, 5);
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(181, 55);
            this.guna2CustomGradientPanel1.TabIndex = 18;
            // 
            // lblCount
            // 
            this.lblCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCount.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = System.Drawing.Color.White;
            this.lblCount.Location = new System.Drawing.Point(12, 27);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(121, 22);
            this.lblCount.TabIndex = 5;
            this.lblCount.Text = "0";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblClassName
            // 
            this.lblClassName.AutoSize = true;
            this.lblClassName.BackColor = System.Drawing.Color.Transparent;
            this.lblClassName.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClassName.ForeColor = System.Drawing.Color.White;
            this.lblClassName.Location = new System.Drawing.Point(7, 5);
            this.lblClassName.Name = "lblClassName";
            this.lblClassName.Size = new System.Drawing.Size(46, 17);
            this.lblClassName.TabIndex = 4;
            this.lblClassName.Text = "label1";
            // 
            // At_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.guna2CustomGradientPanel1);
            this.Name = "At_View";
            this.Size = new System.Drawing.Size(186, 60);
            this.guna2CustomGradientPanel1.ResumeLayout(false);
            this.guna2CustomGradientPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblClassName;
    }
}
