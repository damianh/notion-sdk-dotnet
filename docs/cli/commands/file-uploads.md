# notion file-uploads

Upload and manage files that can be referenced by blocks and page properties.

See the [CLI overview](../README.md) for authentication and global options.

## Upload Workflow

Files are uploaded using a three-step process:

1. **`create`** — Initiate an upload session. Notion returns a `file-upload-id`.
2. **`send-part`** — Upload the file content (or a single part for multi-part uploads).
3. **`complete`** — Mark the upload as complete.

For simple single-file uploads, use the **`upload`** convenience command, which performs all three steps automatically.

---

## notion file-uploads create

Initiate a new file upload session.

**Options:**

| Option | Type | Description |
|---|---|---|
| `--filename <name>` | string | The original filename of the file being uploaded. |
| `--content-type <mime>` | string | The MIME type of the file (e.g. image/png). |
| `--size <bytes>` | long | The total size of the file in bytes. |
| `--number-of-parts <n>` | int | The number of parts for a multi-part upload. |

All options are optional. Omit all options to create an upload session with no metadata.

**Example:**

```bash
notion file-uploads create --filename photo.png --content-type image/png --size 204800
```

---

## notion file-uploads get

Retrieve the current status and metadata of a file upload.

**Arguments:**

| Name | Type | Required | Description |
|---|---|---|---|
| `file-upload-id` | string | yes | The file upload identifier. |

**Example:**

```bash
notion file-uploads get abc123
```

---

## notion file-uploads send-part

Upload a single part (or the entirety) of the file content.

**Arguments:**

| Name | Type | Required | Description |
|---|---|---|---|
| `file-upload-id` | string | yes | The file upload identifier. |

**Options:**

| Option | Type | Required | Description |
|---|---|---|---|
| `--file <path>` | string | **yes** | Path to the file to upload. |
| `--content-type <mime>` | string | **yes** | The MIME type of the file. |
| `--part-number <n>` | int | no | The part number for multi-part uploads. |

**Example:**

```bash
notion file-uploads send-part abc123 --file photo.png --content-type image/png
```

---

## notion file-uploads complete

Mark a file upload as complete.

**Arguments:**

| Name | Type | Required | Description |
|---|---|---|---|
| `file-upload-id` | string | yes | The file upload identifier. |

**Example:**

```bash
notion file-uploads complete abc123
```

---

## notion file-uploads upload

Convenience: create + send-part + complete in one step.

**Options:**

| Option | Type | Required | Description |
|---|---|---|---|
| `--file <path>` | string | **yes** | Path to the file to upload. |
| `--content-type <mime>` | string | no | The MIME type of the file. Inferred from extension if not specified. |

When `--content-type` is omitted, the MIME type is inferred from the file extension:

| Extension | Content-Type |
|---|---|
| `.png` | `image/png` |
| `.jpg`, `.jpeg` | `image/jpeg` |
| `.gif` | `image/gif` |
| `.webp` | `image/webp` |
| `.svg` | `image/svg+xml` |
| `.pdf` | `application/pdf` |
| `.txt` | `text/plain` |
| `.md` | `text/markdown` |
| `.json` | `application/json` |
| `.csv` | `text/csv` |
| `.zip` | `application/zip` |
| *(other)* | `application/octet-stream` |

**Examples:**

```bash
# Upload with inferred content type
notion file-uploads upload --file photo.png

# Upload with explicit content type
notion file-uploads upload --file report.pdf --content-type application/pdf
```
