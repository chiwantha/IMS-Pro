using CrystalDecisions.CrystalReports.Engine;
using IMS_Upgraded_C_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS_Upgrade_To_C_.Resources
{
    public partial class pgIDCardPortal : Form
    {
        Connection connection;
        public pgIDCardPortal()
        {
            InitializeComponent();
            connection = new Connection();
        }

        private void LoadStudent()
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    try
                    {
                        string selectQuery = "SELECT id, name FROM Student WHERE id LIKE @id OR rfid LIKE @rfid OR contact LIKE @contact";
                        using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                        {
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
            }
            catch (Exception)
            {
                MessageBox.Show("Process Unsuccessful");
            }
        }

        private void LoadClass()
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    try
                    {
                        string selectQuery = "SELECT id, class FROM class where state = 1";
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
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to load class data: " + e.Message);
            }
        }

        private string month = DateTime.Now.ToString("MMMM");

        private Task Filter()
        {
            ReportDocument rpt = new ReportDocument();
            string selection = "";

            string reportPath = Application.StartupPath + "\\Reports\\ID.RPT";
            rpt.Load(reportPath);

            // Set the database connection
            CrystalDecisions.Shared.TableLogOnInfo logonInfo = rpt.Database.Tables[0].LogOnInfo;
            logonInfo.ConnectionInfo.ServerName = connection.Get_path();
            logonInfo.ConnectionInfo.DatabaseName = "IMS";
            logonInfo.ConnectionInfo.UserID = "sa";
            logonInfo.ConnectionInfo.Password = "";
            foreach (Table table in rpt.Database.Tables)
            {
                table.ApplyLogOnInfo(logonInfo);
            }

            // Set the record selection formula based on your criteria
            if (rbStudent.Checked)
            {
                selection = "{student.id}='" + txtStudent.SelectedValue + "'";
                rpt.RecordSelectionFormula = selection;
            }
            else if (rbEnrollment.Checked)
            {
                selection = "{student.month}='" + txtMonth.Text + "' and {student.year}='" + txtyear.Text + "'";
                rpt.RecordSelectionFormula = selection;
            }
            else if (rbClass.Checked)
            {
                selection = "{study.class_id}='" + txtClass.SelectedValue + "'";
                rpt.RecordSelectionFormula = selection;
            }
            else if (rbImageTool.Checked)
            {
                string from = dtpFrom.Text;
                string to = dtpTo.Text;
                selection = "{ student.image_update_date } in '" + from + "' to '" + to + "'";
                rpt.RecordSelectionFormula = selection;
            }
            else if (rbAll.Checked)
            {
                // Additional logic for 'All' case if needed
            }

            // Set the printer and its page size manually
            PrinterSettings printerSettings = new PrinterSettings();
            PageSettings pageSettings = new PageSettings(printerSettings);

            // Set the printer name (replace "YourPrinterName" with the actual printer name)
            printerSettings.PrinterName = "YourPrinterName";

            // Set the page size to A4
            pageSettings.PaperSize = new PaperSize("A4", 827, 1169); // A4 size in hundredths of an inch

            // Uncomment the line below if you need to refresh the report
            // rpt.Refresh();

            // Set the report source for the CrystalReportViewer
            rpt.Refresh();
            crystalReportViewer.ReportSource = rpt;
            crystalReportViewer.Zoom(100);
            return Task.CompletedTask;
        }

        private async void pgIDExport_Load(object sender, EventArgs e)
        {
            await Filter();
            LoadStudent();
            LoadClass();
            txtMonth.Text = month;
        }

        private async void btnBuild_Click(object sender, EventArgs e)
        {
            await Filter();
        }

        private void txtStudentID_TextChanged(object sender, EventArgs e)
        {
            LoadStudent();
        }

    }
}
