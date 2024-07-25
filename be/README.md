## Backend
Note: Main used packager are separate on a Building Block project so they can be reused in other projects

### Packages
- Carter: A library that allows you to create a web API with minimal code
- Marten: A library that allows you to interact with a Postgres database
- MediatR: A library that allows you to implement the mediator pattern


### Services
#### Catalog

Service responsible for managing the products in the system.
It allows you to create, update, delete and list products.

**Architecture Decisions**
- The backend is built using the CQRS pattern
- The backend is built using the mediator pattern

### How to run
- Install Docker
- Run the following command in the root folder of the project:
```bash
docker-compose up
```
- The backend will be available at http://localhost:5000
- The database will be available at http://localhost:5432
