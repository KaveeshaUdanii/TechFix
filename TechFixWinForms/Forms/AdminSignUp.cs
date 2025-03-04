﻿using System;
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

namespace TechFixWinForms
{
    public partial class AdminSignUp : Form
    {
        SqlConnection con;
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
                con = ConnectionManager.GetConnection();
                con.Open();
                MessageBox.Show("Connection successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }

        private void AdminSignUp_Load(object sender, EventArgs e)
        {

        }
    }
}
