# notion users

Operations on Notion users.

See the [CLI overview](../README.md) for authentication, pagination, and global options.

## notion users get-self

Retrieve the bot user associated with the current integration token.

No arguments or command-specific options.

**Example:**

```bash
notion users get-self
```

---

## notion users get

Retrieve a user by their identifier.

**Arguments:**

| Name | Type | Required | Description |
|---|---|---|---|
| `user-id` | string | yes | The user identifier. |

**Example:**

```bash
notion users get abc123
```

---

## notion users list

Return a paginated list of all users in the workspace.

**Options:**

| Option | Type | Description |
|---|---|---|
| `--start-cursor <cursor>` | string | Resume from a cursor returned by a previous call. |
| `--page-size <n>` | int | Number of results per page (1–100). |
| `--all` | flag | Auto-paginate and collect all users into a single JSON array. |

See [pagination](../README.md#pagination) for details on `--all` vs paginated output.

**Examples:**

```bash
# Single page of users
notion users list

# All users in the workspace
notion users list --all

# Specific page size
notion users list --page-size 50
```
