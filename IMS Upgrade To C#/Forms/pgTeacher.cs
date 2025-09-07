using IMS_Upgraded_C_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS_Upgrade_To_C_
{
    public partial class pgTeacher : Form
    {
        Connection connection;
        public pgTeacher()
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

                string Get = "select MAX(id) from teacher";
                SqlCommand cmd = new SqlCommand(Get, con);
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    int maxStid = Convert.ToInt32(result);
                    int nextStid = maxStid + 1;

                    // Format the integer as a string with leading zeros
                    string formattedNextStid = nextStid.ToString("D6");
                    return formattedNextStid;
                }
                else
                {
                    return "000000"; // Default value
                }
            }
        }

        bool add;
        string image_path;

        private void Clear()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtAddress.Text = "";
            txtContact1.Text = "";
            txtContact2.Text = "";
            txtEmail.Text = "";
            image_path = "";
            Picture.BackgroundImage = Image.FromFile(Application.StartupPath + "\\UX\\.Support\\.icon\\teacher.png");
        }

        private void ReBtn()
        {
            btnNew.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
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

                    try
                    {
                        string selectQuery = "SELECT * FROM teacher";

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con);
                        DataTable dataTable = new DataTable();

                        dataAdapter.Fill(dataTable);

                        dgTeacher.AutoGenerateColumns = false;
                        dgTeacher.DataSource = dataTable;
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
                MessageBox.Show("Process Unsuccessful: " + txtID);
            }
        }

        private void LoadSubject()
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

                        txtSubject.DataSource = dataTable;
                        txtSubject.ValueMember = "id";
                        txtSubject.DisplayMember = "subject";
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
                MessageBox.Show("Process Unsuccessful: " + txtID);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Clear();
            txtID.Enabled = false;
            txtID.Text = GetNextNum();

            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            add = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtID.Enabled = false;

            btnNew.Enabled = false;
            btnEdit.Enabled = false;
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

                        string Del = "delete from teacher where id=@id";
                        SqlCommand cmd = new SqlCommand(Del, con);
                        cmd.Parameters.AddWithValue("id", txtID.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully Deleted: " + txtID.Text);
                        Clear();

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Process Unsuccessful: " + txtID.Text);
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
                        string insert = "insert into teacher values (@id,@name,@subject,@contact,@tele,@email,@address,@image)";
                        SqlCommand cmd = new SqlCommand(insert, con);
                        cmd.Parameters.AddWithValue("@id", txtID.Text);
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@subject", txtSubject.SelectedValue);
                        cmd.Parameters.AddWithValue("@contact", txtContact1.Text);
                        cmd.Parameters.AddWithValue("@tele", txtContact2.Text);
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        try
                        {
                            cmd.Parameters.AddWithValue("@image", connection.getphoto(image_path));
                        }
                        catch (Exception)
                        {
                            cmd.Parameters.AddWithValue("@image", connection.getphoto(Application.StartupPath + "\\UX\\.Support\\.icon\\teacher.png"));
                        }
                        cmd.ExecuteNonQuery();

                        Clear();
                        txtID.Text = GetNextNum();
                    }
                    else
                    {
                        string update = "update teacher set " +
                            "name=@name,subject=@subject,contact=@contact,telepone=@tele,email=@email,address=@address,image=@image where id=@id";
                        SqlCommand cmd = new SqlCommand(update, con);
                        cmd.Parameters.AddWithValue("@id", txtID.Text);
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@subject", txtSubject.SelectedValue);
                        cmd.Parameters.AddWithValue("@contact", txtContact1.Text);
                        cmd.Parameters.AddWithValue("@tele", txtContact2.Text);
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        try
                        {
                            cmd.Parameters.AddWithValue("@image", connection.getphoto(image_path));
                        }
                        catch (Exception)
                        {
                            cmd.Parameters.AddWithValue("@image", connection.putphoto(Picture.BackgroundImage));
                        }
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Updated Instructor Info: " + txtID.Text);
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
            Picture.BackgroundImage = Image.FromFile(Application.StartupPath + "\\UX\\.Support\\.icon\\teacher.png");
        }

        private void Picture_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                image_path = openFileDialog1.FileName;
                Picture.BackgroundImage = Image.FromFile(image_path);
            }
        }

        private void dgTeacher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                byte[] image = null;
                if (e.RowIndex >= 0 && e.RowIndex < dgTeacher.RowCount)
                {
                    txtID.Text = dgTeacher.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtName.Text = dgTeacher.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtSubject.SelectedValue = dgTeacher.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtContact1.Text = dgTeacher.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtContact2.Text = dgTeacher.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtEmail.Text = dgTeacher.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtAddress.Text = dgTeacher.Rows[e.RowIndex].Cells[6].Value.ToString();

                    image = (byte[])dgTeacher.Rows[e.RowIndex].Cells[7].Value;
                    if (image != null)
                    {
                        MemoryStream ms = new MemoryStream(image);
                        Picture.BackgroundImage = Image.FromStream(ms);
                    }
                    else
                    {
                        Picture.BackgroundImage = null;
                        Picture.BackgroundImage = Image.FromFile(Application.StartupPath + "\\UX\\.Support\\.icon\\teacher.png");
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void pgTeacher_Load(object sender, EventArgs e)
        {
            try
            {
                LoadToGrid();
            }
            catch (Exception) { }

            try
            {
                LoadSubject();
            }
            catch (Exception) { }
            ReBtn();
        }

        private void pgTeacher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnNew.PerformClick();
                txtName.Focus();
            }
            else if (e.KeyCode == Keys.F6)
            {
                btnEdit.PerformClick();
            }
            else if (e.KeyCode == Keys.F7)
            {
                btnSave.PerformClick();
            }
            else if (e.KeyCode == Keys.F8)
            {
                btnCancel.PerformClick();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                btnDelete.PerformClick();
            }
        }

    }
}
