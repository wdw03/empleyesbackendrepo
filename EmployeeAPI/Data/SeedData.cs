using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Data;

public static class SeedData
{
    public static async Task InitializeAsync(AppDbContext context)
    {
        // Migrate
        await context.Database.MigrateAsync();

        // Seed Roles
        if (!await context.Roles.AnyAsync())
        {
            var roles = new List<Role>
            {
                new() { Name = "Admin", Description = "Full system access" },
                new() { Name = "HR", Description = "Human resources management" },
                new() { Name = "Manager", Description = "Team and department management" },
                new() { Name = "Employee", Description = "Standard employee access" }
            };
            context.Roles.AddRange(roles);
            await context.SaveChangesAsync();
        }

        // Seed Departments
        if (!await context.Departments.AnyAsync())
        {
            var departments = new List<Department>
            {
                new() { Name = "Engineering", Description = "Software development and engineering" },
                new() { Name = "Human Resources", Description = "Employee relations and recruitment" },
                new() { Name = "Marketing", Description = "Marketing and brand management" },
                new() { Name = "Finance", Description = "Financial planning and accounting" },
                new() { Name = "Operations", Description = "Business operations and logistics" }
            };
            context.Departments.AddRange(departments);
            await context.SaveChangesAsync();
        }

        var adminRole = await context.Roles.FirstAsync(r => r.Name == "Admin");
        var hrRole = await context.Roles.FirstAsync(r => r.Name == "HR");
        var managerRole = await context.Roles.FirstAsync(r => r.Name == "Manager");
        var employeeRole = await context.Roles.FirstAsync(r => r.Name == "Employee");

        var engineering = await context.Departments.FirstAsync(d => d.Name == "Engineering");
        var hr = await context.Departments.FirstAsync(d => d.Name == "Human Resources");
        var marketing = await context.Departments.FirstAsync(d => d.Name == "Marketing");
        var finance = await context.Departments.FirstAsync(d => d.Name == "Finance");
        var operations = await context.Departments.FirstAsync(d => d.Name == "Operations");

        // Seed Employees
        if (!await context.Employees.AnyAsync())
        {
            var employees = new List<Employee>
            {
                new()
                {
                    EmployeeCode = "EMP001", FirstName = "Rajesh", LastName = "Kumar",
                    Email = "admin@company.com", Phone = "+91-9876543210",
                    DateOfBirth = new DateTime(1985, 3, 15), Gender = Gender.Male,
                    Address = "123 Admin Street", City = "Mumbai", State = "Maharashtra", PostalCode = "400001",
                    DepartmentId = engineering.Id, RoleId = adminRole.Id, Designation = "CTO",
                    DateOfJoining = new DateTime(2020, 1, 10), Salary = 250000,
                    EmploymentType = EmploymentType.FullTime, Status = EmployeeStatus.Active
                },
                new()
                {
                    EmployeeCode = "EMP002", FirstName = "Priya", LastName = "Sharma",
                    Email = "hr@company.com", Phone = "+91-9876543211",
                    DateOfBirth = new DateTime(1990, 7, 22), Gender = Gender.Female,
                    Address = "456 HR Avenue", City = "Delhi", State = "Delhi", PostalCode = "110001",
                    DepartmentId = hr.Id, RoleId = hrRole.Id, Designation = "HR Director",
                    DateOfJoining = new DateTime(2021, 3, 15), Salary = 180000,
                    EmploymentType = EmploymentType.FullTime, Status = EmployeeStatus.Active
                },
                new()
                {
                    EmployeeCode = "EMP003", FirstName = "Amit", LastName = "Patel",
                    Email = "manager@company.com", Phone = "+91-9876543212",
                    DateOfBirth = new DateTime(1988, 11, 5), Gender = Gender.Male,
                    Address = "789 Manager Blvd", City = "Bangalore", State = "Karnataka", PostalCode = "560001",
                    DepartmentId = engineering.Id, RoleId = managerRole.Id, Designation = "Engineering Manager",
                    DateOfJoining = new DateTime(2021, 6, 1), Salary = 200000,
                    EmploymentType = EmploymentType.FullTime, Status = EmployeeStatus.Active
                },
                new()
                {
                    EmployeeCode = "EMP004", FirstName = "Sneha", LastName = "Reddy",
                    Email = "employee@company.com", Phone = "+91-9876543213",
                    DateOfBirth = new DateTime(1995, 4, 18), Gender = Gender.Female,
                    Address = "321 Dev Lane", City = "Hyderabad", State = "Telangana", PostalCode = "500001",
                    DepartmentId = engineering.Id, RoleId = employeeRole.Id, Designation = "Software Engineer",
                    DateOfJoining = new DateTime(2022, 8, 20), Salary = 120000,
                    EmploymentType = EmploymentType.FullTime, Status = EmployeeStatus.Active
                },
                new()
                {
                    EmployeeCode = "EMP005", FirstName = "Vikram", LastName = "Singh",
                    Email = "vikram.singh@company.com", Phone = "+91-9876543214",
                    DateOfBirth = new DateTime(1992, 9, 30), Gender = Gender.Male,
                    Address = "654 Marketing St", City = "Pune", State = "Maharashtra", PostalCode = "411001",
                    DepartmentId = marketing.Id, RoleId = employeeRole.Id, Designation = "Marketing Specialist",
                    DateOfJoining = new DateTime(2022, 2, 14), Salary = 95000,
                    EmploymentType = EmploymentType.FullTime, Status = EmployeeStatus.Active
                },
                new()
                {
                    EmployeeCode = "EMP006", FirstName = "Ananya", LastName = "Gupta",
                    Email = "ananya.gupta@company.com", Phone = "+91-9876543215",
                    DateOfBirth = new DateTime(1993, 12, 8), Gender = Gender.Female,
                    Address = "987 Finance Rd", City = "Chennai", State = "Tamil Nadu", PostalCode = "600001",
                    DepartmentId = finance.Id, RoleId = employeeRole.Id, Designation = "Financial Analyst",
                    DateOfJoining = new DateTime(2023, 1, 5), Salary = 110000,
                    EmploymentType = EmploymentType.FullTime, Status = EmployeeStatus.Active
                },
                new()
                {
                    EmployeeCode = "EMP007", FirstName = "Rahul", LastName = "Verma",
                    Email = "rahul.verma@company.com", Phone = "+91-9876543216",
                    DateOfBirth = new DateTime(1991, 6, 25), Gender = Gender.Male,
                    Address = "147 Ops Center", City = "Kolkata", State = "West Bengal", PostalCode = "700001",
                    DepartmentId = operations.Id, RoleId = managerRole.Id, Designation = "Operations Manager",
                    DateOfJoining = new DateTime(2021, 9, 12), Salary = 160000,
                    EmploymentType = EmploymentType.FullTime, Status = EmployeeStatus.Active
                },
                new()
                {
                    EmployeeCode = "EMP008", FirstName = "Deepika", LastName = "Nair",
                    Email = "deepika.nair@company.com", Phone = "+91-9876543217",
                    DateOfBirth = new DateTime(1997, 2, 14), Gender = Gender.Female,
                    Address = "258 Intern Way", City = "Bangalore", State = "Karnataka", PostalCode = "560002",
                    DepartmentId = engineering.Id, RoleId = employeeRole.Id, Designation = "Junior Developer",
                    DateOfJoining = new DateTime(2024, 6, 1), Salary = 55000,
                    EmploymentType = EmploymentType.Intern, Status = EmployeeStatus.Active
                },
                new()
                {
                    EmployeeCode = "EMP009", FirstName = "Suresh", LastName = "Menon",
                    Email = "suresh.menon@company.com", Phone = "+91-9876543218",
                    DateOfBirth = new DateTime(1986, 8, 19), Gender = Gender.Male,
                    Address = "369 Contract Blvd", City = "Jaipur", State = "Rajasthan", PostalCode = "302001",
                    DepartmentId = marketing.Id, RoleId = employeeRole.Id, Designation = "Marketing Consultant",
                    DateOfJoining = new DateTime(2023, 11, 1), Salary = 130000,
                    EmploymentType = EmploymentType.Contract, Status = EmployeeStatus.Active
                },
                new()
                {
                    EmployeeCode = "EMP010", FirstName = "Kavitha", LastName = "Iyer",
                    Email = "kavitha.iyer@company.com", Phone = "+91-9876543219",
                    DateOfBirth = new DateTime(1994, 5, 3), Gender = Gender.Female,
                    Address = "741 Part Time Ln", City = "Ahmedabad", State = "Gujarat", PostalCode = "380001",
                    DepartmentId = hr.Id, RoleId = employeeRole.Id, Designation = "HR Coordinator",
                    DateOfJoining = new DateTime(2023, 4, 17), Salary = 70000,
                    EmploymentType = EmploymentType.PartTime, Status = EmployeeStatus.Active
                },
                new()
                {
                    EmployeeCode = "EMP011", FirstName = "Arun", LastName = "Joshi",
                    Email = "arun.joshi@company.com", Phone = "+91-9876543220",
                    DateOfBirth = new DateTime(1989, 1, 28), Gender = Gender.Male,
                    Address = "852 Leave St", City = "Lucknow", State = "Uttar Pradesh", PostalCode = "226001",
                    DepartmentId = finance.Id, RoleId = employeeRole.Id, Designation = "Senior Accountant",
                    DateOfJoining = new DateTime(2021, 7, 20), Salary = 140000,
                    EmploymentType = EmploymentType.FullTime, Status = EmployeeStatus.OnLeave
                },
                new()
                {
                    EmployeeCode = "EMP012", FirstName = "Meera", LastName = "Chopra",
                    Email = "meera.chopra@company.com", Phone = "+91-9876543221",
                    DateOfBirth = new DateTime(1996, 10, 12), Gender = Gender.Female,
                    Address = "963 Inactive Rd", City = "Chandigarh", State = "Punjab", PostalCode = "160001",
                    DepartmentId = operations.Id, RoleId = employeeRole.Id, Designation = "Logistics Coordinator",
                    DateOfJoining = new DateTime(2022, 5, 8), Salary = 85000,
                    EmploymentType = EmploymentType.FullTime, Status = EmployeeStatus.Inactive
                },
                new()
                {
                    EmployeeCode = "EMP013", FirstName = "Karthik", LastName = "Rajan",
                    Email = "karthik.rajan@company.com", Phone = "+91-9876543222",
                    DateOfBirth = new DateTime(1987, 7, 7), Gender = Gender.Male,
                    Address = "159 Terminated Ave", City = "Coimbatore", State = "Tamil Nadu", PostalCode = "641001",
                    DepartmentId = engineering.Id, RoleId = employeeRole.Id, Designation = "DevOps Engineer",
                    DateOfJoining = new DateTime(2020, 11, 3), Salary = 150000,
                    EmploymentType = EmploymentType.FullTime, Status = EmployeeStatus.Terminated
                },
                new()
                {
                    EmployeeCode = "EMP014", FirstName = "Pooja", LastName = "Deshmukh",
                    Email = "pooja.deshmukh@company.com", Phone = "+91-9876543223",
                    DateOfBirth = new DateTime(1998, 3, 21), Gender = Gender.Female,
                    Address = "357 New Join Blvd", City = "Nagpur", State = "Maharashtra", PostalCode = "440001",
                    DepartmentId = marketing.Id, RoleId = employeeRole.Id, Designation = "Content Strategist",
                    DateOfJoining = DateTime.UtcNow.AddDays(-10), Salary = 80000,
                    EmploymentType = EmploymentType.FullTime, Status = EmployeeStatus.Active
                },
                new()
                {
                    EmployeeCode = "EMP015", FirstName = "Nikhil", LastName = "Saxena",
                    Email = "nikhil.saxena@company.com", Phone = "+91-9876543224",
                    DateOfBirth = new DateTime(1993, 11, 15), Gender = Gender.Male,
                    Address = "468 Recent St", City = "Indore", State = "Madhya Pradesh", PostalCode = "452001",
                    DepartmentId = operations.Id, RoleId = employeeRole.Id, Designation = "Supply Chain Analyst",
                    DateOfJoining = DateTime.UtcNow.AddDays(-5), Salary = 90000,
                    EmploymentType = EmploymentType.FullTime, Status = EmployeeStatus.Active
                }
            };
            context.Employees.AddRange(employees);
            await context.SaveChangesAsync();
        }

        // Seed Users
        if (!await context.Users.AnyAsync())
        {
            var emp1 = await context.Employees.FirstAsync(e => e.EmployeeCode == "EMP001");
            var emp2 = await context.Employees.FirstAsync(e => e.EmployeeCode == "EMP002");
            var emp3 = await context.Employees.FirstAsync(e => e.EmployeeCode == "EMP003");
            var emp4 = await context.Employees.FirstAsync(e => e.EmployeeCode == "EMP004");

            var users = new List<User>
            {
                new()
                {
                    Username = "admin", Email = "admin@company.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    RoleId = adminRole.Id, EmployeeId = emp1.Id, IsActive = true
                },
                new()
                {
                    Username = "hruser", Email = "hr@company.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Hr@12345"),
                    RoleId = hrRole.Id, EmployeeId = emp2.Id, IsActive = true
                },
                new()
                {
                    Username = "manager", Email = "manager@company.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Manager@123"),
                    RoleId = managerRole.Id, EmployeeId = emp3.Id, IsActive = true
                },
                new()
                {
                    Username = "employee", Email = "employee@company.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Employee@123"),
                    RoleId = employeeRole.Id, EmployeeId = emp4.Id, IsActive = true
                }
            };
            context.Users.AddRange(users);
            await context.SaveChangesAsync();
        }
    }
}
