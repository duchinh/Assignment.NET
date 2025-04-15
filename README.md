# Assignment.NET

## Mô tả
Đây là project ASP.NET Core MVC quản lý thông tin thành viên (Person), gồm các chức năng CRUD, phân trang, lọc, và các thao tác nâng cao. Dự án gồm 2 phần:
- **Assignment.NET**: Source code chính (Web MVC, Service, Model, View)
- **Assignment.NET.Test**: Unit test cho service và controller (NUnit, Moq)

## Yêu cầu
- .NET 8 SDK
- Visual Studio hoặc VS Code (khuyến nghị dùng C# extension)

## Cách build và chạy
```sh
# Build project
cd Assignment.NET
 dotnet build

# Chạy web app
 dotnet run
```

Truy cập http://localhost:5000 hoặc cổng được hiển thị trên terminal.

## Chạy unit test
```sh
cd Assignment.NET.Test
 dotnet test
```

## Cấu trúc thư mục
- `Assignment.NET/` - Source code ASP.NET Core MVC
  - `Controllers/` - Controller cho web app
  - `Models/` - Định nghĩa model (Person, PaginationModel...)
  - `Services/` - Service và interface xử lý logic
  - `Views/` - Razor view cho từng chức năng
  - `wwwroot/` - Static files (css, js, lib)
- `Assignment.NET.Test/` - Unit test (NUnit, Moq)

