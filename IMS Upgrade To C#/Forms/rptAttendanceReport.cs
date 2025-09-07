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
    public partial class rptAttendanceReport : Form
    {
        Connection connection;
        public rptAttendanceReport()
        {
            InitializeComponent();
            connection = new Connection();
        }
        void LoadClass()
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

        void LoadClassExtra()
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
                    string selectQuery = "SELECT id, class FROM extra_class";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    txtExtra.DataSource = dataTable;
                    txtExtra.ValueMember = "id";
                    txtExtra.DisplayMember = "class";
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
                    string selectQuery = "SELECT id,name FROM Student WHERE id LIKE @id OR rfid LIKE @rfid";
                    SqlCommand cmd = new SqlCommand(selectQuery, con);
                    cmd.Parameters.AddWithValue("@id", "%" + txtStudentID.Text + "%");
                    cmd.Parameters.AddWithValue("@rfid", "%" + txtStudentID.Text + "%");

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
            if (rbAll.Checked)
            {
                ReportDocument rep = new ReportDocument();
                string selection = "";

                string reportPath = Application.StartupPath + "\\Reports\\Attendance_Sheet.RPT";
                rep.Load(reportPath);

                CrystalDecisions.Shared.TableLogOnInfo logonInfo = rep.Database.Tables[0].LogOnInfo;
                logonInfo.ConnectionInfo.ServerName = connection.Get_path();
                logonInfo.ConnectionInfo.DatabaseName = "IMS";
                logonInfo.ConnectionInfo.UserID = "sa";
                logonInfo.ConnectionInfo.Password = "";
                foreach (CrystalDecisions.CrystalReports.Engine.Table table in rep.Database.Tables)
                {
                    table.ApplyLogOnInfo(logonInfo);
                }

                selection = "{Vw_Attendance.date} in datevalue('" + dtpFrom.Value.ToString("yyyy-MM-dd") + "') to datevalue('" + dtpTo.Value.ToString("yyyy-MM-dd") + "')";
                rep.RecordSelectionFormula = selection;

                rep.Refresh();

                //rep.SetParameterValue("fromDate", dtpFrom.Value.ToString("yyyy-MM-dd"));
                //rep.SetParameterValue("toDate", dtpTo.Value.ToString("yyyy-MM-dd"));

                ReportViewer rv = new ReportViewer(rep);
                rv.Show();
            }
            else if (rbStudent.Checked)
            {
                ReportDocument rep = new ReportDocument();
                string selection = "";

                string reportPath = Application.StartupPath + "\\Reports\\Attendance_Sheet.RPT";
                rep.Load(reportPath);

                CrystalDecisions.Shared.TableLogOnInfo logonInfo = rep.Database.Tables[0].LogOnInfo;
                logonInfo.ConnectionInfo.ServerName = connection.Get_path();
                logonInfo.ConnectionInfo.DatabaseName = "IMS";
                logonInfo.ConnectionInfo.UserID = "sa";
                logonInfo.ConnectionInfo.Password = "";
                foreach (CrystalDecisions.CrystalReports.Engine.Table table in rep.Database.Tables)
                {
                    table.ApplyLogOnInfo(logonInfo);
                }

                selection = "";
                if (txtStudent.Text != "")
                {
                    if (selection == "")
                    {
                        selection = "{Vw_Attendance.student_id}='" + txtStudent.SelectedValue + "'";
                    }
                    else
                    {
                        selection += " and {Vw_Attendance.student_id}='" + txtStudent.SelectedValue + "'";
                    }
                }
                if (txtClass.Text != "")
                {
                    if (selection == "")
                    {
                        selection = "{Vw_Attendance.class_id}='" + txtClass.SelectedValue + "'";
                    }
                    else
                    {
                        selection += " and {Vw_Attendance.class_id}='" + txtClass.SelectedValue + "'";
                    }
                }
                if (txtMonth.Text != "")
                {
                    if (selection == "")
                    {
                        selection = "{Vw_Attendance.month}='" + txtMonth.Text + "'";
                    }
                    else
                    {
                        selection += " and {Vw_Attendance.month}='" + txtMonth.Text + "'";
                    }
                }
                if (txtyear.Text != "")
                {
                    if (selection == "")
                    {
                        selection = "{Vw_Attendance.year}='" + txtyear.Text + "'";
                    }
                    else
                    {
                        selection += " and {Vw_Attendance.year}='" + txtyear.Text + "'";
                    }
                }
                rep.RecordSelectionFormula = selection;

                rep.Refresh();

                //rep.SetParameterValue("fromDate", dtpFrom.Value.ToString("yyyy-MM-dd"));
                //rep.SetParameterValue("toDate", dtpTo.Value.ToString("yyyy-MM-dd"));

                ReportViewer rv = new ReportViewer(rep);
                rv.Show();
            }
            else if (rbClass.Checked)
            {
                ReportDocument rep = new ReportDocument();
                string selection = "";

                if (txtClass.Text != "" && txtExtra.Text == "")
                {
                    string reportPath = Application.StartupPath + "\\Reports\\Attendance_Sheet.RPT";
                    rep.Load(reportPath);

                    CrystalDecisions.Shared.TableLogOnInfo logonInfo = rep.Database.Tables[0].LogOnInfo;
                    logonInfo.ConnectionInfo.ServerName = connection.Get_path();
                    logonInfo.ConnectionInfo.DatabaseName = "IMS";
                    logonInfo.ConnectionInfo.UserID = "sa";
                    logonInfo.ConnectionInfo.Password = "";
                    foreach (CrystalDecisions.CrystalReports.Engine.Table table in rep.Database.Tables)
                    {
                        table.ApplyLogOnInfo(logonInfo);
                    }

                    selection = "";
                    if (txtClass.Text != "")
                    {
                        if (selection == "")
                        {
                            selection = "{Vw_Attendance.class_id}='" + txtClass.SelectedValue + "'";
                        }
                        else
                        {
                            selection += " and {Vw_Attendance.class_id}='" + txtClass.SelectedValue + "'";
                        }
                    }
                    if (txtMonth.Text != "")
                    {
                        if (selection == "")
                        {
                            selection = "{Vw_Attendance.month}='" + txtMonth.Text + "'";
                        }
                        else
                        {
                            selection += " and {Vw_Attendance.month}='" + txtMonth.Text + "'";
                        }
                    }
                    if (txtyear.Text != "")
                    {
                        if (selection == "")
                        {
                            selection = "{Vw_Attendance.year}='" + txtyear.Text + "'";
                        }
                        else
                        {
                            selection += " and {Vw_Attendance.year}='" + txtyear.Text + "'";
                        }
                    }
                }
                else if (txtClass.Text == "" && txtExtra.Text != "")
                {
                    string reportPath = Application.StartupPath + "\\Reports\\Attendance_Sheet_Extra.RPT";
                    rep.Load(reportPath);

                    CrystalDecisions.Shared.TableLogOnInfo logonInfo = rep.Database.Tables[0].LogOnInfo;
                    logonInfo.ConnectionInfo.ServerName = connection.Get_path();
                    logonInfo.ConnectionInfo.DatabaseName = "IMS";
                    logonInfo.ConnectionInfo.UserID = "sa";
                    logonInfo.ConnectionInfo.Password = "";
                    foreach (CrystalDecisions.CrystalReports.Engine.Table table in rep.Database.Tables)
                    {
                        table.ApplyLogOnInfo(logonInfo);
                    }

                    selection = "";
                    if (txtExtra.Text != "")
                    {
                        if (selection == "")
                        {
                            selection = "{Vw_Attendance_Extra.class_id}='" + txtExtra.SelectedValue + "'";
                        }
                        else
                        {
                            selection += " and {Vw_Attendance_Extra.class_id}='" + txtExtra.SelectedValue + "'";
                        }
                    }
                    if (txtMonth.Text != "")
                    {
                        if (selection == "")
                        {
                            selection = "{Vw_Attendance_Extra.month}='" + txtMonth.Text + "'";
                        }
                        else
                        {
                            selection += " and {Vw_Attendance_Extra.month}='" + txtMonth.Text + "'";
                        }
                    }
                    if (txtyear.Text != "")
                    {
                        if (selection == "")
                        {
                            selection = "{Vw_Attendance_Extra.year}='" + txtyear.Text + "'";
                        }
                        else
                        {
                            selection += " and {Vw_Attendance_Extra.year}='" + txtyear.Text + "'";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Only One Type of Class. You Selected Both. Clear it!", "Clear!");
                }

                rep.RecordSelectionFormula = selection;

                rep.Refresh();

                ReportViewer rv = new ReportViewer(rep);
                rv.Show();
            }
        }

        private void rptAttendanceReport_Load(object sender, EventArgs e)
        {
            txtStudentID.Enabled = false;
            txtStudent.Enabled = false;
            txtClass.Enabled = true;
            txtMonth.Enabled = true;
            dtpFrom.Enabled = false;
            dtpTo.Enabled = false;
            txtyear.Enabled = false;

            LoadClass();
            LoadStudent();
            LoadClassExtra();
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            txtStudentID.Enabled = false;
            txtStudent.Enabled = false;
            txtClass.Enabled = false;
            txtMonth.Enabled = false;
            dtpFrom.Enabled = true;
            dtpTo.Enabled = true;
            txtyear.Enabled = false;
        }

        private void rbStudent_CheckedChanged(object sender, EventArgs e)
        {
            txtStudentID.Enabled = true;
            txtStudent.Enabled = true;
            txtClass.Enabled = true;
            txtMonth.Enabled = true;
            txtyear.Enabled = true;
            dtpFrom.Enabled = false;
            dtpTo.Enabled = false;
        }

        private void rbClass_CheckedChanged(object sender, EventArgs e)
        {
            txtStudentID.Enabled = false;
            txtStudent.Enabled = false;
            txtClass.Enabled = true;
            txtMonth.Enabled = true;
            txtyear.Enabled = true;
            dtpFrom.Enabled = false;
            dtpTo.Enabled = false;
        }

        private void txtStudentID_TextChanged(object sender, EventArgs e)
        {
            LoadStudent();
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

    }
}
