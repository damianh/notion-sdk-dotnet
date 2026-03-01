// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Models;
using DamianH.NotionClient.Models.Pagination;
using DamianH.NotionClient.Models.Properties.Values;
using DamianH.NotionClient.Models.Requests;

namespace DamianH.NotionClient.Endpoints;

/// <summary>
/// Operations on Notion pages — the primary content objects that live inside databases
/// or as standalone top-level items.
/// <see href="https://developers.notion.com/reference/page"/>
/// </summary>
public interface IPagesClient
{
    /// <summary>Creates a new page as a child of the specified parent (page or database).</summary>
    Task<Page> Create(CreatePageRequest request, CancellationToken cancellationToken = default);

    /// <summary>Retrieves a page by its identifier, including its property values.</summary>
    Task<Page> Get(string pageId, CancellationToken cancellationToken = default);

    /// <summary>Updates page properties, cover, icon, or archive status.</summary>
    Task<Page> Update(string pageId, UpdatePageRequest request, CancellationToken cancellationToken = default);

    /// <summary>Retrieves a single page property value, with pagination for multi-value properties (e.g., relation, rollup).</summary>
    Task<PropertyValue> GetProperty(string pageId, string propertyId, PaginationParameters? pagination = null, CancellationToken cancellationToken = default);
}
