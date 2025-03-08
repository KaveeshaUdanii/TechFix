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
using System.Text.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using System.Net.Http.Json;
using TechFixWinForms.Connection;
using Guna.Charts.WinForms;

namespace TechFixWinForms
{
    public partial class TechFixWindows : Form
    {
        private static readonly HttpClient client = new HttpClient();

        public TechFixWindows()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            DisplaySuppliers();
            DisplayProducts();
            DisplayCategories();
            DisplayOrders();
            LoadTotalOrders();
            LoadTotalSuppliers();
            LoadTotalProducts();
            LoadTopSuppliersChart();
            LoadTopOrderedProductsChart();

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
        public class Category
        {
            public string CID { get; set; }  
            public string Name { get; set; }
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
                    ClearSupplierFields();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed to add supplier: {errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearSupplierFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearSupplierFields();
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
                    ClearCategoryTextBoxes();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed to add category: {errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearCategoryTextBoxes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearCategoryTextBoxes();
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
        {
        }
        


        private void guna2ComboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //LoadSuppliers();
        }

        private async void PlaceOrderButton_Click(object sender, EventArgs e)
        {
            // Ensure that ProductID and SupplierID are selected
            var productId = ProductID.SelectedItem?.ToString();  // Get selected ProductID
            var supplierId = SupplierID.SelectedItem?.ToString();  // Get selected SupplierID

            // Validate the input to make sure PID and SID are selected
            if (string.IsNullOrEmpty(productId) || string.IsNullOrEmpty(supplierId))
            {
                MessageBox.Show("Please select both Product ID and Supplier ID.");
                return;
            }

            var quantity = (int)guna2NumericUpDown1.Value;  // Ensure quantity is an integer
            var orderDate = guna2DateTimePicker1.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");  // ISO 8601 format for OrderDate with milliseconds

            // Create an object for the order in the correct format
            var order = new
            {
                pid = productId,  // Product ID
                quantity = quantity,
                sid = supplierId,  // Supplier ID
                orderDate = orderDate  // Order Date
            };

            // Convert the object to JSON
            var jsonOrder = JsonConvert.SerializeObject(order);

            // Set up HttpClient
            using (var client = new HttpClient())
            {
                // Set the base URL for your API
                client.BaseAddress = new Uri("https://localhost:7201");

                // Create the content for the request with the appropriate Content-Type header
                var content = new StringContent(jsonOrder, Encoding.UTF8, "application/json");

                try
                {
                    // Make the POST request to the API
                    var response = await client.PostAsync("/api/Order/add", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Order placed successfully!");
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Failed to place order: " + errorMessage);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void guna2HtmlLabel23_Click(object sender, EventArgs e)
        {

        }



        private void ProductID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        // display data ---------------------------------------------------------------------------------------------------
        private void DisplaySuppliers()
        {
            string connectionString = "Server=DESKTOP-8D8VJB1\\SQLEXPRESS;Database=TechFixDB;Trusted_Connection=True;MultipleActiveResultSets=true;";
            SqlConnection con = new SqlConnection(connectionString);

            try
            {
                // Open connection
                con.Open();

                // Prepare SQL query
                string query = "SELECT * FROM Supplier";
                SqlCommand cmd = new SqlCommand(query, con);

                // Execute the query
                var reader = cmd.ExecuteReader();

                // Load data into DataTable
                DataTable table = new DataTable();
                table.Load(reader);

                // Bind the DataTable to DataGridView
                guna2DataGridView1.DataSource = table;

                // Optionally, set the column headers manually (if you need to customize them)
                foreach (DataGridViewColumn column in guna2DataGridView1.Columns)
                {
                    column.HeaderText = table.Columns[column.Index].ColumnName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close connection
                con.Close();
            }
        }




        private void DisplayProducts()
        {
            string connectionString = "Server=DESKTOP-8D8VJB1\\SQLEXPRESS;Database=TechFixDB;Trusted_Connection=True;MultipleActiveResultSets=true;";
            SqlConnection con = new SqlConnection(connectionString);

            try
            {
                // Open connection
                con.Open();

                // Prepare SQL query
                string query = "SELECT * FROM Product";  // Replace with your actual table name
                SqlCommand cmd = new SqlCommand(query, con);

                // Execute the query
                var reader = cmd.ExecuteReader();

                // Load data into DataTable
                DataTable table = new DataTable();
                table.Load(reader);

                // Bind the DataTable to the second DataGridView
                guna2DataGridView2.DataSource = table;

                // Optionally, set the column headers manually (if you need to customize them)
                foreach (DataGridViewColumn column in guna2DataGridView2.Columns)
                {
                    column.HeaderText = table.Columns[column.Index].ColumnName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close connection
                con.Close();
            }
        }



        private void DisplayCategories()
        {
            string connectionString = "Server=DESKTOP-8D8VJB1\\SQLEXPRESS;Database=TechFixDB;Trusted_Connection=True;MultipleActiveResultSets=true;";
            SqlConnection con = new SqlConnection(connectionString);

            try
            {
                // Open connection
                con.Open();

                // Prepare SQL query
                string query = "SELECT * FROM Category";  // Replace with your actual table name
                SqlCommand cmd = new SqlCommand(query, con);

                // Execute the query
                var reader = cmd.ExecuteReader();

                // Load data into DataTable
                DataTable table = new DataTable();
                table.Load(reader);

                // Bind the DataTable to the third DataGridView
                guna2DataGridView4.DataSource = table;

                // Optionally, set the column headers manually (if you need to customize them)
                foreach (DataGridViewColumn column in guna2DataGridView4.Columns)
                {
                    column.HeaderText = table.Columns[column.Index].ColumnName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close connection
                con.Close();
            }
        }




        private void DisplayOrders()
        {
            string connectionString = "Server=DESKTOP-8D8VJB1\\SQLEXPRESS;Database=TechFixDB;Trusted_Connection=True;MultipleActiveResultSets=true;";
            SqlConnection con = new SqlConnection(connectionString);

            try
            {
                // Open connection
                con.Open();

                // Prepare SQL query
                string query = "SELECT * FROM [Order]";  // Replace with your actual table name
                SqlCommand cmd = new SqlCommand(query, con);

                // Execute the query
                var reader = cmd.ExecuteReader();

                // Load data into DataTable
                DataTable table = new DataTable();
                table.Load(reader);

                // Bind the DataTable to the fourth DataGridView
                guna2DataGridView3.DataSource = table;

                // Optionally, set the column headers manually (if you need to customize them)
                foreach (DataGridViewColumn column in guna2DataGridView3.Columns)
                {
                    column.HeaderText = table.Columns[column.Index].ColumnName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close connection
                con.Close();
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            // Get the panel graphics
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create rounded rectangle path
            int radius = 50; // Adjust the curve radius
            System.Drawing.Rectangle rect = guna2Panel1.ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90); // Top-left corner
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90); // Top-right corner
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90); // Bottom-right corner
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90); // Bottom-left corner
            path.CloseFigure();

            // Apply gradient colors
            System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                rect,
                Color.FromArgb(255, 94, 148, 255),  // Start Color (Blue)
                Color.FromArgb(255, 0, 212, 255),   // End Color (Cyan)
                System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
            );

            g.FillPath(brush, path); // Fill gradient
            g.DrawPath(new Pen(Color.FromArgb(200, 255, 255, 255), 2), path); // Add a border
        }


        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void gunaChart1_Load(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel29_Click(object sender, EventArgs e)
        {

        }

        public async Task LoadTotalOrders()
        {
            string apiUrl = "https://localhost:7201/api/Order/total-orders";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        int totalOrders = JsonConvert.DeserializeObject<int>(result);
                        guna2HtmlLabel29.Text = " " + totalOrders;
                    }
                    else
                    {
                        guna2HtmlLabel29.Text = "Error loading orders!";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("API Error: " + ex.Message);
            }
        }
        public async Task LoadTotalSuppliers()
        {
            string apiUrl = "https://localhost:7201/api/Supplier/total-suppliers";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        int totalSuppliers = JsonConvert.DeserializeObject<int>(result);
                        guna2HtmlLabel30.Text = " " + totalSuppliers;
                    }
                    else
                    {
                        guna2HtmlLabel30.Text = "Error loading suppliers!";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("API Error: " + ex.Message);
            }
        }

        public async Task LoadTotalProducts()
        {
            string apiUrl = "https://localhost:7201/api/Product/total-products";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        int totalProducts = JsonConvert.DeserializeObject<int>(result);
                        guna2HtmlLabel31.Text = " " + totalProducts;
                    }
                    else
                    {
                        guna2HtmlLabel31.Text = "Error loading products!";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("API Error: " + ex.Message);
            }
        }

        private async Task LoadTopSuppliersChart()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = "https://localhost:7201/api/Supplier/top-suppliers";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();

                        // Deserialize JSON response
                        List<SupplierData> topSuppliers = JsonConvert.DeserializeObject<List<SupplierData>>(jsonString);

                        // Clear old data
                        gunaBarDataset1.DataPoints.Clear(); // Make sure this dataset is added in Guna UI Designer

                        // Add new data points
                        foreach (var supplier in topSuppliers)
                        {
                            gunaBarDataset1.DataPoints.Add(new LPoint(supplier.Name, supplier.TotalOrders));
                        }

                        // Refresh the chart
                        gunaChart1.Datasets.Clear();
                        gunaChart1.Datasets.Add(gunaBarDataset1);
                        gunaChart1.Update(); // Refresh chart to show latest data
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async void LoadTopOrderedProductsChart()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = "https://localhost:7201/api/Product/top-products";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var topProducts = JsonConvert.DeserializeObject<List<TopProduct>>(json);

                        // Clear previous data
                        gunaPieDataset1.DataPoints.Clear();
                        gunaChart2.Datasets.Clear();

                        foreach (var product in topProducts)
                        {
                            gunaPieDataset1.DataPoints.Add(product.Name, product.TotalQuantity);
                        }

                        // Add dataset to chart
                        gunaChart2.Datasets.Add(gunaPieDataset1);

                        // Refresh chart
                        gunaChart2.Update();
                    }
                    else
                    {
                        MessageBox.Show("Failed to load top ordered products.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void SearchSupplierButton_Click(object sender, EventArgs e)
        {
            string sid = SIDtextBox.Text.Trim();
            if (string.IsNullOrEmpty(sid))
            {
                MessageBox.Show("Please enter a Supplier ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7201/api/");
                    HttpResponseMessage response = await client.GetAsync($"Supplier/get-supplier/{sid}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonData = await response.Content.ReadAsStringAsync();
                        Supplier supplier = JsonConvert.DeserializeObject<Supplier>(jsonData);

                        // Populate textboxes with supplier data
                        txtSName.Text = supplier.Name;
                        txtSEmail.Text = supplier.Email;
                        txtSContact.Text = supplier.ContactNo;
                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Supplier not found! Server Response: {errorResponse}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ClearSupplierFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching supplier details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearSupplierFields();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }
        private void ClearSupplierFields()
        {
            SIDtextBox.Clear();
            txtSName.Clear();
            txtSEmail.Clear();
            txtSContact.Clear();
        }

        private async void UpdateSupplier_Click(object sender, EventArgs e)
        {
            string sid = SIDtextBox.Text.Trim();
            if (string.IsNullOrEmpty(sid))
            {
                MessageBox.Show("Please enter a Supplier ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create a supplier object with updated values
            Supplier updatedSupplier = new Supplier
            {
                SID = sid,  // SID cannot be changed
                Name = txtSName.Text.Trim(),
                Email = txtSEmail.Text.Trim(),
                ContactNo = txtSContact.Text.Trim()
            };

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7201/api/");
                    string jsonData = JsonConvert.SerializeObject(updatedSupplier);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"Supplier/update-supplier/{sid}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Supplier updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearSupplierFields();
                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Failed to update supplier. Server Response: {errorResponse}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ClearSupplierFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating supplier details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearSupplierFields();
            }
        }

        private async void DeleteSupplier_Click(object sender, EventArgs e)
        {
            string sid = SIDtextBox.Text.Trim();
            if (string.IsNullOrEmpty(sid))
            {
                MessageBox.Show("Please enter a Supplier ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this supplier?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.No)
            {
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7201/api/");
                    HttpResponseMessage response = await client.DeleteAsync($"Supplier/delete-supplier/{sid}");

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Supplier deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearSupplierFields(); // 🧹 Clear fields after deleting
                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Failed to delete supplier. Server Response: {errorResponse}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ClearSupplierFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting supplier: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearSupplierFields();
            }
        }

        private async void SearchP_Click(object sender, EventArgs e)
        {
            
        }

        private async void SearchCategory_Click(object sender, EventArgs e)
        {
            string cid = CID.Text.Trim();  // Get the CID entered by Admin
            if (string.IsNullOrEmpty(cid))
            {
                MessageBox.Show("Please enter a Category ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7201/api/");  // Change to your API base address
                    HttpResponseMessage response = await client.GetAsync($"Category/get-category/{cid}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonData = await response.Content.ReadAsStringAsync();
                        Category category = JsonConvert.DeserializeObject<Category>(jsonData);

                        // Fill the guna2TextBox9 with the category name
                        guna2TextBox9.Text = category.Name;
                    }
                    else
                    {
                        MessageBox.Show("Category not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching category details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void UpdateCategory_Click(object sender, EventArgs e)
        {
            string cid = CID.Text.Trim();  // Get the CID entered by Admin
            string categoryName = guna2TextBox9.Text.Trim();  // Get the category name entered in the textbox

            if (string.IsNullOrEmpty(cid) || string.IsNullOrEmpty(categoryName))
            {
                MessageBox.Show("Please fill all the details.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Create the category object to send to the API
                Category updatedCategory = new Category
                {
                    CID = cid,
                    Name = categoryName
                };

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7201/api/");  // Change to your API base address

                    // Make PUT request to the API with the updated category details
                    HttpResponseMessage response = await client.PutAsJsonAsync($"Category/update-category/{cid}", updatedCategory);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Category updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear textboxes after successful update
                        ClearCategoryTextBoxes();
                    }
                    else
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating category: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Method to clear textboxes after the operation
        private void ClearCategoryTextBoxes()
        {
            CID.Clear();
            guna2TextBox9.Clear();
        }

        private async void DeleteCategory_Click(object sender, EventArgs e)
        {
            string cid = CID.Text.Trim();  // Get the CID entered by Admin

            if (string.IsNullOrEmpty(cid))
            {
                MessageBox.Show("Please enter the Category ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7201/api/");  // Change to your API base address

                    // Make DELETE request to the API with the CID
                    HttpResponseMessage response = await client.DeleteAsync($"Category/delete-category/{cid}");

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Category deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear textboxes after successful delete
                        ClearCategoryTextBoxes();
                    }
                    else
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting category: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
