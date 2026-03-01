# notion databases

Operations on Notion databases.

See the [CLI overview](../README.md) for authentication, JSON input patterns, pagination, and global options.

## notion databases get

Retrieve a database by its identifier.

**Arguments:**

| Name | Type | Required | Description |
|---|---|---|---|
| `database-id` | string | yes | The database identifier. |

**Example:**

```bash
notion databases get abc123
```

---

## notion databases create

Create a new database as a child of the specified parent page.

**Options:**

| Option | Type | Description |
|---|---|---|
| `--json <value>` | string | JSON body for CreateDatabaseRequest. Use @\<path\> to read from a file. |

JSON input is required (inline, `@file`, or stdin).

**Example:**

```bash
notion databases create --json '{
  "parent": {"page_id": "abc123"},
  "title": [{"type": "text", "text": {"content": "My Database"}}],
  "properties": {
    "Name": {"title": {}}
  }
}'
```

---

## notion databases update

Update a database's title, description, or property schema.

**Arguments:**

| Name | Type | Required | Description |
|---|---|---|---|
| `database-id` | string | yes | The database identifier. |

**Options:**

| Option | Type | Description |
|---|---|---|
| `--json <value>` | string | JSON body for UpdateDatabaseRequest. Use @\<path\> to read from a file. |

JSON input is required (inline, `@file`, or stdin).

**Example:**

```bash
notion databases update abc123 --json '{"title": [{"type": "text", "text": {"content": "Renamed Database"}}]}'
```

---

## notion databases query

Query a database and return matching pages.

**Arguments:**

| Name | Type | Required | Description |
|---|---|---|---|
| `database-id` | string | yes | The database identifier. |

**Options:**

| Option | Type | Description |
|---|---|---|
| `--json <value>` | string | Optional JSON body for QueryDatabaseRequest. Use @\<path\> to read from a file. |
| `--start-cursor <cursor>` | string | Resume from a cursor returned by a previous call. |
| `--page-size <n>` | int | Number of results per page (1–100). |
| `--all` | flag | Auto-paginate and collect all matching pages into a single JSON array. |

JSON input is optional — omit `--json` (and do not pipe to stdin) to query without filters.

See [pagination](../README.md#pagination) for details on `--all` vs paginated output.

**Examples:**

```bash
# Query without filters (returns all pages)
notion databases query abc123

# Query with a filter
notion databases query abc123 --json '{
  "filter": {
    "property": "Status",
    "select": {"equals": "Done"}
  }
}'

# Query with filter from file, collect all results
notion databases query abc123 --json @filter.json --all

# Paginate manually
notion databases query abc123 --page-size 25 --start-cursor <cursor>
```
