# notion blocks

Operations on Notion blocks.

See the [CLI overview](../README.md) for authentication, JSON input patterns, pagination, and global options.

## notion blocks get

Retrieve a block by its identifier.

**Arguments:**

| Name | Type | Required | Description |
|---|---|---|---|
| `block-id` | string | yes | The block identifier. |

**Example:**

```bash
notion blocks get abc123
```

---

## notion blocks update

Update the content or appearance of an existing block.

**Arguments:**

| Name | Type | Required | Description |
|---|---|---|---|
| `block-id` | string | yes | The block identifier. |

**Options:**

| Option | Type | Description |
|---|---|---|
| `--json <value>` | string | JSON body representing the Block to update. Use @\<path\> to read from a file. |

JSON input is required (inline, `@file`, or stdin).

**Examples:**

```bash
# Inline JSON
notion blocks update abc123 --json '{"type":"paragraph","paragraph":{"rich_text":[{"type":"text","text":{"content":"Updated text"}}]}}'

# From file
notion blocks update abc123 --json @block.json

# From stdin
cat block.json | notion blocks update abc123
```

---

## notion blocks delete

Archive (soft-delete) a block and its children.

**Arguments:**

| Name | Type | Required | Description |
|---|---|---|---|
| `block-id` | string | yes | The block identifier. |

**Example:**

```bash
notion blocks delete abc123
```

---

## notion blocks list-children

Return a paginated list of child blocks.

**Arguments:**

| Name | Type | Required | Description |
|---|---|---|---|
| `block-id` | string | yes | The block identifier. |

**Options:**

| Option | Type | Description |
|---|---|---|
| `--start-cursor <cursor>` | string | Resume from a cursor returned by a previous call. |
| `--page-size <n>` | int | Number of results per page (1–100). |
| `--all` | flag | Auto-paginate and collect all child blocks into a single JSON array. |

See [pagination](../README.md#pagination) for details on `--all` vs paginated output.

**Examples:**

```bash
# Single page
notion blocks list-children abc123

# All children (auto-paginate)
notion blocks list-children abc123 --all

# Specific page size
notion blocks list-children abc123 --page-size 10
```

---

## notion blocks append-children

Append new child blocks to the specified parent block or page.

**Arguments:**

| Name | Type | Required | Description |
|---|---|---|---|
| `block-id` | string | yes | The block identifier. |

**Options:**

| Option | Type | Description |
|---|---|---|
| `--json <value>` | string | JSON body for AppendBlockChildrenRequest. Use @\<path\> to read from a file. |

JSON input is required (inline, `@file`, or stdin).

**Example:**

```bash
notion blocks append-children abc123 --json '{
  "children": [
    {
      "object": "block",
      "type": "paragraph",
      "paragraph": {
        "rich_text": [{"type": "text", "text": {"content": "A new paragraph"}}]
      }
    }
  ]
}'
```
