using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechFixWinForms.Models;

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
            // Load the suppliers when the form loads
            await LoadSuppliers();
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

                string json = JsonSerializer.Serialize(supplier);
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

        private async Task LoadSuppliers()
        {
            try
            {
                // Send GET request to fetch all suppliers from the API
                HttpResponseMessage response = await client.GetAsync("https://localhost:7201/api/supplier/all");

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string responseData = await response.Content.ReadAsStringAsync();

                    // Deserialize the response into a list of suppliers
                    var suppliers = JsonSerializer.Deserialize<List<Supplier>>(responseData);

                    // Ensure that suppliers is not null
                    if (suppliers != null)
                    {
                        // Clear existing data in DataGridView
                        guna2DataGridView1.Rows.Clear();

                        // Add rows to DataGridView
                        foreach (var supplier in suppliers)
                        {
                            guna2DataGridView1.Rows.Add(supplier.SID, supplier.Name, supplier.Email, supplier.ContactNo);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No suppliers found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Failed to load suppliers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                string json = JsonSerializer.Serialize(supplier);
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
    }
}
