namespace IMS_Upgrade_To_C_
{
    partial class CartItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CartItem));
            this.lblClassName = new System.Windows.Forms.Label();
            this.classPrice = new System.Windows.Forms.Label();
            this.lblPaymentforMonth = new System.Windows.Forms.Label();
            this.lblPaymentforYear = new System.Windows.Forms.Label();
            this.remove = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.classDiscount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.guna2ShadowPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblClassName
            // 
            this.lblClassName.AutoSize = true;
            this.lblClassName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClassName.Location = new System.Drawing.Point(4, 6);
            this.lblClassName.Name = "lblClassName";
            this.lblClassName.Size = new System.Drawing.Size(141, 18);
            this.lblClassName.TabIndex = 0;
            this.lblClassName.Text = "Class Name Here";
            this.lblClassName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // classPrice
            // 
            this.classPrice.AutoSize = true;
            this.classPrice.BackColor = System.Drawing.Color.Transparent;
            this.classPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.classPrice.ForeColor = System.Drawing.Color.Green;
            this.classPrice.Location = new System.Drawing.Point(82, 56);
            this.classPrice.Name = "classPrice";
            this.classPrice.Size = new System.Drawing.Size(76, 24);
            this.classPrice.TabIndex = 1;
            this.classPrice.Text = "000000";
            this.classPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPaymentforMonth
            // 
            this.lblPaymentforMonth.AutoSize = true;
            this.lblPaymentforMonth.BackColor = System.Drawing.Color.Transparent;
            this.lblPaymentforMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentforMonth.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblPaymentforMonth.Location = new System.Drawing.Point(82, 36);
            this.lblPaymentforMonth.Name = "lblPaymentforMonth";
            this.lblPaymentforMonth.Size = new System.Drawing.Size(42, 15);
            this.lblPaymentforMonth.TabIndex = 2;
            this.lblPaymentforMonth.Text = "Month";
            this.lblPaymentforMonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPaymentforYear
            // 
            this.lblPaymentforYear.AutoSize = true;
            this.lblPaymentforYear.BackColor = System.Drawing.Color.Transparent;
            this.lblPaymentforYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentforYear.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblPaymentforYear.Location = new System.Drawing.Point(199, 36);
            this.lblPaymentforYear.Name = "lblPaymentforYear";
            this.lblPaymentforYear.Size = new System.Drawing.Size(32, 15);
            this.lblPaymentforYear.TabIndex = 3;
            this.lblPaymentforYear.Text = "Year";
            this.lblPaymentforYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // remove
            // 
            this.remove.BackColor = System.Drawing.Color.Transparent;
            this.remove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("remove.BackgroundImage")));
            this.remove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.remove.FlatAppearance.BorderSize = 0;
            this.remove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.remove.Location = new System.Drawing.Point(284, 74);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(35, 35);
            this.remove.TabIndex = 4;
            this.remove.UseVisualStyleBackColor = false;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(6, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(70, 75);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.Controls.Add(this.classDiscount);
            this.guna2ShadowPanel1.Controls.Add(this.pictureBox1);
            this.guna2ShadowPanel1.Controls.Add(this.remove);
            this.guna2ShadowPanel1.Controls.Add(this.lblClassName);
            this.guna2ShadowPanel1.Controls.Add(this.classPrice);
            this.guna2ShadowPanel1.Controls.Add(this.lblPaymentforYear);
            this.guna2ShadowPanel1.Controls.Add(this.lblPaymentforMonth);
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.Radius = 5;
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.ShadowStyle = Guna.UI2.WinForms.Guna2ShadowPanel.ShadowMode.ForwardDiagonal;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(328, 117);
            this.guna2ShadowPanel1.TabIndex = 6;
            // 
            // classDiscount
            // 
            this.classDiscount.AutoSize = true;
            this.classDiscount.BackColor = System.Drawing.Color.Transparent;
            this.classDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.classDiscount.ForeColor = System.Drawing.Color.Red;
            this.classDiscount.Location = new System.Drawing.Point(82, 83);
            this.classDiscount.Name = "classDiscount";
            this.classDiscount.Size = new System.Drawing.Size(47, 16);
            this.classDiscount.TabIndex = 6;
            this.classDiscount.Text = "00000";
            this.classDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CartItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.guna2ShadowPanel1);
            this.Name = "CartItem";
            this.Size = new System.Drawing.Size(328, 119);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.guna2ShadowPanel1.ResumeLayout(false);
            this.guna2ShadowPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label classPrice;
        private System.Windows.Forms.Label lblPaymentforMonth;
        private System.Windows.Forms.Label lblPaymentforYear;
        private System.Windows.Forms.Button remove;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblClassName;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private System.Windows.Forms.Label classDiscount;
    }
}
