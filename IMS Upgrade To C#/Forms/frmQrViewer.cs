using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS_Upgrade_To_C_
{
    public partial class frmQrViewer : Form
    {
        public frmQrViewer()
        {
            InitializeComponent();
        }

        public frmQrViewer(byte[] image)
        {
            InitializeComponent();

            if (image != null)
            {
                using (MemoryStream ms = new MemoryStream(image))
                {
                    Picture.BackgroundImage = Image.FromStream(ms);
                }
            }
            else
            {
                Picture.BackgroundImage = null;
                Picture.BackgroundImage = Image.FromFile(Application.StartupPath + "\\UX\\.support\\.icon\\qr.png");
            }
        }

    }
}
