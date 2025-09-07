using CrystalDecisions.CrystalReports.Engine;
using IMS_Upgraded_C_;
using IMS_Upgrade_To_C_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using IMS_Upgrade_To_C_.Libs;

namespace IMS_Upgrade_To_C_
{
    public partial class pgInvoiceCart : Form
    {
        Connection connection;
        Access_cart access_Cart = new Access_cart();
        public pgInvoiceCart(string User)
        {
            InitializeComponent();
            connection = new Connection();
            user = User;
        }

        string user = "";

        // ------------------------------------------------------------------------------------------------------------------------------------------

        Task LoadStudent()
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
                        string selectQuery = "SELECT id,name FROM Student";
                        using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                        {

                            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                            {
                                DataTable dataTable = new DataTable();
                                dataAdapter.Fill(dataTable);

                                txtStudent.DataSource = dataTable;
                                txtStudent.ValueMember = "id";
                                txtStudent.DisplayMember = "name";

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
                MessageBox.Show("Process Unsuccessful : ");
            }

            return Task.CompletedTask;
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
        void LoadClass(string sid)
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
                        string selectQuery = "SELECT ClassID, class FROM Vw_studyStudentList Where StudentID=" + sid + "and state = 1";
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con))
                        {
                            DataTable dataTable = new DataTable();
                            dataAdapter.Fill(dataTable);

                            txtClass.DataSource = dataTable;
                            txtClass.ValueMember = "ClassID";
                            txtClass.DisplayMember = "class";
                            txtClass.SelectedIndex = -1;
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
        void CalculateNet()
        {
            int discount;

            if (txtDiscount.Text == "")
            {
                discount = 0;
            }
            else
            {
                discount = Convert.ToInt32(txtDiscount.Text);
            }

            txtinvTotalNet.Text = "";
            txtinvTotalNet.Text = (Convert.ToInt32(txtinvTotalGross.Text) - discount).ToString();
        }
        async Task load_class_meta(string class_id)
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        await con.OpenAsync();
                    }

