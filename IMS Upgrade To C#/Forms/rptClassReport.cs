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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS_Upgrade_To_C_
{
    public partial class rptClassReport : Form
    {
        Connection connection;
        public rptClassReport()
        {
            InitializeComponent();
            connection = new Connection();
            
        }

        private void Selector()
        {
            if (rbClassDetails.Checked)
            {
                txtMonth.Enabled = false;
                txtBatch.Enabled = true;
            }
            else if (rbClassStudentList.Checked)
            {
                txtMonth.Enabled = false;
                txtBatch.Enabled = false;

            }
            else if (rbClassIncome.Checked)
            {
                txtMonth.Enabled = false;
                txtBatch.Enabled = false;
                txtGrade.Enabled = false;

            } else if (rbClassPy.Checked)
            {
                txtMonth.Enabled = false;
                txtBatch.Enabled = false;
                txtGrade.Enabled = false;

            } else if (rbClassPaidList.Checked)
            {
                txtMonth.Enabled = true;
                txtBatch.Enabled = false;
                txtGrade.Enabled = false;
            }
        }

        private void LoadClass()
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
                    string selectQuery = "SELECT id, class FROM class  where state = 1";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    txtClass.DataSource = dataTable;
                    txtClass.ValueMember = "id";
                    txtClass.DisplayMember = "class";
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error fetching class data: " + e.Message);
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to load class data: " + e.Message);
            }
        }

        private void Report()
        {
            string selection = "";
            if (rbClassDetails.Checked)
            {
                ReportDocument rep = new ReportDocument();
                rep.Load(Application.StartupPath + "\\Reports\\Class_Details.RPT");

                CrystalDecisions.Shared.TableLogOnInfo logonInfo = rep.Database.Tables[0].LogOnInfo;
                logonInfo.ConnectionInfo.ServerName = connection.Get_path();
                logonInfo.ConnectionInfo.DatabaseName = "IMS";
                logonInfo.ConnectionInfo.UserID = "sa";
                logonInfo.ConnectionInfo.Password = "";
                foreach (CrystalDecisions.CrystalReports.Engine.Table table in rep.Database.Tables)
                {
                    table.ApplyLogOnInfo(logonInfo);
                }

                if (selection == "")
                {
                    if (txtClass.Text != "")
                    {
                        selection = "{Vw_Class.id}='" + txtClass.SelectedValue + "'";
                    }
                }
                else
                {
                    if (txtClass.Text != "")
                    {
                        selection += " and {Vw_Class.id}='" + txtClass.SelectedValue + "'";
                    }
                }

                if (selection == "")
                {
                    if (txtMonth.Text != "")
                    {
                        selection = "{Vw_Class.day}='" + txtMonth.Text + "'";
                    }
                }
                else
                {
                    if (txtMonth.Text != "")
                    {
                        selection += " and {Vw_Class.day}='" + txtMonth.Text + "'";
                    }
                }

                if (selection == "")
                {
                    if (txtBatch.Text != "")
                    {
                        selection = "{Vw_Class.batch}='" + txtBatch.Text + "'";
                    }
                }
                else
                {
                    if (txtBatch.Text != "")
                    {
                        selection += " and {Vw_Class.batch}='" + txtBatch.Text + "'";
                    }
                }

                if (selection == "")
                {
                    if (txtGrade.Text != "")
                    {
                        selection = "{Vw_Class.grade}='" + txtGrade.Text + "'";
                    }
                }
                else
                {
                    if (txtGrade.Text != "")
                    {
                        selection += " and {Vw_Class.grade}='" + txtGrade.Text + "'";
                    }
                }

                rep.RecordSelectionFormula = selection;

                rep.Refresh();
                ReportViewer rv = new ReportViewer(rep);
                rv.Show();
            }
            else if (rbClassStudentList.Checked)
            {
                ReportDocument rep = new ReportDocument();
                selection = "";

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

                if (selection == "")
                {
                    if (txtClass.Text != "")
                    {
                        selection = "{Vw_studyStudentList.ClassID} ='" + txtClass.SelectedValue + "'";
                    }
                }
                else
                {
                    if (txtClass.Text != "")
                    {
                        selection += " and {Vw_studyStudentList.ClassID} ='" + txtClass.SelectedValue + "'";
                    }
                }

                if (selection == "")
                {
                    if (txtGrade.Text != "")
                    {
                        selection = "{Vw_studyStudentList.grade} ='" + txtGrade.Text + "'";
                    }
                }
                else
                {
                    if (txtGrade.Text != "")
                    {
                        selection += " and {Vw_studyStudentList.grade} ='" + txtGrade.Text + "'";
                    }
                }

                rep.RecordSelectionFormula = selection;
                rep.Refresh();
                ReportViewer rv = new ReportViewer(rep);
                rv.Show();
            }
            else if (rbClassIncome.Checked)
            {
                ReportDocument rep = new ReportDocument();
                selection = "";

                string reportPath = Application.StartupPath + "\\Reports\\Class_Income.RPT";
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

                if (selection == "")
                {
                    if (txtClass.Text != "")
                    {
                        selection = "{Vw_Recept.class_id} ='" + txtClass.SelectedValue + "'";
                    }
                }

                rep.RecordSelectionFormula = selection;
                rep.Refresh();
                ReportViewer rv = new ReportViewer(rep);
                rv.Show();
            } 
            else if (rbClassPy.Checked)
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

                if (!string.IsNullOrEmpty(txtClass.Text))
                {
                    rep.RecordSelectionFormula = "{Vw_Recept.class_id} = '" + txtClass.SelectedValue + "'";
                }


                rep.Refresh();
                ReportViewer rv = new ReportViewer(rep);
                rv.Show();
            }
            else if (rbClassPaidList.Checked)
            {
                ReportDocument rep = new ReportDocument();
                string reportPath = Application.StartupPath + "\\Reports\\Payment_By_Class.RPT";
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

                if (!string.IsNullOrEmpty(txtClass.Text))
                {
                    if (txtMonth.Text == "")
                    {
                        rep.RecordSelectionFormula = "{Vw_Recept.class_id} = '" + txtClass.SelectedValue + "'";
                    } else
                    {
                        rep.RecordSelectionFormula = "{Vw_Recept.class_id} = '" + txtClass.SelectedValue + "' and {Vw_Recept.month} = '" + txtMonth.Text + "'";
                    }
                } 
                else
                {
                    if (txtMonth.Text == "")
                    {
                        MessageBox.Show("No Filter Selected", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;       
                    }
                    else
                    {
                        rep.RecordSelectionFormula = "{Vw_Recept.month} = '" + txtMonth.Text + "'";
                    }
                }


                rep.Refresh();
                ReportViewer rv = new ReportViewer(rep);
                rv.Show();
            }
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            Report();
        }

        private void rptClassReportList_Load(object sender, EventArgs e)
        {
            LoadClass();
            Selector();
        }

        private void rbClassDetails_CheckedChanged(object sender, EventArgs e)
        {
            Selector();
        }

        private void rbClassStudentList_CheckedChanged(object sender, EventArgs e)
        {
            Selector();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Selector();
        }

        private void rbClassPy_CheckedChanged(object sender, EventArgs e)
        {
            Selector();
        }

        private void rbClassPaidList_CheckedChanged(object sender, EventArgs e)
        {
            Selector();
        }
    }
}
