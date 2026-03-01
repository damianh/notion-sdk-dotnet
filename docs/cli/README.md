# Notion CLI Reference

A .NET CLI tool for the Notion API. Full access to all Notion endpoints from the command line.

## Installation

Install globally (available from any directory):

```bash
dotnet tool install -g DamianH.NotionCli
```

Install locally (available within the current directory tree):

```bash
dotnet tool install DamianH.NotionCli
```

Verify installation:

```bash
notion --help
```

## Authentication

The CLI resolves an API token using the following priority order:

| Priority | Source | Details |
|---|---|---|
| 1 (highest) | `--token` option | Pass directly on the command line |
| 2 | `NOTION_TOKEN` environment variable | Set in your shell or CI environment |
| 3 (lowest) | `~/.notion/config.json` | Persistent local config file |

Config file format:

```json
{"token": "secret_..."}
```

If no token is found through any of these sources, the CLI exits with an error:

```
Error: No token provided. Use --token, NOTION_TOKEN env var, or ~/.notion/config.json
```

> **Note:** OAuth commands (`exchange-token`, `revoke`, `introspect`) do not use this token resolution. They authenticate via `--client-id` and `--client-secret` instead. See [oauth](commands/oauth.md).

## JSON Input Patterns

Many commands accept a JSON request body via `--json`. Three input methods are supported:

**Inline JSON:**

```bash
notion databases create --json '{"parent":{"page_id":"abc123"},"title":[],"properties":{}}'
```

**From a file (prefix with `@`):**

```bash
notion databases create --json @request.json
```

**From stdin (pipe when `--json` is omitted):**

```bash
cat request.json | notion databases create
```

> Some commands require `--json` (e.g., `databases create`), some treat it as optional (e.g., `databases query`), and some do not accept JSON input at all (e.g., `blocks get`).

## Output

- All command output is JSON written to **stdout**.
- By default, output is pretty-printed (indented).
- Use `--no-indent` for compact, single-line JSON output.
- Error messages are written to **stderr**.

## Pagination

The following commands support pagination options:

- `notion blocks list-children`
- `notion databases query`
- `notion pages get-property` *(supports `--start-cursor` and `--page-size` only — does not support `--all`)*
- `notion users list`
- `notion comments list`
- `notion search`
- `notion data-sources query`

**Pagination options:**

| Option | Description |
|---|---|
| `--start-cursor` | Resume from a specific cursor returned by a previous call |
| `--page-size` | Number of results per page (1–100) |
| `--all` | Auto-paginate and collect all results into a single JSON array |

**Without `--all`:** returns a `PaginatedList` object with `results`, `has_more`, and `next_cursor` fields.

**With `--all`:** returns a flat JSON array containing all results across all pages.

## Error Handling

The CLI exits with code **1** on any error. Error messages are written to stderr.

| Error type | Format |
|---|---|
| API error | `Error {status}: {message}` / `Code: {code}` / `Request-ID: {id}` |
| Invalid JSON input | `Invalid JSON: {message}` |
| Network error | `Network error: {message}` |
| Unexpected error | `Unexpected error: {message}` |

Use `--verbose` to see the raw API response body for API errors, or the full stack trace for unexpected errors.

## Global Options

These options are available on all commands (except OAuth commands, which define their own `--verbose`):

| Option | Description |
|---|---|
| `--token <token>` | Notion API bearer token (overrides env/config) |
| `--verbose` | Show diagnostic output on stderr |
| `--no-indent` | Compact (non-indented) JSON output |

## Commands

| Command | Description | Reference |
|---|---|---|
| `blocks` | Operations on Notion blocks | [blocks](commands/blocks.md) |
| `databases` | Operations on Notion databases | [databases](commands/databases.md) |
| `pages` | Operations on Notion pages | [pages](commands/pages.md) |
| `users` | Operations on Notion users | [users](commands/users.md) |
| `comments` | Operations on Notion comments | [comments](commands/comments.md) |
| `search` | Search pages and databases by title | [search](commands/search.md) |
| `file-uploads` | Upload and manage files | [file-uploads](commands/file-uploads.md) |
| `data-sources` | Operations on Notion data sources | [data-sources](commands/data-sources.md) |
| `oauth` | Notion OAuth 2.0 token management | [oauth](commands/oauth.md) |
