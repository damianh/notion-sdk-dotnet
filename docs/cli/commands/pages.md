# notion pages

Operations on Notion pages.

See the [CLI overview](../README.md) for authentication, JSON input patterns, pagination, and global options.

## notion pages get

Retrieve a page by its identifier.

**Arguments:**

| Name | Type | Required | Description |
|---|---|---|---|
| `page-id` | string | yes | The page identifier. |

**Example:**

```bash
notion pages get abc123
```

---

## notion pages create

Create a new page as a child of the specified parent.

**Options:**

| Option | Type | Description |
|---|---|---|
| `--json <value>` | string | JSON body for CreatePageRequest. Use @\<path\> to read from a file. |

JSON input is required (inline, `@file`, or stdin).

**Example:**

```bash
notion pages create --json '{
  "parent": {"database_id": "abc123"},
  "properties": {
    "Name": {
      "title": [{"text": {"content": "My New Page"}}]
    }
  }
}'
```

---

## notion pages update

Update page properties, cover, icon, or archive status.

**Arguments:**

| Name | Type | Required | Description |
|---|---|---|---|
| `page-id` | string | yes | The page identifier. |

**Options:**

| Option | Type | Description |
|---|---|---|
| `--json <value>` | string | JSON body for UpdatePageRequest. Use @\<path\> to read from a file. |

JSON input is required (inline, `@file`, or stdin).

**Example:**

```bash
notion pages update abc123 --json '{
  "properties": {
    "Status": {"select": {"name": "Done"}}
  }
}'
```

---

## notion pages get-property

Retrieve a single page property value.

**Arguments:**

| Name | Type | Required | Description |
|---|---|---|---|
| `page-id` | string | yes | The page identifier. |
| `property-id` | string | yes | The property identifier. |

**Options:**

| Option | Type | Description |
|---|---|---|
| `--start-cursor <cursor>` | string | Resume from a cursor returned by a previous call. |
| `--page-size <n>` | int | Number of results per page (1–100). |

> **Note:** The pagination options (`--start-cursor`, `--page-size`) are passed directly to the Notion API call. This command does not auto-paginate — use `--start-cursor` to manually advance through pages for paginated property types (such as relations and people).

**Examples:**

```bash
# Get a property value
notion pages get-property abc123 prop456

# Get a specific page of results for a paginated property
notion pages get-property abc123 prop456 --page-size 25
```
