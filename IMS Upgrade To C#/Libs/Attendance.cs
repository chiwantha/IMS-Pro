using IMS_Upgraded_C_;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS_Upgrade_To_C_.Libs
{
    internal class Attendance
    {
        Connection connection;

        public Attendance()
        {
            connection = new Connection();
        }

        //-------------------------------------------------------------------------------------------

        public string StudentID { get; set; }
        public string ClassID { get; set; }
        public string ClassName { get; set; }
        public string StudentName { get; set; }
        public byte[] StudentImage { get; set;}
        public string StudentState { get; set;}
        public Color StudentColor { get; set;}
        public bool week1 { get; set;}
        public bool week2 { get; set;}
        public bool week3 { get; set;}
        public bool week4 { get; set;}
        public bool week5 { get; set;}

        public bool week1_2 { get; set;}
        public bool week2_2 { get; set;}
        public bool week3_2 { get; set;}
        public bool week4_2 { get; set;}
        public bool week5_2 { get; set;}

        private void ClearStudentData()
        {
            StudentID = string.Empty;
            ClassID = string.Empty;
            ClassName = string.Empty;
            StudentName = string.Empty;
            StudentImage = null;
            StudentState = string.Empty;
            StudentColor = Color.Empty;
            week1 = false;
            week2 = false;
            week3 = false;
            week4 = false;
            week5 = false;
        }

        string Link = "";
        string dyanamic_key = "";

        string livetime = DateTime.Now.ToString("HH:mm:ss tt");
        string day = DateTime.Now.ToString("dddd");
        string yearr = DateTime.Now.ToString("yyyy");
        string month = DateTime.Now.ToString("MMMM");
        string date = DateTime.Now.ToString("yyyy-MM-dd");
        int weekOfMonth = (DateTime.Now.Day - 1) / 7 + 1;

        //-------------------------------------------------------------------------------------------

        public async Task Check_Attendance_Availability(string id)
        {
            ClearStudentData();
            

            StudentID = id;
            StudentID.Trim();

            if (StudentID.Length == 6)
            {
                dyanamic_key = "student_id";
            } else if (StudentID.Length == 10)
            {
                dyanamic_key = "rfid";
            } else
            {
                return;
            }

            using (SqlConnection con = connection.my_conn())
            {
                con.Open();
                string search = "select student_id, student_name, class_id, class_name, image, in_time, out_time, link " +
                                "from Vw_Attendance_Check where "+dyanamic_key+"=@rfid and day=@day and in_time<=@time and out_time>=@time and state = 1";

                using (SqlCommand searchCmd = new SqlCommand(search, con))
                {
                    searchCmd.Parameters.AddWithValue("@rfid", id);
                    searchCmd.Parameters.AddWithValue("@day", day);
                    searchCmd.Parameters.AddWithValue("@time", livetime);

                    using (SqlDataReader searchReader = searchCmd.ExecuteReader())
                    {
                        if (searchReader.HasRows)
                        {
                            while (searchReader.Read())
                            {
                                StudentID = searchReader["student_id"].ToString();
                                StudentName = searchReader["student_name"].ToString();
                                ClassID = searchReader["class_id"].ToString();
                                ClassName = searchReader["class_name"].ToString();
                                StudentImage = searchReader["image"] as byte[];
                                StudentColor = Color.Green;
                                Link = searchReader["link"].ToString();
                            }

                            await Check_Holidays();

                            return;
                        }
                        else
                        {
                            StudentID = "";
                            StudentName = "";
                            ClassID = "";
                            ClassName = "";
                            StudentImage = null;
                            StudentState = "Not Okay";
                            StudentColor = Color.Red;
                            return;
                        }
                    }
                }
            }
        }

        private async Task Check_Holidays()
        {
            using (SqlConnection con = connection.my_conn())
            {
                con.Open();
                string check = "SELECT class_id, state FROM class_holiday WHERE class_id=@class_id AND date=@date";
                using (SqlCommand checkCmd = new SqlCommand(check, con))
                {
                    checkCmd.Parameters.AddWithValue("@class_id", ClassID);
                    checkCmd.Parameters.AddWithValue("@date", date);

                    using (SqlDataReader checkReader = checkCmd.ExecuteReader())
                    {
                        if (checkReader.HasRows)
                        {
                            while (checkReader.Read())
                            {
                                ClassID = checkReader["class_id"].ToString();
                                StudentState = checkReader["state"].ToString();
                                StudentColor = Color.Red;
                            }
                        }
                        else
                        {
                            if (Link == "")
                            {
                                await Process_Regular_Attendance();
                                return;
                            }
                            else
                            {
                                await Process_Extra_Attendance();
                                return;
                            }
                        }
                    }
                }
            }
        }

        //private async Task LoadAndSetAttendanceAsync(string studentId, string classId, string month, string year, string table)
        //{
        //    // reset all to false first
        //    week1 = false;
        //    week2 = false;
        //    week3 = false;
        //    week4 = false;
        //    week5 = false;

        //    // previous month reset all to false
        //    week1_2 = false;
        //    week2_2 = false;
        //    week3_2 = false;
        //    week4_2 = false;
        //    week5_2 = false;


        //    if (string.IsNullOrWhiteSpace(table))
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        using (SqlConnection con = connection.my_conn())
        //        {
        //            await con.OpenAsync();

        //            string query = @"SELECT week1, week2, week3, week4, week5
        //                     FROM " + table + @" 
        //                     WHERE student_id=@student_id 
        //                       AND class_id=@class_id  
        //                       AND month=@month 
        //                       AND year=@year";

        //            using (SqlCommand cmd = new SqlCommand(query, con))
        //            {
        //                cmd.Parameters.AddWithValue("@student_id", studentId);
        //                cmd.Parameters.AddWithValue("@class_id", classId);
        //                cmd.Parameters.AddWithValue("@month", month);
        //                cmd.Parameters.AddWithValue("@year", year);

        //                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
        //                {
        //                    if (await reader.ReadAsync())
        //                    {
        //                        week1 = reader["week1"]?.ToString() == "1";
        //                        week2 = reader["week2"]?.ToString() == "1";
        //                        week3 = reader["week3"]?.ToString() == "1";
        //                        week4 = reader["week4"]?.ToString() == "1";
        //                        week5 = reader["week5"]?.ToString() == "1";
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error loading attendance: " + ex.Message);
        //    }
        //}




        //-------------------------------------------------------------------------------------------

        private async Task LoadAndSetAttendanceAsync(string studentId, string classId, string monthName, string year, string table)
        {
            // Reset all current month flags
            week1 = week2 = week3 = week4 = week5 = false;

            // Reset all previous month flags
            week1_2 = week2_2 = week3_2 = week4_2 = week5_2 = false;

            if (string.IsNullOrWhiteSpace(table))
                return;

            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    await con.OpenAsync();

                    // Convert current month name to DateTime to get numeric month
                    DateTime currentMonthDate = DateTime.ParseExact(monthName, "MMMM", CultureInfo.InvariantCulture);
                    int currMonth = currentMonthDate.Month;
                    int currYear = int.Parse(year);

                    // Calculate previous month and year
                    int prevMonth = currMonth == 1 ? 12 : currMonth - 1;
                    int prevYear = currMonth == 1 ? currYear - 1 : currYear;

                    // Get full month names again (e.g., "May", "April")
                    string currMonthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(currMonth);
                    string prevMonthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(prevMonth);

                    string query = @"SELECT week1, week2, week3, week4, week5 
                             FROM " + table + @" 
                             WHERE student_id = @student_id 
                               AND class_id = @class_id  
                               AND month = @month 
                               AND year = @year";

                    // Load current month attendance
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@student_id", studentId);
                        cmd.Parameters.AddWithValue("@class_id", classId);
                        cmd.Parameters.AddWithValue("@month", currMonthName);
                        cmd.Parameters.AddWithValue("@year", currYear.ToString());

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                week1 = reader["week1"]?.ToString() == "1";
                                week2 = reader["week2"]?.ToString() == "1";
                                week3 = reader["week3"]?.ToString() == "1";
                                week4 = reader["week4"]?.ToString() == "1";
                                week5 = reader["week5"]?.ToString() == "1";
                            }
                        }
                    }

                    // Load previous month attendance
                    using (SqlCommand cmdPrev = new SqlCommand(query, con))
                    {
                        cmdPrev.Parameters.AddWithValue("@student_id", studentId);
                        cmdPrev.Parameters.AddWithValue("@class_id", classId);
                        cmdPrev.Parameters.AddWithValue("@month", prevMonthName);
                        cmdPrev.Parameters.AddWithValue("@year", prevYear.ToString());

                        using (SqlDataReader readerPrev = await cmdPrev.ExecuteReaderAsync())
                        {
                            if (await readerPrev.ReadAsync())
                            {
                                week1_2 = readerPrev["week1"]?.ToString() == "1";
                                week2_2 = readerPrev["week2"]?.ToString() == "1";
                                week3_2 = readerPrev["week3"]?.ToString() == "1";
                                week4_2 = readerPrev["week4"]?.ToString() == "1";
                                week5_2 = readerPrev["week5"]?.ToString() == "1";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading attendance: " + ex.Message);
            }
        }

        private async Task Process_Regular_Attendance()
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
            {
                con.Open();
                string selectQuery = "SELECT id FROM attendance WHERE class_id=@class_id AND student_id=@student_id AND month=@month AND year=@year";
                using (SqlCommand selectCmd = new SqlCommand(selectQuery, con))
                {
                    selectCmd.Parameters.AddWithValue("@student_id", StudentID);
                    selectCmd.Parameters.AddWithValue("@class_id", ClassID);
                    selectCmd.Parameters.AddWithValue("@year", yearr);
                    selectCmd.Parameters.AddWithValue("@month", month);

                    using (SqlDataReader selectReader = selectCmd.ExecuteReader())
                    {
                        if (selectReader.HasRows)
                        {
                            selectReader.Close();
                            string checkQuery = "SELECT id FROM attendance WHERE class_id=@class_id AND student_id=@student_id AND month=@month AND week" + weekOfMonth + "=@week AND year=@year";
                            using (SqlCommand checkCmd1 = new SqlCommand(checkQuery, con))
                            {
                                checkCmd1.Parameters.AddWithValue("@student_id", StudentID);
                                checkCmd1.Parameters.AddWithValue("@class_id", ClassID);
                                checkCmd1.Parameters.AddWithValue("@month", month);
                                checkCmd1.Parameters.AddWithValue("@week", "1");
                                checkCmd1.Parameters.AddWithValue("@year", yearr);

                                using (SqlDataReader checkReader1 = checkCmd1.ExecuteReader())
                                {
                                    if (!checkReader1.HasRows)
                                    {
                                        Update_Attendance();
                                        await MakeFeeRecordsAsync(StudentID, ClassID, LoadClassPrice(ClassID));
                                        this.Send();

                                            await LoadAndSetAttendanceAsync(StudentID, ClassID, month, yearr, "attendance");
                                            return;
                                    }
                                    else
                                    {
                                        StudentState = "Already Marked";
                                        await MakeFeeRecordsAsync(StudentID, ClassID, LoadClassPrice(ClassID));

                                            await LoadAndSetAttendanceAsync(StudentID, ClassID, month, yearr, "attendance");
                                            return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Insert_Attendance();                           
                            await MakeFeeRecordsAsync(StudentID, ClassID, LoadClassPrice(ClassID));

                            this.Send();

                                await LoadAndSetAttendanceAsync(StudentID, ClassID, month, yearr, "attendance");
                                return;
                        }
                    }
                }
            }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetNextNum_Regular_Attendance()
        {
            using (SqlConnection con = connection.my_conn())
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                string Get = "select MAX(id) from Attendance";
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

        private void Update_Attendance()
        {
            using (SqlConnection con = connection.my_conn())
            {
                con.Open();
                string updateQuery = "UPDATE attendance SET week" + weekOfMonth + "=1,date=@date WHERE class_id=@class_id AND student_id=@student_id AND month=@month AND year=@year";

                using (SqlCommand updateCmd = new SqlCommand(updateQuery, con))
                {
                    updateCmd.Parameters.AddWithValue("@date", date);
                    updateCmd.Parameters.AddWithValue("@student_id", StudentID);
                    updateCmd.Parameters.AddWithValue("@class_id", ClassID);
                    updateCmd.Parameters.AddWithValue("@month", month);
                    updateCmd.Parameters.AddWithValue("@year", yearr);
                    updateCmd.ExecuteNonQuery();
                }

                StudentState = "Ok !";
            }
        }

        private void Insert_Attendance()
        {
            using (SqlConnection con = connection.my_conn())
            {
                con.Open();
                string insertQuery = "INSERT INTO attendance (id, student_id, class_id, time, date, month, year, week" + weekOfMonth + ") VALUES (@id, @student_id, @class_id, @time, @date, @month, @year, @week)";
                using (SqlCommand insertCmd = new SqlCommand(insertQuery, con))
                {
                    insertCmd.Parameters.AddWithValue("@id", GetNextNum_Regular_Attendance());
                    insertCmd.Parameters.AddWithValue("@student_id", StudentID);
                    insertCmd.Parameters.AddWithValue("@class_id", ClassID);
                    insertCmd.Parameters.AddWithValue("@time", livetime);
                    insertCmd.Parameters.AddWithValue("@date", date);
                    insertCmd.Parameters.AddWithValue("@month", month);
                    insertCmd.Parameters.AddWithValue("@week", "1");
                    insertCmd.Parameters.AddWithValue("@year", yearr);

                    insertCmd.ExecuteNonQuery();
                }

                StudentState = "Ok !";
            }
        }
        
        //-------------------------------------------------------------------------------------------

        private async Task Process_Extra_Attendance()
        {
            using (SqlConnection con = connection.my_conn())
            {
                con.Open();
                string selectQuery = "SELECT id FROM Extra_Attendance WHERE class_id=@class_id AND student_id=@student_id AND month=@month AND year=@year";
                using (SqlCommand selectCmd = new SqlCommand(selectQuery, con))
                {
                    selectCmd.Parameters.AddWithValue("@student_id", StudentID);
                    selectCmd.Parameters.AddWithValue("@class_id", ClassID);
                    selectCmd.Parameters.AddWithValue("@year", yearr);
                    selectCmd.Parameters.AddWithValue("@month", month);

                    using (SqlDataReader selectReader = selectCmd.ExecuteReader())
                    {
                        if (selectReader.HasRows)
                        {
                            selectReader.Close();
                            string checkQuery = "SELECT id FROM Extra_Attendance WHERE class_id=@class_id AND student_id=@student_id AND month=@month AND week" + weekOfMonth + "=@week AND year=@year";
                            using (SqlCommand checkCmd2 = new SqlCommand(checkQuery, con))
                            {
                                checkCmd2.Parameters.AddWithValue("@student_id", StudentID);
                                checkCmd2.Parameters.AddWithValue("@class_id", ClassID);
                                checkCmd2.Parameters.AddWithValue("@month", month);
                                checkCmd2.Parameters.AddWithValue("@week", "1");
                                checkCmd2.Parameters.AddWithValue("@year", yearr);

                                using (SqlDataReader checkReader2 = checkCmd2.ExecuteReader())
                                {
                                    if (!checkReader2.HasRows)
                                    {
                                        Update_Extra_Attendance();
                                        await MakeFeeRecordsAsync(StudentID, ClassID, LoadClassPrice(Link));
                                        this.Send();

                                        await LoadAndSetAttendanceAsync(StudentID, ClassID, month, yearr, "Extra_Attendance");
                                        return;
                                    }
                                    else
                                    {
                                        StudentState = "Already Marked";
                                        await MakeFeeRecordsAsync(StudentID, ClassID, LoadClassPrice(Link));
                                        await LoadAndSetAttendanceAsync(StudentID, ClassID, month, yearr, "Extra_Attendance");
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Insert_Extra_Attendance();
                            await MakeFeeRecordsAsync(StudentID, ClassID, LoadClassPrice(Link));
                            this.Send();
                            await LoadAndSetAttendanceAsync(StudentID, ClassID, month, yearr, "Extra_Attendance");
                            return;
                        }
                    }
                }
            }
        }

        private string GetNextNum_Extra_Attendance()
        {
            using (SqlConnection con = connection.my_conn())
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                string Get = "SELECT MIN(id) FROM Extra_Attendance";
                using (SqlCommand cmd = new SqlCommand(Get, con))
                {
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
        }

        private void Update_Extra_Attendance()
        {
            using (SqlConnection con = connection.my_conn())
            {
                con.Open();
                string updateQuery = "UPDATE Extra_Attendance SET week" + weekOfMonth + "=1,date=@date WHERE class_id=@class_id AND student_id=@student_id AND month=@month AND year=@year";

                using (SqlCommand updateCmd = new SqlCommand(updateQuery, con))
                {
                    updateCmd.Parameters.AddWithValue("@date", date);
                    updateCmd.Parameters.AddWithValue("@student_id", StudentID);
                    updateCmd.Parameters.AddWithValue("@class_id", ClassID);
                    updateCmd.Parameters.AddWithValue("@month", month);
                    updateCmd.Parameters.AddWithValue("@year", yearr);

                    updateCmd.ExecuteNonQuery();
                }

                StudentState = "Ok !";
            }
        }

        private void Insert_Extra_Attendance()
        {
            using (SqlConnection con = connection.my_conn())
            {
                con.Open();
                string insertQuery = "INSERT INTO Extra_Attendance (id, link, student_id, class_id, time, date, month, year, week" + weekOfMonth + ") VALUES (@id, @link, @student_id, @class_id, @time, @date, @month, @year, @week)";

                using (SqlCommand insertCmd = new SqlCommand(insertQuery, con))
                {
                    insertCmd.Parameters.AddWithValue("@id", GetNextNum_Extra_Attendance());
                    insertCmd.Parameters.AddWithValue("@link", Link);
                    insertCmd.Parameters.AddWithValue("@student_id", StudentID);
                    insertCmd.Parameters.AddWithValue("@class_id", ClassID);
                    insertCmd.Parameters.AddWithValue("@time", livetime);
                    insertCmd.Parameters.AddWithValue("@date", date);
                    insertCmd.Parameters.AddWithValue("@month", month);
                    insertCmd.Parameters.AddWithValue("@week", "1");
                    insertCmd.Parameters.AddWithValue("@year", yearr);

                    insertCmd.ExecuteNonQuery();
                }

                StudentState = "Ok !";
            }
        }

        //-------------------------------------------------------------------------------------------

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

        string LoadClassPrice(string selectedClassId)
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
                        using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@id", selectedClassId);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        return reader["price"].ToString();
                                    }
                                }
                            }
                        }
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

            return null; // or some default value
        }

        private async Task MakeFeeRecordsAsync(string studentId, string classId, string price)
        {
            using (SqlConnection con = connection.my_conn())
            {
                try
                {
                    await con.OpenAsync();

                    int attCheck = await GetTotalAttendanceAsync(con, studentId, classId, month, yearr);

                    await AlarmAsync(studentId, classId, month, yearr, weekOfMonth.ToString());

                    if (attCheck >= 2)
                    {
                        string card = await GetStudentCardAsync(con, studentId, classId);

                        if (card.Trim() == "Full" || card.Trim() == "Half")
                        {
                            string p = (card.Trim() == "Half") ? (Convert.ToInt32(price) / 2).ToString() : price;

                            if (!await DoesFeeRecordExistAsync(con, studentId, classId, yearr, month))
                            {
                                await InsertFeeRecordAsync(con, studentId, classId, yearr, month, p);
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

        private async Task<int> GetTotalAttendanceAsync(SqlConnection con, string studentId, string classId, string month, string year)
        {
            string query = "SELECT total_attendance FROM Vw_Sum_At WHERE class_id=@class_id AND student_id=@student_id AND month=@month AND year=@year";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@student_id", studentId);
                cmd.Parameters.AddWithValue("@class_id", string.IsNullOrEmpty(Link) ? classId : Link);
                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@year", year);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    int attCheck = 0;

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            attCheck += Convert.ToInt32(reader[0]);
                        }
                    }

                    return attCheck;
                }
            }
        }

        private async Task<string> GetStudentCardAsync(SqlConnection con, string studentId, string classId)
        {
            string query = "SELECT card FROM study WHERE student_id=@student_id AND class_id=@class_id";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@student_id", studentId);
                cmd.Parameters.AddWithValue("@class_id", classId);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            return reader[0].ToString();
                        }
                    }
                }
            }

            return "";
        }

        private async Task<bool> DoesFeeRecordExistAsync(SqlConnection con, string studentId, string classId, string year, string month)
        {
            string query = "SELECT 1 FROM class_fees WHERE student_id=@student_id AND class_id=@class_id AND year=@year AND month=@month";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@student_id", studentId);
                cmd.Parameters.AddWithValue("@class_id", classId);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@month", month);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    return reader.HasRows;
                }
            }
        }

        private async Task InsertFeeRecordAsync(SqlConnection con, string studentId, string classId, string year, string month, string price)
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

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Message: " + e.Message);
            }
        }

        //--------------------------------------------------------------------------------------------

        private async Task AlarmAsync(string studentId, string classId, string month, string year, string week)
        {
            using (SqlConnection con = connection.my_conn())
            {
                con.Open();
                try
                {
                    string check = "SELECT month FROM Vw_PendingPayment WHERE student_id=@sid AND class_id=@cid AND year=@year";
                    using (SqlCommand cmd = new SqlCommand(check, con))
                    {
                        cmd.Parameters.AddWithValue("@sid", studentId);
                        cmd.Parameters.AddWithValue("@cid", classId);
                        cmd.Parameters.AddWithValue("@year", year);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            string getMonth = "";

                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
                                {
                                    getMonth += reader["month"].ToString();
                                }

                                if (Convert.ToInt32(week) >= 3 || getMonth != month)
                                {
                                    StudentState = StudentState + ", Not Paid !";
                                    PlayAlarmSoundAsync();                                                       
                                    reader.Close();
                                }
                            }
                            else
                            {
                                reader.Close();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void PlayAlarmSoundAsync()
        {
            SoundPlayer sound = new SoundPlayer();
            sound.SoundLocation = Application.StartupPath + "\\Audio\\Alam.wav";
            sound.Load();
            sound.Play();
        }

        //--------------------------------------------------------------------------------------------

        string CheckSms()
        {
            string state = "InActive"; // Default value if the state is not found

            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    con.Open();

                    string get = "SELECT state FROM getway WHERE event=@Event";
                    using (SqlCommand cmd = new SqlCommand(get, con))
                    {
                        cmd.Parameters.AddWithValue("@Event", "Attendance");
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            state = result.ToString();
                        }
                    }
                }
            }
            catch (SqlException)
            {
            }

            return state;
        }

        string MsgSms(string studentName, string className, string date)
        {
            string message = "Default Message"; // Default value for message

            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    con.Open();

                    string get = "SELECT message FROM getway WHERE event=@Event";
                    using (SqlCommand cmd = new SqlCommand(get, con))
                    {
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
                }
            }
            catch (MySqlException)
            {

            }

            return message;
        }

        string GetContactNum()
        {
            string number = "0000000000"; // Default value

            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    string get = "SELECT parent FROM student WHERE id=@id";
                    using (SqlCommand cmd = new SqlCommand(get, con))
                    {
                        cmd.Parameters.AddWithValue("@id", StudentID);
                        object result = cmd.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            number = result.ToString();
                        }
                    }
                }
            }
            catch (MySqlException)
            {
            }

            return number;
        }

        void Send()
        {
            string status = CheckSms().Trim();
            if (status == "Active")
            {
                string number = GetContactNum();
                string msg = MsgSms(StudentName, ClassName, DateTime.Now.ToString("yyyy-MM-dd"));
                _ = connection.sms(number, msg);
                // MessageBox.Show(response, "API Response");
            }
        }

    }
}
