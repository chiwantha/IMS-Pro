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
    public partial class pgStudy : Form
    {
        Connection connection;
        public pgStudy()
        {
            InitializeComponent();
            connection = new Connection();
        }
        string GetNextNum()
        {
            using (SqlConnection con = connection.my_conn())
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                string Get = "SELECT MAX(id) FROM study";
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
            } // The connection will be automatically closed and disposed here

        }
        bool add;
        bool Search;
        void Clear()
        {
            txtID.Text = "";
            //txtName.Text = "";
            //txtAddress.Text = "";
            //txtContact1.Text = "";
            //txtGender.Text = "";
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
                    string selectQuery = "SELECT id,StudentID,StudentName,ClassID,Class,card,contact FROM Vw_StudyStudentList where state = 1";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con);
                    DataTable dataTable = new DataTable();

                    dataAdapter.Fill(dataTable);

                    dgStudy.AutoGenerateColumns = false;
                    dgStudy.DataSource = dataTable;
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
                MessageBox.Show("Process Unsuccessful : " + txtID);
            }
        }
        void LoadClassPrice()
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
                    // Assuming txtClass is a ComboBox or DropDownList
                    string selectedId = txtClass.SelectedValue.ToString(); // Retrieve the selected ID

                    string selectQuery = "SELECT price FROM class WHERE id=@id";
                    SqlCommand cmd = new SqlCommand(selectQuery, con);
                    cmd.Parameters.AddWithValue("@id", selectedId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            txtPrice.Text = reader["price"].ToString();
                        }
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error fetching price: " + e.Message);
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to get price: " + e.Message);
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
                    string selectQuery = "SELECT id, class FROM class  where state = 1";
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
            catch (Exception e)
            {
                MessageBox.Show("Failed to load class data: " + e.Message);
            }
        }
        void LoadStudent()
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
                    string selectQuery = "SELECT id,name FROM Student WHERE id LIKE @id OR rfid LIKE @rfid OR contact LIKE @contact";
                    SqlCommand cmd = new SqlCommand(selectQuery, con);
                    cmd.Parameters.AddWithValue("@id", "%" + txtStudentID.Text + "%");
                    cmd.Parameters.AddWithValue("@rfid", "%" + txtStudentID.Text + "%");
                    cmd.Parameters.AddWithValue("@contact", "%" + txtStudentID.Text + "%");

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();

                    dataAdapter.Fill(dataTable);

                    txtStudent.DataSource = dataTable;
                    txtStudent.ValueMember = "id";
                    txtStudent.DisplayMember = "name";
                    //txtStudent.SelectedIndex = -1;

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
                MessageBox.Show("Process Unsuccessful : " + txtID);
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
                cmd.Parameters.AddWithValue("@Event", "Join to Class");
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
                cmd.Parameters.AddWithValue("@Event", "Join to Class");
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    message = result.ToString();

                    message = message.Replace("@student", txtStudent.Text);
                    message = message.Replace("@class", txtClass.Text);
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

            return message;
        }
        string GetContactNum()
        {
            SqlConnection con = connection.my_conn();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            string Get = "select contact from student where id=@id";
            SqlCommand cmd = new SqlCommand(Get, con);
            cmd.Parameters.AddWithValue("@id", txtStudent.SelectedValue);
            object result = cmd.ExecuteScalar();

            if (result != DBNull.Value)
            {
                string number = result.ToString();

                return number;
            }
            else
            {
                return "0788806670"; // Default value
            }
        }
        void send()
        {
            string Status = checksms().Trim();
            string msg = Msgsms();
            string Number = GetContactNum();
            if (Status == "Active")
            {
                _ = connection.sms(Number, msg);
                //MessageBox.Show(response, "API Response");
            }
        }
        void search()
        {
            SqlConnection con = connection.my_conn();
            string selector = "select id,StudentID,StudentName,ClassID,Class,card,contact from [Vw_StudyStudentList] ";
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

                        if (txtStudentID.Text != "")
                        {
                            if (filter == "")
                            {
                                filter = "where StudentID LIKE @Id";
                            }
                            else
                            {
                                filter += " and StudentID LIKE @Id";
                            }
                        }

                        if (txtStudent.Text != "")
                        {
                            if (filter == "")
                            {
                                filter = "where StudentName LIKE @Name";
                            }
                            else
                            {
                                filter += " and StudentName LIKE @Name";
                            }
                        }

                        if (txtStudentID.Text != "")
                        {
                            if (filter == "")
                            {
                                filter = "where Rfid LIKE @Rfid";
                            }
                            else
                            {
                                filter += " and Rfid LIKE @Rfid";
                            }
                        }

                        if (txtStudentID.Text != "")
                        {
                            if (filter == "")
                            {
                                filter = "where Contact LIKE @Contact";
                            }
                            else
                            {
                                filter += " and Contact LIKE @Contact";
                            }
                        }

                        if (txtClass.Text != "")
                        {
                            if (filter == "")
                            {
                                filter = "where classID LIKE @class";
                            }
                            else
                            {
                                filter += " and classID LIKE @class";
                            }
                        }

                        if (txtCard.Text != "")
                        {
                            if (filter == "")
                            {
                                filter = "where card LIKE @card";
                            }
                            else
                            {
                                filter += " and card LIKE @card";
                            }
                        }

                        SqlCommand cmd = new SqlCommand(selector + filter, con);
                        cmd.Parameters.AddWithValue("@Id", "%" + txtStudentID.Text + "%");
                        cmd.Parameters.AddWithValue("@Name", "%" + txtStudent.Text + "%");
                        cmd.Parameters.AddWithValue("@Contact", "%" + txtStudentID.Text + "%");
                        cmd.Parameters.AddWithValue("@Rfid", "%" + txtStudentID.Text + "%");
                        cmd.Parameters.AddWithValue("@Class", "%" + txtClass.SelectedValue + "%");
                        cmd.Parameters.AddWithValue("@card", "%" + txtCard.Text + "%");

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();

                        dataAdapter.Fill(dataTable);

                        dgStudy.AutoGenerateColumns = false;
                        dgStudy.DataSource = dataTable;

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception) { }
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCard.Text != "")
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
                            string insert = "INSERT INTO study VALUES (@id, @stid, @student, @classid, @date, @card)";
                            using (SqlCommand cmd = new SqlCommand(insert, con))
                            {
                                cmd.Parameters.AddWithValue("@id", txtID.Text);
                                cmd.Parameters.AddWithValue("@stid", txtStudent.SelectedValue);
                                cmd.Parameters.AddWithValue("@student", txtStudent.Text);
                                cmd.Parameters.AddWithValue("@classid", txtClass.SelectedValue);
                                cmd.Parameters.AddWithValue("@date", txtED.Text);
                                cmd.Parameters.AddWithValue("@card", txtCard.Text);
                                cmd.ExecuteNonQuery();
                            }

                            try
                            {
                                Thread ts = new Thread(new ThreadStart(send));
                                ts.Start();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Student Added Successfully !");
                            }

                            Clear();
                            txtID.Text = GetNextNum();
                        }
                        else
                        {
                            string update = "UPDATE study SET " +
                                "student_id=@stid, student_name=@student, class_id=@classid, enroll_date=@date, card=@card WHERE id=@id";
                            using (SqlCommand cmd = new SqlCommand(update, con))
                            {
                                cmd.Parameters.AddWithValue("@id", txtID.Text);
                                cmd.Parameters.AddWithValue("@stid", txtStudent.SelectedValue);
                                cmd.Parameters.AddWithValue("@student", txtStudent.Text);
                                cmd.Parameters.AddWithValue("@classid", txtClass.SelectedValue);
                                cmd.Parameters.AddWithValue("@date", txtED.Text);
                                cmd.Parameters.AddWithValue("@card", txtCard.Text);
                                cmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("Updated Study Info : " + txtID.Text);
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
            }
            else
            {
                MessageBox.Show("Please Check The Card Plan Again!", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            }

            LoadToGrid();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtID.Text.Trim();

            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Enter Valid ID");
                return;
            }

            List<(string TableName, string StudentColumn, string ClassColumn)> deleteList = new List<(string, string, string)>
            {
                ("study", "student_id", "class_id"),
                ("attendance", "student_id", "class_id"),
                ("class_fees", "student_id", "class_id"),
                ("Extra_Attendance", "student_id", "class_id")
            };

            // Confirmation dialog
            string tablesToDelete = string.Join(", ", deleteList.Select(t => t.TableName));
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete? This will delete records from the following tables: {tablesToDelete}",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.No)
            {
                return; // User clicked No, so cancel the deletion
            }

            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    con.Open();

                    foreach (var deleteTriple in deleteList)
                    {
                        string tableName = deleteTriple.TableName;
                        string studentColumn = deleteTriple.StudentColumn;
                        string classColumn = deleteTriple.ClassColumn;

                        string removeQuery = $"DELETE FROM {tableName} WHERE {studentColumn} = @stid AND {classColumn} = @cid";
                        using (SqlCommand cmd = new SqlCommand(removeQuery, con))
                        {
                            cmd.Parameters.AddWithValue("stid", txtStudent.SelectedValue);
                            cmd.Parameters.AddWithValue("cid", txtClass.SelectedValue);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show($"Successfully Removed {txtStudent.Text} from {txtClass.Text}");
                    Clear();
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
        private void pgStudy_Load(object sender, EventArgs e)
        {
            try
            {
                LoadToGrid();
            }
            catch (Exception )
            {
                // Handle the exception
            }

            try
            {
                LoadClass();
            }
            catch (Exception)
            {
                // Handle the exception
            }

            try
            {
                LoadStudent();
            }
            catch (Exception)
            {
                // Handle the exception
            }

            try
            {
                LoadClassPrice();
            }
            catch (Exception)
            {
                // Handle the exception
            }

            ReBtn();
        }
        private void txtStudent_TextChanged(object sender, EventArgs e)
        {
            search();
        }
        private void dgStudy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgStudy.RowCount)
                {
                    txtID.Text = dgStudy.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtStudent.SelectedValue = dgStudy.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtClass.SelectedValue = dgStudy.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtCard.Text = dgStudy.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtED.Text = dgStudy.Rows[e.RowIndex].Cells[6].Value.ToString();
                }
            }
            catch (Exception)
            {
                // Handle the exception
            }
        }
        private void txtClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadClassPrice();
            search();
        }
        private void qr_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = connection.my_conn())
            {
                byte[] imageBytes;

                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                try
                {
                    string query = "SELECT image FROM class WHERE id=" + txtClass.SelectedValue;
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                imageBytes = (byte[])reader["image"];
                                frmQrViewer fw = new frmQrViewer(imageBytes);
                                fw.Show(this);
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
        private void txtStudentID_TextChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                LoadStudent();
            }
            search();
        }
        private void txtCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            search();
        }
        private void txtCard_TextChanged(object sender, EventArgs e)
        {
            search();
        }
        private void txtClass_TextChanged(object sender, EventArgs e)
        {
            search();
        }
    }
}
