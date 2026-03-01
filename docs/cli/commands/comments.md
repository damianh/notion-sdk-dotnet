# notion comments

Operations on Notion comments.

See the [CLI overview](../README.md) for authentication, JSON input patterns, pagination, and global options.

## notion comments create

Create a new comment on a page or inside an existing discussion thread.

**Options:**

| Option | Type | Description |
|---|---|---|
| `--json <value>` | string | JSON body for CreateCommentRequest. Use @\<path\> to read from a file. |

JSON input is required (inline, `@file`, or stdin).

**Example:**

```bash
# Comment on a page
notion comments create --json '{
  "parent": {"page_id": "abc123"},
  "rich_text": [{"type": "text", "text": {"content": "Great work!"}}]
}'

# Reply in a discussion thread
notion comments create --json '{
  "discussion_id": "def456",
  "rich_text": [{"type": "text", "text": {"content": "Agreed."}}]
}'
```

---

## notion comments list

Return a paginated list of unresolved comments on a block or page.

**Arguments:**

| Name | Type | Required | Description |
|---|---|---|---|
| `block-id` | string | yes | The block or page identifier to list comments for. |

**Options:**

| Option | Type | Description |
|---|---|---|
| `--start-cursor <cursor>` | string | Resume from a cursor returned by a previous call. |
| `--page-size <n>` | int | Number of results per page (1–100). |
| `--all` | flag | Auto-paginate and collect all comments into a single JSON array. |

See [pagination](../README.md#pagination) for details on `--all` vs paginated output.

**Examples:**

```bash
# Single page of comments
notion comments list abc123

# All comments (auto-paginate)
notion comments list abc123 --all
```
