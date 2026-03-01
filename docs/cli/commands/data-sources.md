# notion data-sources

Operations on Notion data sources.

See the [CLI overview](../README.md) for authentication, JSON input patterns, pagination, and global options.

## notion data-sources get

Retrieve a data source by its identifier.

**Arguments:**

| Name | Type | Required | Description |
|---|---|---|---|
| `data-source-id` | string | yes | The data source identifier. |

**Example:**

```bash
notion data-sources get abc123
```

---

## notion data-sources create

Create a new data source backed by a Notion database.

**Options:**

| Option | Type | Description |
|---|---|---|
| `--json <value>` | string | JSON body for CreateDatabaseRequest. Use @\<path\> to read from a file. |

JSON input is required (inline, `@file`, or stdin).

**Example:**

```bash
notion data-sources create --json '{
  "parent": {"page_id": "abc123"},
  "title": [{"type": "text", "text": {"content": "My Data Source"}}],
  "properties": {
    "Name": {"title": {}}
  }
}'
```

---

## notion data-sources update

Update an existing data source.

**Arguments:**

| Name | Type | Required | Description |
|---|---|---|---|
| `data-source-id` | string | yes | The data source identifier. |

**Options:**

| Option | Type | Description |
|---|---|---|
| `--json <value>` | string | JSON body for UpdateDatabaseRequest. Use @\<path\> to read from a file. |

JSON input is required (inline, `@file`, or stdin).

**Example:**

```bash
notion data-sources update abc123 --json '{"title": [{"type": "text", "text": {"content": "Renamed Source"}}]}'
```

---

## notion data-sources query

Query a data source and return matching pages.

**Arguments:**

| Name | Type | Required | Description |
|---|---|---|---|
| `data-source-id` | string | yes | The data source identifier. |

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
# Query without filters
notion data-sources query abc123

# Query with a filter
notion data-sources query abc123 --json '{
  "filter": {
    "property": "Status",
    "select": {"equals": "Active"}
  }
}'

# Collect all results with a filter
notion data-sources query abc123 --json @filter.json --all

# Manual pagination
notion data-sources query abc123 --page-size 25 --start-cursor <cursor>
```
