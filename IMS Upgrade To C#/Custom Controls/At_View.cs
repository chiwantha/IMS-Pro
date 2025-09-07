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
    public partial class At_View : UserControl
    {
        public At_View()
        {
            InitializeComponent();
        }

        public void SetDetails(string className, string count)
        {
            lblClassName.Text = $"{className}";
            lblCount.Text = $"{count}";
        }
    }
}
