
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
    public partial class frmSubject : Form
    {
        Connection connection;
        public frmSubject()
        {
            InitializeComponent();
            connection = new Connection();
        }
        private string GetNextNum()
        {
            using (SqlConnection con = connection.my_conn())
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                string Get = "select MAX(id) from subject";
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
        bool add;
        void Clear()
        {
            txtID.Text = "";
            txtSubject.Text = "";
            txtType.Text = "";
        }
        void ReBtn()
        {
            btnNew.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
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
                        string selectQuery = "SELECT * FROM subject";

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con);
                        DataTable dataTable = new DataTable();

                        dataAdapter.Fill(dataTable);

                        dgSubject.AutoGenerateColumns = false;
                        dgSubject.DataSource = dataTable;
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
                MessageBox.Show("Process Unsuccessful : " + txtID);
            }
        }
        private void subject_Load(object sender, EventArgs e)
        {
            LoadToGrid();
            ReBtn();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            Clear();
            txtID.Enabled = false;
            txtID.Text = GetNextNum();

            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            add = true;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtID.Enabled = false;

            btnNew.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            add = false;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
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

                        string Del = "delete from subject where id=@id";
                        SqlCommand cmd = new SqlCommand(Del, con);
                        cmd.Parameters.AddWithValue("@id", txtID.Text);
                        cmd.ExecuteNonQuery();
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    if (add)
                    {
                        string insert = "insert into subject values (@id,@subject,@type)";
                        SqlCommand cmd = new SqlCommand(insert, con);
                        cmd.Parameters.AddWithValue("@id", txtID.Text);
                        cmd.Parameters.AddWithValue("@subject", txtSubject.Text);
                        cmd.Parameters.AddWithValue("@type", txtType.Text);
                        cmd.ExecuteNonQuery();

                        Clear();
                        txtID.Text = GetNextNum();
                    }
                    else
                    {
                        string update = "update subject set " +
                            "subject=@subject, type=@type where id=@id";
                        SqlCommand cmd = new SqlCommand(update, con);
                        cmd.Parameters.AddWithValue("@id", txtID.Text);
                        cmd.Parameters.AddWithValue("@subject", txtSubject.Text);
                        cmd.Parameters.AddWithValue("@type", txtType.Text);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Updated Subject Info : " + txtID.Text);
                    }

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

            LoadToGrid();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            ReBtn();
            txtID.Enabled = true;
        }
        private void dgSubject_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgSubject.RowCount)
                {
                    txtID.Text = dgSubject.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtSubject.Text = dgSubject.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtType.Text = dgSubject.Rows[e.RowIndex].Cells[2].Value.ToString();
                }
            }
            catch (Exception)
            {

            }
        }

    }
}
