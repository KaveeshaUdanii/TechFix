using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using TechFixWinForms.Connection;
using TechFixWinForms.Forms;
using System.Xml.Linq;

namespace TechFixWinForms
{
    public partial class AdminSignUp : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        public AdminSignUp()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminLoginForm loginForm = new AdminLoginForm();
            loginForm.Show();
            this.Hide();  // Hides the current form
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con = ConnectionManager.GetConnection();
                con.Open();
                MessageBox.Show("Connection successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            AdminLoginForm loginForm = new AdminLoginForm();
            loginForm.Show();
            this.Hide();  // Hides the current form
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Establish Connection
                con = ConnectionManager.GetConnection();
                con.Open();

                // Get user input from text fields
                string adminId = Guid.NewGuid().ToString(); 
                string name = txtName.Text.Trim();
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();
                string contactNo = txtContact.Text.Trim();

                // Validate input fields
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) ||
                    string.IsNullOrEmpty(password) || string.IsNullOrEmpty(contactNo))
                {
                    MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // SQL Query to insert into the Admin table
                string query = "INSERT INTO Admin (AID, Name, Email, Password, ContactNo) VALUES (@AID, @Name, @Email, @Password, @ContactNo)";

                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AID", adminId);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@ContactNo", contactNo);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Admin registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide(); // Hide sign-up form after success
                    AdminLoginForm loginForm = new AdminLoginForm();
                    loginForm.Show(); // Show login form
                }
                else
                {
                    MessageBox.Show("Error occurred while saving data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con != null)
                {
                    con.Close(); // Close the connection
                }
            }
        }

        private void AdminSignUp_Load(object sender, EventArgs e)
        {

        }
    }
}
