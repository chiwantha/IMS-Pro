using IMS_Upgrade_To_C_.Custom_Controls;
using IMS_Upgrade_To_C_.Libs;
using IMS_Upgraded_C_;
using MySql.Data.MySqlClient;
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
    public partial class pgControlPanel : Form
    {
        Connection connection;
        Attendance attendance;
        Access_cart access_Cart = new Access_cart();

        public pgControlPanel()
        {
            InitializeComponent();
            connection = new Connection();
            attendance = new Attendance();
        }

        private async void key_Pass(string key)
        {
            flowLayoutPanel1.Invoke((MethodInvoker)(() => flowLayoutPanel1.Controls.Clear()));

            string id = "";

            if (key != "" && DuelSelector.Checked)
            {
                id = key.Trim();               
            }
            else if (key == "")
            {
                if (rbNameAtt.Checked)
                {
                    id = txtStudentAtt.SelectedValue.ToString().Trim();
                }
                else if (rbIdAtt.Checked)
                {
                    if (txtIDAtt.Text.Trim().Length == 6)
                    {
                        id = txtIDAtt.Text.Trim();
                    } else
                    {
                        return;
                    }
                }
            }
            else
            {
                return;
            }

            // MessageBox.Show(id);


            await attendance.Check_Attendance_Availability(id);

            // update lables
            lblShow_Student_ID.Text = attendance.StudentID;
            lblShow_Student_Name.Text = attendance.StudentName;
            lblShow_Class_Name.Text = attendance.ClassName;
            lblShow_Stats.Text = attendance.StudentState;
            Back.BackColor = attendance.StudentColor;

            // update check boxes
            await DisplayAttendance(attendance.week1, attendance.week2, attendance.week3, attendance.week4, attendance.week5);

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

            if (AtendanceMode.Checked)
            {
                txtIDAtt.Text = "";
            }
            await UpdateAttendaceUiAsync();
            if (attendance.StudentID != "") {
                await LoadCart(attendance.StudentID);
            }
        }

        async Task LoadDataAsync()
        {
            using (SqlConnection con = connection.my_conn())
            {
                await con.OpenAsync();
                string sql_select = "SELECT * FROM Vw_DashboardData";
                SqlCommand cmd = new SqlCommand(sql_select, con);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        lblstudentCount.Text = Convert.ToInt32(reader["StudentCount"]).ToString();
                        lblliveClass.Text = Convert.ToInt32(reader["LiveClassCount"]).ToString();
                        lblinvoiceCount.Text = Convert.ToInt32(reader["TodayInvoices"]).ToString();
                        lblHolidays.Text = Convert.ToInt32(reader["ActiveHolidays"]).ToString();
                        lblCollection.Text = Convert.ToDecimal(reader["TodayCollection"]).ToString();
                    }
                }
            }

            try
            {
                string originalText =  await connection.GetSMSUnitDetails();
                string newText = originalText.Replace("Remaining SMS units: ", "");
                lblSmsBalance.Text = newText;
                //lblSmsBalance.Text = "No Inc.";
            }
            catch
            {
                lblSmsBalance.Text = "Error !";
            }
        }

        Task LoadStudent()
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
                        string selectQuery = "SELECT id,name FROM Student";
                        using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                        {

                            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                            {
                                DataTable dataTable = new DataTable();
                                dataAdapter.Fill(dataTable);

                                txtStudentAtt.DataSource = dataTable;
                                txtStudentAtt.ValueMember = "id";
                                txtStudentAtt.DisplayMember = "name";

                            }
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Process Unsuccessful : ");
            }

            return Task.CompletedTask;
        }

        void CalculateNet()
        {
            int discount;

            if (txtDiscount.Text == "")
            {
                discount = 0;
            } else
            {
                discount = Convert.ToInt32(txtDiscount.Text);
            }

            txtinvTotalNet.Text = "";
            txtinvTotalNet.Text = (Convert.ToInt32(txtinvTotalGross.Text) - discount).ToString();
        }

        private void clear_invoice()
        {
            txtinvBank.Text = "";
            txtincCashReceve.Text = "";
            txtInvBalance.Text = "";
        }

        Task DisplayAttendance(bool week1, bool week2, bool week3, bool week4, bool week5)
        {
            week1label.Enabled = true;
            week2label.Enabled = true;
            week3label.Enabled = true;
            week4label.Enabled = true;
            week5label.Enabled = true;

            week1label.Checked = week1;
            week2label.Checked = week2;
            week3label.Checked = week3;
            week4label.Checked = week4;
            week5label.Checked = week5;

            week1label.Enabled = false;
            week2label.Enabled = false;
            week3label.Enabled = false;
            week4label.Enabled = false;
            week5label.Enabled = false;
            return Task.CompletedTask;
        }


        //-----------------------------------------------------------------------

        async Task LoadCart(string student_id)
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    //--------------------------------------------------------------------------------------------------------------------------------

                    int discount_value = await access_Cart.Get_Student_Discount_Value(con, student_id);

                    //--------------------------------------------------------------------------------------------------------------------------------

                    try
                    {
                        string selectQuery = "SELECT student_id, class_id, class, grade, batch, month, year, Fee FROM Vw_PendingPayment";
                        string filter = "";

                        if (student_id != "")
                        {
                            if (filter == "")
                            {
                                filter = " WHERE student_id LIKE '%" + student_id + "%'";
                            }
                            else
                            {
                                filter += " AND student_id LIKE '%" + student_id + "%'";
                            }
                        }

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery + filter, con))
                        {
                            DataTable dataTable = new DataTable();
                            dataAdapter.Fill(dataTable);

                            Access_cart access_Cart = new Access_cart();
                            await access_Cart.ClearAllDataAsync();

                            // Use Task.Run to perform the insertions on a background thread
                            await Task.Run(async () =>
                            {
                                bool class_eligibility;

                                foreach (DataRow row in dataTable.Rows)
                                {
                                    class_eligibility = await access_Cart.Check_Class_Discount_Eligible(con, row["class_id"].ToString());

                                    await access_Cart.InsertDataAsync(
                                        row["student_id"].ToString(),
                                        row["class_id"].ToString(),
                                        row["class"].ToString(),
                                        row["grade"].ToString(),
                                        row["batch"].ToString(),
                                        row["month"].ToString(),
                                        row["year"].ToString(),
                                        row["Fee"].ToString(),
                                        class_eligibility ? discount_value.ToString() : "0",
                                        class_eligibility ? (Convert.ToInt32(row["Fee"]) - discount_value).ToString() : row["Fee"].ToString());
                                }
                            });


                            await update_my_cart_ui();
                            txtinvTotalGross.Text = await access_Cart.LoadTotalGrossAsync();
                            txtDiscount.Text = await access_Cart.LoadTotalDiscountAsync();
                            CalculateNet();
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Process Unsuccessful : ");
            }
        }

        async Task update_my_cart_ui()
        {

            flowLayoutPanel1.Invoke((MethodInvoker)(() => flowLayoutPanel1.Controls.Clear()));
            DataTable dataTable = await access_Cart.GetAllDataAsync();
            await Task.Run(() =>
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    CartItem item = new CartItem();
                    item.SetDetails(row["class_name"].ToString(), row["month"].ToString(), row["year"].ToString(), row["amount"].ToString(), row["discount"].ToString());
                    DataRow currentRow = row;
                    item.RemoveButtonClick += (sender, args) => RemoveItemFromCart(currentRow);
                    flowLayoutPanel1.Invoke((MethodInvoker)(() => flowLayoutPanel1.Controls.Add(item)));
                }
            });
        }

        async Task UpdateAttendaceUiAsync()
        {
            flow_at_panel.Controls.Clear();
            int weekOfMonth = (DateTime.Now.Day - 1) / 7 + 1;

            using (SqlConnection con = connection.my_conn())
            {
                await con.OpenAsync();
                string sql_select = "SELECT * FROM Vw_Dashboard_AT_Details";
                SqlCommand cmd = new SqlCommand(sql_select, con);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        At_View at_View = new At_View();
                        at_View.SetDetails(reader["class"].ToString(), reader["week" + weekOfMonth].ToString());
                        flow_at_panel.Controls.Add(at_View);
                    }
                }
            }
        }

        async void RemoveItemFromCart(DataRow row)
        {
            await access_Cart.DeleteItemAsync(row["student_id"].ToString(), row["class_id"].ToString(), row["month"].ToString(), row["year"].ToString());
            await update_my_cart_ui();
            txtinvTotalGross.Text = await access_Cart.LoadTotalGrossAsync();
            txtDiscount.Text = await access_Cart.LoadTotalDiscountAsync();
            CalculateNet();
        }

        //-----------------------------------------------------------------------

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

        private void serialPortRfid_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPortRfid.IsOpen)
            {
                UpdateData(serialPortRfid.ReadLine());
            }
        }

        private void UpdateData(string data)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateData(data)));
                return;
            }

            key_Pass(data);
        }

        //-----------------------------------------------------------------------

        private async void pgControlPanel_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
            Connect.Enabled = false;
            await LoadStudent();
            await UpdateAttendaceUiAsync();
            await DisplayAttendance(false, false, false, false, false);
        }

        private void txtIDAtt_TextChanged(object sender, EventArgs e)
        {
            if (AtendanceMode.Checked)
            {
                key_Pass("");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Connect.Text == "Connect")
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
                            Connect.Text = "Disconnect";
                            Connect.BackColor = Color.Red;
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
                else if (Connect.Text == "Disconnect")
                {
                    serialPortRfid.Close();
                    rbCheck1.Enabled = true;
                    rbCheck2.Enabled = true;
                    rbCheck3.Enabled = true;
                    serialPortRfid.PortName = "0";
                    serialPortRfid.BaudRate = 9600;
                    Connect.Text = "Connect";
                    Connect.BackColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DuelSelector_CheckedChanged(object sender, EventArgs e)
        {
            if (DuelSelector.Checked)
            {
                DuelSelector.BackColor = Color.Green;
                Connect.Enabled = true;
            } 
            else
            {
                DuelSelector.BackColor= Color.Red;
                Connect.Enabled = false;
            }
        }

        private void AtendanceMode_CheckedChanged(object sender, EventArgs e)
        {
            if(AtendanceMode.Checked)
            {
                txtIDAtt.Text = "";
            } else
            {
                txtIDAtt.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            key_Pass("");
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (rbIdAtt.Checked)
            {
                if (txtIDAtt.Text != "") {
                    await LoadCart(txtIDAtt.Text.Trim());
                }
            }
            else if (rbNameAtt.Checked)
            {
                if (txtStudentAtt.Text != "")
                {
                    await LoadCart(txtStudentAtt.SelectedValue.ToString().Trim());
                }
            }
        }

        private void csTxtClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            //await ClassFinder();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {           
            if (txtinvTotalGross.Text != "" && txtinvTotalNet.Text != "")
            {
                if (txtincCashReceve.Text != "")
                {
                    if (Convert.ToInt32(txtinvTotalGross.Text) >= 0)
                    {
                        txtInvBalance.Text = (Convert.ToInt32(txtincCashReceve.Text) - Convert.ToInt32(txtinvTotalNet.Text)).ToString();
                    }
                }
            }
        }

        private async void btnPay_Click(object sender, EventArgs e)
        {
            if (txtinvTotalGross.Text != "" && txtincCashReceve.Text != "" && txtInvBalance.Text != "")
            {
                await access_Cart.Pay(txtinvBank.Text, "Admin", txtincCashReceve.Text, txtInvBalance.Text);
            } else
            {
                MessageBox.Show("Invoice Details Missing !", "warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            await update_my_cart_ui();
            clear_invoice();
            txtinvTotalGross.Text = await access_Cart.LoadTotalGrossAsync();
            txtDiscount.Text = await access_Cart.LoadTotalDiscountAsync();
            CalculateNet();
        }

        private void btnRePrint_Click(object sender, EventArgs e)
        {
            if (rePrintPannel.Visible)
            {
                rePrintPannel.Visible = false;
            } else 
            {
                rePrintPannel.Visible = true;
            }
        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            await access_Cart.Recept(txtRePrintRn.Text);
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateNet();
        }
    }
}
