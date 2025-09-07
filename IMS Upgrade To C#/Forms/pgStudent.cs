using IMS_Upgraded_C_;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS_Upgrade_To_C_
{
    public partial class pgStudent : Form
    {
        Connection connection;
        public pgStudent()
        {
            InitializeComponent();
            connection = new Connection();
        }

        // -------------------------------------------------------------------------------
        bool Search;
        bool add;
        string image_path;
        string Stid = "";
        new string Name = "";
        string month = DateTime.Now.ToString("MMMM");
        string year = DateTime.Now.ToString("yyyy");
        // -------------------------------------------------------------------------------

        public string GetNextNum()
        {
            SqlConnection con = connection.my_conn();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            string Get = "select MAX(id) from student";
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
        void Clear()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtAddress.Text = "";
            txtContact1.Text = "";
            txtComtactParent.Text = "";
            //txtGender.Text = "";
            txtRfid.Text = "";
            Picture.BackgroundImage = Image.FromFile(Application.StartupPath + "\\UX\\.Support\\.icon\\student.png");
        }
        void ReBtn()
        {
            btnNew.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            Search = true;
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
                    string selectQuery = "SELECT * FROM student";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con);
                    DataTable dataTable = new DataTable();

                    dataAdapter.Fill(dataTable);

                    dgStudent.AutoGenerateColumns = false;
                    dgStudent.DataSource = dataTable;

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
        string checksms()
        {
            SqlConnection con = connection.my_conn();
            string state = "InActive"; // Default value if the state is not found

            try
            {
                con.Open();

                string Get = "select state from getway where event=@Event";
                SqlCommand cmd = new SqlCommand(Get, con);
                cmd.Parameters.AddWithValue("@Event", "Welcome");
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    state = result.ToString();
                }
            }
            catch (SqlException)
            {

            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return state;
        }
        string Msgsms()
        {
            SqlConnection con = connection.my_conn();
            string message = "Default Message"; // Default value for message

            try
            {
                con.Open();

                string Get = "select message from getway where event=@Event";
                SqlCommand cmd = new SqlCommand(Get, con);
                cmd.Parameters.AddWithValue("@Event", "Welcome");
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    message = result.ToString();

                    // Replace the placeholder @ID with the actual student ID
                    message = message.Replace("@ID", Stid);
                    message = message.Replace("@student", Name);
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
        void send(string number)
        {
            string Status = checksms().Trim();
            string msg = Msgsms();
            if (Status == "Active")
            {
                _ = connection.sms(number, msg);
                //MessageBox.Show(response, "API Response");
            }

        }
        void search()
        {
            SqlConnection con = connection.my_conn();
            string selector = "select * from [Student] ";
            string filter = "";


            try
            {
                if (checkBox1.Checked == true)
                {
                    if (Search)
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        if (txtID.Text != "")
                        {
                            if (filter == "")
                            {
                                filter = "where id LIKE @Id";
                            }
                            else
                            {
                                filter += " and id LIKE @Id";
                            }
                        }

                        if (txtName.Text != "")
                        {
                            if (filter == "")
                            {
                                filter = "where Name LIKE @Name";
                            }
                            else
                            {
                                filter += " and Name LIKE @Name";
                            }
                        }

                        if (txtRfid.Text != "")
                        {
                            if (filter == "")
                            {
                                filter = "where rfid LIKE @Rfid";
                            }
                            else
                            {
                                filter += " and rfid LIKE @Rfid";
                            }
                        }

                        if (txtContact1.Text != "")
                        {
                            if (filter == "")
                            {
                                filter = "where contact LIKE @Contact";
                            }
                            else
                            {
                                filter += " and contact LIKE @Contact";
                            }
                        }

                        SqlCommand cmd = new SqlCommand(selector + filter, con);
                        cmd.Parameters.AddWithValue("@Id", "%" + txtID.Text + "%");
                        cmd.Parameters.AddWithValue("@Name", "%" + txtName.Text + "%");
                        cmd.Parameters.AddWithValue("@Contact", "%" + txtContact1.Text + "%");
                        cmd.Parameters.AddWithValue("@Rfid", "%" + txtRfid.Text + "%");

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();

                        dataAdapter.Fill(dataTable);

                        dgStudent.AutoGenerateColumns = false;
                        dgStudent.DataSource = dataTable;

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception) { }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                image_path = openFileDialog1.FileName;
                Image originalImage = Image.FromFile(image_path);
                int desiredWidth = 600;
                int desiredHeight = 600;
                Bitmap resizedImage = new Bitmap(originalImage, desiredWidth, desiredHeight);
                Picture.BackgroundImage = resizedImage;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            ReBtn();
            txtID.Enabled = true;
            Picture.BackgroundImage = Image.FromFile(Application.StartupPath + "\\UX\\.Support\\.icon\\student.png");
            LoadToGrid();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            Search = false;
            Clear();
            txtID.Enabled = false;
            txtID.Text = GetNextNum();

            btnEdit.Enabled = false;
            btnNew.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            add = true;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            Search = false;
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
            string id = txtID.Text.Trim();

            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Enter Valid ID");
                return;
            }

            var pairsList = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("student", "id"),
                new KeyValuePair<string, string>("study", "student_id"),
                new KeyValuePair<string, string>("class_fees", "student_id"),
                new KeyValuePair<string, string>("attendance", "student_id"),
                new KeyValuePair<string, string>("Extra_Attendance", "student_id")
            };

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete? This will delete student details, study class details, attendance details, class fees details, and all other related details from the system.",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.No)
            {
                return;
            }

            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    con.Open();

                    foreach (var pair in pairsList)
                    {
                        string table = pair.Key;
                        string column = pair.Value;

                        string delQuery = $"DELETE FROM {table} WHERE {column} = @id";
                        using (SqlCommand cmd = new SqlCommand(delQuery, con))
                        {
                            cmd.Parameters.AddWithValue("id", id);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show($"Successfully Deleted: {id}");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Process Unsuccessful: {id}\nError: {ex.Message}");
            }

            LoadToGrid();
        }
        private void pgStudent_Load(object sender, EventArgs e)
        {
            try
            {
                LoadToGrid();
            }
            catch (Exception) { }
            ReBtn();
            dgStudent.Focus();
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
                    string insert = "insert into student values (@id,@name,@address,@contact,@parent,@gender,@dob,@rfid,@image,@Month,@year,@idate, 'student.kchord.me', @Qr)";
                    SqlCommand cmd = new SqlCommand(insert, con);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@contact", txtContact1.Text);
                    cmd.Parameters.AddWithValue("@parent", txtComtactParent.Text);
                    cmd.Parameters.AddWithValue("@gender", txtGender.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@dob", txtDob.Text);
                    cmd.Parameters.AddWithValue("@rfid", txtRfid.Text);
                    cmd.Parameters.AddWithValue("@Month", month);
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.Parameters.AddWithValue("@iDate", "NULL");
                    try
                    {
                        cmd.Parameters.AddWithValue("@image", connection.getphoto(image_path));
                    }
                    catch (Exception)
                    {
                        cmd.Parameters.AddWithValue("@image", connection.getphoto(Application.StartupPath + "\\UX\\.Support\\.icon\\student.png"));
                    }
                    try
                    {
                        cmd.Parameters.AddWithValue("@Qr", connection.getphoto(connection.GenerateQRCode(txtID.Text.Trim())));
                    } catch (Exception)
                    {
                        MessageBox.Show("Qr Genaration Failed !", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    cmd.ExecuteNonQuery();

                    Stid = txtID.Text;
                    Name = txtName.Text;
                    send(txtContact1.Text);

                    Clear();
                    txtID.Text = GetNextNum();
                    txtName.Focus();
                }
                else
                {
                    string update = "update student set " +
                        "name=@name, address=@address, contact=@contact, parent=@parent, gender=@gender, dob=@dob, rfid=@rfid, image=@image where id=@id";
                    SqlCommand cmd = new SqlCommand(update, con);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@contact", txtContact1.Text);
                    cmd.Parameters.AddWithValue("@parent", txtComtactParent.Text);
                    cmd.Parameters.AddWithValue("@gender", txtGender.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@dob", txtDob.Text);
                    cmd.Parameters.AddWithValue("@rfid", txtRfid.Text);
                    try
                    {
                        cmd.Parameters.AddWithValue("@image", connection.getphoto(image_path));
                    }
                    catch (Exception)
                    {
                        cmd.Parameters.AddWithValue("@image", connection.putphoto(Picture.BackgroundImage));
                    }
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Updated Student Info : " + txtID.Text);
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
        private void dgStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                byte[] image = null;
                if (e.RowIndex >= 0 && e.RowIndex < dgStudent.RowCount)
                {
                    txtID.Text = dgStudent.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtName.Text = dgStudent.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtContact1.Text = dgStudent.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtComtactParent.Text = dgStudent.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtGender.Text = dgStudent.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtAddress.Text = dgStudent.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtDob.Text = dgStudent.Rows[e.RowIndex].Cells[6].Value.ToString();
                    txtRfid.Text = dgStudent.Rows[e.RowIndex].Cells[7].Value.ToString();

                    image = (byte[])dgStudent.Rows[e.RowIndex].Cells[8].Value;
                    if (image != null)
                    {
                        MemoryStream ms = new MemoryStream(image);
                        Picture.BackgroundImage = Image.FromStream(ms);
                    }
                    else
                    {
                        Picture.BackgroundImage = null;
                        Picture.BackgroundImage = Image.FromFile(Application.StartupPath + "\\UX\\.support\\.icon\\student.png");
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        private void pgStudent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                btnNew.PerformClick();
                txtName.Focus();
            }
            else if (e.KeyCode == Keys.Home)
            {
                btnEdit.PerformClick();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                btnSave.PerformClick();
            }
            else if (e.KeyCode == Keys.End)
            {
                btnCancel.PerformClick();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                btnDelete.PerformClick();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                pictureBox1_Click(sender, e);
            }
        }
        private void txtID_TextChanged(object sender, EventArgs e)
        {
            search();
        }
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            search();
        }
        private void txtContact1_TextChanged(object sender, EventArgs e)
        {
            search();
        }
        private void txtRfid_TextChanged(object sender, EventArgs e)
        {
            search();
        }

    }
}
