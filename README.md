# Items API

A minimal .NET 10 Web API for simple CRUD operations on "items", backed by SQLite.

## Tech stack

- ASP.NET Core minimal APIs (.NET 10)
- Entity Framework Core + SQLite (`app.db`)
- Swagger / OpenAPI (Swashbuckle)

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

## Getting started

```bash
cd ~/code
dotnet run
```

- API: `http://localhost:5161`
- Swagger UI: `http://localhost:5161/swagger`

The SQLite database (`app.db`) and its `Items` table are created automatically on startup.

## API endpoints

| Method | Route          | Description              |
| ------ | -------------- | ------------------------ |
| GET    | `/items`       | List all items           |
| GET    | `/items/{id}`  | Get a single item by id  |
| POST   | `/items`       | Create a new item        |

The `Item` model is `record Item(int Id, string Name)`. The `id` field is optional on
`POST` — SQLite auto-increments it.

Example request body:

```json
{ "name": "First item" }
```

## Documentation

More details are in the [`docs/`](docs/) folder:

- [`docs/run.md`](docs/run.md) — running the app
- [`docs/httpie.md`](docs/httpie.md) — HTTPie usage for the endpoints
- [`docs/functional.md`](docs/functional.md) — setup changes and verification notes

## License

[MIT](LICENSE)
