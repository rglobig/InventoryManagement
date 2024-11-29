# CRUD Example Project

## Goal
This project demonstrates CRUD operations using a persistent database and a Web API. Swagger integration is included for API visualization and manual testing.

## Tech Stack
- **ASP.NET Core**: Framework for building the Web API.
- **SQLite Database**: Lightweight database for persistence.
- **EF Core 9**: Entity Framework Core for ORM functionality.
- **C# 12**: Programming language used for development.
- **.NET 8**: The runtime powering the application.
- **xUnit**: Unit testing framework.
- **Moq**: Mocking framework for test isolation.
- **FluentAssertions**: Library for expressive test assertions.

## Project Structure
The project adopts a layered architecture for better organization, testability, and scalability:

- **Api**: Contains Web API controllers and handles application bootstrap/configuration.
- **Application**: Contains business logic abstractions, including services and data transfer objects (DTOs).
- **Domain**: Defines domain-specific entities and logic.
- **Infrastructure**: Implements persistence, including database configurations and migrations.

### Testing
- Each project has a corresponding `*.Tests` project containing unit tests.
- Integration tests for the API are located in `InventoryManagement.Api.IntegrationTests`.

### Future Ideas
4. **Integration Tests Seeding**  
   Create mechanisms to seed test data for integration tests, ensuring consistent test environments.

5. **CI/CD Pipeline**  
   Set up continuous integration and continuous deployment pipelines to streamline development and deployment processes.

6. **C# 13 / .NET 9 Update**  
   Prepare for updates to C# 13 and .NET 9, ensuring compatibility and leveraging new features.

7. **Architecture Tests (e.g., ArchUnitNET)**  
   Introduce architecture tests to enforce design principles and ensure the application's structure aligns with its intended architecture.

8. **Mediator Pattern (CQRS)**  
   Implement the mediator pattern for better separation of concerns and support for the CQRS (Command Query Responsibility Segregation) pattern.