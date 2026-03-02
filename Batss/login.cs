using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Batss
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            //check if username or password is empty

            if (username == "" || password == "")
            {
                MessageBox.Show("Please enter username and password");
                return; // Stop the login process/program
            }
            //Call db connection string

            DBConnect db = new DBConnect();

            try
            {
                //SQL query to count matching username and password
                db.Open();
                string query = "SELECT COUNT(*) FROM users WHERE " + 
                    "username=@username AND password=@password";

                //Create MYSQL command
                MySql.Data.MySqlClient.MySqlCommand cmd = new 
                    MySql.Data.MySqlClient.MySqlCommand(query, db.Connection);
                //Add parameters to prevent SQL injection
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                //Execute query and get results
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 1)
                {
                    //If 1 record found, login successful
                    MessageBox.Show("Login Successful");
                    //Open dashboard
                    dashboard dashboard = new dashboard();
                    dashboard.Show();
                    this.Hide();
                    
                }
                else
                {
                    //No match Found
                    MessageBox.Show("Invalid username or password");
                }
            }
            catch (Exception ex) 
            {
                //Show error message if something goes wrong
                MessageBox.Show(ex.Message);
            }
            finally
            { 
                //Close Database Connection
                db.Close();
            }
        }
    }
}
