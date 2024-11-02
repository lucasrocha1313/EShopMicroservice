## Backend
Note: Main used packager are separate on a Building Block project so they can be reused in other projects

### Packages
- Carter: Simplifies the creation of web APIs with minimal code.
- Marten: Facilitates interaction with a Postgres database.
- MediatR: Implements the mediator pattern for handling requests and notifications.
- Swashbuckle: Generates OpenAPI documentation for the API.
- FluentValidation: Provides a fluent interface for validating objects.

### Services
#### Catalog

This service manages the product lifecycle within the system, including creating, updating, deleting, and listing products.

**Key Architectural Decisions**
- **CQRS Pattern**: The backend employs the Command Query Responsibility Segregation (CQRS) pattern for handling different types of operations.
- **Mediator Pattern**: The Mediator pattern is used to decouple the request handling process, making the codebase more maintainable and modular.

**API Endpoints**
- **GET /products**: Retrieves a list of products. Pagination is supported (For instance, pageNumber=2&pageSize=5).
- **GET /products/{id}**: Retrieves a product by ID.
- **POST /products**: Creates a new product.
- **PUT /products**: Updates an existing product.
- **DELETE /products/{id}**: Deletes a product by ID.
- **GET /products/category/{category}**: Retrieves a list of products by category.

#### Basket
This service manages the shopping cart lifecycle within the system, including adding, updating, deleting, and listing items in the cart.

**Key Architectural Decisions**
- **CQRS Pattern**: The backend employs the Command Query Responsibility Segregation (CQRS) pattern for handling different types of operations.
- **Mediator Pattern**: The Mediator pattern is used to decouple the request handling process, making the codebase more maintainable and modular.
- **Caching**: The service uses Redis to cache the basket items for a specific user. This helps to reduce the load on the database and improve performance.
- **Decorator Pattern**: The decorator pattern is used to add caching functionality to the basket repository without modifying the existing code. This is transparent to the client code and allows for easy extension of functionality.
- **GRPC Client**: The service uses a GRPC client to communicate with the discount service to retrieve discount information for products.

**API Endpoints**
- **GET /basket/{userId}**: Retrieves the basket items for a specific user.
- **POST /basket**: Adds a new item to the basket.
- **DELETE /basket/{userId}**: Deletes the basket items for a specific user.

### Discount
This service manages the discount lifecycle within the system, including creating, updating, deleting, and listing discounts.

**Key Architectural Decisions**
- **GRPC**: The service uses gRPC to communicate with other services in the system. This allows for efficient and type-safe communication between services.
- **SQLite**: The service uses SQLite as the database to store discount information. SQLite is a lightweight database that is easy to set up and use for small-scale applications.
- **N-tier architecture**: The service is designed using an N-tier architecture, with separate layers for the presentation, business logic, and data access.

**GRPC**
- **GetDiscount**: Retrieves a discount by product ID.
- **CreateDiscount**: Creates a new discount.
- **UpdateDiscount**: Updates an existing discount.
- **DeleteDiscount**: Deletes a discount by product ID.
- **GetAllDiscounts**: Retrieves a list of all discounts.

### Ordering
This service manages the order lifecycle within the system, including creating, updating, deleting, and listing orders.

**Key Architectural Decisions**
- **Clean Architecture**: The service is designed using the Clean Architecture pattern, with separate layers for the presentation, application, domain, and infrastructure.
- **Primitive Obsession**: The service uses Value Objects to represent primitive types, such as OrderId, CustomerId, OrderItemId, etc. This helps to avoid primitive obsession and improve the readability and maintainability of the code.
- **Rich Domain Model**: The service uses a rich domain model to encapsulate business logic within the domain entities. This helps to keep the domain logic cohesive and maintainable.

### How to run
- **Install Docker**: Ensure Docker is installed on your system.
- **Start the Services**: Execute the following command in the project's root directory
```bash
docker compose -f docker-compose.yml -f docker-compose.overridel.yml up
```
- **Access the Catalog Service:** The Catalog service will be available at http://localhost:6000/. Example request:
```bash
curl --location 'http://localhost:6000/products?pageNumber=1&pageSize=5'
```
- **Access the Basket Service:** The Basket service will be available at http://localhost:6001/. Example request:
```bash
curl --location --request GET 'http://localhost:6001/basket/some-username'
```


`Note:  HTTPS is not supported in the current configuration due issues in setting up the certificates on linux. I'll fix this on the future.`

### Migration
- **Migrate the Database**: To migrate the database, execute the following command in the Ordering.Infrastructure project directory:
```bash
dotnet ef migrations add {MigrationName} --output-dir Data/Migrations --project ../Ordering.Infrastructure --startup-project ../Ordering.API

```