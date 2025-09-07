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
    public partial class pgHoliday : Form
    {
        Connection connection;
        public pgHoliday()
        {
            InitializeComponent();
            connection = new Connection();
        }

        string class_id = "";
        string class_Message = "";
        string Id;

        private string GetNextNum()
        {
            SqlConnection con = connection.my_conn();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            string Get = "select MAX(id) from class_holiday";
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
                    string selectQuery = "SELECT * FROM Vw_ClassHoliday";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con);
                    DataTable dataTable = new DataTable();

                    dataAdapter.Fill(dataTable);

                    dgHoliday.AutoGenerateColumns = true;
                    dgHoliday.DataSource = dataTable;

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
                MessageBox.Show("");
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
                    string selectQuery = "SELECT id, class FROM class where state = 1";
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
        string checksms()
        {
            SqlConnection con = connection.my_conn();
            string state = "InActive"; // Default value if the state is not found

            try
            {
                con.Open();

                string Get = "select state from getway where event=@Event";
                SqlCommand cmd = new SqlCommand(Get, con);
                cmd.Parameters.AddWithValue("@Event", "Hiloday");
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
        string Msgsms(string class_name, string reason)
        {
            SqlConnection con = connection.my_conn();
            string message = "Default Message"; // Default value for message

            try
            {
                con.Open();

                string Get = "select message from getway where event=@Event";
                SqlCommand cmd = new SqlCommand(Get, con);
                cmd.Parameters.AddWithValue("@Event", "Hiloday");
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    message = result.ToString();

                    message = message.Replace("@class", class_name);
                    message = message.Replace("@reason", reason);
                    message = message.Replace("@date", txtDate.Text);

                    return message;
                }
                else
                {
                    return "none";
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
        void GetContactNumAndSend()
        {
            SqlConnection con = connection.my_conn();
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                string Get = "SELECT contact FROM Vw_studyStudentList WHERE classid=@id";
                SqlCommand cmd = new SqlCommand(Get, con);
                cmd.Parameters.AddWithValue("@id", class_id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string contact = Convert.ToString(reader["contact"]);
                    _ = connection.sms(contact, class_Message);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        void send()
        {
            class_Message = Msgsms(txtClass.Text, txtState.Text);
            class_id = txtClass.SelectedValue.ToString();
            Thread ts = new Thread(new ThreadStart(this.GetContactNumAndSend));
            ts.Start();
        }
        private void pgHoliday_Load(object sender, EventArgs e)
        {
            try
            {
                LoadToGrid();
                LoadClass();
            }
            catch (Exception) { }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = connection.my_conn();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                string insert = "insert into class_holiday values (@id,@class_id,@date,@state)";
                SqlCommand cmd = new SqlCommand(insert, con);
                cmd.Parameters.AddWithValue("@id", GetNextNum());
                cmd.Parameters.AddWithValue("@class_id", txtClass.SelectedValue);
                cmd.Parameters.AddWithValue("@date", txtDate.Text);
                cmd.Parameters.AddWithValue("@state", txtState.Text);
                cmd.ExecuteNonQuery();
                try
                {
                    progressBar1.Value = 0;
                    progressBar1.Value = 50;
                    send();
                    progressBar1.Value = 100;
                }
                catch (Exception) { }

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
            try
            {
                SqlConnection con = connection.my_conn();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                string Del = "delete from class_holiday where id=@id";
                SqlCommand cmd = new SqlCommand(Del, con);
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Deleted");

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Process Unsucessfull");
            }
            Id = "";
            LoadToGrid();
        }
        private void dgHoliday_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgHoliday.RowCount)
                {
                    Id = dgHoliday.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtClass.SelectedValue = dgHoliday.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtDate.Text = dgHoliday.Rows[e.RowIndex].Cells[7].Value.ToString();
                    txtState.Text = dgHoliday.Rows[e.RowIndex].Cells[8].Value.ToString();
                }
            }
            catch (Exception)
            {

            }
        }

    }
}
