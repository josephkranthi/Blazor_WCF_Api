# Blazor_WCF_Api

A .NET solution that demonstrates a full-stack employee management flow using:

- Blazor Server frontend for the UI
- ASP.NET Core Web API as the API gateway layer
- WCF service as the legacy backend for employee data operations

This project shows how a modern Blazor application can interact with a .NET Web API, which in turn communicates with a WCF service.

## Solution structure

```text
Blazor_WCF_Api/
├── BlazorApp1/              # Blazor Server application
│   ├── Components/
│   ├── Models/
│   ├── Pages/
│   ├── Services/
│   ├── Program.cs
│   ├── appsettings.json
│   └── BlazorApp1.csproj
├── WebApi/                  # ASP.NET Core Web API
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   ├── Program.cs
│   ├── appsettings.json
│   └── WebApi.csproj
├── WcfService1/             # ASP.NET WCF service
│   ├── App_Data/
│   ├── EmployeeService.svc
│   ├── Global.asax
│   ├── Web.config
│   ├── packages.config
│   └── WcfService1.csproj
├── Blazor_WCF_Api.slnx     # Solution file
├── .gitignore
├── .gitattributes
└── README.md
```

## What this project does

The application manages employee records with CRUD operations:

- View all employees
- Add a new employee
- Update employee details
- Delete an employee

## Project flow

1. Blazor UI calls the Web API through `EmployeeApiService`.
2. Web API exposes endpoints under `/api/Employee`.
3. The API service layer calls the WCF service using a WCF client channel.
4. The WCF service performs the actual employee data operations.

## Main endpoints

The Web API exposes the following endpoints:

- `GET /api/Employee` - Get all employees
- `GET /api/Employee/{id}` - Get employee by id
- `POST /api/Employee` - Create employee
- `PUT /api/Employee/{id}` - Update employee
- `DELETE /api/Employee/{id}` - Delete employee

## Frontend page

The Blazor application contains an `Employees` page at:

- `/employees`

This page loads all employees, allows create/edit/delete actions, and calls the Web API through the service layer.

## Technologies used

- ASP.NET Core 10
- Blazor Server
- ASP.NET Core Web API
- WCF Service (.NET Framework 4.7.2)
- C#
- HTML/CSS/Bootstrap-style UI

## Prerequisites

- .NET SDK 10 or later
- Visual Studio 2022 / VS Code with C# support
- IIS Express or a compatible host for the WCF project
- Local access to the default development ports used by the projects

## Run the project

### 1. Start the WCF service

Run the `WcfService1` project in IIS Express or your preferred local web host.

Default service URL:

```text
http://localhost:54640/EmployeeService.svc
```

### 2. Start the Web API

Run the `WebApi` project.

The app is configured to call the WCF service using the URL from configuration. The default is:

```text
http://localhost:54640/EmployeeService.svc
```

### 3. Start the Blazor app

Run the `BlazorApp1` project.

The Blazor app uses `HttpClient` with the base address:

```text
http://localhost:5125/
```

Then open the UI in a browser and navigate to:

```text
http://localhost:5125/employees
```

## Notes

- The Web API uses `EmployeeService` to wrap WCF calls.
- The Blazor app is intended as a demo for integrating a modern UI with a legacy WCF backend via ASP.NET Core Web API.
- The project is useful as a reference for communication patterns between Blazor, Web API, and WCF services in a .NET ecosystem.

## License

This project is provided as a sample/demo project and may be used as a learning reference.
