using IMS_Upgrade_To_C_;
using IMS_Upgrade_To_C_.Forms;
using IMS_Upgrade_To_C_.Resources;
using IMS_Upgraded_C_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS_Upgrade_To_C_
{
    public partial class MDI : Form
    {
        Connection connection;
        public MDI()
        {
            InitializeComponent();
            connection = new Connection();
        }
        public MDI(string user, string des)
        {
            InitializeComponent();
            //
            usernamestrip.Text = user;
            if (des != "Management")
            {
                moneyToolStripMenuItem.Enabled = false;
                devicesToolStripMenuItem1.Enabled = false;
                userToolStripMenuItem.Enabled = false;
            }
            else if (des == "Employee")
            {
                using (SqlConnection con = connection.my_conn())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    string select = "select * from UserRights where designation=@des";
                    using (SqlCommand cmd = new SqlCommand(select, con))
                    {
                        cmd.Parameters.AddWithValue("@des", des);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    try
                                    {
                                        userToolStripMenuItem.Enabled = reader["Users"].ToString() == "1";
                                        attendanceViewToolStripMenuItem.Enabled = reader["Attendance_View"].ToString() == "1";
                                        studentToolStripMenuItem.Enabled = reader["Student"].ToString() == "1";
                                        teacherToolStripMenuItem.Enabled = reader["Teacher"].ToString() == "1";
                                        subjectToolStripMenuItem.Enabled = reader["Subject"].ToString() == "1";
                                        classToolStripMenuItem.Enabled = reader["Class"].ToString() == "1";
                                        holidaysToolStripMenuItem.Enabled = reader["Holidays"].ToString() == "1";
                                        studentIDToolStripMenuItem.Enabled = reader["Sms"].ToString() == "1";
                                        studyToolStripMenuItem.Enabled = reader["Study"].ToString() == "1";
                                        eAttendanceMarkToolStripMenuItem.Enabled = reader["Attendance"].ToString() == "1";
                                        dashboardToolStripMenuItem.Enabled = reader["Dashboard"].ToString() == "1";
                                        invoiceToolStripMenuItem.Enabled = reader["Invoice"].ToString() == "1";
                                        moneyToolStripMenuItem.Enabled = reader["Teacher_Payments"].ToString() == "1";
                                        classStudentListToolStripMenuItem.Enabled = reader["Student_Report"].ToString() == "1";
                                        classReportToolStripMenuItem.Enabled = reader["Class_Report"].ToString() == "1";
                                        attendanceReportToolStripMenuItem.Enabled = reader["Attendance_Report"].ToString() == "1";
                                        devicesToolStripMenuItem1.Enabled = reader["Devices"].ToString() == "1";
                                        featuresToolStripMenuItem.Enabled = reader["FSms"].ToString() == "1" || reader["Features"].ToString() == "1";
                                        pendingPaymentsToolStripMenuItem.Enabled = reader["Pending_Payment"].ToString() == "1";
                                    }
                                    catch (Exception e)
                                    {
                                        MessageBox.Show(e.Message);
                                    }
                                }
                            }
                        }
                    }

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            //
        }
        public string get_username_strip()
        {
            return usernamestrip.ToString();
        }
        private Form currentForm;
        public void SwapForms(Form newForm)
        {
            btnPannel.Visible = false;
            pictureBox1.Visible = false;
            top.Visible = false;
            bottom.Visible = false;

            // If there is a current form, close it
            if (currentForm != null)
            {
                currentForm.Close();
            }

            // Set up properties for the newForm
            currentForm = newForm;
            newForm.TopLevel = false;
            newForm.Parent = MainPannel;
            newForm.Dock = DockStyle.Fill;
            newForm.Show();
        }
        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pgStudent pgStudent = new pgStudent();
            SwapForms(pgStudent);
        }
        private void teacherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pgTeacher pgTeacher = new pgTeacher();
            SwapForms(pgTeacher);
        }
        private void subjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSubject frmSubject = new frmSubject();
            frmSubject.ShowDialog();
        }
        private void regularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pgClass pgClass = new pgClass();
            SwapForms(pgClass);
        }
        private void extraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pgExtraClass pgExtraClass = new pgExtraClass();
            SwapForms(pgExtraClass);
        }
        private void holidaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pgHoliday pgHoliday = new pgHoliday();
            SwapForms(pgHoliday);
        }
        private void studentIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pgSMS frmStudentID = new pgSMS();
            SwapForms(frmStudentID);
        }
        private void studyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pgStudy pgStudy = new pgStudy();
            SwapForms(pgStudy);
        }
        private void moneyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pgSalary pgSalary = new pgSalary();
            SwapForms(pgSalary);
        }
        private void pendingPaymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pgPendingPayments pgPendingPayments = new pgPendingPayments();
            SwapForms(pgPendingPayments);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            pgStudent pgStudent = new pgStudent();
            SwapForms(pgStudent);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            pgTeacher pgTeacher = new pgTeacher();
            SwapForms(pgTeacher);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            pgInvoiceCart frmInvoicecart = new pgInvoiceCart(usernamestrip.Text);
            SwapForms(frmInvoicecart);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            pgStudy pgStudy = new pgStudy();
            SwapForms(pgStudy);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            pgClass pgClass = new pgClass();
            SwapForms(pgClass);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            frmE_Attendance frmE_Attendance = new frmE_Attendance();
            frmE_Attendance.Show(this);
        }
        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentForm != null)
            {
                currentForm.Close();
            }
            btnPannel.Visible = true;
            pictureBox1.Visible = true;
            top.Visible = true;
            bottom.Visible = true;

        }
        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUser frmUser = new frmUser();
            frmUser.ShowDialog();
        }
        private void devicesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDevices pgDevices = new frmDevices();
            pgDevices.ShowDialog();
        }
        private void sMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSMS frmSMS = new frmSMS();
            frmSMS.ShowDialog();
        }
        private void databaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pgDatabase pgDatabase = new pgDatabase();
            SwapForms(pgDatabase);
        }
        private void imageBulkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmImageTool frmImageTool = new frmImageTool();
            frmImageTool.ShowDialog();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pgAbout pgAbout = new pgAbout();
            SwapForms(pgAbout);
        }
        private void classStudentListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptStudentReport rptStudentReport = new rptStudentReport();
            rptStudentReport.ShowDialog();
        }
        private void classReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptClassReport rptClassReport = new rptClassReport();
            rptClassReport.ShowDialog();
        }
        private void attendanceReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptAttendanceReport rptAttendanceReport = new rptAttendanceReport();
            rptAttendanceReport.ShowDialog();
        }
        private void studentIDPortalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pgIDCardPortal pgIDCardPortal = new pgIDCardPortal();
            SwapForms(pgIDCardPortal);
        }
        private void incomeSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Not Available On This Version !", "Upgrade !", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
        private void attendanceViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pgAttendanceView pgAttendanceView = new pgAttendanceView();
            SwapForms(pgAttendanceView);
        }
        private void manualAttendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManualAttendance frmManualAttendance = new frmManualAttendance();
            frmManualAttendance.Show(this);
        }
        private void eAttendanceMarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmE_Attendance frmE_Attendance = new frmE_Attendance();
            frmE_Attendance.Show(this);
        }
        private void MDI_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Application.Exit();
            } catch (Exception)
            {
                Environment.Exit(0);
            }
        }
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pgSettings pgSettings = new pgSettings();
            SwapForms(pgSettings);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pgControlPanel pgControlPanel = new pgControlPanel();
            SwapForms(pgControlPanel);
        }

        private void studentPortalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is Locked !", "Locked !", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void onlineClassesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is Locked !", "Locked !", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void ticketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is Locked !", "Locked !", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void admissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is Locked !", "Locked !", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void noticeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is Locked !", "Locked !", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void appkchordcomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "http://www.kchord.com";

            try
            {
                // Open default web browser and navigate to the URL
                Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void ShowContinueMessageBox()
        {
            DialogResult result = MessageBox.Show("Control Server failed. Do you want to continue anyway?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (result == DialogResult.No)
            {
                
            }
            flushServerLocalToolStripMenuItem.Text = "Flush Server ( Start )";
            flushServerLocalToolStripMenuItem.BackColor = System.Drawing.Color.LightCoral;
        }

        private void flushServerLocalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string shortcutPath = System.IO.Path.Combine(Application.StartupPath, "XAMPP Control Panel.lnk");

                // Use ProcessStartInfo to specify the "runas" verb
                ProcessStartInfo psi = new ProcessStartInfo(shortcutPath);
                psi.Verb = "runas";

                // Start the process with elevated privileges
                Process.Start(psi);
                flushServerLocalToolStripMenuItem.Text = "Server Running ..";
                flushServerLocalToolStripMenuItem.BackColor = System.Drawing.Color.LightGreen;

            }
            catch
            {
                ShowContinueMessageBox();
            }
        }

        private void dashboardToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            pgControlPanel pgControlPanel = new pgControlPanel();
            SwapForms(pgControlPanel);
        }

        private void invoiceToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            pgInvoiceCart pgInvoiceCart = new pgInvoiceCart(usernamestrip.Text);
            SwapForms(pgInvoiceCart);
        }

        private void singleItemInvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pgInvoice frmInvoice = new pgInvoice(usernamestrip.Text);
            SwapForms(frmInvoice);
        }

        private void timeTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptTimeTable pgTimeTable = new rptTimeTable();
            pgTimeTable.ShowDialog();
        }

        private void qRLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQrLink frmQrLink = new frmQrLink();
            frmQrLink.ShowDialog();
        }
    }
}
