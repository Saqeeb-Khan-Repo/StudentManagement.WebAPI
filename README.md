```markdown
# Student Management Web API

A simple Student Management REST API built with ASP.NET Core Web API, C#, Dapper, and SQL Server.

## Technologies

- C#
- ASP.NET Core Web API
- Dapper
- SQL Server
- Stored Procedures
- POSTMAN

## Features

- Create Student
- Get All Students
- Get Student By ID
- Update Student
- Delete Student
- Email Validation
- DTOs
- Async/Await
- Dependency Injection

## Architecture

```text
Controller
    ↓
Service
    ↓
Repository
    ↓
Dapper
    ↓
Stored Procedures
    ↓
SQL Server
```

## Project Structure

```text
StudentManagement.WebAPI
│
├── Controllers
├── Models
├── Repository
├── RepositoryContracts
├── Services
├── ServiceContracts
│   └── DTOs
├── SqlConnection1
├── Program.cs
└── appsettings.json
```

## Student Fields

```
StudentID
Name
Age
Gender
Email
Course
Marks
CreatedAt
```

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/students` | Get all students |
| GET | `/api/students/{studentID}` | Get student by ID |
| POST | `/api/students` | Create student |
| PUT | `/api/students/{studentID}` | Update student |
| DELETE | `/api/students/{studentID}` | Delete student |

## Stored Procedures

```text
CreateStudent
GetAllStudents
GetStudentByID
CheckStudentEmail
UpdateStudent
DeleteStudentByID
```

## Validation

- Student ID is validated as a GUID.
- Email is checked before creating a student.
- Duplicate emails are rejected.
- Non-existing students return `404 Not Found`.

## HTTP Status Codes

```text
200 OK
201 Created
400 Bad Request
404 Not Found
409 Conflict
500 Internal Server Error
```


## Run the Project

```bash
dotnet restore
dotnet build
dotnet run
```

After starting the application, open POSTMAN to test the API.

## Author

Saqeeb Khan
