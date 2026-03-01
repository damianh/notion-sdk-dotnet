// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Endpoints;

namespace DamianH.NotionClient;

/// <summary>
/// Entry point for the Notion API.  Exposes strongly-typed sub-clients for each
/// API resource (pages, databases, blocks, users, comments, search, file uploads,
/// data sources, and OAuth).
/// </summary>
public interface INotionClient
{
    /// <summary>Operations on Notion blocks (content elements inside a page).</summary>
    IBlocksClient Blocks { get; }

    /// <summary>Operations on Notion databases (collections of pages with a schema).</summary>
    IDatabasesClient Databases { get; }

    /// <summary>Operations on Notion pages.</summary>
    IPagesClient Pages { get; }

    /// <summary>Operations on Notion users (people and bots in the workspace).</summary>
    IUsersClient Users { get; }

    /// <summary>Operations on Notion comments attached to pages or discussions.</summary>
    ICommentsClient Comments { get; }

    /// <summary>Full-text search across all pages and databases the integration can access.</summary>
    ISearchClient Search { get; }

    /// <summary>Upload and manage files that can be referenced by blocks.</summary>
    IFileUploadsClient FileUploads { get; }

    /// <summary>Operations on Notion data sources.</summary>
    IDataSourcesClient DataSources { get; }

    /// <summary>OAuth 2.0 token exchange, revocation, and introspection for public integrations.</summary>
    IOAuthClient OAuth { get; }
}
