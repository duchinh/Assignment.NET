# EFCoreWebAPI

Dự án Web API sử dụng Entity Framework Core để quản lý dữ liệu nhân viên, phòng ban và dự án.

## Công nghệ sử dụng

- .NET 8.0
- Entity Framework Core 8.0
- SQL Server
- Swagger/OpenAPI
- AutoMapper
- FluentValidation

## Các tính năng chính

### 1. Quản lý Phòng ban (Department)
- Xem danh sách phòng ban
- Xem chi tiết phòng ban
- Thêm phòng ban mới
- Cập nhật thông tin phòng ban
- Xóa phòng ban

### 2. Quản lý Nhân viên (Employee)
- Xem danh sách nhân viên
- Xem chi tiết nhân viên
- Thêm nhân viên mới
- Cập nhật thông tin nhân viên
- Xóa nhân viên
- Tìm kiếm nhân viên theo tên
- Lọc nhân viên theo phòng ban
- Lấy danh sách nhân viên tham gia dự án

### 3. Quản lý Dự án (Project)
- Xem danh sách dự án
- Xem chi tiết dự án
- Thêm dự án mới
- Cập nhật thông tin dự án
- Xóa dự án
- Thêm nhân viên vào dự án
- Xóa nhân viên khỏi dự án

### 4. Quản lý Lương (Salary)
- Xem lương của nhân viên
- Cập nhật lương cho nhân viên
- Test transaction khi cập nhật lương

## Cấu trúc dự án

```
EFCoreWebAPI/
├── Controllers/           # Các controller xử lý API endpoints
├── Data/                 # Data seeding và migration
├── DTOs/                 # Data Transfer Objects
├── Models/              # Entity models
├── Services/            # Business logic services
├── Validators/          # FluentValidation rules
└── Program.cs           # Cấu hình ứng dụng
```

## Cài đặt và chạy

1. Clone repository
2. Cài đặt SQL Server
3. Cập nhật connection string trong `appsettings.json`
4. Chạy các lệnh sau:

```bash
dotnet restore
dotnet ef database update
dotnet run
```

5. Truy cập Swagger UI tại: `https://localhost:5001/swagger`

## API Endpoints

### Department
- GET /api/departments
- GET /api/departments/{id}
- POST /api/departments
- PUT /api/departments/{id}
- DELETE /api/departments/{id}

### Employee
- GET /api/employees
- GET /api/employees/{id}
- POST /api/employees
- PUT /api/employees/{id}
- DELETE /api/employees/{id}
- GET /api/employees/search?name={name}
- GET /api/employees/department/{departmentId}
- GET /api/employees/project/{projectId}

### Project
- GET /api/projects
- GET /api/projects/{id}
- POST /api/projects
- PUT /api/projects/{id}
- DELETE /api/projects/{id}
- POST /api/projects/{projectId}/employees/{employeeId}
- DELETE /api/projects/{projectId}/employees/{employeeId}

### Transaction Test
- POST /api/transactiontest/UpdateSalary/{employeeId}
- POST /api/transactiontest/UpdateSalaryWithError/{employeeId}

## Quan hệ giữa các bảng

- Department (1) - (n) Employee
- Employee (1) - (1) Salary
- Employee (n) - (n) Project

## Validation Rules

### Department
- Tên phòng ban không được để trống
- Tên phòng ban không được vượt quá 100 ký tự

### Employee
- Tên nhân viên không được để trống
- Tên nhân viên không được vượt quá 100 ký tự
- Email phải đúng định dạng
- Số điện thoại phải đúng định dạng
- Ngày vào làm phải nhỏ hơn hoặc bằng ngày hiện tại

### Project
- Tên dự án không được để trống
- Tên dự án không được vượt quá 100 ký tự
- Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc

### Salary
- Số tiền lương phải lớn hơn 0