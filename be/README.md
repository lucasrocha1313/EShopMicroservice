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
docker compose -f docker-compose.yml -f docker-compose.overridel.yml up
```
- The Catalog service will be available at http://localhost:6000
  - HTTPS is not working for any service yet due certificate configuration
