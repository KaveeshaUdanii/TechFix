  📌 TechFix Desktop Application - Setup Instructions
  
📢 Note: This application is configured to use localhost:7201 as the backend API.

🔄 If you need to change it to localhost:5000 or another port, update all API links in the code accordingly.


________________________________________

🚀 Step 1: Install Required Software

Before setting up the application, ensure you have the following installed:

✅ Microsoft Visual Studio (Latest version, Community/Enterprise)

✅ .NET SDK (6.0 or above)

✅ Microsoft SQL Server & SQL Server Management Studio (SSMS)


________________________________________

🖥️ Step 2: Set Up the Database

1️⃣ Attach the provided database in SQL Server Management Studio (SSMS):

•	Open SSMS and connect to your SQL Server instance.

•	In Object Explorer, right-click Databases → Select Attach...

•	Click Add and locate the provided .mdf file.

•	Click OK to attach the database.

2️⃣ Update the Connection String (If Needed):

•	Open appsettings.json in the .NET API project

•	Modify the connection string to match your SQL Server instance:

"ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=TechFixDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}

3️⃣ Run Database Migrations (If Needed):

•	Open a terminal in Visual Studio and navigate to the API project directory

•	Run the following command to apply migrations:

dotnet ef database update


________________________________________

🔗 Step 3: Run the .NET Web API

1️⃣ Open Visual Studio and load the TechFix API project.

2️⃣ Click on Run (▶️) or press F5 to start the API.

3️⃣ The API should be running on https://localhost:7201.

📌 If you want to change the API port:

•	Open launchSettings.json inside the API project.

•	Modify the port from 7201 to your desired port (e.g., 5000).


________________________________________

🖥️ Step 4: Run the Desktop Application (WinForms)

1️⃣ Open Visual Studio and load the TechFix Desktop Application project.

2️⃣ If needed, update API URLs in the C# code (HttpClient requests).

•	Example:

client.BaseAddress = new Uri("https://localhost:7201"); 

•	Change 7201 to 5000 if your API is running on a different port.

3️⃣ Click on Run (▶️) or press F5 to start the application.





________________________________________

Use Cases

✅ Login & Sign Up:

•	Create an admin account and log in.

✅ Supplier Management:

•	View Add, update, search, and delete suppliers.

✅ Product Management:

•	View, add, update, search, and delete products.

✅ Category Management:

•	View, add, update, search, and delete categories.

✅ Orders & Procurement:

•	Place orders and view past orders.

•	Check procurement stats.



 Steps: Supplier Web App (React)
Now that your TechFix desktop app is working, you can move on to developing the React-based supplier web app 🚀.
Would you like setup instructions for the React supplier app as well? 😊

