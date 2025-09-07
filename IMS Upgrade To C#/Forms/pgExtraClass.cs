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
    public partial class pgExtraClass : Form
    {
        Connection connection;
        public pgExtraClass()
        {
            InitializeComponent();
            connection = new Connection();
        }
        private string GetNextNum()
        {
            SqlConnection con = connection.my_conn();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            string Get = "select MIN(id) from extra_class";
            SqlCommand cmd = new SqlCommand(Get, con);
            object result = cmd.ExecuteScalar();

            if (result != DBNull.Value)
            {
                int maxStid = Convert.ToInt32(result);
                int nextStid = maxStid - 1;

                // Format the integer as a string with leading zeros
                string formattedNextStid = nextStid.ToString("D6");
                return formattedNextStid;
            }
            else
            {
                return "999999"; // Default value
            }
        }

        bool add;
        void Clear()
        {
            txtID.Text = "";
            txtClass.Text = "";
            txtPrice.Text = "";
            txtGrade.Text = "";
            txtBatch.Text = "";
            // Picture.BackgroundImage = Image.FromFile("E:\\c++\\CMS\\CMS\\student.png");
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
                SqlConnection con = connection.my_conn();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                try
                {
                    string selectQuery = "SELECT * FROM extra_class";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con);
                    DataTable dataTable = new DataTable();

                    dataAdapter.Fill(dataTable);

                    dgClass.AutoGenerateColumns = true;
                    dgClass.DataSource = dataTable;

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
            catch (Exception)
            {
                MessageBox.Show("Process Unsucessfull : " + txtID);
            }
        }
        void LoadSubject()
        {
            try
            {
                SqlConnection con = connection.my_conn();
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
            catch (Exception)
            {
                MessageBox.Show("Process Unsucessfull : " + txtID);
            }
        }
        void LoadTeacher()
        {
            try
            {
                SqlConnection con = connection.my_conn();
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

                    txtTeacher.DataSource = dataTable;
                    txtTeacher.ValueMember = "id";
                    txtTeacher.DisplayMember = "name";

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
            catch (Exception)
            {
                MessageBox.Show("Process Unsucessfull : " + txtID);
            }
        }
        void LoadClass()
        {
            try
            {
                SqlConnection con = connection.my_conn();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                try
                {
                    string selectQuery = "SELECT id, class FROM class";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    txtLink.DataSource = dataTable;
                    txtLink.ValueMember = "id";
                    txtLink.DisplayMember = "class";
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
            catch (Exception e)
            {
                MessageBox.Show("Failed to load class data: " + e.Message);
            }
        }
        void Get_Sub_Details()
        {
            try
            {
                SqlConnection con = connection.my_conn();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                try
                {
                    string selectQuery = "SELECT subject,teacher,grade,batch,price FROM class where id='" + txtLink.SelectedValue + "'";
                    SqlCommand cmd = new SqlCommand(selectQuery, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            txtBatch.Text = reader["batch"].ToString();
                            txtPrice.Text = reader["price"].ToString();
                            txtGrade.Text = reader["grade"].ToString();
                            txtSubject.SelectedValue = reader["subject"].ToString();
                            txtTeacher.SelectedValue = reader["teacher"].ToString();
                        }
                        reader.Close();
                    }
                    else
                    {
                        reader.Close();
                    }
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
            catch (Exception e)
            {
                MessageBox.Show("Failed to load class data: " + e.Message);
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
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            add = false;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                MessageBox.Show("Enter Valid ID");
            }
            else
            {
                try
                {
                    SqlConnection con = connection.my_conn();
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    string Del = "delete from extra_class where id=@id";
                    SqlCommand cmd = new SqlCommand(Del, con);
                    cmd.Parameters.AddWithValue("id", txtID.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Deleted : " + txtID);
                    Clear();

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Process Unsucessfull : " + txtID);
                }
            }

            LoadToGrid();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = connection.my_conn();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                if (add)
                {
                    string insert = "insert into extra_class values (@id,@link,@class,@subject,@day,@price,@teacher,@grade,@batch,@start_time,@end_time,@att_time,@state)";
                    SqlCommand cmd = new SqlCommand(insert, con);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.Parameters.AddWithValue("@link", txtLink.SelectedValue);
                    cmd.Parameters.AddWithValue("@class", txtClass.Text);
                    cmd.Parameters.AddWithValue("@subject", txtSubject.SelectedValue);
                    cmd.Parameters.AddWithValue("@day", txtDay.Text);
                    cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                    cmd.Parameters.AddWithValue("@teacher", txtTeacher.SelectedValue);
                    cmd.Parameters.AddWithValue("@grade", txtGrade.Text);
                    cmd.Parameters.AddWithValue("@batch", txtBatch.Text);
                    cmd.Parameters.AddWithValue("@start_time", txtStart_time.Text);
                    cmd.Parameters.AddWithValue("@end_time", txtEnd_time.Text);
                    cmd.Parameters.AddWithValue("@att_time", txtAttendance_time.Text);
                    cmd.Parameters.AddWithValue("@state", "1");
                    cmd.ExecuteNonQuery();

                    Clear();
                    txtID.Text = GetNextNum();
                }
                else
                {
                    string update = "update extra_class set " +
                                    "link=@link,class=@class,subject=@subject,day=@day,price=@price,teacher=@teacher,grade=@grade,batch=@batch" +
                                    ",start_time=@start_time,end_time=@end_time,attendanceIn=@att_time where id=@id";
                    SqlCommand cmd = new SqlCommand(update, con);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.Parameters.AddWithValue("@link", txtLink.SelectedValue);
                    cmd.Parameters.AddWithValue("@class", txtClass.Text);
                    cmd.Parameters.AddWithValue("@subject", txtSubject.SelectedValue);
                    cmd.Parameters.AddWithValue("@day", txtDay.Text);
                    cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                    cmd.Parameters.AddWithValue("@teacher", txtTeacher.SelectedValue);
                    cmd.Parameters.AddWithValue("@grade", txtGrade.Text);
                    cmd.Parameters.AddWithValue("@batch", txtBatch.Text);
                    cmd.Parameters.AddWithValue("@start_time", txtStart_time.Text);
                    cmd.Parameters.AddWithValue("@end_time", txtEnd_time.Text);
                    cmd.Parameters.AddWithValue("@att_time", txtAttendance_time.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Updated Class Info : " + txtID.Text);
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
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
        private void txtLink_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_Sub_Details();
        }
        private void dgClass_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgClass.RowCount)
                {
                    txtID.Text = dgClass.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtLink.SelectedValue = dgClass.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtClass.Text = dgClass.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtSubject.SelectedValue = dgClass.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtDay.Text = dgClass.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtPrice.Text = dgClass.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtTeacher.SelectedValue = dgClass.Rows[e.RowIndex].Cells[6].Value.ToString();
                    txtGrade.Text = dgClass.Rows[e.RowIndex].Cells[7].Value.ToString();
                    txtBatch.Text = dgClass.Rows[e.RowIndex].Cells[8].Value.ToString();
                    txtStart_time.Text = dgClass.Rows[e.RowIndex].Cells[9].Value.ToString();
                    txtEnd_time.Text = dgClass.Rows[e.RowIndex].Cells[10].Value.ToString();
                    txtAttendance_time.Text = dgClass.Rows[e.RowIndex].Cells[11].Value.ToString();
                }
            }
            catch (Exception)
            {

            }
        }
        private void pgExtraClass_Load(object sender, EventArgs e)
        {
            try
            {
                LoadSubject();
            } catch (Exception) { }

            try
            {
                LoadTeacher();
            }
            catch (Exception) { }

            try
            {
                LoadClass();
            }
            catch (Exception) { }

            try
            {
                LoadToGrid();
            }
            catch (Exception) { }
        }

    }
}
