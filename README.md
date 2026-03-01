# Notion SDK for .NET

[![CI](https://github.com/damianh/notion-sdk-dotnet/actions/workflows/ci.yml/badge.svg)](https://github.com/damianh/notion-sdk-dotnet/actions/workflows/ci.yml)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg)](https://dotnet.microsoft.com/)
[![GitHub Stars](https://img.shields.io/github/stars/damianh/notion-sdk-dotnet.svg)](https://github.com/damianh/notion-sdk-dotnet/stargazers)

A .NET 10 client library and CLI tool for the [Notion API](https://developers.notion.com/) (API version `2025-09-03`), ported from the official [notion-sdk-js](https://github.com/makenotion/notion-sdk-js) v5.9.0.

| Package | NuGet | Downloads |
|---------|-------|-----------|
| **DamianH.NotionClient** — .NET client library for the Notion API | [![NuGet](https://img.shields.io/nuget/v/DamianH.NotionClient.svg)](https://www.nuget.org/packages/DamianH.NotionClient/) | [![Downloads](https://img.shields.io/nuget/dt/DamianH.NotionClient.svg)](https://www.nuget.org/packages/DamianH.NotionClient/) |
| **DamianH.NotionCli** — .NET CLI tool for the Notion API | [![NuGet](https://img.shields.io/nuget/v/DamianH.NotionCli.svg)](https://www.nuget.org/packages/DamianH.NotionCli/) | [![Downloads](https://img.shields.io/nuget/dt/DamianH.NotionCli.svg)](https://www.nuget.org/packages/DamianH.NotionCli/) |

## Table of Contents

### NotionClient
- [Features](#features)
- [Installation](#installation)
- [Quick Start](#quick-start)
  - [Dependency Injection (recommended)](#dependency-injection-recommended)
  - [Standalone](#standalone)
- [Endpoint Groups](#endpoint-groups)
- [Pagination](#pagination)

### NotionCli
- [Overview](#notion-cli)
- [Installation](#cli-installation)
- [Authentication](#authentication)
- [Usage Examples](#usage-examples)
- [Command Reference](#command-reference)

### General
- [Contributing](#contributing)
- [License](#license)

---

## Features

> **Package:** `DamianH.NotionClient` — A .NET client library for the Notion API with full coverage of all endpoints.

- **30 API endpoints** across 9 endpoint groups (Blocks, Databases, Pages, Users, Comments, Search, FileUploads, DataSources, OAuth)
- **AOT-compatible and trimmable** — source-generated `System.Text.Json` serialization with zero reflection
- **`IAsyncEnumerable<T>` pagination helpers** — iterate or collect all pages with a single call
- **`IHttpClientFactory` / DI integration** via `services.AddNotionClient(...)`
- **Strongly-typed models** with polymorphic deserialization

## Installation

```bash
dotnet add package DamianH.NotionClient
```

## Quick Start

### Dependency Injection (recommended)

```csharp
services
    .AddNotionClient()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri("https://api.notion.com/v1/");
        c.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", configuration["Notion:Token"]);
    });
```

Resolve `INotionClient` from the container and use it:

```csharp
var page = await notionClient.Pages.Get("page-id");
```

### Standalone

```csharp
var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://api.notion.com/v1/")
};
httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", "secret_...");
httpClient.DefaultRequestHeaders.Add("Notion-Version", "2025-09-03");

var client = new NotionClient(httpClient);
var page = await client.Pages.Get("page-id");
```

## Endpoint Groups

| Group | Interface | Description |
|-------|-----------|-------------|
| Blocks | `IBlocksClient` | Retrieve, append, update, and delete blocks |
| Databases | `IDatabasesClient` | Create, query, retrieve, and update databases |
| Pages | `IPagesClient` | Create, retrieve, update pages and page properties |
| Users | `IUsersClient` | List and retrieve users, get bot user |
| Comments | `ICommentsClient` | Create and list comments |
| Search | `ISearchClient` | Search pages and databases by title |
| FileUploads | `IFileUploadsClient` | Upload and manage files |
| DataSources | `IDataSourcesClient` | Query and manage data sources |
| OAuth | `IOAuthClient` | Token exchange, revocation, and introspection |

## Pagination

Use `PaginationHelpers` to iterate all pages of a paginated endpoint:

```csharp
await foreach (var block in PaginationHelpers.Enumerate(
    (cursor, ct) => client.Blocks.ListChildren("block-id",
        new PaginationParameters { StartCursor = cursor }, ct)))
{
    // process each block
}
```

Or collect everything into a single list:

```csharp
IReadOnlyList<User> allUsers = await PaginationHelpers.CollectAll(
    (cursor, ct) => client.Users.List(
        new PaginationParameters { StartCursor = cursor }, ct));
```

---

## Notion CLI

> **Package:** `DamianH.NotionCli` — A .NET CLI tool providing full access to all Notion API endpoints from the command line.

### CLI Installation

Install as a global .NET tool:

```bash
dotnet tool install -g DamianH.NotionCli
```

Or install locally (available within the current directory tree):

```bash
dotnet tool install DamianH.NotionCli
```

Verify installation:

```bash
notion --help
```

### Authentication

The CLI resolves an API token using the following priority order:

| Priority | Source | Details |
|----------|--------|---------|
| 1 (highest) | `--token` option | Pass directly on the command line |
| 2 | `NOTION_TOKEN` env var | Set in your shell or CI environment |
| 3 (lowest) | `~/.notion/config.json` | Persistent local config file (`{"token": "secret_..."}`) |

### Usage Examples

**Query a database:**

```bash
notion databases query --id "database-id"
```

**Get a page:**

```bash
notion pages get --id "page-id"
```

**Search by title:**

```bash
notion search --json '{"query": "Meeting Notes"}'
```

**List all users (auto-paginate):**

```bash
notion users list --all
```

**Create a database from a file:**

```bash
notion databases create --json @request.json
```

### Command Reference

| Command | Description | Reference |
|---------|-------------|-----------|
| `blocks` | Operations on Notion blocks | [docs](docs/cli/commands/blocks.md) |
| `databases` | Operations on Notion databases | [docs](docs/cli/commands/databases.md) |
| `pages` | Operations on Notion pages | [docs](docs/cli/commands/pages.md) |
| `users` | Operations on Notion users | [docs](docs/cli/commands/users.md) |
| `comments` | Operations on Notion comments | [docs](docs/cli/commands/comments.md) |
| `search` | Search pages and databases by title | [docs](docs/cli/commands/search.md) |
| `file-uploads` | Upload and manage files | [docs](docs/cli/commands/file-uploads.md) |
| `data-sources` | Operations on Notion data sources | [docs](docs/cli/commands/data-sources.md) |
| `oauth` | Notion OAuth 2.0 token management | [docs](docs/cli/commands/oauth.md) |

For the full CLI reference including pagination, JSON input patterns, error handling, and global options, see [docs/cli/README.md](docs/cli/README.md).

---

## Contributing

Bug reports should be accompanied by a reproducible test case in a pull request.

## License

MIT
