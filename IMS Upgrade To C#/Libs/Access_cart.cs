using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMS_Upgraded_C_;
using System.Web.Services.Description;
using System.Management;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace IMS_Upgrade_To_C_.Libs
{
    internal class Access_cart
    {
        Connection connection;
        public Access_cart() 
        { 
            connection = new Connection();
        }

        string user;
        public async Task InsertDataAsync(string studentId, string classId, string className, string grade, string batch, string month, string year, string fee, string discount, string net)
        {
            string dbFilePath = Path.Combine(Application.StartupPath, "Transaction_Temp.db"); // SQLite database file path
            string tableName = "Temp_Cart"; // SQLite table name

            try
            {
                string connectionString = $"Data Source={dbFilePath};Version=3;";

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    await conn.OpenAsync();

                    // Use parameterized query to prevent SQL injection
                    string sql = $"INSERT INTO {tableName} (student_id, class_id, class_name, grade, batch, [month], [year], [amount], [discount], [net]) " +
                                 $"VALUES (@studentId, @classId, @className, @grade, @batch, @month, @year, @fee, @discount, @net)";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        // Add parameters explicitly
                        cmd.Parameters.AddWithValue("@studentId", studentId.Trim());
                        cmd.Parameters.AddWithValue("@classId", classId.Trim());
                        cmd.Parameters.AddWithValue("@className", className);
                        cmd.Parameters.AddWithValue("@grade", grade);
                        cmd.Parameters.AddWithValue("@batch", batch);
                        cmd.Parameters.AddWithValue("@month", month.Trim());
                        cmd.Parameters.AddWithValue("@year", year.Trim());
                        cmd.Parameters.AddWithValue("@fee", fee);
                        cmd.Parameters.AddWithValue("@discount", discount);
                        cmd.Parameters.AddWithValue("@net", net);

                        await cmd.ExecuteNonQueryAsync();
                        //MessageBox.Show("Data inserted successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        public async Task<bool> CheckStudentInClassAsync(SqlConnection con, string studentID, string classId)
        {
            try
            {
                string selectQuery = "SELECT 1 FROM Vw_studyStudentList WHERE studentID = @studentID AND classID = @classID AND state = 1";

                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    cmd.Parameters.AddWithValue("@studentID", studentID);
                    cmd.Parameters.AddWithValue("@classID", classId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        return reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                MessageBox.Show(ex.Message);
                throw; // Consider rethrowing or logging based on your application's needs
            }
        }


        //---------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<string> Check_already_exist(string studentId, string classId, string month, string year)
        {
            string itemExists = "";

            using (SqlConnection con = connection.my_conn())
            {
                if (con.State != ConnectionState.Open)
                {
                    await con.OpenAsync();
                }

                string check = "SELECT id FROM class_fees " +
                                "WHERE class_id=@class_id and student_id=@student_id and month=@month and year = @year and recept_no IS NULL";

                using (SqlCommand cmd2 = new SqlCommand(check, con))
                {
                    cmd2.Parameters.AddWithValue("@student_id", studentId);
                    cmd2.Parameters.AddWithValue("@class_id",classId);
                    cmd2.Parameters.AddWithValue("@month", month);
                    cmd2.Parameters.AddWithValue("@year", year);

                    using (SqlDataReader allreader = await cmd2.ExecuteReaderAsync())
                    {
                        if (allreader.HasRows)
                        {
                            allreader.Close();

                            itemExists = await CartChecker(studentId, classId, month, year);
                            //---------------------------------------------
                        }
                        else
                        {
                            allreader.Close();

                            string check2 = "SELECT id FROM class_fees " +
                                            "WHERE class_id=@class_id and student_id=@student_id and month=@month and year = @year  and recept_no IS NOT NULL";

                            using (SqlCommand cmd3 = new SqlCommand(check2, con))
                            {
                                cmd3.Parameters.AddWithValue("@student_id", studentId);
                                cmd3.Parameters.AddWithValue("@class_id", classId);
                                cmd3.Parameters.AddWithValue("@month", month);
                                cmd3.Parameters.AddWithValue("@year", year);

                                using (SqlDataReader allreader2 = await cmd3.ExecuteReaderAsync())
                                {
                                    if (allreader2.HasRows)
                                    {
                                        allreader2.Close();                                       
                                        itemExists = "PAID";
                                    }
                                    else
                                    {
                                        itemExists = await CartChecker(studentId, classId, month, year);
                                        //--------------------------------------------
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return itemExists;
        }

        private async Task<string> CartChecker(string studentId, string classId, string month, string year)
        {
            string itemExists = "";
            string dbFilePath = Path.Combine(Application.StartupPath, "Transaction_Temp.db"); // SQLite database file path
            string tableName = "Temp_Cart"; // SQLite table name

            try
            {
                string connectionString = $"Data Source={dbFilePath};Version=3;";

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    await conn.OpenAsync();

                    string sql = $"SELECT * FROM {tableName} WHERE student_id = @studentId AND class_id = @classId AND month = @month AND year = @year";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        studentId = studentId.Trim();
                        cmd.Parameters.AddWithValue("@studentId", studentId.Trim());
                        cmd.Parameters.AddWithValue("@classId", classId.Trim());
                        cmd.Parameters.AddWithValue("@month", month.Trim());
                        cmd.Parameters.AddWithValue("@year", year.Trim());

                        using (SQLiteDataReader reader = (SQLiteDataReader)await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                itemExists = "EXIST";
                            }
                            else
                            {
                                itemExists = "ADD";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            return itemExists;
        }


        //---------------------------------------------------------------------------------------------------------------------------------------------

        private async Task<Dictionary<string, string>> Check_Discount_Settings(SqlConnection con)
        {
            Dictionary<string, string> discountSettings = new Dictionary<string, string>();
            try
            {
                string selectQuery = "SELECT * FROM discount_settings";

                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                discountSettings[reader.GetName(i)] = reader[i].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            return discountSettings;
        }

        private async Task<int> CheckEligibleStudentClassCount(SqlConnection con, string student_id)
        {
            int class_count = 0;
            try
            {
                string selectQuery = "SELECT class_count FROM Vw_Discount_Eligable_Students where student_id = @studentId";

                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    cmd.Parameters.AddWithValue("@studentId", student_id);

                    object result = await cmd.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                    {
                        class_count = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            return class_count;
        }

        public async Task<int> Get_Student_Discount_Value(SqlConnection con, string student_id)
        {
            try
            {
                Dictionary<string, string> discountSettings = await Check_Discount_Settings(con);
                string discount_option_state = discountSettings["discount_option_state"];
                string discount_by_1_state = discountSettings["discount_by_1_state"];
                string discount_by_1 = discountSettings["discount_by_1"];
                string discount_by_1_value = discountSettings["discount_by_1_value"];
                string discount_by_2_state = discountSettings["discount_by_2_state"];
                string discount_by_2 = discountSettings["discount_by_2"];
                string discount_by_2_value = discountSettings["discount_by_2_value"];
                string discount_by_3_state = discountSettings["discount_by_3_state"];
                string discount_by_3 = discountSettings["discount_by_3"];
                string discount_by_3_value = discountSettings["discount_by_3_value"];

                int eligible_class_count = await CheckEligibleStudentClassCount(con, student_id);
                int D_Value = 0;

                if (discount_option_state == "1")
                {
                    if (discount_by_1_state == "1")
                    {
                        if (eligible_class_count < Convert.ToInt32(discount_by_1))
                        {
                            D_Value = 0;
                            return D_Value;
                        }
                        else if (eligible_class_count >= Convert.ToInt32(discount_by_1))
                        {
                            D_Value = Convert.ToInt32(discount_by_1_value);
                        }
                    }

                    if (discount_by_1_state == "1" && discount_by_2_state == "1")
                    {
                        if (eligible_class_count >= Convert.ToInt32(discount_by_1) && eligible_class_count < Convert.ToInt32(discount_by_2))
                        {
                            D_Value = Convert.ToInt32(discount_by_1_value);
                        }
                        else if (eligible_class_count > Convert.ToInt32(discount_by_1) && eligible_class_count >= Convert.ToInt32(discount_by_2))
                        {
                            D_Value = Convert.ToInt32(discount_by_2_value);
                        }
                    }

                    if (discount_by_1_state == "1" && discount_by_2_state == "1" && discount_by_3_state == "1")
                    {
                        if (eligible_class_count >= Convert.ToInt32(discount_by_3))
                        {
                            D_Value = Convert.ToInt32(discount_by_3_value);
                        }
                    }
                }

                return D_Value;
            }
            catch (Exception)
            {
                // Log or handle the exception appropriately
                throw;
            }
        }

        public async Task<bool> Check_Class_Discount_Eligible(SqlConnection con, string class_id)
        {
            try
            {
                string selectQuery = "SELECT discount FROM class WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    cmd.Parameters.AddWithValue("@id", class_id);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                if (reader["discount"] == DBNull.Value)
                                {
                                    return false;
                                }
                                else
                                {
                                    return Convert.ToInt32(reader["discount"]) == 1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Default to false if there are any errors or no rows
            return false;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------

        public async Task ClearAllDataAsync()
        {
            string dbFilePath = Path.Combine(Application.StartupPath, "Transaction_Temp.db"); // SQLite database file path
            string tableName = "Temp_Cart"; // SQLite table name

            try
            {
                string connectionString = $"Data Source={dbFilePath};Version=3;";

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    await conn.OpenAsync();

                    string sql = $"DELETE FROM {tableName}";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        public async Task<DataTable> GetAllDataAsync()
        {
            DataTable dataTable = new DataTable();
            string dbFilePath = Path.Combine(Application.StartupPath, "Transaction_Temp.db"); // SQLite database file path
            string tableName = "Temp_Cart"; // SQLite table name

            try
            {
                string connectionString = $"Data Source={dbFilePath};Version=3;";

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    await conn.OpenAsync();

                    string sql = $"SELECT * FROM {tableName}";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        using (SQLiteDataReader reader = (SQLiteDataReader)await cmd.ExecuteReaderAsync())
                        {
                            // Create columns in DataTable based on reader's schema
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                dataTable.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                            }

                            // Read data and populate DataTable
                            while (await reader.ReadAsync())
                            {
                                DataRow row = dataTable.NewRow();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row[i] = reader.GetValue(i);
                                }

                                dataTable.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            return dataTable;
        }

        public async Task<string> LoadTotalGrossAsync()
        {
            string amount = "";
            int total = 0;
            string dbFilePath = Path.Combine(Application.StartupPath, "Transaction_Temp.db"); // SQLite database file path
            string tableName = "Temp_Cart"; // SQLite table name

            try
            {
                string connectionString = $"Data Source={dbFilePath};Version=3;";

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    await conn.OpenAsync();

                    string sql = $"SELECT SUM(amount) FROM {tableName}";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        // ExecuteScalarAsync returns the first column of the first row
                        // Cast the result to Nullable<long> to handle possible null values
                        object result = await cmd.ExecuteScalarAsync();

                        // Check if the result is not null and convert it to an integer
                        if (result != DBNull.Value && result != null)
                        {
                            total = Convert.ToInt32(result);
                        }

                        amount = total.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            return amount;
        }

        public async Task<string> LoadTotalDiscountAsync()
        {
            string amount = "";
            int total = 0;
            string dbFilePath = Path.Combine(Application.StartupPath, "Transaction_Temp.db"); // SQLite database file path
            string tableName = "Temp_Cart"; // SQLite table name

            try
            {
                string connectionString = $"Data Source={dbFilePath};Version=3;";

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    await conn.OpenAsync();

                    string sql = $"SELECT SUM(discount) FROM {tableName}";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        // ExecuteScalarAsync returns the first column of the first row
                        // Cast the result to Nullable<long> to handle possible null values
                        object result = await cmd.ExecuteScalarAsync();

                        // Check if the result is not null and convert it to an integer
                        if (result != DBNull.Value && result != null)
                        {
                            total = Convert.ToInt32(result);
                        }

                        amount = total.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            return amount;
        }

        public async Task DeleteItemAsync(string studentId, string classId, string month, string year)
        {
            string dbFilePath = Path.Combine(Application.StartupPath, "Transaction_Temp.db"); // SQLite database file path
            string tableName = "Temp_Cart"; // SQLite table name

            try
            {
                string connectionString = $"Data Source={dbFilePath};Version=3;";

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    await conn.OpenAsync();

                    string sql = $"DELETE FROM {tableName} WHERE student_id = @studentId AND class_id = @classId AND month = @month AND year = @year";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        // Add parameters to the command to avoid SQL injection
                        cmd.Parameters.AddWithValue("@studentId", studentId);
                        cmd.Parameters.AddWithValue("@classId", classId);
                        cmd.Parameters.AddWithValue("@month", month);
                        cmd.Parameters.AddWithValue("@year", year);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        //---------------------------------------------------------------------------------------------------------------------------------------------

        private string GetNextNum()
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

        private string GetNextNumfee()
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

        public async Task Pay(string bankrecept, string User, string cash_received, string cash_balance)
        {
            string ReceptNumber = GetNextNum();
            string dbFilePath = Path.Combine(Application.StartupPath, "Transaction_Temp.db"); // SQLite database file path
            string tableName = "Temp_Cart"; // SQLite table name
            user = User;

            try
            {
                string connectionString = $"Data Source={dbFilePath};Version=3;";

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    await conn.OpenAsync();

                    string sql = $"SELECT * FROM {tableName}";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        using (SQLiteDataReader reader = (SQLiteDataReader)await cmd.ExecuteReaderAsync())
                        {
                            string message = "";
                            bool checkDone = false;
                            string student = "";

                            while (await reader.ReadAsync())
                            {
                                student = reader["student_id"].ToString();

                                using (SqlConnection con = connection.my_conn())
                                {
                                    if (con.State != ConnectionState.Open)
                                    {
                                        await con.OpenAsync();
                                    }

                                    string check = "SELECT id FROM class_fees " +
                                                    "WHERE class_id=@class_id and student_id=@student_id and month=@month and year = @year and recept_no IS NULL";

                                    using (SqlCommand cmd2 = new SqlCommand(check, con))
                                    {
                                        cmd2.Parameters.AddWithValue("@student_id", reader["student_id"].ToString());
                                        cmd2.Parameters.AddWithValue("@class_id", reader["class_id"].ToString());
                                        cmd2.Parameters.AddWithValue("@month", reader["month"].ToString());
                                        cmd2.Parameters.AddWithValue("@year", reader["year"].ToString());

                                        using (SqlDataReader allreader = await cmd2.ExecuteReaderAsync())
                                        {
                                            if (allreader.HasRows)
                                            {
                                                allreader.Close();

                                                try
                                                {
                                                    await UpdateClassFeesAndInsertReceipt(con, reader["class_id"].ToString(), reader["month"].ToString(), reader["year"].ToString()
                                                        , reader["amount"].ToString(), reader["student_id"].ToString(), ReceptNumber, cash_received, cash_balance, bankrecept
                                                        , reader["discount"].ToString(), reader["net"].ToString());

                                                    message += $"The Payment of @{reader["amount"]} to @{reader["class_name"]} class for @{reader["month"]} month @{reader["year"]} has" +
                                                        $" Successfully Paid on @{DateTime.Now.ToString("yyyy-MM-dd")}. ReceptNo : @{ReceptNumber} ";
                                                }
                                                catch (Exception e)
                                                {
                                                    MessageBox.Show(e.Message);
                                                }

                                                checkDone = true;
                                            }
                                            else
                                            {
                                                allreader.Close();

                                                string checkPaid = "SELECT * FROM class_fees " +
                                                                  "WHERE class_id=@class_id and student_id=@student_id and month=@month and year = @year and recept_no IS NOT NULL";

                                                using (SqlCommand paidCmd = new SqlCommand(checkPaid, con))
                                                {
                                                    paidCmd.Parameters.AddWithValue("@student_id", reader["student_id"].ToString());
                                                    paidCmd.Parameters.AddWithValue("@class_id", reader["class_id"].ToString());
                                                    paidCmd.Parameters.AddWithValue("@month", reader["month"].ToString());
                                                    paidCmd.Parameters.AddWithValue("@year", reader["year"].ToString());

                                                    using (SqlDataReader reader2 = await paidCmd.ExecuteReaderAsync())
                                                    {
                                                        if (!reader2.HasRows)
                                                        {
                                                            reader2.Close();

                                                            try
                                                            {
                                                                await InsertClassFeesAndReceipt(con, reader["class_id"].ToString(), reader["month"].ToString(), reader["year"].ToString()
                                                                    , reader["amount"].ToString(), reader["student_id"].ToString(), ReceptNumber, cash_received, cash_balance, bankrecept
                                                                    , reader["discount"].ToString(), reader["net"].ToString());

                                                                message += $"The Payment of @{reader["amount"]} to @{reader["class_name"]} class for @{reader["month"]} month @{reader["year"]} has" +
                                                                    $" Successfully Paid on @{DateTime.Now.ToString("yyyy-MM-dd")}. ReceptNo : @{ReceptNumber} ";
                                                            }
                                                            catch (Exception e)
                                                            {
                                                                MessageBox.Show(e.Message);
                                                            }

                                                            checkDone = true;
                                                        }
                                                        else
                                                        {
                                                            reader2.Close();
                                                            MessageBox.Show("Paid", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            checkDone = false;
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (checkDone)
                            {
                                await GetContactNumAndSendParent(message, student);
                                await GetContactNumAndSendStudent(message, student);
                            }

                            await Recept(ReceptNumber);
                            await ClearAllDataAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task UpdateClassFeesAndInsertReceipt(SqlConnection con, string class_id, string paymonth, string payyear, string amount
            , string student_id, string recept_number, string cash_received, string cash_balance, string bankrecept, string discount, string net)
        {
            string updateClassFees =
                "UPDATE class_fees SET recept_no=@RN WHERE student_id=@student_id and class_id=@class_id and month=@month and year=@year";

            using (SqlCommand cmdUpdate = new SqlCommand(updateClassFees, con))
            {
                cmdUpdate.Parameters.AddWithValue("@RN", recept_number);
                cmdUpdate.Parameters.AddWithValue("@student_id", student_id);
                cmdUpdate.Parameters.AddWithValue("@class_id", class_id);
                cmdUpdate.Parameters.AddWithValue("@month", paymonth);
                cmdUpdate.Parameters.AddWithValue("@year", payyear);
                await cmdUpdate.ExecuteNonQueryAsync();
            }

            await InsertReceipt(con, class_id, paymonth, payyear, amount, student_id, recept_number, cash_received, cash_balance, bankrecept, discount, net);
        }

        private async Task InsertClassFeesAndReceipt(SqlConnection con, string class_id, string paymonth, string payyear, string amount
            , string student_id, string recept_number, string cash_received, string cash_balance, string bankrecept, string discount, string net)
        {
            string insertClassFees = "INSERT INTO class_fees VALUES (@id,@student_id,@class_id,@year,@month,@price,@RN)";

            using (SqlCommand cmdInsert = new SqlCommand(insertClassFees, con))
            {
                cmdInsert.Parameters.AddWithValue("@id", GetNextNumfee());
                cmdInsert.Parameters.AddWithValue("@student_id", student_id);
                cmdInsert.Parameters.AddWithValue("@class_id", class_id);
                cmdInsert.Parameters.AddWithValue("@year", payyear);
                cmdInsert.Parameters.AddWithValue("@month", paymonth);
                cmdInsert.Parameters.AddWithValue("@price", amount);
                cmdInsert.Parameters.AddWithValue("@RN", recept_number);
                await cmdInsert.ExecuteNonQueryAsync();
            }

            await InsertReceipt(con, class_id, paymonth, payyear, amount, student_id, recept_number, cash_received, cash_balance, bankrecept, discount, net);
        }

        private async Task InsertReceipt(SqlConnection con, string class_id, string paymonth, string payyear
            , string amount, string student_id, string recept_number, string cash_received, string cash_balance, string bankrecept, string discount, string net)
        {
            string paymentMethod = (string.IsNullOrEmpty(bankrecept)) ? "cash" : "bank";

            string insertReceipt = @"
        INSERT INTO recept 
        VALUES (@recept_no, @student_id, @class_id, @month, @year, @date, @payment_method, @amount, @discount, @net, @collect_by, @slipt_No, @cash_rec, @balance)";

            using (SqlCommand cmdReceipt = new SqlCommand(insertReceipt, con))
            {
                cmdReceipt.Parameters.AddWithValue("@recept_no", recept_number);
                cmdReceipt.Parameters.AddWithValue("@student_id", student_id);
                cmdReceipt.Parameters.AddWithValue("@class_id", class_id);
                cmdReceipt.Parameters.AddWithValue("@month", paymonth);
                cmdReceipt.Parameters.AddWithValue("@year", payyear);
                cmdReceipt.Parameters.AddWithValue("@date", DateTime.UtcNow.ToString("yyyy/MM/dd"));
                cmdReceipt.Parameters.AddWithValue("@payment_method", paymentMethod);
                cmdReceipt.Parameters.AddWithValue("@amount", amount);
                cmdReceipt.Parameters.AddWithValue("@discount", discount);
                cmdReceipt.Parameters.AddWithValue("@net", net);
                cmdReceipt.Parameters.AddWithValue("@collect_by", user);
                cmdReceipt.Parameters.AddWithValue("@slipt_No", bankrecept);
                cmdReceipt.Parameters.AddWithValue("@cash_rec", cash_received);
                cmdReceipt.Parameters.AddWithValue("@balance", cash_balance);

                await cmdReceipt.ExecuteNonQueryAsync();
            }
        }

        private async Task GetContactNumAndSendParent(string message, string student)
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    await con.OpenAsync();

                    string getContact = "SELECT parent FROM student WHERE id=@id";
                    using (SqlCommand cmd = new SqlCommand(getContact, con))
                    {
                        cmd.Parameters.AddWithValue("@id", student);
                        object result = await cmd.ExecuteScalarAsync();

                        if (result != DBNull.Value)
                        {
                            string parentNumber = result.ToString();
                            _ = connection.sms(parentNumber, message);
                        }
                        else
                        {
                            // Default value or action if the result is DBNull.Value
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log them, or throw them further if needed
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async Task GetContactNumAndSendStudent(string message, string student)
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    await con.OpenAsync();

                    string getContact = "SELECT contact FROM student WHERE id=@id";
                    using (SqlCommand cmd = new SqlCommand(getContact, con))
                    {
                        cmd.Parameters.AddWithValue("@id", student);
                        object result = await cmd.ExecuteScalarAsync();

                        if (result != DBNull.Value)
                        {
                            string studentNumber = result.ToString();
                            _ = connection.sms(studentNumber, message);
                        }
                        else
                        {
                            // Default value or action if the result is DBNull.Value
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log them, or throw them further if needed
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public Task Recept(string text)
        {
            ReportDocument rep = new ReportDocument();
            rep.Load(Application.StartupPath + "\\Reports\\Invoice_recept.RPT");

            CrystalDecisions.Shared.TableLogOnInfo logonInfo = rep.Database.Tables[0].LogOnInfo;
            logonInfo.ConnectionInfo.ServerName = connection.Get_path();
            logonInfo.ConnectionInfo.DatabaseName = "IMS";
            logonInfo.ConnectionInfo.UserID = "sa";
            logonInfo.ConnectionInfo.Password = "";

            foreach (Table table in rep.Database.Tables)
            {
                table.ApplyLogOnInfo(logonInfo);
            }

            rep.RecordSelectionFormula = "{Vw_Recept.recept_no} ='" + text + "'";
            rep.Refresh();
            // rep.PrintToPrinter(1, false,0,0);

            ReportViewer rv = new ReportViewer(rep);
            rv.Show();
            return Task.CompletedTask;
        }
    }
}
