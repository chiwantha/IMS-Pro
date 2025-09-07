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
    public partial class pgSMS : Form
    {
        Connection connection;
        public pgSMS()
        {
            InitializeComponent();
            connection = new Connection();
        }

        // -------------------------------------------------------------------------------------
        string class_id = "";
        string class_Message = "";
        // -------------------------------------------------------------------------------------

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
                    string selectQuery = "SELECT contact,name FROM Student WHERE id LIKE @id OR name LIKE @name OR contact LIKE @contact or rfid LIKE @rfid";
                    SqlCommand cmd = new SqlCommand(selectQuery, con);
                    cmd.Parameters.AddWithValue("@id", "%" + txtID.Text + "%");
                    cmd.Parameters.AddWithValue("@name", "%" + txtID.Text + "%");
                    cmd.Parameters.AddWithValue("@contact", "%" + txtID.Text + "%");
                    cmd.Parameters.AddWithValue("@rfid", "%" + txtID.Text + "%");

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();

                    dataAdapter.Fill(dataTable);

                    txtStudent.DataSource = dataTable;
                    txtStudent.ValueMember = "contact";
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
                MessageBox.Show("Process Unsucessfull");
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
                    string selectQuery = "SELECT id,class FROM class";
                    SqlCommand cmd = new SqlCommand(selectQuery, con);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();

                    dataAdapter.Fill(dataTable);

                    txtClass.DataSource = dataTable;
                    txtClass.ValueMember = "id";
                    txtClass.DisplayMember = "class";
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
                MessageBox.Show("Process Unsucessfull");
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
                    string selectQuery = "SELECT contact,name FROM teacher";
                    SqlCommand cmd = new SqlCommand(selectQuery, con);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();

                    dataAdapter.Fill(dataTable);

                    txtTeacher.DataSource = dataTable;
                    txtTeacher.ValueMember = "contact";
                    txtTeacher.DisplayMember = "name";
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
                MessageBox.Show("Process Unsucessfull");
            }
        }

        // -------------------------------------------------------------------------------------

        void GetContactNumAndSend()
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    con.Open();

                    int totalStudent = GetTotalStudentCount(con);
                    DisplayTotalStudentCount(totalStudent);

                    if (totalStudent > 0)
                    {
                        ProcessStudentContacts(con, totalStudent);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        int GetTotalStudentCount(SqlConnection con)
        {
            string countQuery = "SELECT COUNT(contact) FROM Vw_studyStudentList WHERE classid=@id";

            using (SqlCommand cmdCount = new SqlCommand(countQuery, con))
            {
                cmdCount.Parameters.AddWithValue("@id", class_id);
                return (int)cmdCount.ExecuteScalar();
            }
        }
        void DisplayTotalStudentCount(int totalStudent)
        {
            total_count.Text = $"Student Count: {totalStudent}";
        }
        void ProcessStudentContacts(SqlConnection con, int totalStudent)
        {
            string contactQuery = "SELECT contact FROM Vw_studyStudentList WHERE classid=@id";

            using (SqlCommand cmdContacts = new SqlCommand(contactQuery, con))
            {
                cmdContacts.Parameters.AddWithValue("@id", class_id);

                using (SqlDataReader contactReader = cmdContacts.ExecuteReader())
                {
                    if (contactReader.HasRows)
                    {
                        progressBar2.Maximum = totalStudent;
                        progressBar2.Value = 0;

                        ProcessContacts(contactReader);
                    }
                }
            }
        }
        void ProcessContacts(SqlDataReader reader)
        {
            int sent = 0;
            int skipped = 0;

            while (reader.Read())
            {
                string contactValue = reader["contact"].ToString();

                if (!string.IsNullOrEmpty(contactValue) && contactValue != "0")
                {
                    _ = connection.sms(contactValue, txtMessageCl.Text);
                    UpdateSentCount(++sent);
                }
                else
                {
                    UpdateSkippedCount(++skipped);
                }

                progressBar2.Value++;
            }
        }
        void UpdateSentCount(int sent)
        {
            sent_count.Text = $"Sent Count: {sent}";
        }
        void UpdateSkippedCount(int skipped)
        {
            skipped_count.Text = $"Skipped Count: {skipped}";
        }
        void HandleException(Exception ex)
        {
            // Handle exceptions, e.g., log or display an error message
        }


        // -------------------------------------------------------------------------------------

        private void pgSMS_Load(object sender, EventArgs e)
        {
            try
            {
                LoadClass();
                LoadTeacher();
                LoadStudent();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message, "Error");
            }
        }
        private void txtID_TextChanged(object sender, EventArgs e)
        {
            LoadStudent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Value = 50;
                _ = connection.sms(txtStudent.SelectedValue.ToString(), txtMessageSt.Text);
                progressBar1.Value = 100;
            }
            catch (Exception)
            {
                progressBar1.Value = 0;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                class_id = txtClass.SelectedValue.ToString();
                class_Message = txtMessageCl.Text.ToString();
                progressBar2.Value = 50;
                GetContactNumAndSend();
                progressBar2.Value = 100;
            }
            catch (Exception)
            {
                progressBar3.Value = 0;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar3.Value = 50;
                _ = connection.sms(txtTeacher.SelectedValue.ToString(), txtMessaget.Text);
                progressBar3.Value = 100;
            }
            catch (Exception)
            {
                progressBar3.Value = 0;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar4.Value = 50;
                _ = connection.sms(txtNumber.Text, txtMsg.Text);
                progressBar4.Value = 100;
            }
            catch (Exception)
            {
                progressBar3.Value = 0;
            }
        }


    }
}
