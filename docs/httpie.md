Here's how to use **HTTPie** (the `http` CLI) against the running API at `http://localhost:5161`.

### Basic usage

HTTPie defaults to `GET` and uses `http://localhost` when you give it a port with `:`, so these two are equivalent:

```bash
http :5161/items
http GET http://localhost:5161/items
```

### All the endpoints

```bash
# 1. List all items (GET /items)
http :5161/items

# 2. Get a single item (GET /items/{id})
http :5161/items/1

# 3. Create an item (POST /items) — sends JSON body
http POST :5161/items name="First item"

# 4. Create with explicit id (0 = let SQLite auto-assign)
http POST :5161/items id:=0 name="Second item"

# 5. See full request/response headers + body
http -v POST :5161/items name="Verbose item"

# 6. Swagger UI / OpenAPI JSON
http :5161/swagger/index.html
http :5161/swagger/v1/swagger.json
```

### Important syntax details

- **`name="..."`** → sends a JSON *string*: `{"name": "First item"}`
- **`id:=0`** → sends raw JSON (an integer, not a string): `{"id": 0}`
- **`=`** (single colon-equals is the separator; `:=` is "raw JSON", `=` is "string")

The `Item` model is `record Item(int Id, string Name)`. JSON binding in ASP.NET Core minimal APIs is case-insensitive, so both `id`/`Id` and `name`/`Name` work. The `id` field is optional on POST — SQLite auto-increments it (that's why `id` is an `INTEGER ... PRIMARY KEY AUTOINCREMENT`).

### Example output

```console
$ http POST :5161/items name="First item"
HTTP/1.1 201 Created
Content-Type: application/json; charset=utf-8
Location: /items/1

{
    "id": 1,
    "name": "First item"
}
```

### Tip: pipe into jq

```bash
http :5161/items | jq
```

If HTTPie isn't installed, you can get it with `pip install httpie`, `brew install httpie`, or `apt install httpie`.