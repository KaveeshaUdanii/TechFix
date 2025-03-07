using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechFixWinForms.Models;
using System.Text.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;


namespace TechFixWinForms
{
    public partial class TechFixWindows : Form
    {
        private static readonly HttpClient client = new HttpClient();
        
        public TechFixWindows()
        {
            InitializeComponent();
        }

        public class Supplier
        {
            [Key]
            [JsonPropertyName("SID")] // Ensure property name matches the JSON response
            public string SID { get; set; }

            [Required]
            [JsonPropertyName("Name")] // Ensure property name matches the JSON response
            public string Name { get; set; }

            [Required, EmailAddress]
            [JsonPropertyName("Email")] // Ensure property name matches the JSON response
            public string Email { get; set; }

            [Required]
            [JsonPropertyName("ContactNo")] // Ensure property name matches the JSON response
            public string ContactNo { get; set; }
        }
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2VScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }

        private void guna2NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void guna2DataGridView1_CellContentClickAsync(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void guna2HtmlLabel10_Click(object sender, EventArgs e)
        {

        }

        private void guna2VScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void guna2HtmlLabel21_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void addS_Click(object sender, EventArgs e)
        {
            try
            {
                // Get input values
                string supplierName = txtSName.Text.Trim();
                string supplierEmail = txtSEmail.Text.Trim();
                string supplierContact = txtSContact.Text.Trim();
                string adminId = "Admin123"; // Change this to actual admin ID

                if (string.IsNullOrEmpty(supplierName) || string.IsNullOrEmpty(supplierEmail) || string.IsNullOrEmpty(supplierContact))
                {
                    MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create supplier object
                var supplier = new
                {
                    Name = supplierName,
                    Email = supplierEmail,
                    ContactNo = supplierContact,
                    AID = adminId,
                };

                string json = JsonConvert.SerializeObject(supplier); // Use JsonConvert here
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send POST request to API
                HttpResponseMessage httpResponseMessage = await client.PostAsync("https://localhost:7201/api/supplier/add", content);
                HttpResponseMessage response = httpResponseMessage;

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Supplier added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed to add supplier: {errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2HtmlLabel24_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private async void AddC_Click(object sender, EventArgs e)
        {
            try
            {
                // Get input values
                string categoryName = guna2TextBox9.Text.Trim();
                

                if (string.IsNullOrEmpty(categoryName))
                {
                    MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create supplier object
                var supplier = new
                {
                    Name = categoryName,
                   
                };

                string json = JsonConvert.SerializeObject(supplier);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send POST request to API
                HttpResponseMessage httpResponseMessage = await client.PostAsync("https://localhost:7201/api/category/add", content);
                HttpResponseMessage response = httpResponseMessage;

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Category added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed to add category: {errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void CID_TextChanged(object sender, EventArgs e)
        {

        }

        private async void AddP_Click(object sender, EventArgs e)
        {// Get user input values
            string productName = PName.Text;
            string productPrice = PPrice.Text;
            string productQuantity = PQuantity.Text;
            string productDescription = PDescription.Text;
            string categoryName = guna2ComboBox1.SelectedItem?.ToString();
            string supplierName = guna2ComboBox2.SelectedItem?.ToString();

            // Validate inputs
            if (string.IsNullOrWhiteSpace(productName) ||
                string.IsNullOrWhiteSpace(productPrice) ||
                string.IsNullOrWhiteSpace(productQuantity) ||
                string.IsNullOrWhiteSpace(productDescription) ||
                string.IsNullOrWhiteSpace(categoryName) ||
                string.IsNullOrWhiteSpace(supplierName))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Convert category and supplier names to their respective IDs
            string categoryID = GetCategoryID(categoryName);
            string supplierID = GetSupplierID(supplierName);

            if (string.IsNullOrWhiteSpace(categoryID) || string.IsNullOrWhiteSpace(supplierID))
            {
                MessageBox.Show("Invalid Category or Supplier.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Convert Price to integer (or decimal if appropriate)
            if (!decimal.TryParse(productPrice, out decimal price))
            {
                MessageBox.Show("Invalid price format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create product object
            var product = new
            {
                PID = Guid.NewGuid().ToString(), // Generate a unique ID
                Name = productName,
                Stock = int.Parse(productQuantity),
                Price = (int)price, // If API expects an integer, convert it
                SID = supplierID,
                Description = productDescription,
                CID = categoryID
            };

            // Convert object to JSON
            string json = JsonConvert.SerializeObject(product);
            Console.WriteLine(json); // Debugging: print the JSON string

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            // Send HTTP POST request
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.PostAsync("https://localhost:7201/api/product/add", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Failed to add product. " + response.ReasonPhrase + "\n" + responseContent, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string GetCategoryID(string categoryName)
        {
            string categoryID = "";
            string connectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT CID FROM Category WHERE Name = @CategoryName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        categoryID = result.ToString();
                    }
                }
            }
            return categoryID;
        }

        private string GetSupplierID(string supplierName)
        {
            string supplierID = "";
            string connectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT SID FROM Supplier WHERE Name = @SupplierName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SupplierName", supplierName);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        supplierID = result.ToString();
                    }
                }
            }
            return supplierID;
        }


        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {/*
            LoadCategories();
            if (guna2ComboBox1.SelectedItem != null)
            {
                // Get the selected category
                string selectedCategory = guna2ComboBox1.SelectedItem.ToString();
                MessageBox.Show("Selected Category: " + selectedCategory);
            }*/
        }
        /*
        private void LoadCategories()
        {
            guna2ComboBox1.Items.Clear(); // Clear existing items before loading new ones

            string connectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT Name FROM Category";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Add the category names to the ComboBox
                                guna2ComboBox1.Items.Add(reader["Name"].ToString());
                            }

                            // If items were added, set the first item as selected (optional)
                            if (guna2ComboBox1.Items.Count > 0)
                            {
                                guna2ComboBox1.SelectedIndex = 0; // Automatically select the first item
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }*/


        private void guna2ComboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //LoadSuppliers();
        }
        /*
        private void LoadSuppliers()
        {
            guna2ComboBox2.Items.Clear(); // Clear existing items

            string connectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT Name FROM Supplier";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                guna2ComboBox2.Items.Add(reader["Name"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }*/

    }
}
