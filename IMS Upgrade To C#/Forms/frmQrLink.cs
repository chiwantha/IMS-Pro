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

namespace IMS_Upgrade_To_C_.Forms
{
    public partial class frmQrLink : Form
    {
        Connection connection;
        public frmQrLink()
        {
            InitializeComponent();
            connection = new Connection();
        }



        private void btnGen_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = connection.my_conn(); // Use your connection method
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                try
                {
                    // Step 1: Get all students where qr is NULL
                    string selectQuery = "SELECT Id FROM student WHERE qr IS NULL";
                    SqlCommand cmd = new SqlCommand(selectQuery, con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Step 2: Set ProgressBar Maximum
                    progressBar1.Maximum = dataTable.Rows.Count;
                    progressBar1.Value = 0;

                    foreach (DataRow row in dataTable.Rows)
                    {
                        string studentId = row["Id"].ToString();

                        try
                        {
                            // Generate QR Code and get the file path
                            string qrPath = connection.GenerateQRCode(studentId);

                            // Step 3: Update the student table with the new QR path
                            string updateQuery = "UPDATE student SET qr = @Qr WHERE Id = @Id";
                            SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                            updateCmd.Parameters.AddWithValue("@Qr", connection.getphoto(connection.GenerateQRCode(studentId)));
                            updateCmd.Parameters.AddWithValue("@Id", studentId);
                            updateCmd.ExecuteNonQuery();

                            // Step 4: Update ProgressBar
                            progressBar1.Value += 1;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show($"QR Generation Failed for ID: {studentId}", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Close connection after completion
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                MessageBox.Show("QR Link Completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("QR Link Unsuccessful!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
