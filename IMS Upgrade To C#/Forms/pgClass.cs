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
    public partial class pgClass : Form
    {
        Connection connection;
        public pgClass()
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

                string Get = "select MAX(id) from class";
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
        void Clear()
        {
            txtID.Text = "";
            txtClass.Text = "";
            txtPrice.Text = "";
            txtGrade.Text = "";
            txtBatch.Text = "";
            Picture.BackgroundImage = Image.FromFile(Application.StartupPath + "\\UX\\.support\\.icon\\qr.png");
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
                        string selectQuery = "SELECT * FROM class where state = 1";

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con);
                        DataTable dataTable = new DataTable();

                        dataAdapter.Fill(dataTable);

                        dgClass.AutoGenerateColumns = false;
                        dgClass.DataSource = dataTable;
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
        void LoadSubject()
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
                        string selectQuery = "SELECT id,subject FROM subject";

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con);
                        DataTable dataTable = new DataTable();

                        dataAdapter.Fill(dataTable);

                        txtSubject.DataSource = dataTable;
                        txtSubject.ValueMember = "id";
                        txtSubject.DisplayMember = "subject";
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
        void LoadTeacher()
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
                        string selectQuery = "SELECT id,name FROM teacher";

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con);
                        DataTable dataTable = new DataTable();

                        dataAdapter.Fill(dataTable);

                        txtTeacher.DataSource = dataTable;
                        txtTeacher.ValueMember = "id";
                        txtTeacher.DisplayMember = "name";
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
        void Delete_Class()
        {
            string id = txtID.Text.Trim();

            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Enter Valid ID");
                return;
            }

            List<(string TableName, string ClassColumn)> deleteList = new List<(string, string)>
            {
                ("class", "id"),
                ("study", "class_id"),
                ("class_fees", "class_id"),
                ("Attendance", "class_id"),
                ("class_holiday", "class_id"),
                ("Extra_Attendance", "Link"),
                ("extra_class", "Link"),
                ("recept", "class_id")
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
                        string classColumn = deleteTriple.ClassColumn;

                        string removeQuery = $"DELETE FROM {tableName} WHERE {classColumn} = @cid";
                        using (SqlCommand cmd = new SqlCommand(removeQuery, con))
                        {
                            cmd.Parameters.AddWithValue("cid", txtID.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show($"Successfully Removed {txtClass.Text} from System !");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            LoadToGrid();
        }
        private void pgClass_Load(object sender, EventArgs e)
        {
            txtDay.SelectedIndex = 0;
            

            try
            {
                LoadSubject();
            }
            catch (Exception) { }

            try
            {
                LoadTeacher();
            }
            catch (Exception) { }

            try
            {
                LoadToGrid();
            }
            catch (Exception) { }

            ReBtn();
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
            Delete_Class();
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
                        string insert = "insert into class values (@id,@class,@subject,@day,@price,@teacher,@grade,@batch,@start_time,@end_time,@att_time,@image,@discount,@state)";
                        SqlCommand cmd = new SqlCommand(insert, con);
                        cmd.Parameters.AddWithValue("@id", txtID.Text);
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
                        cmd.Parameters.AddWithValue("@discount", "0");
                        cmd.Parameters.AddWithValue("@state", "1");

                        try
                        {
                            cmd.Parameters.AddWithValue("@image", connection.getphoto(image_path));
                        }
                        catch (Exception)
                        {
                            cmd.Parameters.AddWithValue("@image", connection.getphoto(Application.StartupPath + "\\UX\\.support\\.icon\\qr.png"));
                        }
                        cmd.ExecuteNonQuery();

                        Clear();
                        txtID.Text = GetNextNum();
                    }
                    else
                    {
                        string update = "update class set " +
                            "class=@class,subject=@subject,day=@day,price=@price,teacher=@teacher,grade=@grade,batch=@batch" +
                            ",start_time=@start_time,end_time=@end_time,attendanceIn=@att_time,image=@image where id=@id";
                        SqlCommand cmd = new SqlCommand(update, con);
                        cmd.Parameters.AddWithValue("@id", txtID.Text);
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

                        try
                        {
                            cmd.Parameters.AddWithValue("@image", connection.getphoto(image_path));
                        }
                        catch (Exception)
                        {
                            cmd.Parameters.AddWithValue("@image", connection.putphoto(Picture.BackgroundImage));
                        }
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Updated Class Info : " + txtID.Text);
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
            Picture.BackgroundImage = Image.FromFile(Application.StartupPath + "\\UX\\.support\\.icon\\qr.png");
        }
        private void dgClass_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                byte[] image = null;
                if (e.RowIndex >= 0 && e.RowIndex < dgClass.RowCount)
                {
                    txtID.Text = dgClass.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtClass.Text = dgClass.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtSubject.SelectedValue = dgClass.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtDay.Text = dgClass.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtPrice.Text = dgClass.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtTeacher.SelectedValue = dgClass.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtGrade.Text = dgClass.Rows[e.RowIndex].Cells[6].Value.ToString();
                    txtBatch.Text = dgClass.Rows[e.RowIndex].Cells[7].Value.ToString();
                    txtStart_time.Text = dgClass.Rows[e.RowIndex].Cells[8].Value.ToString();
                    txtEnd_time.Text = dgClass.Rows[e.RowIndex].Cells[9].Value.ToString();
                    txtAttendance_time.Text = dgClass.Rows[e.RowIndex].Cells[10].Value.ToString();

                    image = dgClass.Rows[e.RowIndex].Cells[11].Value as byte[];
                    if (image != null)
                    {
                        MemoryStream ms = new MemoryStream(image);
                        Picture.BackgroundImage = Image.FromStream(ms);
                    }
                    else
                    {
                        Picture.BackgroundImage = null;
                        Picture.BackgroundImage = Image.FromFile(Application.StartupPath + "\\UX\\.support\\.icon\\qr.png");
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        private void Picture_Click(object sender, EventArgs e)
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
        private void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.PageDown)
            {
                //frmAttendanceView aw = new frmAttendanceView("key");
                //aw->dothis();
            }
        }

    }
}
