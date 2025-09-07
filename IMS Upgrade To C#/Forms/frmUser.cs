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
    public partial class frmUser : Form
    {
        Connection connection;
        public frmUser()
        {
            InitializeComponent();
            connection = new Connection();
        }
        public frmUser(string Make)
        {
            InitializeComponent();
            //
            txtMake.Text = Make;
            //
        }

        private string GetNextNum()
        {
            using (SqlConnection con = connection.my_conn())
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                string Get = "select MAX(id) from [user]";
                SqlCommand cmd = new SqlCommand(Get, con);
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    int maxStid = Convert.ToInt32(result);
                    int nextStid = maxStid + 1;

                    // Format the integer as a string with leading zeros
                    string formattedNextStid = nextStid.ToString("D4");
                    return formattedNextStid;
                }
                else
                {
                    return "0000"; // Default value
                }
            }
        }
        private void Clear()
        {
            txtID.Text = "";
            txtUsername.Text = "";
            // txtGender.Text = "";
        }
        private void ReBtn()
        {
            btnNew.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = true;
            btnSave.Enabled = true;
            // btnNew.Enabled = true;
        }
        private void LoadToGrid()
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    string selectQuery = "SELECT * FROM [user]";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con);
                    DataTable dataTable = new DataTable();

                    dataAdapter.Fill(dataTable);

                    dgUser.AutoGenerateColumns = false;
                    dgUser.DataSource = dataTable;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Process Unsuccessful : " + txtID);
            }
        }
        private void LoadAccessTree()
        {
            using (SqlConnection con = connection.my_conn())
            {
                try
                {
                    con.Open();

                    string check = "select * from UserRights where designation=@des";
                    SqlCommand cmd = new SqlCommand(check, con);
                    cmd.Parameters.AddWithValue("@des", accList.Text);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UpdateCheckBoxState("Home", "Users", reader["Users"].ToString());
                            UpdateCheckBoxState("Home", "Attendance View", reader["Attendance_View"].ToString());
                            UpdateCheckBoxState("Master", "Student", reader["Student"].ToString());
                            UpdateCheckBoxState("Master", "Teacher", reader["Teacher"].ToString());
                            UpdateCheckBoxState("Master", "Subject", reader["Subject"].ToString());
                            UpdateCheckBoxState("Master", "Class", reader["Class"].ToString());
                            UpdateCheckBoxState("Master", "Holidays", reader["Holidays"].ToString());
                            UpdateCheckBoxState("Master", "Sms", reader["Sms"].ToString());
                            UpdateCheckBoxState("Manage", "Study", reader["Study"].ToString());
                            UpdateCheckBoxState("Manage", "Attendance", reader["Attendance"].ToString());
                            UpdateCheckBoxState("Manage", "Dashboard", reader["Dashboard"].ToString());
                            UpdateCheckBoxState("Transaction", "Invoice", reader["Invoice"].ToString());
                            UpdateCheckBoxState("Transaction", "Teacher Payments", reader["Teacher_Payments"].ToString());
                            UpdateCheckBoxState("Transaction", "Pending Payment", reader["Pending_Payment"].ToString());
                            UpdateCheckBoxState("Report", "Student Report", reader["Student_Report"].ToString());
                            UpdateCheckBoxState("Report", "Class Report", reader["Class_Report"].ToString());
                            UpdateCheckBoxState("Report", "Attendance Report", reader["Attendance_Report"].ToString());
                            UpdateCheckBoxState("System", "Devices", reader["Devices"].ToString());
                            UpdateCheckBoxState("System", "Features", reader["Features"].ToString());
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }
        void UpdateCheckBoxState(string parentNodeText, string childNodeText, string valueFromDatabase)
        {
            foreach (TreeNode parentNode in treeView1.Nodes)
            {
                if (parentNode.Text == parentNodeText)
                {
                    foreach (TreeNode childNode in parentNode.Nodes)
                    {
                        if (childNode.Text == childNodeText)
                        {
                            childNode.Checked = (valueFromDatabase == "1");
                            break;
                        }
                    }
                    break;
                }
            }
        }
        void UpdateDatabaseWithCheckboxValues()
        {
            using (SqlConnection con = connection.my_conn())
            {
                try
                {
                    con.Open();

                    // Iterate through the TreeView nodes and update database columns based on checkbox states
                    foreach (TreeNode parentNode in treeView1.Nodes)
                    {
                        foreach (TreeNode childNode in parentNode.Nodes)
                        {
                            string columnName = childNode.Text.ToLower().Replace(" ", "_"); // Convert node text to column name
                            int checkboxValue = (childNode.Checked) ? 1 : 0;

                            // Update database column with checkbox value
                            string updateQuery = "UPDATE UserRights SET " + columnName + " = @value WHERE designation = @des";
                            using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                            {
                                cmd.Parameters.AddWithValue("@value", checkboxValue);
                                cmd.Parameters.AddWithValue("@des", accList.Text);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageBox.Show("Database updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void frmUser_Load(object sender, EventArgs e)
        {
            try
            {
                LoadToGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            LoadAccessTree();

            try
            {
                txtMake.Enabled = false;
                txtID.Text = "";
                txtID.Enabled = true;
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
                btnNew.Enabled = true;
            }
            catch (Exception) { }
        }
        private void accList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAccessTree();
        }
        private void access_btn_save_Click(object sender, EventArgs e)
        {
            UpdateDatabaseWithCheckboxValues();
        }
        private void btnNew_Click_1(object sender, EventArgs e)
        {
            txtID.Text = GetNextNum();
            txtID.Enabled = false;
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            btnNew.Enabled = false;
        }
        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                MessageBox.Show("Enter Valid ID");
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

                        string Del = "delete from [user] where id=@id";
                        using (SqlCommand cmd = new SqlCommand(Del, con))
                        {
                            cmd.Parameters.AddWithValue("@id", txtID.Text);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Successfully Deleted : " + txtID.Text);
                        Clear();

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Process Unsuccessful : " + txtID.Text);
                }
            }

            LoadToGrid();
        }
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    string insert = "insert into [user] values (@id,@username,@designation,@make,@password)";
                    using (SqlCommand cmd = new SqlCommand(insert, con))
                    {
                        cmd.Parameters.AddWithValue("@id", txtID.Text);
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@designation", txtDesignation.Text);
                        cmd.Parameters.AddWithValue("@make", txtMake.Text);
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                        cmd.ExecuteNonQuery();
                    }

                    Clear();
                    txtID.Text = GetNextNum();

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    LoadToGrid();
                }
            }
            catch (Exception) { }
        }
        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtID.Enabled = true;
            btnSave.Enabled = false;
            btnDelete.Enabled = true;
            btnNew.Enabled = true;
        }


    }
}
