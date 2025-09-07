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
    public partial class pgAttendanceView : Form
    {
        Connection connection;
        public pgAttendanceView()
        {
            InitializeComponent();
            connection = new Connection();
        }

        private void LoadStudent()
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
                        string selectQuery = "SELECT id, name FROM Student";
                        using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                        {
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                            DataTable dataTable = new DataTable();

                            dataAdapter.Fill(dataTable);

                            txtStudent.DataSource = dataTable;
                            txtStudent.ValueMember = "id";
                            txtStudent.DisplayMember = "name";
                            txtStudent.Text = "";
                        }
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

            }
        }

        private void LoadClass()
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
                        string selectQuery = "SELECT id, class FROM Class where state = 1";
                        using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                        {
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                            DataTable dataTable = new DataTable();

                            dataAdapter.Fill(dataTable);

                            txtClass.DataSource = dataTable;
                            txtClass.ValueMember = "id";
                            txtClass.DisplayMember = "class";
                            txtClass.Text = "";
                        }
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

            }
        }

        private void Filter()
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
                        string selectQuery = "SELECT * FROM Vw_Attendance";
                        string filter = "";

                        if (txtStudent.Text != "")
                        {
                            if (checkBox1.Checked)
                            {
                                if (filter == "")
                                {
                                    filter = " WHERE rfid LIKE '%" + txtStudent.Text + "%'";
                                }
                                else
                                {
                                    filter += " AND rfid LIKE '%" + txtStudent.Text + "%'";
                                }
                            }
                            else
                            {
                                if (filter == "")
                                {
                                    filter = " WHERE student_id LIKE '%" + txtStudent.SelectedValue + "%'";
                                }
                                else
                                {
                                    filter += " AND student_id LIKE '%" + txtStudent.SelectedValue + "%'";
                                }
                            }
                        }

                        if (txtClass.Text != "")
                        {
                            if (filter == "")
                            {
                                filter = " WHERE class_id LIKE '%" + txtClass.SelectedValue + "%'";
                            }
                            else
                            {
                                filter += " AND class_id LIKE '%" + txtClass.SelectedValue + "%'";
                            }
                        }

                        if (txtFromDate.Text != "" && txtToDate.Text != "")
                        {
                            if (filter == "")
                            {
                                filter = " WHERE date BETWEEN '" + txtFromDate.Value.ToString("yyyy-MM-dd") + "' AND '" + txtToDate.Value.ToString("yyyy-MM-dd") + "'";
                            }
                            else
                            {
                                filter += " AND date BETWEEN '" + txtFromDate.Value.ToString("yyyy-MM-dd") + "' AND '" + txtToDate.Value.ToString("yyyy-MM-dd") + "'";
                            }
                        }

                        if (txtFromTime.Text != "" && txtToTime.Text != "")
                        {
                            if (filter == "")
                            {
                                filter = " WHERE punch_time BETWEEN '" + txtFromTime.Value.ToString("HH:mm:ss tt") + "' AND '" + txtToTime.Value.ToString("HH:mm:ss tt") + "'";
                            }
                            else
                            {
                                filter += " AND punch_time BETWEEN '" + txtFromTime.Value.ToString("HH:mm:ss tt") + "' AND '" + txtToTime.Value.ToString("HH:mm:ss tt") + "'";
                            }
                        }

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery + filter, con);
                        DataTable dataTable = new DataTable();

                        dataAdapter.Fill(dataTable);

                        dgAttendance.AutoGenerateColumns = true;
                        dgAttendance.DataSource = dataTable;
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
                MessageBox.Show("Process Unsuccessful");
            }
        }

        private string getMax(string column, string table)
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
                        string selectQuery = "SELECT MAX(" + column + ") FROM " + table;

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con);
                        SqlCommand cmd = new SqlCommand(selectQuery, con);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                return reader.GetValue(0).ToString();
                            }
                        }
                        else
                        {
                            return "";
                        }
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
                MessageBox.Show("Process Unsuccessful");
            }

            return "";
        }

        private string getMin(string column, string table)
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
                        string selectQuery = "SELECT MIN(" + column + ") FROM " + table;

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con);
                        SqlCommand cmd = new SqlCommand(selectQuery, con);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                return reader.GetValue(0).ToString();
                            }
                        }
                        else
                        {
                            return "";
                        }
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
                MessageBox.Show("Process Unsuccessful");
            }

            return "";
        }

        private void pgAttendance_Load(object sender, EventArgs e)
        {
            try
            {
                Filter();
            }
            catch (Exception)
            {

            }

            try
            {
                LoadStudent();
            }
            catch (Exception)
            {

            }

            try
            {
                LoadClass();
            }
            catch (Exception)
            {

            }

            txtFromDate.Text = getMin("date", "Vw_Attendance");
            txtFromTime.Text = getMin("punch_time", "Vw_Attendance");
            txtToDate.Text = getMax("date", "Vw_Attendance");
            txtToTime.Text = getMax("punch_time", "Vw_Attendance");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void txtStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void txtClass_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void txtStudent_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void txtToDate_ValueChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void txtToTime_ValueChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void txtFromTime_ValueChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void txtFromDate_ValueChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Filter();
            }
            catch (Exception)
            {

            }

            try
            {
                LoadStudent();
            }
            catch (Exception)
            {

            }

            try
            {
                LoadClass();
            }
            catch (Exception)
            {

            }

            txtFromDate.Text = getMin("date", "Vw_Attendance");
            txtFromTime.Text = getMin("punch_time", "Vw_Attendance");
            txtToDate.Text = getMax("date", "Vw_Attendance");
            txtToTime.Text = getMax("punch_time", "Vw_Attendance");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
