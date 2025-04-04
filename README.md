# Task Management API

Đây là một API đơn giản để quản lý các công việc (tasks) được xây dựng bằng ASP.NET Core theo kiến trúc Clean Architecture.

## Các tính năng

- Tạo một task mới
- Liệt kê tất cả các task
- Lấy thông tin một task cụ thể
- Xóa một task
- Cập nhật thông tin task
- Thêm nhiều task cùng lúc
- Xóa nhiều task cùng lúc

## Cài đặt

1. Clone repository
2. Mở solution trong Visual Studio hoặc VS Code
3. cd den TaskManagement.API
4. Chạy lệnh `dotnet restore` để cài đặt các package
5. Chạy lệnh `dotnet run` để khởi động API

## Các endpoint

### GET /api/tasks
Lấy danh sách tất cả các task

### GET /api/tasks/{id}
Lấy thông tin một task cụ thể

### POST /api/tasks
Tạo một task mới

Request body:
```json
{
    "title": "Task title",
    "isCompleted": false
}
```

### POST /api/tasks/bulk
Tạo nhiều task cùng lúc

Request body:
```json
[
    {
        "title": "Task 1",
        "isCompleted": false
    },
    {
        "title": "Task 2",
        "isCompleted": true
    }
]
```

### PUT /api/tasks/{id}
Cập nhật thông tin một task

Request body:
```json
{
    "title": "Updated title",
    "isCompleted": true
}
```

### DELETE /api/tasks/{id}
Xóa một task

### DELETE /api/tasks/bulk
Xóa nhiều task cùng lúc

Request body:
```json
["id1", "id2", "id3"]
```

## Kiến trúc

Ứng dụng được xây dựng theo kiến trúc Clean Architecture với các layer:

- Core: Chứa các entities, interfaces và business logic
- Infrastructure: Chứa các implementation của repositories
- API: Chứa các controllers và configuration

## Công nghệ sử dụng

- ASP.NET Core 8.0
- Swagger/OpenAPI
- Clean Architecture 