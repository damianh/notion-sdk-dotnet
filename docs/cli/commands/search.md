# notion search

Search pages and databases by title.

See the [CLI overview](../README.md) for authentication, JSON input patterns, pagination, and global options.

`search` is a top-level command — it has no subcommands.

## Options

| Option | Type | Description |
|---|---|---|
| `--query <text>` | string | Text to search for across page and database titles. |
| `--filter <value>` | string | Restrict results to `page` or `database`. |
| `--sort <direction>` | string | Sort direction: `ascending` or `descending`. |
| `--json <value>` | string | Full SearchRequest JSON body (overrides individual options). Use @\<path\> to read from a file. |
| `--start-cursor <cursor>` | string | Resume from a cursor returned by a previous call. |
| `--page-size <n>` | int | Number of results per page (1–100). |
| `--all` | flag | Auto-paginate and collect all results into a single JSON array. |

When `--json` is provided, it overrides `--query`, `--filter`, and `--sort`. When none of these options are provided, all pages and databases accessible to the integration are returned.

See [pagination](../README.md#pagination) for details on `--all` vs paginated output.

## Examples

```bash
# Search by title
notion search --query "Project Plan"

# Filter results to pages only
notion search --query "Meeting" --filter page

# Filter to databases, sorted ascending
notion search --filter database --sort ascending

# Collect all results matching a query
notion search --query "Budget" --all

# Use a full SearchRequest JSON body (overrides individual options)
notion search --json '{
  "query": "Project Plan",
  "filter": {"value": "page", "property": "object"},
  "sort": {"direction": "descending", "timestamp": "last_edited_time"}
}'

# Full JSON body from file
notion search --json @search.json --all
```