                    string selectQuery = "SELECT batch, grade, price FROM class WHERE id = @class_id";
                    using (SqlCommand command = new SqlCommand(selectQuery, con))
                    {
                        command.Parameters.AddWithValue("@class_id", class_id);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
                                {
                                    string batch = reader["batch"].ToString();
                                    string grade = reader["grade"].ToString();
                                    string amount = reader["price"].ToString();

                                    // Update UI elements
                                    UpdateUI(batch, grade, amount);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to load class data: " + e.Message);
            }
        }
        private void UpdateUI(string batch, string grade, string amount)
        {
            // Invoke on UI thread if necessary
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate () {
                    txtBatch.Text = batch;
                    txtGrade.Text = grade;
                    txtAmmount.Text = amount;
                });
            }
            else
            {
                txtBatch.Text = batch;
                txtGrade.Text = grade;
                txtAmmount.Text = amount;
            }
        }

        // -------------------------------------------------------------------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------------------------------------------------------------------

        private void clear_invoice()
        {
            txtinvBank.Text = "";
            txtincCashReceve.Text = "";
            txtInvBalance.Text = "";
        }

        // -------------------------------------------------------------------------------------------------------------------------------------------

        async Task LoadCart(string student_id)
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    //--------------------------------------------------------------------------------------------------------------------------------

                    int discount_value = await access_Cart.Get_Student_Discount_Value(con, student_id);

                    //--------------------------------------------------------------------------------------------------------------------------------

                    try
                    {
                        string selectQuery = "SELECT student_id, class_id, class, grade, batch, month, year, Fee FROM Vw_PendingPayment";
                        string filter = "";

                        if (student_id != "")
                        {
                            if (filter == "")
                            {
                                filter = " WHERE student_id LIKE '%" + student_id + "%'";
                            }
                            else
                            {
                                filter += " AND student_id LIKE '%" + student_id + "%'";
                            }
                        }

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery + filter, con))
                        {
                            DataTable dataTable = new DataTable();
                            dataAdapter.Fill(dataTable);

                            Access_cart access_Cart = new Access_cart();
                            await access_Cart.ClearAllDataAsync();

                            // Use Task.Run to perform the insertions on a background thread
                            await Task.Run(async () =>
                            {
                                bool class_eligibility;

                                foreach (DataRow row in dataTable.Rows)
                                {
                                    class_eligibility = await access_Cart.Check_Class_Discount_Eligible(con, row["class_id"].ToString());

                                    await access_Cart.InsertDataAsync(
                                        row["student_id"].ToString(),
                                        row["class_id"].ToString(),
                                        row["class"].ToString(),
                                        row["grade"].ToString(),
                                        row["batch"].ToString(),
                                        row["month"].ToString(),
                                        row["year"].ToString(),
                                        row["Fee"].ToString(),
                                        class_eligibility ? discount_value.ToString() : "0",
                                        class_eligibility ? (Convert.ToInt32(row["Fee"]) - discount_value).ToString() : row["Fee"].ToString());
                                }
                            });


                            await update_my_cart_ui();
                            txtinvTotalGross.Text = await access_Cart.LoadTotalGrossAsync();
                            txtDiscount.Text = await access_Cart.LoadTotalDiscountAsync();
                            CalculateNet();
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
                MessageBox.Show("Process Unsuccessful : ");
            }
        }

        async Task update_my_cart_ui()
        {
            // Clear the UI controls before updating
            flowLayoutPanel1.Invoke((MethodInvoker)(() => flowLayoutPanel1.Controls.Clear()));

            // Retrieve cart data asynchronously
            DataTable dataTable = await access_Cart.GetAllDataAsync();

            // Use Task.Run to update the UI on a background thread
            await Task.Run(() =>
            {
                // Iterate through the retrieved data and update the UI
                foreach (DataRow row in dataTable.Rows)
                {
                    // Create and configure a cart item
                    CartItem item = new CartItem();
                    item.SetDetails(row["class_name"].ToString(), row["month"].ToString(), row["year"].ToString(), row["amount"].ToString(), row["discount"].ToString());

                    // Capture the current row variable in a local variable to avoid issues with closures
                    DataRow currentRow = row;

                    // Handle the remove button click event
                    item.RemoveButtonClick += (sender, args) => RemoveItemFromCart(currentRow);

                    // Add the cart item to the flowLayoutPanel
                    flowLayoutPanel1.Invoke((MethodInvoker)(() => flowLayoutPanel1.Controls.Add(item)));
                }
            });
        }

        async void RemoveItemFromCart(DataRow row)
        {
            await access_Cart.DeleteItemAsync(row["student_id"].ToString(), row["class_id"].ToString(), row["month"].ToString(), row["year"].ToString());
            await update_my_cart_ui();
            txtinvTotalGross.Text = await access_Cart.LoadTotalGrossAsync();
            txtDiscount.Text = await access_Cart.LoadTotalDiscountAsync();
            CalculateNet();
        }

        public async Task Add_Item_Form()
        {
            string studentId = "";

            if (rbId.Checked && string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Error: Student ID is empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                studentId = txtID.Text;
            }

            if (rbName.Checked && txtStudent.SelectedItem == null)
            {
                MessageBox.Show("Error: Student Name selection is empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                studentId = txtStudent.SelectedValue.ToString();
            }

            if (string.IsNullOrEmpty(txtClass.Text) || string.IsNullOrEmpty(txtGrade.Text) || string.IsNullOrEmpty(txtBatch.Text) ||
                string.IsNullOrEmpty(txtAmmount.Text) || string.IsNullOrEmpty(txtMonth.Text) || string.IsNullOrEmpty(txtYear.Text))
            {
                MessageBox.Show("Error: Some invoice details are empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string itemState = await access_Cart.Check_already_exist(studentId, txtClass.SelectedValue.ToString(), txtMonth.Text, txtYear.Text);

            using (SqlConnection con = connection.my_conn())
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                //--------------------------------------------------------------------------------------------------------------------------------

                if (!await access_Cart.CheckStudentInClassAsync(con, studentId, txtClass.SelectedValue.ToString()))
                {
                    MessageBox.Show("Student Does Not in The Class that you selected of you select a disabled class!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //--------------------------------------------------------------------------------------------------------------------------------

                int discount_value = await access_Cart.Get_Student_Discount_Value(con, studentId);

                //--------------------------------------------------------------------------------------------------------------------------------

                bool class_eligibility;

                if (itemState == "ADD")
                {
                    class_eligibility = await access_Cart.Check_Class_Discount_Eligible(con, txtClass.SelectedValue.ToString());

                    await access_Cart.InsertDataAsync(
                        studentId,
                        txtClass.SelectedValue.ToString(),
                        txtClass.Text,
                        txtGrade.Text,
                        txtBatch.Text,
                        txtMonth.Text,
                        txtYear.Text,
                        txtAmmount.Text,
                        class_eligibility ? discount_value.ToString() : "0",
                        class_eligibility ? (Convert.ToInt32(txtAmmount.Text) - discount_value).ToString() : txtAmmount.Text.ToString());

                    await update_my_cart_ui();
                    txtinvTotalGross.Text = await access_Cart.LoadTotalGrossAsync();
                    txtDiscount.Text = await access_Cart.LoadTotalDiscountAsync();
                    CalculateNet();
                }
                else if (itemState == "EXIST")
                {
                    MessageBox.Show("This item already exists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (itemState == "PAID")
                {
                    MessageBox.Show("Already Paid !", "PAID", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        // -------------------------------------------------------------------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------------------------------------------------------------------

        private async void pgInvoiceCart_Load(object sender, EventArgs e)
        {
            txtMonth.Text = DateTime.Now.ToString("MMMM");

            try
            {
                await LoadStudent();
            }
            catch (Exception) { }

            try
            {
                LoadClass();
            }
            catch (Exception) { }
        }
        private void txtID_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (txtStudent.Text != "" && rbName.Checked)
                {
                    LoadClass(txtStudent.SelectedValue.ToString());
                } else if (txtID.Text != "" && rbId.Checked)
                {
                    LoadClass(txtID.Text.ToString());
                }
                
            }
        }
        private void txtStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (txtStudent.Text != "" && rbName.Checked)
                {
                    LoadClass(txtStudent.SelectedValue.ToString());
                }
                else if (txtID.Text != "" && rbId.Checked)
                {
                    LoadClass(txtID.Text.ToString());
                }
            }
        }
        private async void txtClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtClass.Text != "")
            {
                try
                {
                    await load_class_meta(txtClass.SelectedValue.ToString());
                }
                catch { }
                
            }
        }

        // -------------------------------------------------------------------------------------------------------------------------------------------

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (txtinvTotalGross.Text != "" && txtinvTotalNet.Text != "")
            {
                if (txtincCashReceve.Text != "")
                {
                    if (Convert.ToInt32(txtinvTotalGross.Text) >= 0)
                    {
                        txtInvBalance.Text = (Convert.ToInt32(txtincCashReceve.Text) - Convert.ToInt32(txtinvTotalNet.Text)).ToString();
                    }
                }
            }
        }
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (rbId.Checked)
            {
                if (txtID.Text != "")
                {
                    try
                    {
                        await LoadCart(txtID.Text.Trim());
                    } catch { }
                }
            }
            else if (rbName.Checked)
            {
                if (txtStudent.Text != "")
                {
                    try
                    {
                        await LoadCart(txtStudent.SelectedValue.ToString().Trim());
                    } catch { }
                }
            }
        }
        private async void guna2Button2_Click(object sender, EventArgs e)
        {
            if (txtinvTotalGross.Text != "" && txtincCashReceve.Text != "" && txtInvBalance.Text != "")
            {
                await access_Cart.Pay(txtinvBank.Text, "Admin", txtincCashReceve.Text, txtInvBalance.Text);
            }
            else
            {
                MessageBox.Show("Invoice Details Missing !", "warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            await update_my_cart_ui();
            clear_invoice();
            txtinvTotalGross.Text = await access_Cart.LoadTotalGrossAsync();
            txtDiscount.Text = await access_Cart.LoadTotalDiscountAsync();
            CalculateNet();
        }
        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            await access_Cart.Recept(txtRePrintRn.Text);
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            btnSearch.PerformClick();
        }
        private async void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            await Add_Item_Form();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                LoadClass();
            } else if (checkBox1.Checked)
            {
                if (txtStudent.Text != "" && rbName.Checked)
                {
                    LoadClass(txtStudent.SelectedValue.ToString());
                }
                else if (txtID.Text != "" && rbId.Checked)
                {
                    LoadClass(txtID.Text.ToString());
                }
            }
        }
        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateNet();
        }
    }
}
