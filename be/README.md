## Backend
Note: Main used packager are separate on a Building Block project so they can be reused in other projects

### Packages
- Carter: Simplifies the creation of web APIs with minimal code.
- Marten: Facilitates interaction with a Postgres database.
- MediatR: Implements the mediator pattern for handling requests and notifications.


### Services
#### Catalog

This service manages the product lifecycle within the system, including creating, updating, deleting, and listing products.

**Key Architectural Decisions**
- **CQRS Pattern**: The backend employs the Command Query Responsibility Segregation (CQRS) pattern for handling different types of operations.
- **Mediator Pattern**: The Mediator pattern is used to decouple the request handling process, making the codebase more maintainable and modular.

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
- HTTPS is not supported in the current configuration due issues in setting up the certificates on linux. I'll fix this on the future.
