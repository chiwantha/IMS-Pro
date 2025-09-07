using System;
using IMS_Upgrade_To_C_;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMS_Upgraded_C_;
using ZstdSharp.Unsafe;
using static Guna.UI2.Native.WinApi;
using System.IO;

namespace IMS_Upgrade_To_C_
{
    public partial class pgSettings : Form
    {
        Connection connection;
        public pgSettings()
        {          
            InitializeComponent();
            connection = new Connection();
        }

        //-------------------------------------------------------------------------------------------

        async Task LoadClass()
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        await con.OpenAsync();
                    }


                    string selectQuery = "SELECT id, class, grade, batch, state, discount, price FROM class";

                    using (SqlCommand command = new SqlCommand(selectQuery, con))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);

                            dgClassState.AutoGenerateColumns = false;
                            dgClassState.DataSource = dataTable;

                            dgDiscount.AutoGenerateColumns = false;
                            dgDiscount.DataSource = dataTable;                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        async Task LoadStudents(string id)
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    await con.OpenAsync();

                    string selectQuery = "SELECT id, name FROM student WHERE id LIKE @Id";

                    using (SqlCommand command = new SqlCommand(selectQuery, con))
                    {
                        command.Parameters.AddWithValue("@Id", "%" + id + "%");

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);

                            txtInvStudent_Name.DataSource = dataTable;
                            txtInvStudent_Name.DisplayMember = "name";
                            txtInvStudent_Name.ValueMember = "id";
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        async Task LoadPayments(string value)
        {
            value = value.Trim();
            string key = "student_id";
            if (rbInvStudent.Checked)
            {
                key = "student_id";
            }
            else if (rbInvRecept.Checked)
            {
                key = "recept_no";
            }

            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    await con.OpenAsync();

                    string selectQuery = "SELECT id,student_id,class_id,year,month,price,recept_no " +
                        "FROM class_fees WHERE " + key + " LIKE @value";

                    using (SqlCommand command = new SqlCommand(selectQuery, con))
                    {
                        command.Parameters.AddWithValue("@value", "%" + value + "%");

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);

                            dgInvView.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        async Task LoadRecept(string recept)
        {
            if (recept == null || recept == "")
            {
                MessageBox.Show("There is No Recept for This Invoice !\nStill Not Paid !");
                return;
            }
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    await con.OpenAsync();

                    string selectQuery = "SELECT * " +
                        "FROM recept WHERE recept_no LIKE @value";

                    using (SqlCommand command = new SqlCommand(selectQuery, con))
                    {
                        command.Parameters.AddWithValue("@value", "%" + recept + "%");

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);

                            dgRecView.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        private async Task load_discount_data()
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    await con.OpenAsync();

                    string selectQuery = "SELECT * FROM discount_settings";

                    using (SqlCommand command = new SqlCommand(selectQuery, con))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    discount_option_state.Checked = reader["discount_option_state"].ToString() == "1";
                                    discount_by_1_state.Checked = reader["discount_by_1_state"].ToString() == "1";
                                    discount_by_1.Value = Convert.ToInt32(reader["discount_by_1"]);
                                    discount_by_1_value.Text = reader["discount_by_1_value"].ToString();
                                    discount_by_2_state.Checked = reader["discount_by_2_state"].ToString() == "1";
                                    discount_by_2.Value = Convert.ToInt32(reader["discount_by_2"]);
                                    discount_by_2_value.Text = reader["discount_by_2_value"].ToString();
                                    discount_by_3_state.Checked = reader["discount_by_3_state"].ToString() == "1";
                                    discount_by_3.Value = Convert.ToInt32(reader["discount_by_3"]);
                                    discount_by_3_value.Text = reader["discount_by_3_value"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task load_salary_calculation_data()
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
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    txtCalculation.SelectedIndex = Convert.ToInt32(reader["SalaryCalOn"]);
                                }
                                else
                                {
                                    txtCalculation.SelectedIndex = 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task load_printing_settings()
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    await con.OpenAsync();

                    string selectQuery = "SELECT * FROM printing_details";
                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, con))
                    {
                        using (SqlDataReader reader = await selectCmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                await reader.ReadAsync();

                                txtCompanyName.Text = reader["name"].ToString();
                                txtCompanySubtitle.Text = reader["subtitle"].ToString();
                                txtCompanyAddress.Text = reader["address"].ToString();
                                txtCompanyContact.Text = reader["contact"].ToString();                              

                                if (!(reader["header"] is DBNull))
                                {
                                    byte[] headerBytes = (byte[])reader["header"];
                                    using (MemoryStream ms = new MemoryStream(headerBytes))
                                    {
                                        HeaderView.BackgroundImage = Image.FromStream(ms);
                                    }
                                }

                                if (!(reader["footer"] is DBNull))
                                {
                                    byte[] footerBytes = (byte[])reader["footer"];
                                    using (MemoryStream ms = new MemoryStream(footerBytes))
                                    {
                                        FooterView.BackgroundImage = Image.FromStream(ms);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task load_getway_settings()
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    await con.OpenAsync();

                    string selectQuery = "SELECT * FROM Api";
                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, con))
                    using (SqlDataReader reader = await selectCmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            txtApiKey.Text = reader["apikey"].ToString();
                            txtSenderId.Text = reader["apiscrect"].ToString();
                            btnsmsGetwayStatus.Checked = reader["state"].ToString() == "1";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //-------------------------------------------------------------------------------------------

        private async void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            int NormalClassupdated = 0;
            int NormalClassnotUpdated = 0;

            using (SqlConnection con = connection.my_conn())
            {
                await con.OpenAsync();

                foreach (DataGridViewRow row in dgClassState.Rows)
                {
                    string classId = row.Cells[0].Value?.ToString();
                    string state = row.Cells[4].Value?.ToString();

                    int class_result = await UpdateClassStateAsync(con, classId, state);
                    int extra_class_result = await UpdateExtraClassStateAsync(con, classId, state);

                    if (class_result == 1)
                    {
                        NormalClassupdated++;
                    }
                    else
                    {
                        NormalClassnotUpdated++;
                    }
                }
            }

            MessageBox.Show($"Updated: {NormalClassupdated}\nSkipped: {NormalClassnotUpdated - 1}\nEffect Also affect For Extra Classes That Was Ran Under Regular Classes !",
                "Update Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LoadClass();
        }

        private async Task<int> UpdateClassStateAsync(SqlConnection con, string classId, string state)
        {
            if (string.IsNullOrEmpty(classId) || string.IsNullOrEmpty(state))
            {
                return 0; // Not updated
            }

            try
            {
                string query = "UPDATE class SET state=@state WHERE id=@classId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@state", state);
                cmd.Parameters.AddWithValue("@classId", classId);
                await cmd.ExecuteNonQueryAsync();
                return 1; // Updated
            }
            catch (Exception)
            {
                return 0; // Not updated
            }
        }

        private async Task<int> UpdateExtraClassStateAsync(SqlConnection con, string classId, string state)
        {
            if (string.IsNullOrEmpty(classId) || string.IsNullOrEmpty(state))
            {
                return 0; // Not updated
            }

            try
            {
                string query = "UPDATE extra_class SET state=@state WHERE link=@classId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@state", state);
                cmd.Parameters.AddWithValue("@classId", classId);
                await cmd.ExecuteNonQueryAsync();
                return 1; // Updated
            }
            catch (Exception)
            {
                return 0; // Not updated
            }
        }


        //----------------------------------------------------------------------------------

        private async void btnInvSearch_Click(object sender, EventArgs e)
        {
            if (rbInvStudent.Checked)
            {
                await LoadPayments(txtInvStudent_Name.SelectedValue.ToString());
            } else
            {
                await LoadPayments(txtInvReceptNumber.Text.ToString());
            }
        }

        private async void txtInvStudentId_TextChanged(object sender, EventArgs e)
        {
            await LoadStudents(txtInvStudentId.Text.ToString());
        }

        private async void dgInvView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string recept_no;

            if (e.RowIndex >= 0 && e.RowIndex < dgInvView.RowCount)
            {
                recept_no = dgInvView.Rows[e.RowIndex].Cells[6].Value.ToString();
            } else
            {
                return;
            }

           if (recept_no == "")
            {
                return;
            }

            await LoadRecept(recept_no);
        }

        //----------------------------------------------------------------------------------

        private async void btnUpdateDiscount_Click(object sender, EventArgs e)
        {
            int NormalClassupdated = 0;
            int NormalClassnotUpdated = 0;

            using (SqlConnection con = connection.my_conn())
            {
                await con.OpenAsync();

                foreach (DataGridViewRow row in dgDiscount.Rows)
                {
                    string classId = row.Cells[0].Value?.ToString();
                    string state = row.Cells[4].Value?.ToString();

                    int class_result = await UpdateClassDiscountAsync(con, classId, state);

                    if (class_result == 1)
                    {
                        NormalClassupdated++;
                    }
                    else
                    {
                        NormalClassnotUpdated++;
                    }
                }     
            }

            MessageBox.Show($"Updated: {NormalClassupdated}\nSkipped: {NormalClassnotUpdated - 1}",
                "Update Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await set_discount_settings();
            await LoadClass();
            await load_discount_data();
        }

        private async Task<int> UpdateClassDiscountAsync(SqlConnection con, string classId, string discount)
        {
            if (string.IsNullOrEmpty(classId) || string.IsNullOrEmpty(discount))
            {
                return 0; // Not updated
            }

            try
            {
                string query = "UPDATE class SET discount=@discount WHERE id=@classId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@discount", discount);
                cmd.Parameters.AddWithValue("@classId", classId);
                await cmd.ExecuteNonQueryAsync();
                return 1; // Updated
            }
            catch (Exception)
            {
                return 0; // Not updated
            }
        }

        private async Task set_discount_settings()
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    await con.OpenAsync();

                    string checkQuery = "SELECT COUNT(*) FROM discount_settings";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                    int rowCount = (int)await checkCmd.ExecuteScalarAsync();

                    if (rowCount > 0)
                    {
                        string updateQuery = "UPDATE discount_settings SET " +
                            "discount_option_state=@discount_option_state, " +
                            "discount_by_1_state=@discount_by_1_state, " +
                            "discount_by_1=@discount_by_1, " +
                            "discount_by_1_value=@discount_by_1_value, " +
                            "discount_by_2_state=@discount_by_2_state, " +
                            "discount_by_2=@discount_by_2, " +
                            "discount_by_2_value=@discount_by_2_value, " +
                            "discount_by_3_state=@discount_by_3_state, " +
                            "discount_by_3=@discount_by_3, " +
                            "discount_by_3_value=@discount_by_3_value";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, con))
                        {
                            updateCmd.Parameters.AddWithValue("@discount_option_state", discount_option_state.Checked ? "1" : "0");
                            updateCmd.Parameters.AddWithValue("@discount_by_1_state", discount_by_1_state.Checked ? "1" : "0");
                            updateCmd.Parameters.AddWithValue("@discount_by_1", Convert.ToInt32(discount_by_1.Value));
                            updateCmd.Parameters.AddWithValue("@discount_by_1_value", Convert.ToInt32(discount_by_1_value.Text));
                            updateCmd.Parameters.AddWithValue("@discount_by_2_state", discount_by_2_state.Checked ? "1" : "0");
                            updateCmd.Parameters.AddWithValue("@discount_by_2", Convert.ToInt32(discount_by_2.Value));
                            updateCmd.Parameters.AddWithValue("@discount_by_2_value", Convert.ToInt32(discount_by_2_value.Text));
                            updateCmd.Parameters.AddWithValue("@discount_by_3_state", discount_by_3_state.Checked ? "1" : "0");
                            updateCmd.Parameters.AddWithValue("@discount_by_3", Convert.ToInt32(discount_by_3.Value));
                            updateCmd.Parameters.AddWithValue("@discount_by_3_value", Convert.ToInt32(discount_by_3_value.Text));

                            await updateCmd.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        string insertQuery = "INSERT INTO discount_settings (discount_option_state, " +
                            "discount_by_1_state, discount_by_1, discount_by_1_value, " +
                            "discount_by_2_state, discount_by_2, discount_by_2_value, " +
                            "discount_by_3_state, discount_by_3, discount_by_3_value) " +
                            "VALUES (@discount_option_state, @discount_by_1_state, @discount_by_1, @discount_by_1_value, " +
                            "@discount_by_2_state, @discount_by_2, @discount_by_2_value, " +
                            "@discount_by_3_state, @discount_by_3, @discount_by_3_value)";

                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, con))
                        {
                            insertCmd.Parameters.AddWithValue("@discount_option_state", discount_option_state.Checked ? "1" : "0");
                            insertCmd.Parameters.AddWithValue("@discount_by_1_state", discount_by_1_state.Checked ? "1" : "0");
                            insertCmd.Parameters.AddWithValue("@discount_by_1", Convert.ToInt32(discount_by_1.Value));
                            insertCmd.Parameters.AddWithValue("@discount_by_1_value", Convert.ToInt32(discount_by_1_value.Text));
                            insertCmd.Parameters.AddWithValue("@discount_by_2_state", discount_by_2_state.Checked ? "1" : "0");
                            insertCmd.Parameters.AddWithValue("@discount_by_2", Convert.ToInt32(discount_by_2.Value));
                            insertCmd.Parameters.AddWithValue("@discount_by_2_value", Convert.ToInt32(discount_by_2_value.Text));
                            insertCmd.Parameters.AddWithValue("@discount_by_3_state", discount_by_3_state.Checked ? "1" : "0");
                            insertCmd.Parameters.AddWithValue("@discount_by_3", Convert.ToInt32(discount_by_3.Value));
                            insertCmd.Parameters.AddWithValue("@discount_by_3_value", Convert.ToInt32(discount_by_3_value.Text));

                            await insertCmd.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //----------------------------------------------------------------------------------
        private async void btnSaveSalarySettings_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    await con.OpenAsync();

                    string Query = "UPDATE data set SalaryCalOn = "+txtCalculation.SelectedIndex;
                    SqlCommand Cmd = new SqlCommand(Query, con);
                    try
                    {
                        Cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }             
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            await load_salary_calculation_data();
        }

        //----------------------------------------------------------------------------------

        private async void pgSettings_Load(object sender, EventArgs e)
        {
            await LoadClass();
            await LoadStudents("");
            await load_discount_data();
            await load_salary_calculation_data();
            await load_printing_settings();
            await load_getway_settings();
        }

        //----------------------------------------------------------------------------------

        private void btnHeaderReset_Click(object sender, EventArgs e)
        {
            HeaderView.BackgroundImage = null;
            string image_path = Application.StartupPath + "\\UX\\.Support\\.icon\\PrintHeader.png";
            Image originalImage = Image.FromFile(image_path);
            int desiredWidth = 594;
            int desiredHeight = 84;
            Bitmap resizedImage = new Bitmap(originalImage, desiredWidth, desiredHeight);
            HeaderView.BackgroundImage = resizedImage;
        }

        private void btnFooterReset_Click(object sender, EventArgs e)
        {
            FooterView.BackgroundImage = null;
            string image_path = Application.StartupPath + "\\UX\\.Support\\.icon\\PrintFooter.png";
            Image originalImage = Image.FromFile(image_path);
            int desiredWidth = 594;
            int desiredHeight = 36;
            Bitmap resizedImage = new Bitmap(originalImage, desiredWidth, desiredHeight);
            FooterView.BackgroundImage = resizedImage;
        }

        private void btnHeaderUpload_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                HeaderView.BackgroundImage = null;
                string image_path;
                image_path = openFileDialog1.FileName;
                Image originalImage = Image.FromFile(image_path);
                int desiredWidth = 594;
                int desiredHeight = 84;
                Bitmap resizedImage = new Bitmap(originalImage, desiredWidth, desiredHeight);
                HeaderView.BackgroundImage = resizedImage;
            }
        }

        private void btnFooterUpload_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FooterView.BackgroundImage = null;
                string image_path;
                image_path = openFileDialog1.FileName;
                Image originalImage = Image.FromFile(image_path);
                int desiredWidth = 594;
                int desiredHeight = 36;
                Bitmap resizedImage = new Bitmap(originalImage, desiredWidth, desiredHeight);
                FooterView.BackgroundImage = resizedImage;
            }
        }

        public byte[] GetPhotoFromPictureBox(PictureBox pictureBox)
        {
            if (pictureBox == null || pictureBox.BackgroundImage == null)
            {
                throw new ArgumentNullException("PictureBox image is null.");
            }

            byte[] imageBinary;

            try
            {
                // Clone the image to avoid GDI+ exceptions
                using (Image originalImage = new Bitmap(pictureBox.BackgroundImage))
                using (MemoryStream ms = new MemoryStream())
                {
                    originalImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // Change format if needed
                    imageBinary = ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error processing PictureBox image: " + ex.Message);
            }

            return imageBinary;
        }


        private async void btnSavePrintSetting_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    await con.OpenAsync();

                    string selectQuery = "SELECT * FROM printing_details";
                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, con))
                    {
                        using (SqlDataReader reader = await selectCmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                reader.Close();
                                string updateQuery = "UPDATE printing_details SET name=@name,subtitle=@subtitle , address=@address, contact=@contact, header=@header, footer=@footer";
                                using (SqlCommand updateCmd = new SqlCommand(updateQuery, con))
                                {
                                    updateCmd.Parameters.AddWithValue("@name", txtCompanyName.Text != "" ? txtCompanyName.Text : "Unknown Name !");
                                    updateCmd.Parameters.AddWithValue("@subtitle", txtCompanySubtitle.Text != "" ? txtCompanySubtitle.Text : "Unknown subtitle !");
                                    updateCmd.Parameters.AddWithValue("@address", txtCompanyAddress.Text != "" ? txtCompanyAddress.Text : "Unknown Address !");
                                    updateCmd.Parameters.AddWithValue("@contact", txtCompanyContact.Text != "" ? txtCompanyContact.Text : "Unknown contact !");                                   
                                    updateCmd.Parameters.AddWithValue("@header", GetPhotoFromPictureBox(HeaderView));
                                    updateCmd.Parameters.AddWithValue("@footer", GetPhotoFromPictureBox(FooterView));

                                    await updateCmd.ExecuteNonQueryAsync();
                                }
                            }
                            else
                            {
                                reader.Close();
                                string insertQuery = "INSERT INTO printing_details (name, subtitle, address, contact, header, footer) VALUES (@name, @subtitle, @address, @contact, @header, @footer)";
                                using (SqlCommand insertCmd = new SqlCommand(insertQuery, con))
                                {
                                    insertCmd.Parameters.AddWithValue("@name", txtCompanyName.Text != "" ? txtCompanyName.Text : "Unknown Name !");
                                    insertCmd.Parameters.AddWithValue("@subtitle", txtCompanySubtitle.Text != "" ? txtCompanySubtitle.Text : "Unknown subtitle !");
                                    insertCmd.Parameters.AddWithValue("@address", txtCompanyAddress.Text != "" ? txtCompanyAddress.Text : "Unknown Address !");
                                    insertCmd.Parameters.AddWithValue("@contact", txtCompanyContact.Text != "" ? txtCompanyContact.Text : "Unknown contact !");
                                    insertCmd.Parameters.AddWithValue("@email", txtCompanySubtitle.Text != "" ? txtCompanySubtitle.Text : "Unknown email !");
                                    insertCmd.Parameters.AddWithValue("@header", GetPhotoFromPictureBox(HeaderView));
                                    insertCmd.Parameters.AddWithValue("@footer", GetPhotoFromPictureBox(FooterView));

                                    await insertCmd.ExecuteNonQueryAsync();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            await load_printing_settings();
        }

        //----------------------------------------------------------------------------------

        private async void btnApiSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = connection.my_conn())
                {
                    await con.OpenAsync();

                    string updateQuery = "UPDATE api SET apikey=@apikey,apiscrect=@apiscrect , state=@state where id='S'";
                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, con))
                    {
                        updateCmd.Parameters.AddWithValue("@apikey", txtApiKey.Text != "" ? txtApiKey.Text : "Not Set Api Key !");
                        updateCmd.Parameters.AddWithValue("@apiscrect", txtSenderId.Text != "" ? txtSenderId.Text : "Not Set Api Sender ID");
                        updateCmd.Parameters.AddWithValue("@state", btnsmsGetwayStatus.Checked == true ? "1" : "0");
                        await updateCmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            await load_getway_settings();
        }
    }
}
