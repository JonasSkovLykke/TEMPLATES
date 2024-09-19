# Backend - Clean Architecture & DDD

## DotNET

This is a template for building larger backend solutions using .NET.

### Table of Contents

- [Backend - Clean Architecture \& DDD](#backend---clean-architecture--ddd)
  - [DotNET](#dotnet)
    - [Table of Contents](#table-of-contents)
    - [Project Description](#project-description)
    - [Technologies Used](#technologies-used)
    - [Getting Started](#getting-started)
      - [Prerequisites](#prerequisites)
      - [Installation](#installation)
    - [Usage](#usage)
    - [Contributing](#contributing)
    - [License](#license)
    - [Acknowledgements](#acknowledgements)
    - [Architecture](#architecture)

### Project Description

This project serves as a starting point for building robust backend solutions using the .NET framework. It is designed to provide a solid foundation with best practices, tools, and libraries for developing complex and scalable applications. Whether you're building a web application, microservices, or any other backend system, this template aims to streamline your development process.

### Technologies Used

List the main technologies and frameworks that your project relies on. For example:

- Microsoft SQL Server
- Microsoft .NET 8
- Serilog
- Entity Framework Core 8
- ASP.NET Identity
- ErrorOr
- MediatR
- FluentValidation
- Polly
- Quartz
- xUnit
- FluentAssertions

### Getting Started

Explain how to get a copy of the project up and running on a local machine. Include step-by-step instructions.

Run Migrations for the Authentication DB
```bash
    cd DotNET
    add-migration CreateDb -context AuthenticationDbContext -o Migrations/AuthenticationDb
    update-database -context AuthenticationDbContext
```

Run Migrations for the DbContext
```bash
    cd DotNET
    add-migration CreateDb -context <name-of-project>DbContext -o Migrations/<name-of-project>
    update-database -context <name-of-project>DbContext
```

```bash
    cd DotNET
    dotnet restore
    dotnet build
    dotnet run
```

#### Prerequisites

List any prerequisites that users need to have installed before they can get started with your project. For example:

- Microsoft SQL Server
- .NET Core SDK
- Visual Studio or Visual Studio Code (optional)

#### Installation

1. Clone the repository to your local machine:

   ```bash
   git clone https://github.com/IOspect/TEMPLATES.git

### Usage

### Contributing

### License

### Acknowledgements

### Architecture
[Full Architecture Description](/Backend/Clean%20Architecture%20%26%20DDD/DotNET/docs/Architecture.md)