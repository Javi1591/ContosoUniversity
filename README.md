# ContosoUniversity
## Xavier Nazario

# Project Description
This project follows the Contoso University getting started tutorial from Microsoft Docs. It is created using ASP.NET Core Razor Page web application. It demonstrates how to build a simple university management system using, while showing best practices like responsive design, partial views, and accessibility improvements:

- ASP.NET Core MVC / Razor Pages
- Entity Framework Core
- SQL Server
- Bootstrap for responsive layout

# How to Run
- Ste 1: Clone the repository from GitHub url
  - http://github.com/Javi1591/ContosoUniversity
- Step 2: Navigate to the project directory, using command "cd ContosoUniversity" in the command console
-  Step 3: Install the needed dependencies, as noted below, using the command "npm install"
  - Packages
    - Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore v9.0
    - Microsoft.EntityFrameworkCore.SqlServer v9.0
    - Microsoft.EntityFrameworkCore.Tools v9.0
  - Frameworks
    - Microsoft.AspNetCore.App
    - Microsoft.NETCore.App
- Step 4: Run the application using the command "npm start"

# Features Include
- Responsive Layout using default ASP.NET Core MVC layout file `_Layout.cshtml`
- Navigation Bar
- Partial view created for repeating sections
- Accessibility checks
  - Labels linked to inputs  
  - Error messages clearly displayed  
  - Logical keyboard focus order

# Test Plan
- Verify core CRUD flows for Students, Courses, Enrollments
- Ensure pages render with no runtime errors and database interactions are correct.
- Prevent regressions
