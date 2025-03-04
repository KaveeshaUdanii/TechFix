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
using TechFixWinForms.Connection;

namespace TechFixWinForms.Forms
{
    public partial class AdminLoginForm : Form
    {
        public AdminLoginForm()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();  
            string password = txtPassword.Text.Trim();  

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = ConnectionManager.GetConnection())  // Using ConnectionManager
                {
                    con.Open();
                    string query = "SELECT COUNT(1) FROM Admin WHERE Name = @username AND Password = @password";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count == 1)
                        {
                            MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Redirect to another form (e.g., Admin Dashboard)
                            TechFixWindows dashboard = new TechFixWindows();  
                            dashboard.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Connection Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AdminSignUp signUpForm = new AdminSignUp(); 
            signUpForm.Show(); 
            this.Hide(); 
        }
    }
}
