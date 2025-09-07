using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS_Upgrade_To_C_
{
    public partial class CartItem : UserControl
    {
        public event EventHandler RemoveButtonClick;
        public CartItem()
        {
            InitializeComponent();
        }

        // Inside your CartItem user control
        public void SetDetails(string className, string month, string year, string amount, string discount)
        {
            lblClassName.Text = $"Class: {className}";
            lblPaymentforMonth.Text = $"Month: {month}";
            lblPaymentforYear.Text = $"Year: {year}";
            classPrice.Text = $"Amount: {amount}";
            classDiscount.Text = "( "+discount + " off )";
        }

        private void remove_Click(object sender, EventArgs e)
        {
            RemoveButtonClick?.Invoke(this, EventArgs.Empty);
        }
    }
}
