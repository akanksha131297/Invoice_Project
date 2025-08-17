ASP.NET Core Web API project that manages invoices, stores them in SQLite, and publishes invoice messages to RabbitMQ.
This project demonstrates JWT authentication, Entity Framework Core, Dependency Injection, and message-based architecture using RabbitMQ.
And A Console Application .net console named Invoice Subscriber that consumes the messages from RabbitMQ .

# Features
- JWT-based authentication
- Create, save, and retrieve invoices
- Store invoice data in SQLite using EF Core
- Publish invoice messages to RabbitMQ
-Console application to consume invoice messages named Invoice Subscriber
-Swagger documentation included
-Unit tests.

# Tech Stack
Backend: ASP.NET Core 
Database: SQLite
ORM: Entity Framework Core
Messaging: RabbitMQ using https://www.cloudamqp.com/
Authentication: JWT Bearer tokens
Testing: Swagger,XUnit

# Getting Started
1. Clone the repository
2. Install the required dependencies using `dotnet restore`
3. Update the `appsettings.json` file with your RabbitMQ connection string and SQLite database path
4. Run the application using `dotnet run`
5. Access the API.
6. Use Swagger to explore the API endpoints
7. To run the Invoice Subscriber console application, navigate to the `InvoiceSubscriber` directory and run `dotnet run`
8. To run the unit tests, navigate to the `InVoiceAPI.tests` directory and run `dotnet run`.
 
 # Database Setup
- SQLite database file: `invoices.db`
- EF Core automatically creates the database when running the API.
- You can view the database using `SQLite Extension in VS code` for SQLite.


# RabbitMQ Setup
-  use a cloud service like CloudAMQP.
- Update the `appsettings.json` file with your RabbitMQ connection details.

# Authentication
- JWT authentication is implemented.
- Get a token via the /auth/login endpoint.
- Include the token in Authorization header for invoice requests.

Username: `test`
Password: `password`

# API Endpoints
| Method | Endpoint                | Description                          |
|--------|-------------------------|--------------------------------------|
| POST   | /auth/login             | Authenticate user and get JWT token  |
| POST   | /invoices               | Create a new invoice                 |
|-------------------------------------------------------------------------|


# Provided POSTMAN collection to test both the endpoints
