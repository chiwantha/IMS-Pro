using IMS_Upgraded_C_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace IMS_Upgrade_To_C_
{
    public partial class frmManualAttendance : Form
    {
        Connection connection;
        public frmManualAttendance()
        {
            InitializeComponent();
            connection = new Connection();
        }

        string Link = "";

        private string GetNextNum()
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    string getQuery = "SELECT MAX(id) FROM Attendance";
                    SqlCommand cmd = new SqlCommand(getQuery, con);
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        int maxStid = Convert.ToInt32(result);
                        int nextStid = maxStid + 1;

                        // Format the integer as a string with leading zeros
                        string formattedNextStid = nextStid.ToString("D10");
                        return formattedNextStid;
                    }
                    else
                    {
                        return "0000000000"; // Default value
                    }
                }
            }
            catch (Exception)
            {
                // Handle exceptions as needed
                return "0000000000"; // Default value
            }
        }

        private string GetNextNumExtra()
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    string getQuery = "SELECT MIN(id) FROM Extra_Attendance";
                    SqlCommand cmd = new SqlCommand(getQuery, con);
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        long maxStid = Convert.ToInt64(result);
                        long nextStid = maxStid - 1;
                        string formattedNextStid = nextStid.ToString("D10");
                        return formattedNextStid;
                    }
                    else
                    {
                        return "9999999999"; // Default value
                    }
                }
            }
            catch (Exception)
            {
                // Handle exceptions as needed
                return "9999999999"; // Default value
            }
        }

        private string GetNextNumfee()
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    string getQuery = "SELECT MAX(id) FROM class_fees";
                    SqlCommand cmd = new SqlCommand(getQuery, con);
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        int maxStid = Convert.ToInt32(result);
                        int nextStid = maxStid + 1;
                        string formattedNextStid = nextStid.ToString("D10");
                        return formattedNextStid;
                    }
                    else
                    {
                        return "0000000000"; // Default value
                    }
                }
            }
            catch (Exception)
            {
                // Handle exceptions as needed
                return "0000000000"; // Default value
            }
        }

        private void Mark()
        {
            string Student_ID = "";
            byte[] image;
            string State;
            string Class_ID = "";
            string Student_Name = "";
            string In_Time = "";
            string Out_Time = "";
            string Class_Name = "";

            DateTime now = DateTime.Now;
            string day = dtpDate.Value.ToString("dddd");
            string yearr = dtpDate.Value.ToString("yyyy");
            string month = dtpDate.Value.ToString("MMMM");
            string date = dtpDate.Value.ToString("yyyy-MM-dd");
            int weekOfMonth = (Convert.ToInt32(dtpDate.Value.ToString("dd")) - 1) / 7 + 1;

            using (SqlConnection con = connection.my_conn())
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                //----------------------------------------------------------------------------------------------------------------
                string IID = txtStudent.SelectedValue.ToString().Trim();

                if (IID.Length == 6)
                {
                    try
                    {
                        string search = "select student_id, student_name, class_id, class_name, image, in_time, out_time, link from Vw_Attendance_Check where " +
                                        "student_id=@rfid and day=@day and in_time<=@time and out_time>=@time";
                        SqlCommand cmd = new SqlCommand(search, con);
                        cmd.Parameters.AddWithValue("@rfid", txtStudent.SelectedValue);
                        cmd.Parameters.AddWithValue("@day", day);
                        cmd.Parameters.AddWithValue("@time", time.Text);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Student_ID = reader["student_id"].ToString();
                                Student_Name = reader["student_name"].ToString();
                                Class_ID = reader["class_id"].ToString();
                                Class_Name = reader["class_name"].ToString();
                                In_Time = reader["in_time"].ToString();
                                Out_Time = reader["out_time"].ToString();
                                Link = reader["link"].ToString();
                                image = reader["image"] as byte[];

                                Back.BackColor = Color.Green;
                                lblShow_Student_ID.Text = Student_ID;
                                lblShow_Class_Name.Text = Class_Name;
                                lblShow_Student_Name.Text = Student_Name;

                                if (image != null)
                                {
                                    using (MemoryStream ms = new MemoryStream(image))
                                    {
                                        Picture.BackgroundImage = Image.FromStream(ms);
                                    }
                                }
                                else
                                {
                                    Picture.BackgroundImage = null;
                                    Picture.BackgroundImage = Image.FromFile(Application.StartupPath + "\\UX\\.support\\.icon\\student.png");
                                }
                            }

                            reader.Close();

                            string check = "select class_id, date, state from class_holiday where class_id=@class_id and date=@date";
                            SqlCommand cmd2 = new SqlCommand(check, con);
                            cmd2.Parameters.AddWithValue("@class_id", Class_ID);
                            cmd2.Parameters.AddWithValue("@date", date);
                            SqlDataReader reader2 = cmd2.ExecuteReader();

                            if (reader2.HasRows)
                            {
                                while (reader2.Read())
                                {
                                    Class_ID = reader2["class_id"].ToString();
                                    State = reader2["state"].ToString();

                                    lblShow_Stats.Text = State;
                                    Back.BackColor = Color.Red;
                                }

                                reader2.Close();
                            }
                            else
                            {
                                if (Link == "")
                                {
                                    reader2.Close();

                                    string InsertQuery = "select id from attendance where class_id=@class_id and student_id=@student_id and month=@month and year=@year";
                                    SqlCommand cmd3 = new SqlCommand(InsertQuery, con);
                                    cmd3.Parameters.AddWithValue("@student_id", lblShow_Student_ID.Text);
                                    cmd3.Parameters.AddWithValue("@class_id", Class_ID);
                                    cmd3.Parameters.AddWithValue("@year", yearr);
                                    cmd3.Parameters.AddWithValue("@month", month);
                                    SqlDataReader reader3 = cmd3.ExecuteReader();

                                    if (reader3.HasRows)
                                    {
                                        reader3.Close();

                                        string InsertQuery2 = "select id from attendance where class_id=@class_id and student_id=@student_id and month=@month and week" + weekOfMonth + "=@week and year=@year";
                                        SqlCommand cmd4 = new SqlCommand(InsertQuery2, con);
                                        cmd4.Parameters.AddWithValue("@student_id", lblShow_Student_ID.Text);
                                        cmd4.Parameters.AddWithValue("@class_id", Class_ID);
                                        cmd4.Parameters.AddWithValue("@month", month);
                                        cmd4.Parameters.AddWithValue("@week", "1");
                                        cmd4.Parameters.AddWithValue("@year", yearr);
                                        SqlDataReader reader4 = cmd4.ExecuteReader();

                                        if (!reader4.HasRows)
                                        {
                                            reader4.Close();

                                            string InsertQuery3 = "update attendance set week" + weekOfMonth + "=1 where class_id=@class_id and student_id=@student_id and month=@month and year=@year";
                                            SqlCommand cmd5 = new SqlCommand(InsertQuery3, con);
                                            cmd5.Parameters.AddWithValue("@student_id", lblShow_Student_ID.Text);
                                            cmd5.Parameters.AddWithValue("@class_id", Class_ID);
                                            cmd5.Parameters.AddWithValue("@month", month);
                                            cmd5.Parameters.AddWithValue("@year", yearr);
                                            cmd5.ExecuteNonQuery();

                                            lblShow_Stats.Text = "Ok !";
                                            MakeFeeRecords(lblShow_Student_ID.Text, Class_ID, LoadClassPrice(Class_ID));

                                            try
                                            {
                                                Thread ts = new Thread(new ThreadStart(this.Send));
                                                ts.Start();
                                            }
                                            catch (Exception) { }
                                        }
                                        else
                                        {
                                            lblShow_Stats.Text = "Already Marked";
                                            MakeFeeRecords(lblShow_Student_ID.Text, Class_ID, LoadClassPrice(Class_ID));
                                        }
                                    }
                                    else
                                    {
                                        reader3.Close();

                                        string InsertQuery4 = "Insert into attendance (id,student_id,class_id,time,date,month,year,week" + weekOfMonth + ") values (@id,@student_id,@class_id,@time,@date,@month,@year,@week)";
                                        SqlCommand cmd6 = new SqlCommand(InsertQuery4, con);
                                        cmd6.Parameters.AddWithValue("@id", GetNextNum());
                                        cmd6.Parameters.AddWithValue("@student_id", lblShow_Student_ID.Text);
                                        cmd6.Parameters.AddWithValue("@class_id", Class_ID);
                                        cmd6.Parameters.AddWithValue("@time", time.Text);
                                        cmd6.Parameters.AddWithValue("@date", date);
                                        cmd6.Parameters.AddWithValue("@month", month);
                                        cmd6.Parameters.AddWithValue("@week", "1");
                                        cmd6.Parameters.AddWithValue("@year", yearr);
                                        cmd6.ExecuteNonQuery();

                                        lblShow_Stats.Text = "Ok !";
                                        MakeFeeRecords(lblShow_Student_ID.Text, Class_ID, LoadClassPrice(Class_ID));
                                    }
                                }
                                else
                                {
                                    reader2.Close();

                                    string InsertQuery = "select id from Extra_Attendance where class_id=@class_id and student_id=@student_id and month=@month and year=@year";
                                    SqlCommand cmd7 = new SqlCommand(InsertQuery, con);
                                    cmd7.Parameters.AddWithValue("@student_id", lblShow_Student_ID.Text);
                                    cmd7.Parameters.AddWithValue("@class_id", Class_ID);
                                    cmd7.Parameters.AddWithValue("@year", yearr);
                                    cmd7.Parameters.AddWithValue("@month", month);
                                    SqlDataReader reader7 = cmd7.ExecuteReader();

                                    if (reader7.HasRows)
                                    {
                                        reader7.Close();

                                        string InsertQuery5 = "select id from Extra_Attendance where class_id=@class_id and student_id=@student_id and month=@month and week" + weekOfMonth + "=@week and year=@year";
                                        SqlCommand cmd8 = new SqlCommand(InsertQuery5, con);
                                        cmd8.Parameters.AddWithValue("@student_id", lblShow_Student_ID.Text);
                                        cmd8.Parameters.AddWithValue("@class_id", Class_ID);
                                        cmd8.Parameters.AddWithValue("@month", month);
                                        cmd8.Parameters.AddWithValue("@week", "1");
                                        cmd8.Parameters.AddWithValue("@year", yearr);
                                        SqlDataReader reader8 = cmd8.ExecuteReader();

                                        if (!reader8.HasRows)
                                        {
                                            reader8.Close();

                                            string InsertQuery9 = "update Extra_Attendance set week" + weekOfMonth + "=1 where class_id=@class_id and student_id=@student_id and month=@month and year=@year";
                                            SqlCommand cmd9 = new SqlCommand(InsertQuery9, con);
                                            cmd9.Parameters.AddWithValue("@student_id", lblShow_Student_ID.Text);
                                            cmd9.Parameters.AddWithValue("@class_id", Class_ID);
                                            cmd9.Parameters.AddWithValue("@month", month);
                                            cmd9.Parameters.AddWithValue("@year", yearr);
                                            cmd9.ExecuteNonQuery();

                                            lblShow_Stats.Text = "Ok !";
                                            MakeFeeRecords(lblShow_Student_ID.Text, Link, LoadClassPrice(Link));

                                            try
                                            {
                                                Thread ts = new Thread(new ThreadStart(this.Send));
                                                ts.Start();
                                            }
                                            catch (Exception) { }
                                        }
                                        else
                                        {
                                            lblShow_Stats.Text = "Already Marked";
                                            MakeFeeRecords(lblShow_Student_ID.Text, Link, LoadClassPrice(Link));
                                        }
                                    }
                                    else
                                    {
                                        reader7.Close();

                                        string InsertQuery6 = "Insert into Extra_Attendance (id,link,student_id,class_id,time,date,month,year,week" + weekOfMonth + ") values (@id,@link,@student_id,@class_id,@time,@date,@month,@year,@week)";
                                        SqlCommand cmd10 = new SqlCommand(InsertQuery6, con);
                                        cmd10.Parameters.AddWithValue("@id", GetNextNumExtra());
                                        cmd10.Parameters.AddWithValue("@link", Link);
                                        cmd10.Parameters.AddWithValue("@student_id", lblShow_Student_ID.Text);
                                        cmd10.Parameters.AddWithValue("@class_id", Class_ID);
                                        cmd10.Parameters.AddWithValue("@time", time.Text);
                                        cmd10.Parameters.AddWithValue("@date", date);
                                        cmd10.Parameters.AddWithValue("@month", month);
                                        cmd10.Parameters.AddWithValue("@week", "1");
                                        cmd10.Parameters.AddWithValue("@year", yearr);
                                        cmd10.ExecuteNonQuery();

                                        lblShow_Stats.Text = "Ok !";
                                        MakeFeeRecords(lblShow_Student_ID.Text, Link, LoadClassPrice(Link));
                                    }
                                }
                            }
                        }
                        else
                        {
                            Back.BackColor = Color.Red;
                            lblShow_Student_ID.Text = "";
                            lblShow_Class_Name.Text = "";
                            lblShow_Student_Name.Text = "";
                            Picture.BackgroundImage = Image.FromFile(Application.StartupPath + "\\UX\\.support\\.icon\\student.png");
                            lblShow_Stats.Text = "Not Okay !";
                            reader.Close();
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            //----------------------------------------------------------------------------------------------------------------


        }

        private string LoadClassPrice(string selectedClassId)
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
                        string selectQuery = "SELECT price FROM class WHERE id=@id";
                        SqlCommand cmd = new SqlCommand(selectQuery, con);
                        cmd.Parameters.AddWithValue("@id", selectedClassId);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                return reader["price"].ToString();
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
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to get price: " + e.Message);
            }

            return null; // or some default value based on your requirements
        }

        // -----------------------------------------------------------------------------------------------------------------

        private void MakeFeeRecords(string studentId, string classId, string price)
        {
            string year = txtyear.Text;
            string month = txtMonth.Text;
            int weekOfMonth = (Convert.ToInt32(dtpDate.Value.ToString("dd")) - 1) / 7 + 1;

            using (SqlConnection con = connection.my_conn())
            {
                try
                {
                    con.Open();

                    int attCheck = GetTotalAttendance(con, studentId, classId, month, year);

                    Alarm(studentId, classId, month, year, weekOfMonth.ToString());

                    if (attCheck >= 2)
                    {
                        string card = GetStudentCard(con, studentId, classId);

                        if (card.Trim() == "Full" || card.Trim() == "Half")
                        {
                            string p = (card.Trim() == "Half") ? (Convert.ToInt32(price) / 2).ToString() : price;

                            if (!DoesFeeRecordExist(con, studentId, classId, year, month))
                            {
                                InsertFeeRecord(con, studentId, classId, year, month, p);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Message: " + e.Message);
                }
            }
        }

        private int GetTotalAttendance(SqlConnection con, string studentId, string classId, string month, string year)
        {
            string query = "SELECT total_attendance FROM Vw_Sum_At WHERE class_id=@class_id AND student_id=@student_id AND month=@month AND year=@year";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@student_id", studentId);
                cmd.Parameters.AddWithValue("@class_id", string.IsNullOrEmpty(Link) ? classId : Link);
                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@year", year);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int attCheck = 0;

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            attCheck += Convert.ToInt32(reader[0]);
                        }
                    }

                    return attCheck;
                }
            }
        }

        private string GetStudentCard(SqlConnection con, string studentId, string classId)
        {
            string query = "SELECT card FROM study WHERE student_id=@student_id AND class_id=@class_id";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@student_id", studentId);
                cmd.Parameters.AddWithValue("@class_id", classId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return reader[0].ToString();
                        }
                    }
                }
            }

            return "";
        }

        private bool DoesFeeRecordExist(SqlConnection con, string studentId, string classId, string year, string month)
        {
            string query = "SELECT id FROM class_fees WHERE student_id=@student_id AND class_id=@class_id AND year=@year AND month=@month";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@student_id", studentId);
                cmd.Parameters.AddWithValue("@class_id", classId);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@month", month);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }

        private void InsertFeeRecord(SqlConnection con, string studentId, string classId, string year, string month, string price)
        {
            try
            {
                string query = "INSERT INTO class_fees(id,student_id,class_id,year,month,price) VALUES (@id,@student_id,@class_id,@year,@month,@price)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", GetNextNumfee());
                    cmd.Parameters.AddWithValue("@student_id", studentId);
                    cmd.Parameters.AddWithValue("@class_id", classId);
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.Parameters.AddWithValue("@month", month);
                    cmd.Parameters.AddWithValue("@price", price);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Message: " + e.Message);
            }
        }


        // -----------------------------------------------------------------------------------------------------------------

        private void Alarm(string studentId, string classId, string month, string year, string week)
        {
            SqlConnection con = connection.my_conn();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            try
            {
                string check = "SELECT student_id, month FROM Vw_PendingPayment WHERE student_id=@sid AND class_id=@cid AND year=@year";
                SqlCommand cmd = new SqlCommand(check, con);
                cmd.Parameters.AddWithValue("@sid", studentId);
                cmd.Parameters.AddWithValue("@cid", classId);
                //cmd->Parameters->AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@year", year);
                SqlDataReader reader = cmd.ExecuteReader();

                string getMonth = "";

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        getMonth += reader["month"].ToString();
                    }

                    if (Convert.ToInt32(week) >= 3 || getMonth != month)
                    {
                        SoundPlayer sound = new SoundPlayer();
                        sound.SoundLocation = Application.StartupPath + "\\Audio\\Alam.wav";
                        sound.Load();
                        sound.Play();
                        reader.Close();
                        lblShow_Stats.Text = "Verify Attendance Manually";
                    }
                }
                else
                {
                    reader.Close();
                }
            }
            catch (Exception)
            {
                // Handle exceptions as needed
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        private string CheckSms()
        {
            SqlConnection con = connection.my_conn();
            string state = "InActive"; // Default value if the state is not found

            try
            {
                con.Open();

                string get = "SELECT state FROM getway WHERE event=@Event";
                SqlCommand cmd = new SqlCommand(get, con);
                cmd.Parameters.AddWithValue("@Event", "Attendance");
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

        private string MsgSms(string studentName, string className, string date)
        {
            SqlConnection con = connection.my_conn();
            string message = "Default Message"; // Default value for message

            try
            {
                con.Open();

                string get = "SELECT message FROM getway WHERE event=@Event";
                SqlCommand cmd = new SqlCommand(get, con);
                cmd.Parameters.AddWithValue("@Event", "Attendance");
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    message = result.ToString();

                    message = message.Replace("@kidName", studentName);
                    message = message.Replace("@class", className);
                    message = message.Replace("@currentDate", date);
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

        private string GetContactNum()
        {
            string number = "0000000000"; // Default value

            using (SqlConnection con = connection.my_conn())
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                string get = "SELECT parent FROM student WHERE id=@id";
                using (SqlCommand cmd = new SqlCommand(get, con))
                {
                    cmd.Parameters.AddWithValue("@id", lblShow_Student_ID.Text);
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        number = result.ToString();
                    }
                }
            }

            return number;
        }

        private void Send()
        {
            string status = CheckSms().Trim();
            if (status == "Active")
            {
                string number = GetContactNum();
                string msg = MsgSms(lblShow_Student_Name.Text, lblShow_Class_Name.Text, DateTime.Now.ToString("yyyy-MM-dd"));
                _ = connection.sms(number, msg);
                //MessageBox.Show(response, "API Response");
            }
        }

        private void LoadStudent()
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
                    string selectQuery = "SELECT id, name FROM Student WHERE id LIKE @id";
                    SqlCommand cmd = new SqlCommand(selectQuery, con);
                    cmd.Parameters.AddWithValue("@id", "%" + txtStudentID.Text + "%");


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
                MessageBox.Show("Process Unsuccessful");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Aye You Want to Mark This Manual ?", "Warning !", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Mark();
            }
        }

        private void pgManualAttendance_Load(object sender, EventArgs e)
        {
            LoadStudent();
            txtMonth.Text = DateTime.Now.ToString("MMMM");
        }

        private void txtStudentID_TextChanged(object sender, EventArgs e)
        {
            LoadStudent();
        }

    }
}
