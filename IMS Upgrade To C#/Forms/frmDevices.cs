using IMS_Upgraded_C_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace IMS_Upgrade_To_C_
{
    public partial class frmDevices : Form
    {
        Connection connection;
        public frmDevices()
        {
            InitializeComponent();
            connection = new Connection();
        }

        void SelectD()
        {
            if (rbNetwork.Checked)
            {
                txtIP.Enabled = true;
                txtPort.Enabled = false;
                txtBoardRate.Enabled = false;
            }
            else
            {
                txtIP.Enabled = false;
                txtPort.Enabled = true;
                txtBoardRate.Enabled = true;
            }
        }

        string GetNextNum()
        {
            using (SqlConnection con = connection.my_conn())
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                string Get = "select MAX(id) from device";
                using (SqlCommand cmd = new SqlCommand(Get, con))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        int maxStid = Convert.ToInt32(result);
                        int nextStid = maxStid + 1;

                        // Format the integer as a string with leading zeros
                        string formattedNextStid = nextStid.ToString("D3");
                        return formattedNextStid;
                    }
                    else
                    {
                        return "000"; // Default value
                    }
                }
            }
        }

        void LoadToGrid()
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
                        string selectQuery = "SELECT * FROM device";

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con))
                        {
                            DataTable dataTable = new DataTable();
                            dataAdapter.Fill(dataTable);

                            dgList.AutoGenerateColumns = false;
                            dgList.DataSource = dataTable;
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
            catch (Exception)
            {

            }
        }

        private void frmDevices_Load(object sender, EventArgs e)
        {
            try
            {
                string[] portNames = SerialPort.GetPortNames();
                foreach (string portName in portNames)
                {
                    txtPort.Items.Add(portName);
                    txtPort.SelectedIndex = 0;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving serial port names: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            txtBoardRate.SelectedIndex = 0;
            SelectD();
            LoadToGrid();
            txtName.SelectedIndex = 0;
        }

        private void rbNetwork_CheckedChanged(object sender, EventArgs e)
        {
            SelectD();
        }

        private void rbSerial_CheckedChanged(object sender, EventArgs e)
        {
            SelectD();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (rbSerial.Checked)
            {
                if (string.IsNullOrEmpty(txtPort.Text) || string.IsNullOrEmpty(txtBoardRate.Text) || string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("Enter The Correct Com & Board Rate");
                }
                else
                {
                    try
                    {
                        using (SqlConnection con = connection.my_conn())
                        {
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }

                            string insert = "insert into device values (@id,@rate,@com,@name)";
                            using (SqlCommand cmd = new SqlCommand(insert, con))
                            {
                                cmd.Parameters.AddWithValue("@id", GetNextNum());
                                cmd.Parameters.AddWithValue("@rate", txtBoardRate.Text);
                                cmd.Parameters.AddWithValue("@com", txtPort.Text);
                                cmd.Parameters.AddWithValue("@name", txtName.Text);
                                cmd.ExecuteNonQuery();
                            }

                            LoadToGrid();

                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtIP.Text) || string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("Enter Valid Ip or Name");
                }
                else
                {
                    try
                    {
                        using (SqlConnection con = connection.my_conn())
                        {
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }

                            string insert = "insert into device values (@id,@rate,@com,@name)";
                            using (SqlCommand cmd = new SqlCommand(insert, con))
                            {
                                cmd.Parameters.AddWithValue("@id", GetNextNum());
                                cmd.Parameters.AddWithValue("@rate", "NULL");
                                cmd.Parameters.AddWithValue("@com", txtIP.Text);
                                cmd.Parameters.AddWithValue("@name", txtName.Text);
                                cmd.ExecuteNonQuery();
                            }

                            LoadToGrid();

                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    string Del = "delete from device where com=@com";
                    using (SqlCommand cmd = new SqlCommand(Del, con))
                    {
                        cmd.Parameters.AddWithValue("@com", txtPort.Text);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Successfully Deleted : " + txtName.Text);
                    txtName.Text = "";

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Process Unsuccessful : " + txtName.Text);
            }

            LoadToGrid();
        }

        private void dgList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgList.RowCount)
                {
                    txtBoardRate.Text = dgList.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtPort.Text = dgList.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtName.Text = dgList.Rows[e.RowIndex].Cells[3].Value.ToString();
                }
            }
            catch (Exception)
            {

            }
        }

    }
}
