using IMS_Upgraded_C_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS_Upgrade_To_C_
{
    public partial class pgDatabase : Form
    {
        Connection connection;
        public pgDatabase()
        {
            InitializeComponent();
            connection = new Connection();
        }
        void Bat()
        {
            // Specify the path to your batch file
            string batchFilePath = Application.StartupPath + "\\Backup\\SyncFolders.BAT";

            // Create a new process
            Process process = new Process();

            try
            {
                // Set the process start info
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = batchFilePath;
                startInfo.UseShellExecute = true; // UseShellExecute set to true to open without capturing output
                startInfo.CreateNoWindow = false; // Show the console window

                process.StartInfo = startInfo;

                // Start the process
                process.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = connection.my_conn();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            string date = DateTime.Now.ToString("yyyyMMdd");
            string time = DateTime.Now.ToString("hhmmss");
            string databaseName = txtDatabase.Text;
            string backupPath = Application.StartupPath + "\\Backup\\BackupFile" + date + "_" + time + ".bak";

            if (con.State == ConnectionState.Open)
            {
                progressBar1.Value = 0;
                string backupCommand = "BACKUP DATABASE [" + databaseName + "] TO DISK='" + backupPath + "'";
                SqlCommand cmd = new SqlCommand(backupCommand, con);

                try
                {
                    cmd.ExecuteNonQuery();
                    progressBar1.Value = 50;
                    Bat();
                    progressBar1.Value = 100;
                    MessageBox.Show("Backup created successfully at: " + backupPath, "Backup Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    progressBar1.Value = 0;
                    MessageBox.Show("Backup failed: " + ex.Message, "Backup Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
            }
        }

    }
}
