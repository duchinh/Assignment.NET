using EFCoreWebAPI.Models;
using Microsoft.Extensions.Logging;

namespace EFCoreWebAPI.Data
{
    public static class DataSeeder
    {
        public static void SeedData(AppDbContext context)
        {
            // Đảm bảo database đã được tạo
            context.Database.EnsureCreated();
            Console.WriteLine("Database đã được tạo");

            // Kiểm tra xem đã có dữ liệu chưa
            if (context.Employees.Any() || context.Salaries.Any())
            {
                return; // Đã có dữ liệu rồi thì không cần seed nữa
            }

            Console.WriteLine("Bắt đầu seeding dữ liệu...");

            // Seed Departments
            var departments = new Department[]
            {
                new Department { Name = "Software Development" },
                new Department { Name = "Finance" },
                new Department { Name = "Accountant" },
                new Department { Name = "HR" },
                new Department { Name = "Marketing" },
                new Department { Name = "Sales" },
                new Department { Name = "Customer Service" },
                new Department { Name = "Research & Development" }
            };
            context.Departments.AddRange(departments);
            context.SaveChanges();
            Console.WriteLine("Đã thêm departments");

            // Seed Projects
            var projects = new Project[]
            {
                new Project { Name = "Project A" },
                new Project { Name = "Project B" },
                new Project { Name = "Project C" },
                new Project { Name = "Project D" },
                new Project { Name = "Project E" },
                new Project { Name = "Project F" }
            };
            context.Projects.AddRange(projects);
            context.SaveChanges();
            Console.WriteLine("Đã thêm projects");

            // Lấy departments đã được seed
            var departmentsList = context.Departments.ToList();

            // Tạo danh sách employees
            var employees = new List<Employee>();
            var random = new Random();

            // Tạo 25 nhân viên với dữ liệu đa dạng
            var employeeData = new[]
            {
                ("John Doe", 0, new DateTime(2024, 1, 1), 150.00m),
                ("Jane Smith", 1, new DateTime(2024, 2, 1), 200.00m),
                ("Bob Johnson", 2, new DateTime(2024, 3, 1), 120.00m),
                ("Alice Brown", 3, new DateTime(2024, 4, 1), 90.00m),
                ("Michael Wilson", 4, new DateTime(2023, 12, 15), 180.00m),
                ("Sarah Davis", 5, new DateTime(2023, 11, 20), 160.00m),
                ("David Miller", 6, new DateTime(2023, 10, 5), 140.00m),
                ("Emma Garcia", 7, new DateTime(2024, 1, 15), 170.00m),
                ("James Rodriguez", 0, new DateTime(2024, 2, 10), 155.00m),
                ("Linda Martinez", 1, new DateTime(2024, 3, 5), 185.00m),
                ("William Taylor", 2, new DateTime(2023, 9, 1), 130.00m),
                ("Patricia Anderson", 3, new DateTime(2023, 8, 15), 95.00m),
                ("Thomas Jackson", 4, new DateTime(2024, 1, 20), 175.00m),
                ("Jennifer White", 5, new DateTime(2024, 2, 15), 165.00m),
                ("Robert Lee", 6, new DateTime(2024, 3, 10), 145.00m),
                ("Elizabeth Clark", 7, new DateTime(2023, 7, 1), 190.00m),
                ("Joseph Wright", 0, new DateTime(2023, 6, 15), 160.00m),
                ("Margaret Hall", 1, new DateTime(2024, 1, 5), 170.00m),
                ("Christopher Allen", 2, new DateTime(2024, 2, 20), 140.00m),
                ("Susan Young", 3, new DateTime(2024, 3, 15), 110.00m),
                ("Daniel King", 4, new DateTime(2023, 5, 1), 200.00m),
                ("Nancy Scott", 5, new DateTime(2023, 4, 15), 180.00m),
                ("Paul Green", 6, new DateTime(2024, 1, 10), 150.00m),
                ("Betty Baker", 7, new DateTime(2024, 2, 5), 160.00m),
                ("Kevin Nelson", 0, new DateTime(2024, 3, 20), 175.00m)
            };

            foreach (var (name, deptIndex, joinedDate, salary) in employeeData)
            {
                var employee = new Employee
                {
                    Name = name,
                    DepartmentId = departmentsList[deptIndex].Id,
                    Department = departmentsList[deptIndex],
                    JoinedDate = joinedDate
                };
                employees.Add(employee);
            }

            context.Employees.AddRange(employees);
            context.SaveChanges();

            // Tạo Salaries
            var salaries = new List<Salary>();
            for (int i = 0; i < employees.Count; i++)
            {
                var salary = new Salary
                {
                    Amount = employeeData[i].Item4,
                    EmployeeId = employees[i].Id,
                    Employee = employees[i]
                };
                salaries.Add(salary);
                employees[i].Salary = salary;
            }

            context.Salaries.AddRange(salaries);
            context.SaveChanges();

            Console.WriteLine("Đã thêm employees và salaries");

            // Seed ProjectEmployees với nhiều mối quan hệ hơn
            var projectEmployees = new List<ProjectEmployee>();
            
            // Mỗi nhân viên sẽ tham gia 1-3 dự án ngẫu nhiên
            foreach (var employee in employees)
            {
                var numProjects = random.Next(1, 4); // 1-3 projects
                var projectIndices = Enumerable.Range(0, projects.Length).OrderBy(x => random.Next()).Take(numProjects);
                
                foreach (var projectIndex in projectIndices)
                {
                    projectEmployees.Add(new ProjectEmployee
                    {
                        ProjectId = projects[projectIndex].Id,
                        EmployeeId = employee.Id,
                        Enable = random.Next(100) < 80, // 80% chance of being enabled
                        Project = projects[projectIndex],
                        Employee = employee
                    });
                }
            }

            context.ProjectEmployees.AddRange(projectEmployees);
            context.SaveChanges();
            Console.WriteLine("Đã thêm projectEmployees");
            Console.WriteLine("Hoàn thành seeding dữ liệu!");
        }
    }
} 