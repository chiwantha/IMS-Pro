using IMS_Upgraded_C_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace IMS_Upgrade_To_C_
{
    public partial class pgInvoice : Form
    {
        Connection connection;
        public pgInvoice(string User)
        {
            InitializeComponent();
            connection = new Connection();
            user = User;
        }

        string user = "";
        string Stid = "";
        string ReceptNumber = "";


        string GetNextNum()
        {
            using (SqlConnection con = connection.my_conn())
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                string Get = "select MAX(recept_no) from recept";
                using (SqlCommand cmd = new SqlCommand(Get, con))
                {
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
        }
        string GetNextNumfee()
        {
            using (SqlConnection con = connection.my_conn())
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                string Get = "select MAX(id) from class_fees";
                using (SqlCommand cmd = new SqlCommand(Get, con))
                {
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
        }
        void LoadClass()
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
                        string selectQuery = "SELECT id, class FROM class where state = 1";
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con))
                        {
                            DataTable dataTable = new DataTable();
                            dataAdapter.Fill(dataTable);

                            txtClass.DataSource = dataTable;
                            txtClass.ValueMember = "id";
                            txtClass.DisplayMember = "class";
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error fetching class data: " + e.Message);
                    }
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
                using (SqlConnection con = connection.my_conn())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    try
                    {
                        string selectQuery = "SELECT id,name FROM Student WHERE id LIKE @id OR name LIKE @name OR contact LIKE @contact or Rfid LIKE @rfid";
                        using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@id", "%" + txtID.Text + "%");
                            cmd.Parameters.AddWithValue("@name", "%" + txtID.Text + "%");
                            cmd.Parameters.AddWithValue("@contact", "%" + txtID.Text + "%");
                            cmd.Parameters.AddWithValue("@rfid", "%" + txtID.Text + "%");

                            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                            {
                                DataTable dataTable = new DataTable();
                                dataAdapter.Fill(dataTable);

                                txtStudent.DataSource = dataTable;
                                txtStudent.ValueMember = "id";
                                txtStudent.DisplayMember = "name";
                                //txtStudent->SelectedIndex = -1;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Process Unsuccessful : " + txtID);
            }
        }
        void filter()
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
                        string selectQuery = "SELECT * FROM Vw_PendingPayment";
                        string filter = "";

                        if (txtStudent.Text != "")
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

                        if (txtGrade.Text != "")
                        {
                            if (filter == "")
                            {
                                filter = " WHERE grade LIKE '%" + txtGrade.Text + "%'";
                            }
                            else
                            {
                                filter += " AND grade LIKE '%" + txtGrade.Text + "%'";
                            }
                        }

                        if (txtBatch.Text != "")
                        {
                            if (filter == "")
                            {
                                filter = " WHERE batch LIKE '%" + txtBatch.Text + "%'";
                            }
                            else
                            {
                                filter += " AND batch LIKE '%" + txtBatch.Text + "%'";
                            }
                        }

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery + filter, con))
                        {
                            DataTable dataTable = new DataTable();
                            dataAdapter.Fill(dataTable);

                            // dgInvoice->AutoGenerateColumns = false;
                            dgInvoice.DataSource = dataTable;
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Process Unsuccessful : " + txtID);
            }
        }
        void Clear()
        {
            txtID.Text = "";
            txtAmmount.Text = "";
            txtGrade.Text = "";
            txtBatch.Text = "";
            // Picture->BackgroundImage = Image::FromFile("E:\\c++\\CMS\\CMS\\student.png");
        }
        void Recept(string text)
        {
            ReportDocument rep = new ReportDocument();
            rep.Load(Application.StartupPath + "\\Reports\\Invoice_recept.RPT");

            CrystalDecisions.Shared.TableLogOnInfo logonInfo = rep.Database.Tables[0].LogOnInfo;
            logonInfo.ConnectionInfo.ServerName = connection.Get_path();
            logonInfo.ConnectionInfo.DatabaseName = "IMS";
            logonInfo.ConnectionInfo.UserID = "sa";
            logonInfo.ConnectionInfo.Password = "";

            foreach (CrystalDecisions.CrystalReports.Engine.Table table in rep.Database.Tables)
            {
                table.ApplyLogOnInfo(logonInfo);
            }

            rep.RecordSelectionFormula = "{Vw_Recept.recept_no} ='" + text + "'";
            rep.Refresh();

            ReportViewer rv = new ReportViewer(rep);
            rv.Show();
        }
        void Pay()
        {
            if (string.IsNullOrEmpty(txtGrade.Text) || string.IsNullOrEmpty(txtStudent.Text) || string.IsNullOrEmpty(txtClass.Text) ||
                string.IsNullOrEmpty(txtBatch.Text) || string.IsNullOrEmpty(txtAmmount.Text) || string.IsNullOrEmpty(txtYearr.Text))
            {
                MessageBox.Show("Please Fill in the Invoice!");
            }
            else
            {
                using (SqlConnection con = connection.my_conn())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    string check = "SELECT * FROM class_fees " +
                                    "WHERE class_id=@class_id and student_id=@student_id and month=@month and year = @year and recept_no IS NULL";

                    using (SqlCommand cmd = new SqlCommand(check, con))
                    {
                        cmd.Parameters.AddWithValue("@student_id", txtStudent.SelectedValue);
                        cmd.Parameters.AddWithValue("@class_id", txtClass.SelectedValue);
                        cmd.Parameters.AddWithValue("@month", txtMonth.Text);
                        cmd.Parameters.AddWithValue("@year", txtYearr.Text);

                        using (SqlDataReader allreader = cmd.ExecuteReader())
                        {
                            if (allreader.HasRows)
                            {
                                allreader.Close();
                                try
                                {
                                    UpdateClassFeesAndInsertReceipt(con);
                                }
                                catch (Exception e)
                                {
                                    MessageBox.Show(e.Message);
                                }
                            }
                            else
                            {
                                allreader.Close();
                                string checkPaid = "SELECT * FROM class_fees " +
                                                  "WHERE class_id=@class_id and student_id=@student_id and month=@month and year = @year and recept_no IS NOT NULL";

                                using (SqlCommand paidCmd = new SqlCommand(checkPaid, con))
                                {
                                    paidCmd.Parameters.AddWithValue("@student_id", txtStudent.SelectedValue);
                                    paidCmd.Parameters.AddWithValue("@class_id", txtClass.SelectedValue);
                                    paidCmd.Parameters.AddWithValue("@month", txtMonth.Text);
                                    paidCmd.Parameters.AddWithValue("@year", txtYearr.Text);

                                    using (SqlDataReader reader = paidCmd.ExecuteReader())
                                    {
                                        if (!reader.HasRows)
                                        {
                                            reader.Close();
                                            try
                                            {
                                                InsertClassFeesAndReceipt(con);
                                            }
                                            catch (Exception e)
                                            {
                                                MessageBox.Show(e.Message);
                                            }
                                        }
                                        else
                                        {
                                            reader.Close();
                                            MessageBox.Show("Paid", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    try
                    {
                        Clear();
                        Recept(txtRN.Text);
                        Stid = txtStudent.SelectedValue.ToString();
                        ReceptNumber = txtRN.Text;

                        if (txtSentTo.Text == "Both")
                        {
                            try
                            {
                                Thread ts = new Thread(new ThreadStart(GetContactNumAndSendParent));
                                ts.Start();
                            }
                            catch (Exception) { }

                            try
                            {
                                Thread ts = new Thread(new ThreadStart(GetContactNumAndSendStudent));
                                ts.Start();
                            }
                            catch (Exception) { }
                        }
                        else if (txtSentTo.Text == "Parent")
                        {
                            try
                            {
                                Thread ts = new Thread(new ThreadStart(GetContactNumAndSendParent));
                                ts.Start();
                            }
                            catch (Exception) { }
                        }
                        else if (txtSentTo.Text == "Student")
                        {
                            try
                            {
                                Thread ts = new Thread(new ThreadStart(GetContactNumAndSendStudent));
                                ts.Start();
                            }
                            catch (Exception) { }
                        }
                        else
                        {
                            MessageBox.Show("Error on Send To, Selector on Bill");
                        }

                        txtRN.Text = GetNextNum();
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

                filter();
            }
        }
        void UpdateClassFeesAndInsertReceipt(SqlConnection con)
        {
            string updateClassFees =
                "UPDATE class_fees SET recept_no=@RN WHERE student_id=@student_id and class_id=@class_id and month=@month and year=@year";

            using (SqlCommand cmdUpdate = new SqlCommand(updateClassFees, con))
            {
                cmdUpdate.Parameters.AddWithValue("@RN", txtRN.Text);
                cmdUpdate.Parameters.AddWithValue("@student_id", txtStudent.SelectedValue);
                cmdUpdate.Parameters.AddWithValue("@class_id", txtClass.SelectedValue);
                cmdUpdate.Parameters.AddWithValue("@month", txtMonth.Text);
                cmdUpdate.Parameters.AddWithValue("@year", txtYearr.Text);
                cmdUpdate.ExecuteNonQuery();
            }

            InsertReceipt(con);
        }
        void InsertClassFeesAndReceipt(SqlConnection con)
        {
            string insertClassFees = "INSERT INTO class_fees VALUES (@id,@student_id,@class_id,@year,@month,@price,@RN)";

            using (SqlCommand cmdInsert = new SqlCommand(insertClassFees, con))
            {
                cmdInsert.Parameters.AddWithValue("@id", GetNextNumfee());
                cmdInsert.Parameters.AddWithValue("@student_id", txtStudent.SelectedValue);
                cmdInsert.Parameters.AddWithValue("@class_id", txtClass.SelectedValue);
                cmdInsert.Parameters.AddWithValue("@year", txtYearr.Text);
                cmdInsert.Parameters.AddWithValue("@month", txtMonth.Text);
                cmdInsert.Parameters.AddWithValue("@price", txtAmmount.Text);
                cmdInsert.Parameters.AddWithValue("@RN", txtRN.Text);
                cmdInsert.ExecuteNonQuery();
            }

            InsertReceipt(con);
        }
        void InsertReceipt(SqlConnection con)
        {
            string insertReceipt =
                "INSERT INTO recept VALUES (@recept_no,@student_id,@class_id,@month,@year,@date,@payment_method,@ammount,@collect_by,@slipt_No,@cash_rec,@balance)";

            using (SqlCommand cmdReceipt = new SqlCommand(insertReceipt, con))
            {
                cmdReceipt.Parameters.AddWithValue("@recept_no", txtRN.Text);
                cmdReceipt.Parameters.AddWithValue("@student_id", txtStudent.SelectedValue);
                cmdReceipt.Parameters.AddWithValue("@class_id", txtClass.SelectedValue);
                cmdReceipt.Parameters.AddWithValue("@month", txtMonth.Text);
                cmdReceipt.Parameters.AddWithValue("@year", txtYearr.Text);
                cmdReceipt.Parameters.AddWithValue("@date", txtDate.Text);
                cmdReceipt.Parameters.AddWithValue("@payment_method", txtType.Text);
                cmdReceipt.Parameters.AddWithValue("@ammount", txtAmmount.Text);
                cmdReceipt.Parameters.AddWithValue("@collect_by", user);
                cmdReceipt.Parameters.AddWithValue("@slipt_No", txtBank.Text);
                cmdReceipt.Parameters.AddWithValue("@cash_rec", tcash_receved.Text);
                cmdReceipt.Parameters.AddWithValue("@balance", tbalance.Text);
                cmdReceipt.ExecuteNonQuery();
            }
        }
        string checksms()
        {
            string state = "InActive"; // Default value if the state is not found

            using (SqlConnection con = connection.my_conn())
            {
                try
                {
                    con.Open();
                    string get = "SELECT state FROM getway WHERE event=@Event";
                    using (SqlCommand cmd = new SqlCommand(get, con))
                    {
                        cmd.Parameters.AddWithValue("@Event", "Class Fees");
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            state = result.ToString();
                        }
                    }
                }
                catch (SqlException)
                {
                    // Handle SQL exceptions here
                }
            }

            return state;
        }
        string Msgsms()
        {
            string message = "Default Message"; // Default value for message

            using (SqlConnection con = connection.my_conn())
            {
                try
                {
                    con.Open();
                    string get = "SELECT message FROM getway WHERE event=@Event";
                    using (SqlCommand cmd = new SqlCommand(get, con))
                    {
                        cmd.Parameters.AddWithValue("@Event", "Class Fees");
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            message = result.ToString();

                            message = message.Replace("@Student", txtStudent.Text);
                            message = message.Replace("@ammount", txtAmmount.Text);
                            message = message.Replace("@Class", txtClass.Text);
                            message = message.Replace("@month", txtMonth.Text);
                            message = message.Replace("@receptNO", ReceptNumber);
                            message = message.Replace("@Date", txtDate.Text);
                            message = message.Replace("@year", txtYearr.Text);
                        }
                    }
                }
                catch (SqlException)
                {
                    // Handle SQL exceptions here
                }
            }

            return message;
        }
        void GetContactNumAndSendParent()
        {
            using (SqlConnection con = connection.my_conn())
            {
                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    string get = "SELECT parent FROM student WHERE id=@id";
                    using (SqlCommand cmd = new SqlCommand(get, con))
                    {
                        cmd.Parameters.AddWithValue("@id", Stid);
                        object result = cmd.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            string number = result.ToString();

                            send(number);
                        }
                        else
                        {
                            // Default value
                        }
                    }
                }
                catch (Exception)
                {
                    // Handle exceptions, log them, or throw them further if needed
                }
            }
        }
        void GetContactNumAndSendStudent()
        {
            using (SqlConnection con = connection.my_conn())
            {
                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    string get = "SELECT contact FROM student WHERE id=@id";
                    using (SqlCommand cmd = new SqlCommand(get, con))
                    {
                        cmd.Parameters.AddWithValue("@id", Stid);
                        object result = cmd.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            string number = result.ToString();

                            send(number);
                        }
                        else
                        {
                            // Default value
                        }
                    }
                }
                catch (Exception)
                {
                    // Handle exceptions, log them, or throw them further if needed
                }
            }
        }
        void send(string number)
        {
            string Status = checksms().Trim();
            if (Status == "Active")
            {
                string msg = Msgsms();
                _ = connection.sms(number, msg);
                //MessageBox.Show(response, "API Response");
            }
        }
        private void pgInvoice_Load(object sender, EventArgs e)
        {
            txtMonth.Text = DateTime.Now.ToString("MMMM");
            txtYearr.Text = DateTime.Now.ToString("yyyy");
            txtType.SelectedIndex = 0;
            txtRN.Text = GetNextNum();
            grpRePrint.Visible = false;
            payment.Visible = false;

            try
            {
                filter();
            }
            catch (Exception) { }

            try
            {
                LoadClass();
            }
            catch (Exception) { }

            try
            {
                LoadStudent();
            }
            catch (Exception) { }

            txtStudent.SelectedIndex = -1;
            txtClass.SelectedIndex = -1;
        }
        private void txtID_TextChanged(object sender, EventArgs e)
        {
            LoadStudent();
        }
        private void txtStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            filter();
        }
        private void txtClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            filter();
        }
        private void txtGrade_TextChanged(object sender, EventArgs e)
        {
            filter();
        }
        private void txtBatch_TextChanged(object sender, EventArgs e)
        {
            filter();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            filter();
        }
        private void dgInvoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgInvoice.RowCount)
                {
                    txtStudent.SelectedValue = dgInvoice.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtClass.SelectedValue = dgInvoice.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtGrade.Text = dgInvoice.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtBatch.Text = dgInvoice.Rows[e.RowIndex].Cells[6].Value.ToString();
                    txtMonth.Text = dgInvoice.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtAmmount.Text = dgInvoice.Rows[e.RowIndex].Cells[8].Value.ToString();
                    txtYearr.Text = dgInvoice  .Rows[e.RowIndex].Cells[10].Value.ToString().Trim();
                }
            }
            catch (Exception)
            {
                // Handle exceptions
            }
        }
        private void btnPay_Click(object sender, EventArgs e)
        {
            if (txtType.Text == "Bank")
            {
                txtBank.Enabled = true;
            }
            else
            {
                txtBank.Enabled = false;
            }

            payment.Visible = true;
            trecept_number.Text = txtRN.Text;
            tprice.Text = txtAmmount.Text;
        }
        private void btnRePrint_Click(object sender, EventArgs e)
        {
            grpRePrint.Visible = true;
        }
        private void cancel_Click(object sender, EventArgs e)
        {
            grpRePrint.Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRePrint.Text))
            {
                try
                {
                    Recept(txtRePrint.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Invalid Recept Number!");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            payment.Visible = false;
        }
        private void btnBillPay_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlan.Text))
            {
                MessageBox.Show("Reach the Invoice, Something Missing!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Pay();
            }
            payment.Visible = false;
        }
        private void tcash_receved_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(tcash_receved.Text, out double cashReceived) && double.TryParse(tprice.Text, out double price))
            {
                double balance = cashReceived - price;
                tbalance.Text = balance.ToString();
            }
            else
            {
                // Handle the case where input is not a valid number
                tbalance.Text = "Invalid input";
            }
        }
    }
}
