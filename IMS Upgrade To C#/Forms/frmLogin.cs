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
using MySql.Data.MySqlClient;

namespace IMS_Upgrade_To_C_
{
    public partial class frmLogin : Form
    {
        Connection connection;
        public frmLogin()
        {
            InitializeComponent();
            connection = new Connection();
            //Program.AddOpenForm(this);
        }

        private void saveuser_settigns()
        {
            Properties.Settings.Default.LogUsername = txtUser.Text.ToString();
            Properties.Settings.Default.Save();
        }

        private void loaduser_settigns()
        {
            if (Properties.Settings.Default.LogUsername != "")
            {
                txtUser.Text = Properties.Settings.Default.LogUsername;
            }
        }

        private void prove_login(string username, string password)
        {
            if (username == null && password == null)
            {

            }
            else
            {
                try
                {
                    SqlConnection con = connection.my_conn();
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    string query = "select designation from [user] where username=@username and password=@password";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    SqlDataReader reader = cmd.ExecuteReader();

                    string des = "";

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            des = reader["designation"].ToString();
                        }
                        if (rem_user.Checked)
                        {
                            saveuser_settigns();
                        }
                        MDI mdi = new MDI(txtUser.Text, des);
                        mdi.Show();
                        this.Close();
                        
                    }
                    else
                    {
                        MessageBox.Show("Unknown User or Invalid credentials! ", "Access Declined !", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        Properties.Settings.Default.LogUsername ="";
                        Properties.Settings.Default.Save();
                    }

                    if (con.State != ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            prove_login(txtUser.Text, txtPass.Text);
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            //Splash splash = new Splash();
            //splash.Show();
            loaduser_settigns();
        }

        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPass.Focus();
            }
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                prove_login(txtUser.Text, txtPass.Text);
            }
        }

        private void frmLogin_Activated(object sender, EventArgs e)
        {
            if (txtUser.Text != "")
            {
                txtPass.Focus();
            }
            else
            {
                txtUser.Focus();
            }
        }
    }
}
