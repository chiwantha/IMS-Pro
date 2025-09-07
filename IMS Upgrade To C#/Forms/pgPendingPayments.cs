using IMS_Upgraded_C_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS_Upgrade_To_C_
{
    public partial class pgPendingPayments : Form
    {
        Connection connection;
        public pgPendingPayments()
        {
            InitializeComponent();
            connection = new Connection();
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
                        String selectQuery = "SELECT id, class FROM class  where state = 1";
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
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load class data: " + ex.Message);
            }
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
                        String selectQuery = "SELECT id,name FROM Student WHERE id LIKE @id OR name LIKE @name OR contact LIKE @contact or Rfid LIKE @rfid";
                        SqlCommand cmd = new SqlCommand(selectQuery, con);
                        cmd.Parameters.AddWithValue("@id", "%" + txtID.Text + "%");
                        cmd.Parameters.AddWithValue("@name", "%" + txtID.Text + "%");
                        cmd.Parameters.AddWithValue("@contact", "%" + txtID.Text + "%");
                        cmd.Parameters.AddWithValue("@rfid", "%" + txtID.Text + "%");

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();

                        dataAdapter.Fill(dataTable);

                        txtStudent.DataSource = dataTable;
                        txtStudent.ValueMember = "id";
                        txtStudent.DisplayMember = "name";
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
                MessageBox.Show("Process Unsuccessful : " + txtID);
            }
        }
        private void Filter()
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
                        String selectQuery = "SELECT * FROM Vw_PendingPayment";
                        String filterStatement = "";

                        if (!string.IsNullOrEmpty(txtStudent.Text))
                        {
                            if (string.IsNullOrEmpty(filterStatement))
                            {
                                filterStatement = " WHERE student_id LIKE '%" + txtStudent.SelectedValue + "%'";
                            }
                            else
                            {
                                filterStatement += " AND student_id LIKE '%" + txtStudent.SelectedValue + "%'";
                            }
                        }

                        if (!string.IsNullOrEmpty(txtClass.Text))
                        {
                            if (string.IsNullOrEmpty(filterStatement))
                            {
                                filterStatement = " WHERE class_id LIKE '%" + txtClass.SelectedValue + "%'";
                            }
                            else
                            {
                                filterStatement += " AND class_id LIKE '%" + txtClass.SelectedValue + "%'";
                            }
                        }

                        if (txtMonth.Text != "All")
                        {
                            if (string.IsNullOrEmpty(filterStatement))
                            {
                                filterStatement = " WHERE month LIKE '%" + txtMonth.Text + "%'";
                            }
                            else
                            {
                                filterStatement += " AND month LIKE '%" + txtMonth.Text + "%'";
                            }
                        }

                        if (txtMonth.Text != "All")
                        {
                            if (string.IsNullOrEmpty(filterStatement))
                            {
                                filterStatement = " WHERE month LIKE '%" + txtMonth.Text + "%'";
                            }
                            else
                            {
                                filterStatement += " AND month LIKE '%" + txtMonth.Text + "%'";
                            }
                        }

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery + filterStatement, con);
                        DataTable dataTable = new DataTable();

                        dataAdapter.Fill(dataTable);

                        dgPendingPayment.AutoGenerateColumns = false;
                        dgPendingPayment.DataSource = dataTable;
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
                MessageBox.Show("Process Unsuccessful : " + txtID);
            }
        }
        private string MsgSms()
        {
            SqlConnection con = connection.my_conn();
            string message = "Default Message";

            try
            {
                con.Open();

                String get = "select message from getway where event=@Event";
                SqlCommand cmd = new SqlCommand(get, con);
                cmd.Parameters.AddWithValue("@Event", "PendingPayment");
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    message = result.ToString();
                    return message;
                }
                else
                {
                    return "none";
                }
            }
            catch (SqlException)
            {
                // Handle database exceptions here
                // Log the exception, throw, or handle it according to your needs
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return message;
        }
        private void SendSmsToGrid(string target, int totalRows)
        {
            try
            {
                int sentCount = 0;
                string messageTemplate = MsgSms();

                foreach (DataGridViewRow row in dgPendingPayment.Rows)
                {
                    if (row.IsNewRow) continue;

                    string name = Convert.ToString(row.Cells["sn"].Value);
                    string clas = Convert.ToString(row.Cells["c"].Value);
                    string month = Convert.ToString(row.Cells["Month"].Value);
                    string year = Convert.ToString(row.Cells["year"].Value);

                    string contact = target == "Students"
                        ? Convert.ToString(row.Cells["contact"].Value)
                        : Convert.ToString(row.Cells["Parent_No"].Value);

                    contact = contact.Trim();

                    // Validate contact number
                    if (string.IsNullOrWhiteSpace(contact) || contact.Length != 10 || !contact.All(char.IsDigit))
                    {
                        continue; // Skip invalid numbers
                    }

                    string message = messageTemplate.Replace("@Student", name)
                                                    .Replace("@Class", clas)
                                                    .Replace("@Month", month)
                                                    .Replace("@Year", year);

                    _ = connection.sms(contact, message);

                    sentCount++;

                    // Update ProgressBar safely on UI thread
                    Invoke((MethodInvoker)delegate
                    {
                        progressBar1.Value = (int)((sentCount / (float)totalRows) * 100);
                    });
                }


                Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show($"SMS sent to {target}(s) successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show("Error sending SMS: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
        }
        private void Send()
        {
            progressBar1.Value = 0;

            int totalRows = dgPendingPayment.Rows.Count-1;
            if (totalRows == 0)
            {
                MessageBox.Show("No records found to send SMS.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtSendTo.Text == "Both")
            {
                Thread studentThread = new Thread(() => SendSmsToGrid("Students", totalRows));
                Thread parentThread = new Thread(() => SendSmsToGrid("Parents", totalRows));
                studentThread.Start();
                parentThread.Start();
            }
            else if (txtSendTo.Text == "Students" || txtSendTo.Text == "Parents")
            {
                string target = txtSendTo.Text == "Students" ? "Students" : "Parents";
                Thread smsThread = new Thread(() => SendSmsToGrid(target, totalRows));
                smsThread.Start();
            }
            else
            {
                MessageBox.Show("Error in Send Selector!", "SMS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }
        private void LastSendDateApply()
        {
            SqlConnection con = connection.my_conn();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            try
            {
                string check = "select LastSendDate from data";
                SqlCommand cmd = new SqlCommand(check, con);
                SqlDataReader reader = cmd.ExecuteReader();

                string lDate = "";

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lDate = Convert.ToString(reader["LastSendDate"]);
                    }
                    reader.Close();
                    String update = "update data set LastSendDate=@date where LastSendDate=@ld";
                    SqlCommand cmdUpdate = new SqlCommand(update, con);
                    cmdUpdate.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmdUpdate.Parameters.AddWithValue("@ld", lDate);
                    cmdUpdate.ExecuteNonQuery();
                }
                else
                {
                    reader.Close();
                    String insert = "insert into data (LastSendDate) values (@date)";
                    SqlCommand cmdInsert = new SqlCommand(insert, con);
                    cmdInsert.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmdInsert.ExecuteNonQuery();
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

            LastSendDateGet();
        }
        private void LastSendDateGet()
        {
            SqlConnection con = connection.my_conn();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            String check = "select LastSendDate from data";
            SqlCommand cmd = new SqlCommand(check, con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string lDate = Convert.ToString(reader["LastSendDate"]);
                    lblSD.Text = "Last Sms Sent Date : " + lDate;
                }
                reader.Close();
            }
            else
            {
                reader.Close();
                lblSD.Text = "No Sms Sent Before";
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        private void pgPendingPayments_Load(object sender, EventArgs e)
        {
            LoadClass();
            LoadStudent();
            Filter();
            LastSendDateGet();
        }
        private void txtID_TextChanged(object sender, EventArgs e)
        {
            LoadStudent();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            Filter();
        }
        private void txtMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter();
        }
        private void txtClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter();
        }
        private void txtStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Send();
            LastSendDateApply();
        }
    }
}
