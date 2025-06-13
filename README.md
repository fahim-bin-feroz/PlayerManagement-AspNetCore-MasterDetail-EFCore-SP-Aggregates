# PlayerManagement - ASP.NET Core MVC | EF Core | Stored Procedures | Aggregates

![.NET](https://img.shields.io/badge/.NET-8.0-blue)
![License](https://img.shields.io/badge/license-MIT-green)

A modular, enterprise-grade ASP.NET Core MVC web application showcasing advanced data operations using **Entity Framework Core (EF Core)**, **SQL Server Stored Procedures**, and **Master-Detail architecture**. Designed to manage and visualize player-related data, the application features integrated reporting components, dynamic view rendering, and aggregate analytics.

---

## 📚 Table of Contents

- [Key Features](#🧩-Key-Features)
- [Tech Stack](#🏗️-Tech-Stack)
- [Project Architecture Overview](#📁-Project-Architecture-Overview)
- [Output Overview](#📊-Output-Overview)
- [Setup Instructions](#⚙️-Setup-Instructions)
- [Development Notes](#🧪-Development-Notes)
- [Contribution & Support](#🤝-Contribution--Support)
- [License](#📄-License)

---

## 🧩 Key Features

- **Master-Detail Interface**: Manage players and their associated skills in a structured and intuitive layout.
- **Stored Procedures Integration**: Perform database insert and update operations using parameterized stored procedures (`spInsert`, `spUpdate`).
- **Data Aggregation & Visualization**: Display statistical summaries (Min, Max, Sum, Avg) and group-by results per country.
- **View Components**: Modular rendering of player counts and aggregate insights using reusable components.
- **Clean UI Design**: Responsive front-end built with Bootstrap and jQuery from the `wwwroot` static assets.
- **Code Separation**: Follows best practices with clear separation of concerns across Controllers, DAL, ViewModels, and Views.

---

## 🏗️ Tech Stack

| Layer             | Technology                                         |
|------------------|-----------------------------------------------------|
| Framework         | ASP.NET Core MVC                                   |
| ORM               | Entity Framework Core 8.0.5                         |
| Database          | SQL Server (with Stored Procedures)                |
| Client Libraries  | Bootstrap, jQuery                                   |
| Code Generation   | Microsoft.VisualStudio.Web.CodeGeneration.Design v8.0.7 |
| Tooling           | EF Core CLI / EF Core Migrations                    |    

---

## 📁 Project Architecture Overview

## Controllers

- Handle HTTP requests and coordinate data between the View and Model.

- Example: PlayersController.cs

## Models

- Represent the application's core data structures.

- Example: Player.cs, PlayerSkill, Citizenship

## ViewModels

- Shape and combine data to be displayed in Views.

- Example: PlayerViewModel.cs, AggregateViewModel, GroupByViewModel

## Views

- Razor pages that present data to the user and collect input.

- Folders: Views/Players/, Views/Home/, Views/Shared/

## Data

- Manages EF Core context and stored procedure SQL scripts.

- Example: AppDbContext.cs
  
## wwwroot

- Static files like CSS, JavaScript, and images.

## Configuration

- appsettings.json: Stores app and DB configuration.

- Program.cs & Startup.cs: Configure services and middleware.

---

## 📊 Output Overview

### 1. Aggregate Statistics (Signing Fee)
| Metric | Value |
|--------|---------|
| Min    | ✔️     |
| Max    | ✔️     |
| Sum    | ✔️     |
| Avg    | ✔️     | 


---

### 2. Grouped Statistics by Country
| Country | Min | Max | Sum | Avg | Count |
|---------|---------|---------|---------|---------|---------| 


---

### 3. Player Count Distribution
| Country Name | Total Players |
|---------|---------| 


---

## ⚙️ Setup Instructions

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- SQL Server 2016 or later
- Code Editor (e.g., Visual Studio 2022)


## Installation Steps

### STEP 1: Clone the repository
git clone https://github.com/fahim-bin-feroz/PlayerManagement-AspNetCore-MasterDetail-EFCore-SP-Aggregates.git
cd PlayerManagement-AspNetCore-MasterDetail-EFCore-SP-Aggregates

### STEP 2: Update the SQL connection string in appsettings.json

### STEP 3: Apply EF Core migrations to create the database
dotnet ef database update

### STEP 4: Run the application
dotnet run


---

## 🧪 Development Notes

All major business logic is encapsulated within ViewModels and ViewComponents for testability and reusability.

Stored Procedures (spInsert, spUpdate) are pre-configured and managed via EF Core migrations.

Views are rendered using the Razor engine with layout sharing across pages via _Layout.cshtml.

---

## 🤝 Contribution & Support

Feel free to fork, open issues, or submit pull requests for improvements or bug fixes.

For direct inquiries, please reach out via the Issues tab or contact the repository maintainer.

---

## 📄 License

This project is licensed under the MIT License. See the LICENSE file for more information.

Developed as a clean reference implementation for ASP.NET Core enterprise applications with real-world features.
