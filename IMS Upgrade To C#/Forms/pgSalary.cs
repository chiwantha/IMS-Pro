using CrystalDecisions.CrystalReports.Engine;
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
    public partial class pgSalary : Form
    {
        Connection connection;
        public pgSalary()
        {
            InitializeComponent();
            connection = new Connection();
        }

        private async Task<int> LoadSalaryCalculationData()
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    await con.OpenAsync();

                    string selectQuery = "SELECT SalaryCalOn FROM Data";

                    using (SqlCommand command = new SqlCommand(selectQuery, con))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return Convert.ToInt32(reader["SalaryCalOn"]);
                            }
                            else
                            {
                                throw new InvalidOperationException("No data found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        string GetNextNum()
        {
            using (SqlConnection con = connection.my_conn())
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                string Get = "SELECT MAX(id) FROM Teacher_salary";
                using (SqlCommand gcmd = new SqlCommand(Get, con))
                {
                    object result = gcmd.ExecuteScalar();

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
                MessageBox.Show("Process Unsuccessful");
            }
        }
        void LoadPaids()
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
                    string selectQuery = "SELECT * FROM Vw_Salary where Teacher_ID = '" + txtTeacher.SelectedValue + "'";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con);
                    DataTable dataTable = new DataTable();

                    dataAdapter.Fill(dataTable);

                    dgView.AutoGenerateColumns = true;
                    dgView.DataSource = dataTable;
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
        void LoadGridData_ClassTree()
        {
            SqlConnection con = connection.my_conn();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            try
            {
                string Select = "Select * from Vw_Sal_Class_Tree_Details where Teacher_ID=@tid and month=@month and year=@year";
                SqlCommand cmd = new SqlCommand(Select, con);
                cmd.Parameters.AddWithValue("@tid", txtTeacher.SelectedValue);
                cmd.Parameters.AddWithValue("@month", txtMonth.Text);
                cmd.Parameters.AddWithValue("@year", txtYear.Text);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();

                dataAdapter.Fill(dataTable);

                dgClassTree.AutoGenerateColumns = true;
                dgClassTree.DataSource = dataTable;
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
        void LoadGridData_SalaryCal()
        {
            SqlConnection con = connection.my_conn();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            try
            {
                string Select = "Select * from Vw_Salary_Cal where Teacher_ID=@tid and month=@month and year=@year";
                SqlCommand cmd = new SqlCommand(Select, con);
                cmd.Parameters.AddWithValue("@tid", txtTeacher.SelectedValue);
                cmd.Parameters.AddWithValue("@month", txtMonth.Text);
                cmd.Parameters.AddWithValue("@year", txtYear.Text);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();

                dataAdapter.Fill(dataTable);

                dgSalaryCal.AutoGenerateColumns = false;
                dgSalaryCal.DataSource = dataTable;
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
        void GetSalaryAndExpenses()
        {
            SqlConnection con = connection.my_conn();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            try
            {
                // Calculate Salary
                string salarySelect = "Select Total_Class_Estimates,Total_Class_Outstandings,Number_Of_Class from Vw_Salary_Cal where Teacher_ID=@tid and month=@month and year=@year";
                SqlCommand salaryCmd = new SqlCommand(salarySelect, con);
                salaryCmd.Parameters.AddWithValue("@tid", txtTeacher.SelectedValue);
                salaryCmd.Parameters.AddWithValue("@month", txtMonth.Text);
                salaryCmd.Parameters.AddWithValue("@year", txtYear.Text);
                SqlDataReader salaryReader = salaryCmd.ExecuteReader();

                if (salaryReader.HasRows)
                {
                    while (salaryReader.Read())
                    {
                        txtClassCount.Text = salaryReader["Number_Of_Class"].ToString();
                        string totalClassEstimate = salaryReader["Total_Class_Estimates"].ToString();
                        string totalOutstanding = salaryReader["Total_Class_Outstandings"].ToString();
                        Vw_Estimate_Salary_Cal.Text = totalClassEstimate;
                        Vw_Total_Outstandings.Text = totalOutstanding;
                        Vw_Total_Earnings.Text = (Convert.ToInt32(totalClassEstimate) - Convert.ToInt32(totalOutstanding)).ToString();
                    }
                    salaryReader.Close();
                }
                else
                {
                    salaryReader.Close();
                    Vw_Estimate_Salary_Cal.Text = "0"; // Set default value if no salary data found
                    Vw_Total_Outstandings.Text = "0";
                    Vw_Total_Earnings.Text = "0";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        async void rateCal()
        {
            int Estimate = Convert.ToInt32(Vw_Estimate_Salary_Cal.Text);            
            int earning = Convert.ToInt32(Vw_Total_Earnings.Text);
            int rate = Convert.ToInt32(txtRate.Text);

            int Salary = 0;
            int Institute_Charge = 0;
            int OnInstitute = 0;

            if (await LoadSalaryCalculationData() == 0)
            {
                Salary = Estimate * rate / 100;
                Institute_Charge = Estimate - Salary;              

            } 
            else if (await LoadSalaryCalculationData() == 1)
            {
                Salary = earning * rate / 100;
                Institute_Charge = earning - Salary;
            }

            if (earning < Salary)
            {
                OnInstitute = earning - Salary;
            }
            else
            {
                OnInstitute = 0;
            }

            Vw_Salary.Text = Salary.ToString();
            Vw_OnIstitute.Text = OnInstitute.ToString();
            Vw_Institute_Charge.Text = Institute_Charge.ToString();
        }
        async void pay()
        {
            // Simple Profit Cal ---------------------------------------------------------------------------------------------------------------------------
            int Profit = 0;
            try
            {
                int Estimate = Convert.ToInt32(Vw_Estimate_Salary_Cal.Text);
                int Outstanding = Convert.ToInt32(Vw_Total_Outstandings.Text);
                int Salary = 0;
                int rate = Convert.ToInt32(txtRate.Text);
                Salary = Estimate * rate / 100;
                Profit = (Estimate - Salary) - Outstanding;
            }
            catch (Exception) { }
            // ---------------------------------------------------------------------------------------------------------------------------------------------

            SqlConnection con = connection.my_conn();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            try
            {
                string checkQuery = "SELECT * FROM Teacher_salary WHERE teacher_id=@tid AND year=@year AND month=@month";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@tid", txtTeacher.SelectedValue);
                checkCmd.Parameters.AddWithValue("@year", txtYear.Text);
                checkCmd.Parameters.AddWithValue("@month", txtMonth.Text);
                SqlDataReader reader = checkCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Close();
                    if (MessageBox.Show("Do you want to update the record?", "Record Exists!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        // Update existing record
                        string updateQuery = "UPDATE Teacher_salary SET " +
                            "rate=@rate, class_count=@class_count,ClassEstimated=@ClassEstimated, ClassEarnings=@ClassEarnings,calculate_by=@calculate_by, " +
                            "ClassOutstandings=@ClassOutstandings, onInstitute=@onInstitute, instituteProfit=@instituteProfit, instituteCharge=@instituteCharge, Salary=@Salary, date=@date " +
                            "WHERE teacher_id=@teacher_id AND month=@month AND year=@year";
                        SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                        updateCmd.Parameters.AddWithValue("@teacher_id", txtTeacher.SelectedValue);
                        updateCmd.Parameters.AddWithValue("@year", txtYear.Text);
                        updateCmd.Parameters.AddWithValue("@month", txtMonth.Text);
                        updateCmd.Parameters.AddWithValue("@rate", txtRate.Text);
                        updateCmd.Parameters.AddWithValue("@calculate_by", await LoadSalaryCalculationData() == 0 ? "On Estimate" : "On Earnings");
                        updateCmd.Parameters.AddWithValue("@class_count", txtClassCount.Text);
                        updateCmd.Parameters.AddWithValue("@ClassEstimated", Vw_Estimate_Salary_Cal.Text);
                        updateCmd.Parameters.AddWithValue("@ClassEarnings", Vw_Total_Earnings.Text);
                        updateCmd.Parameters.AddWithValue("@ClassOutstandings", Vw_Total_Outstandings.Text);
                        updateCmd.Parameters.AddWithValue("@onInstitute", Vw_OnIstitute.Text);
                        updateCmd.Parameters.AddWithValue("@instituteProfit", Profit);
                        updateCmd.Parameters.AddWithValue("@instituteCharge", Vw_Institute_Charge.Text);
                        updateCmd.Parameters.AddWithValue("@Salary", Vw_Salary.Text);
                        updateCmd.Parameters.AddWithValue("@date", txtDate.Value);
                        updateCmd.ExecuteNonQuery();
                        MessageBox.Show("Salary amount updated successfully!", "Success !");
                        report(txtTeacher.SelectedValue.ToString(), txtMonth.Text, txtYear.Text);
                    }
                }
                else
                {
                    reader.Close();
                    // Insert new record
                    string insertQuery = "INSERT INTO Teacher_salary VALUES " +
                        "(@ID ,@teacher_id, @year, @month, @rate, @calculate_by, @class_count, @ClassEstimated, " +
                        "@ClassEarnings, @ClassOutstandings, @onInstitute, @instituteProfit, @instituteCharge, @salary, @Date)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                    insertCmd.Parameters.AddWithValue("@ID", GetNextNum());
                    insertCmd.Parameters.AddWithValue("@teacher_id", txtTeacher.SelectedValue);
                    insertCmd.Parameters.AddWithValue("@year", txtYear.Text);
                    insertCmd.Parameters.AddWithValue("@month", txtMonth.Text);
                    insertCmd.Parameters.AddWithValue("@rate", txtRate.Text);
                    insertCmd.Parameters.AddWithValue("@calculate_by", await LoadSalaryCalculationData() == 0 ? "On Estimate" : "On Earnings");
                    insertCmd.Parameters.AddWithValue("@class_count", txtClassCount.Text);
                    insertCmd.Parameters.AddWithValue("@ClassEstimated", Vw_Estimate_Salary_Cal.Text);
                    insertCmd.Parameters.AddWithValue("@ClassEarnings", Vw_Total_Earnings.Text);
                    insertCmd.Parameters.AddWithValue("@ClassOutstandings", Vw_Total_Outstandings.Text);
                    insertCmd.Parameters.AddWithValue("@onInstitute", Vw_OnIstitute.Text);
                    insertCmd.Parameters.AddWithValue("@instituteProfit", Profit);
                    insertCmd.Parameters.AddWithValue("@instituteCharge", Vw_Institute_Charge.Text);
                    insertCmd.Parameters.AddWithValue("@Salary", Vw_Salary.Text);
                    insertCmd.Parameters.AddWithValue("@date", txtDate.Value);
                    insertCmd.ExecuteNonQuery();
                    MessageBox.Show("Paid Salary Amount Successfully!", "Payment Successful !");
                    report(txtTeacher.SelectedValue.ToString(), txtMonth.Text, txtYear.Text);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                LoadPaids();
            }
        }
        async void report(string Teacher_id, string month, string year)
        {
            
            //rest--------------------------------------------------------------------------------------
            try
            {
                ReportDocument rep = new ReportDocument();
                string selection = "";

                string reportPath;

                if (await LoadSalaryCalculationData() == 0)
                {
                    reportPath = Application.StartupPath + "\\Reports\\Teacher_Salary_on_es.RPT";
                } 
                else if (await LoadSalaryCalculationData() == 1)
                {
                    reportPath = Application.StartupPath + "\\Reports\\Teacher_Salary_on_er.RPT";
                } else
                {
                    reportPath = "";
                    MessageBox.Show("Error On Cal Data Loading on Report !", " Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                rep.Load(reportPath);

                CrystalDecisions.Shared.TableLogOnInfo logonInfo = rep.Database.Tables[0].LogOnInfo;
                logonInfo.ConnectionInfo.ServerName = connection.Get_path();
                logonInfo.ConnectionInfo.DatabaseName = "IMS";
                logonInfo.ConnectionInfo.UserID = "sa";
                logonInfo.ConnectionInfo.Password = "";
                foreach (CrystalDecisions.CrystalReports.Engine.Table table in rep.Database.Tables)
                {
                    table.ApplyLogOnInfo(logonInfo);
                }

                selection = "{Vw_Salary.teacher_id} = '" + Teacher_id + "' and {Vw_Salary.month}='" + month + "' and {Vw_Salary.Year}='" + year + "' and " +
                    "{Vw_Sal_Class_Tree_Details.Teacher_ID} = '" + Teacher_id + "' and {Vw_Sal_Class_Tree_Details.month}='" + month + "' and {Vw_Sal_Class_Tree_Details.year}='" + year + "' and" +
                    "{Vw_Salary_Cal.Teacher_ID} = '" + Teacher_id + "' and {Vw_Salary_Cal.month}='" + month + "' and {Vw_Salary_Cal.year}='" + year + "'";
                rep.RecordSelectionFormula = selection;

                rep.Refresh();

                ReportViewer rv = new ReportViewer(rep);
                rv.Show();
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                LoadGridData_ClassTree();
                LoadGridData_SalaryCal();
                GetSalaryAndExpenses();
                rateCal();
                LoadPaids();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void pgTeacherSalary_Load(object sender, EventArgs e)
        {
            txtMonth.Text = DateTime.Now.ToString("MMMM");
            LoadTeacher();
        }
        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            rateCal();
        }
        private void btnPayment_Click(object sender, EventArgs e)
        {
            pay();
        }

    }
}
