# Assignment 2 - Person Management API

Đây là một ứng dụng Web API quản lý thông tin người dùng, được xây dựng bằng .NET Core.

## Yêu cầu hệ thống

- .NET 8.0 SDK
- SQL Server
- Visual Studio 2022 hoặc Visual Studio Code

## Cài đặt và chạy ứng dụng

1. Clone repository:
```bash
git clone [repository-url]
cd Assignment2
```

2. Cấu hình kết nối database:
- Mở file `appsettings.json`
- Cập nhật chuỗi kết nối SQL Server trong `ConnectionStrings.DefaultConnection`

3. Build và chạy ứng dụng:
```bash
dotnet build
dotnet run
```

4. Truy cập Swagger UI:
- Mở trình duyệt và truy cập: `https://localhost:5129/swagger`

## API Endpoints

### 1. Lấy danh sách người
- **GET** `/api/Person`
- Trả về danh sách tất cả người

### 2. Lấy thông tin người theo ID
- **GET** `/api/Person/{id}`
- Trả về thông tin người có ID tương ứng

### 3. Lọc theo tên
- **GET** `/api/Person/filter/name/{name}`
- Trả về danh sách người có tên chứa từ khóa tìm kiếm

### 4. Lọc theo giới tính
- **GET** `/api/Person/filter/gender/{gender}`
- Trả về danh sách người theo giới tính (Male/Female/Other)

### 5. Lọc theo nơi sinh
- **GET** `/api/Person/filter/birthplace/{birthPlace}`
- Trả về danh sách người theo nơi sinh

### 6. Thêm người mới
- **POST** `/api/Person`
- Body:
```json
{
    "firstName": "string",
    "lastName": "string",
    "dateOfBirth": "yyyy-MM-dd",
    "gender": "Male|Female|Other",
    "birthPlace": "string"
}
```

### 7. Cập nhật thông tin người
- **PUT** `/api/Person/{id}`
- Body: Tương tự như thêm người mới

### 8. Xóa người
- **DELETE** `/api/Person/{id}`

## Cấu trúc dự án

```
Assignment2/
├── Application/                 # Application Layer
│   └── DTOs/                   # Data Transfer Objects
│       └── PersonDto.cs        # DTO cho Person
│
├── Domain/                     # Domain Layer
│   ├── Entities/              # Domain Entities
│   │   └── Person.cs          # Entity Person
│   └── Interfaces/            # Domain Interfaces
│       ├── IPersonService.cs  # Interface cho Service
│       └── IPersonRepository.cs # Interface cho Repository
│
├── Infrastructure/            # Infrastructure Layer
│   ├── Data/                 # Data Access
│   │   ├── ApplicationDbContext.cs # DbContext
│   │   └── DataSeeder.cs     # Dữ liệu mẫu
│   ├── Repositories/         # Repository Implementations
│   │   └── PersonRepository.cs
│   └── Services/            # Service Implementations
│       └── PersonService.cs
│
├── Presentation/            # Presentation Layer
│   └── Controllers/        # API Controllers
│       └── PersonController.cs
│
├── Migrations/             # Entity Framework Migrations
│   └── ...
│
├── Program.cs             # Application Entry Point
├── appsettings.json       # Configuration
└── README.md             # Project Documentation
```

### Chi tiết các layer:

1. **Application Layer**
   - Chứa các DTOs để truyền dữ liệu giữa các layer
   - Có thể thêm các mappers, validators trong tương lai

2. **Domain Layer**
   - Chứa các entities và business logic cốt lõi
   - Định nghĩa các interfaces cho service và repository
   - Không phụ thuộc vào các layer khác

3. **Infrastructure Layer**
   - Implement các interfaces từ Domain Layer
   - Xử lý truy cập database, external services
   - Chứa các configurations và data seeding

4. **Presentation Layer**
   - Chứa các controllers để xử lý HTTP requests
   - Chuyển đổi giữa DTOs và Domain Models
   - Xử lý authentication/authorization

## Dữ liệu mẫu

Khi ứng dụng khởi động lần đầu, hệ thống sẽ tự động thêm 10 bản ghi mẫu vào database:
- 5 người nam (Male)
- 5 người nữ (Female)
- Mỗi người có đầy đủ thông tin: FirstName, LastName, DateOfBirth, Gender, BirthPlace

## Lưu ý

- Đảm bảo SQL Server đang chạy trước khi khởi động ứng dụng
- Trong môi trường Development, Swagger UI sẽ tự động mở
- API sử dụng HTTPS trong môi trường Production 