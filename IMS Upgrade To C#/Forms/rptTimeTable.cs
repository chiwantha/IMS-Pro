using CrystalDecisions.CrystalReports.Engine;
using IMS_Upgraded_C_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS_Upgrade_To_C_.Forms
{
    public partial class rptTimeTable : Form
    {
        Connection connection;
        public rptTimeTable()
        {
            InitializeComponent();
            connection = new Connection();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ReportDocument rep = new ReportDocument();
            string reportPath = Application.StartupPath + "\\Reports\\TimeTable.RPT";
            rep.Load(reportPath);

            CrystalDecisions.Shared.TableLogOnInfo logonInfo = rep.Database.Tables[0].LogOnInfo;
            logonInfo.ConnectionInfo.ServerName = connection.Get_path();
            logonInfo.ConnectionInfo.DatabaseName = "IMS";
            logonInfo.ConnectionInfo.UserID = "sa";
            logonInfo.ConnectionInfo.Password = "";

            // Apply the updated credentials to all tables in the report
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in rep.Database.Tables)
            {
                table.ApplyLogOnInfo(logonInfo);
            }

            if (!string.IsNullOrEmpty(dtpPick.Text))
            {
                rep.RecordSelectionFormula = "{Vw_All_Classes.day} = '" + dtpPick.Value.DayOfWeek.ToString() + "'";
            }

            rep.Refresh();
            rep.SetParameterValue("Day", dtpPick.Value.DayOfWeek.ToString());
            rep.SetParameterValue("date", dtpPick.Value.ToString("yyyy-MM-dd"));
            ReportViewer rv = new ReportViewer(rep);
            rv.Show();
        }
    }
}
