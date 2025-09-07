using IMS_Upgraded_C_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS_Upgrade_To_C_
{
    public partial class frmSMS : Form
    {
        Connection connection;
        public frmSMS()
        {
            InitializeComponent();
            connection = new Connection();
        }

        void LoadEvent()
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
                        string selectQuery = "SELECT id,event FROM getway";

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con))
                        {
                            DataTable dataTable = new DataTable();
                            dataAdapter.Fill(dataTable);

                            eventTem.DataSource = dataTable;
                            eventTem.ValueMember = "id";
                            eventTem.DisplayMember = "event";

                            txtEvent.DataSource = dataTable;
                            txtEvent.ValueMember = "id";
                            txtEvent.DisplayMember = "event";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Process Unsuccessful: " + ex.Message);
            }
        }

        void getmessage(string evento)
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
                        string selectQuery = "SELECT message,state FROM getway WHERE event=@event";
                        using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@event", evento);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    messagesTem.Text = reader["message"].ToString();
                                    txtMessage.Text = reader["message"].ToString();
                                    txtState.Text = reader["state"].ToString();
                                }
                                else
                                {
                                    messagesTem.Text = "No message found for the selected event.";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Process Unsuccessful: " + ex.Message);
            }
        }

        void update()
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
                        string updateQuery = "update getway set message=@message, state=@state WHERE event=@event";
                        using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@event", eventTem.Text);
                            cmd.Parameters.AddWithValue("@message", eventTem.Text);
                            cmd.Parameters.AddWithValue("@state", eventTem.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Process Unsuccessful: " + ex.Message);
            }
        }

        void send()
        {
            _ = connection.sms(txtNum.Text.ToString(), txtMessage.Text.ToString());
            //MessageBox.Show(response, "API Response");
        }

        string GetBalance()
        {
            WebClient sms = new WebClient();
            sms.Headers.Add("accept", "application/json");
            sms.Headers.Add("authorization", "Bearer 1481|sm95wmZwh0n15VP75cTobJQfnaH5watlRZ3DnPkY"); // Updated API key

            try
            {
                byte[] responseData = sms.DownloadData("https://sms.send.lk/api/v3/balance");
                string responseString = Encoding.UTF8.GetString(responseData);

                // Parse JSON response
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("\"status\":\"(\\w+)\",\"data\":\"([^\"]+)\"");
                System.Text.RegularExpressions.Match match = regex.Match(responseString);

                if (match.Success && match.Groups.Count == 3 && match.Groups[1].Value == "success")
                {
                    string balanceInfo = match.Groups[2].Value;
                    return "SMS Unit Details: " + balanceInfo;
                }
                else
                {
                    return "Error: Invalid API response";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        string GetExpiry()
        {
            WebClient sms = new WebClient();
            sms.Headers.Add("accept", "application/json");
            sms.Headers.Add("authorization", "Bearer 1481|sm95wmZwh0n15VP75cTobJQfnaH5watlRZ3DnPkY");

            try
            {
                byte[] responseData = sms.DownloadData("https://sms.send.lk/api/v3/balance");
                string responseString = Encoding.UTF8.GetString(responseData);

                // Parse JSON response
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("\"remaining_unit\":\"(\\d+)\",\"expired_on\":\"([^\"]+)\"");
                System.Text.RegularExpressions.Match match = regex.Match(responseString);

                if (match.Success)
                {
                    string expiryDate = match.Groups[2].Value;
                    return "Sender Id Expiry Date is " + expiryDate + " , contact Developer a month before the expiry!";
                }
                else
                {
                    return "Error: Invalid API response";
                }
            }
            catch (WebException ex)
            {
                // Handle exception (if any) here
                //MessageBox.Show(ex.Message, "Error");
                return "Error: " + ex.Message;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Your code for button1_Click
        }

        private void frmSMS_Load(object sender, EventArgs e)
        {
            LoadEvent();
            getmessage(eventTem.Text.ToString());
        }

        private void eventTem_SelectedIndexChanged(object sender, EventArgs e)
        {
            getmessage(eventTem.Text.ToString());
        }

        private void txtEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            getmessage(eventTem.Text.ToString());
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Thread ts = new Thread(new ThreadStart(send));
            ts.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            update();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = await connection.GetSMSUnitDetails();
                textBox2.Text = await connection.GetSMSExpireDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
