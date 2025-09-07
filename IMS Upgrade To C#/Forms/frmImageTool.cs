using IMS_Upgraded_C_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS_Upgrade_To_C_
{
    public partial class frmImageTool : Form
    {
        Connection connection;
        public frmImageTool()
        {
            InitializeComponent();
            connection = new Connection();
        }

        int itemCount = 0;
        string[] imageFiles;
        int updatedCount = 0;
        int skippedCount = 0;

        void GetFiles()
        {
            try
            {
                folderBrowserDialog1.ShowDialog();
                // After selecting the folder, you should read the image files and get the count
                imageFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.jpg");
                itemCount = imageFiles.Length;
                MessageBox.Show("Successfully Loaded : " + itemCount + " images.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !");
            }
        }
        void UpdateImageBulk()
        {
            progressBar1.Maximum = itemCount;
            SqlConnection con = connection.my_conn();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            try
            {
                for (int i = 0; i < itemCount; i++)
                {
                    // Get the image file name (assuming it's the student ID)
                    string id = Path.GetFileNameWithoutExtension(imageFiles[i]);
                    string query = "Update student SET image=@image,image_update_date=@iDate where id = @StudentID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@image", connection.getphoto(Path.Combine(folderBrowserDialog1.SelectedPath, imageFiles[i])));
                    cmd.Parameters.AddWithValue("@StudentID", id);
                    cmd.Parameters.AddWithValue("@iDate", DateTime.Now.ToString("yyyy/MM/dd"));

                    // Check if the image name matches the student ID before updating
                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // Image name matches student ID, update the record
                            updatedCount++;
                        }
                        else
                        {
                            // Image name does not match student ID, skip the record
                            skippedCount++;
                        }
                    }
                    catch (Exception)
                    {
                        // Handle other exceptions (e.g., SQL errors) here
                        skippedCount++;
                    }

                    progressBar1.Value += 1;
                }

                // Display the message with the format "Updated : X | Skipped : Y"
                MessageBox.Show($"Updated : {updatedCount} | Skipped : {skippedCount}", "Update Summary");
            }
            catch (Exception ex)
            {
                progressBar1.Value = 0;
                MessageBox.Show(ex.Message, "Error");
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        void DownloadImageBulk()
        {
            // Show the folder browser dialog
            progressBar2.Value = 20;
            folderBrowserDialog1.Description = "Select the folder to export images";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                SqlConnection con = connection.my_conn();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                string query = "select id, image from student";
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader reader = cmd.ExecuteReader();

                // Loop through the result set
                progressBar2.Value = 50;
                while (reader.Read())
                {
                    // Get the ID as a string and image bytes
                    string id = reader["id"] != DBNull.Value ? (string)reader["id"] : null;
                    byte[] imageBytes = reader["image"] != DBNull.Value ? (byte[])reader["image"] : null;

                    // Save the image to the chosen folder with the filename as the ID
                    if (id != null && imageBytes != null)
                    {
                        string fileName = Path.Combine(folderBrowserDialog1.SelectedPath, id + ".jpg");
                        File.WriteAllBytes(fileName, imageBytes);
                    }
                }
                progressBar2.Value = 75;

                reader.Close();
                con.Close();
                progressBar2.Value = 100;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            GetFiles();
        }
        private void btnUpdateImages_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                UpdateImageBulk();
            }
            else
            {
                MessageBox.Show("Please Read All The Instructions And Mark The Checkbox !", "Attention !");
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar2.Value = 0;
                DownloadImageBulk();
            }
            catch (Exception ex)
            {
                progressBar2.Value = 0;
                MessageBox.Show(ex.Message);
            }
        }

    }
}
