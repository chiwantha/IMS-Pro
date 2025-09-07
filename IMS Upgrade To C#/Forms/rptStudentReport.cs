using CrystalDecisions.CrystalReports.Engine;
using IMS_Upgraded_C_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS_Upgrade_To_C_
{
    public partial class rptStudentReport : Form
    {
        Connection connection;
        public rptStudentReport()
        {
            InitializeComponent();
            connection = new Connection();
        }

        void Selector()
        {
            if (rbStudentDetails.Checked || rbStudentClassList.Checked || rbStudentPayments.Checked)
            {
                dtpFrom.Enabled = false;
                dtpTo.Enabled = false;
            }
        }

        void LoadStudent()
        {
            try
            {
                SqlConnection con = connection.my_conn();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                try
                {
                    string selectQuery = "SELECT id,name FROM Student WHERE id LIKE @id OR rfid LIKE @rfid OR contact LIKE @contact";
                    SqlCommand cmd = new SqlCommand(selectQuery, con);
                    cmd.Parameters.AddWithValue("@id", "%" + txtStudentID.Text + "%");
                    cmd.Parameters.AddWithValue("@rfid", "%" + txtStudentID.Text + "%");
                    cmd.Parameters.AddWithValue("@contact", "%" + txtStudentID.Text + "%");

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();

                    dataAdapter.Fill(dataTable);

                    txtStudent.DataSource = dataTable;
                    txtStudent.ValueMember = "id";
                    txtStudent.DisplayMember = "name";
                    //txtStudent.SelectedIndex = -1;

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Process Unsuccessful");
            }
        }

        void GenerateReport()
        {
            if (rbStudentDetails.Checked)
            {
                ReportDocument rep = new ReportDocument();
                string reportPath = Application.StartupPath + "\\Reports\\Student_Details.RPT";
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

                if (txtStudent.Text != "")
                {
                    rep.RecordSelectionFormula = "{student.id} ='" + txtStudent.SelectedValue + "'";
                }

                rep.Refresh();
                ReportViewer rv = new ReportViewer(rep);
                rv.Show();
            }
            else if (rbStudentPayments.Checked)
            {
                ReportDocument rep = new ReportDocument();
                string reportPath = Application.StartupPath + "\\Reports\\Student_Py.RPT";
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

                if (txtStudent.Text != "")
                {
                    rep.RecordSelectionFormula = "{Vw_Recept.student_id} ='" + txtStudent.SelectedValue + "'";
                }

                rep.Refresh();
                ReportViewer rv = new ReportViewer(rep);
                rv.Show();
            }
            else if (rbStudentClassList.Checked)
            {
                ReportDocument rep = new ReportDocument();
                string selection = "";

                string reportPath = Application.StartupPath + "\\Reports\\ClassStudentList.RPT";
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

                if (txtStudent.Text != "")
                {
                    selection = "{Vw_studyStudentList.StudentID} ='" + txtStudent.SelectedValue + "'";
                }

                rep.RecordSelectionFormula = selection;
                rep.Refresh();
                ReportViewer rv = new ReportViewer(rep);
                rv.Show();
            }
            else if (rbID.Checked)
            {
                ReportDocument rpt = new ReportDocument();
                rpt.Load(Application.StartupPath + "\\Reports\\ID3.RPT");

                CrystalDecisions.Shared.TableLogOnInfo logonInfo = rpt.Database.Tables[0].LogOnInfo;
                logonInfo.ConnectionInfo.ServerName = connection.Get_path();
                logonInfo.ConnectionInfo.DatabaseName = "IMS";
                logonInfo.ConnectionInfo.UserID = "sa";
                logonInfo.ConnectionInfo.Password = "";

                // Apply the updated credentials to all tables in the report
                foreach (CrystalDecisions.CrystalReports.Engine.Table table in rpt.Database.Tables)
                {
                    table.ApplyLogOnInfo(logonInfo);
                }

                rpt.RecordSelectionFormula = "{student.id}='" + txtStudent.SelectedValue + "'";
                rpt.Refresh();
                ReportViewer rv = new ReportViewer(rpt);
                rv.Show();
            }
        }

        private void txtStudentID_TextChanged(object sender, EventArgs e)
        {
            LoadStudent();
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void rptStudentReportList_Load(object sender, EventArgs e)
        {
            Selector();
            LoadStudent();
        }

        private void rbStudentDetails_CheckedChanged(object sender, EventArgs e)
        {
            Selector();
        }

        private void rbStudentPayments_CheckedChanged(object sender, EventArgs e)
        {
            Selector();
        }

        private void rbStudentClassList_CheckedChanged(object sender, EventArgs e)
        {
            Selector();
        }

        private void rbID_CheckedChanged(object sender, EventArgs e)
        {
            Selector();
        }

    }
}
