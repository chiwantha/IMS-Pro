using IMS_Upgrade_To_C_.Libs;
using IMS_Upgraded_C_;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS_Upgrade_To_C_
{
    public partial class frmE_Attendance : Form
    {
        Connection connection;
        Attendance attendance;
        public frmE_Attendance()
        {
            InitializeComponent();
            connection = new Connection();
            attendance = new Attendance();
        }


        private async void key_Pass(string key)
        {
            string id = "";

            if (key != "" && cbSelector.Checked)
            {
                id = key.Trim();
            } 
            else if (key == "" && txtID.Text.Trim().Length == 6)
            {
                id = txtID.Text.Trim();
            } else
            {
                return;
            }

            // MessageBox.Show(id);

            
            await attendance.Check_Attendance_Availability(id);
            
            lblShow_Student_ID.Text = attendance.StudentID;
            lblShow_Student_Name.Text = attendance.StudentName;
            lblShow_Class_Name.Text = attendance.ClassName;
            lblShow_Stats.Text = attendance.StudentState;
            Back.BackColor = attendance.StudentColor;
            if (attendance.StudentImage != null)
            {
                MemoryStream ms = new MemoryStream(attendance.StudentImage);
                Picture.BackgroundImage = Image.FromStream(ms);
            }
            else
            {
                Picture.BackgroundImage = null;
                Picture.BackgroundImage = Image.FromFile(Application.StartupPath + "\\UX\\.support\\.icon\\student.png");
            }

            txtID.Text = "";
        }
   
     
        // -----------------------------------------------------------------------------------------------------------------

        private void UpdateData(string data)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateData(data)));
                return;
            }

            key_Pass(data);
        }

        private void serialPortRfid_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPortRfid.IsOpen)
            {
                UpdateData(serialPortRfid.ReadLine());
            }
        }

        private void GetDevice(string device)
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT com, rate FROM device WHERE name=@device", con))
                    {
                        cmd.Parameters.AddWithValue("@device", device);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                serialPortRfid.PortName = reader["com"].ToString();
                                serialPortRfid.BaudRate = Convert.ToInt32(reader["rate"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleDatabaseException($"Error getting device information: {ex.Message}");
                // Log the exception for debugging purposes
            }
        }

        private void HandleDatabaseException(string errorMessage)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => HandleDatabaseException(errorMessage)));
                return;
            }

            MessageBox.Show(errorMessage);
        }

        // -----------------------------------------------------------------------------------------------------------------

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (button1.Text == "Connect")
                {
                    string device = "";
                    if (rbCheck1.Checked)
                    {
                        device = "Checkpoint1";
                    }
                    else if (rbCheck2.Checked)
                    {
                        device = "Checkpoint2";
                    }
                    else if (rbCheck3.Checked)
                    {
                        device = "Checkpoint3";
                    }

                    try
                    {
                        GetDevice(device); // Some method to get the device information
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Device Not Found");
                        return;
                    }

                    try
                    {
                        serialPortRfid.Open();
                        this.Invoke((MethodInvoker)delegate
                        {
                            button1.Text = "Disconnect";
                            button1.BackColor = Color.Red;
                            rbCheck1.Enabled = false;
                            rbCheck2.Enabled = false;
                            rbCheck3.Enabled = false;
                        });
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("No Devices Connected");
                    }
                }
                else if (button1.Text == "Disconnect")
                {
                    serialPortRfid.Close();
                    rbCheck1.Enabled = true;
                    rbCheck2.Enabled = true;
                    rbCheck3.Enabled = true;
                    serialPortRfid.PortName = "0";
                    serialPortRfid.BaudRate = 9600;
                    button1.Text = "Connect";
                    button1.BackColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // -----------------------------------------------------------------------------------------------------------------

        private void timer1_Tick(object sender, EventArgs e)
        {
            time.Text = DateTime.Now.ToString("HH:mm:ss tt");
            // LoadToGrid();
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            if (cbSelector.Checked == false)
            {
                key_Pass("");
            }
        }

        private void frmAttendanceView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.PageDown)
            {
                txtID.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmManualAttendance fm = new frmManualAttendance();
            fm.ShowDialog();
        }

        private void frmE_Attendance_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPortRfid.Close();
            serialPortRfid.Dispose();
        }

        private void rbCheck2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbCheck1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbCheck3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
