using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS_Upgrade_To_C_.Custom_Controls
{
    public partial class ClassViewer : UserControl
    {
        public ClassViewer()
        {
            InitializeComponent();
        }

        public void SetDetails(string id, string className, string price, string start, string end, string grade, string batch, string day)
        {
            LblId.Text = $"ID : {id}";
            lblClassName.Text = $"{className}";
            lblPrice.Text = $"Price : {price}";
            lblStart.Text = $"Start : {start}";
            lblEnd.Text = $"End : {end}";
            lblGrade.Text = $"Grade : {grade}";
            lblBatch.Text = $"Batch : {batch}";
            lblDay.Text = $"Day : {day}";
        }
    }
}
