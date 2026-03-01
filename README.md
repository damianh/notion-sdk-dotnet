# DamianH.NotionClient

A .NET 10 client library for the [Notion API](https://developers.notion.com/) (API version `2025-09-03`), ported from the official [notion-sdk-js](https://github.com/makenotion/notion-sdk-js) v5.9.0.

## Features

- 30 API endpoints across 9 endpoint groups (Blocks, Databases, Pages, Users, Comments, Search, FileUploads, DataSources, OAuth)
- AOT-compatible and trimmable — source-generated `System.Text.Json` serialization with zero reflection
- `IAsyncEnumerable<T>` pagination helpers
- `IHttpClientFactory` / DI integration via `services.AddNotionClient(...)`
- Strongly-typed models with polymorphic deserialization

## Installation

```sh
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

## License

MIT
