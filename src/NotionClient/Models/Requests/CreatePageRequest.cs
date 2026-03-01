// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Blocks;
using DamianH.NotionClient.Models.Common;
using DamianH.NotionClient.Models.Properties.Values;

namespace DamianH.NotionClient.Models.Requests;

/// <summary>
/// Request body for the <see href="https://developers.notion.com/reference/post-page">Create a page</see> API endpoint.
/// Creates a new page under a parent page or as a row in a parent database.
/// </summary>
public sealed class CreatePageRequest
{
    /// <summary>Gets the parent under which the new page will be created. Can be a page or a database.</summary>
    [JsonPropertyName("parent")]
    public required Parent Parent { get; init; }

    /// <summary>Gets the property values for the new page, keyed by property name or ID. Required when the parent is a database.</summary>
    [JsonPropertyName("properties")]
    public IReadOnlyDictionary<string, PropertyValue>? Properties { get; init; }

    /// <summary>Gets the initial block content to populate the body of the new page.</summary>
    [JsonPropertyName("children")]
    public IReadOnlyList<Block>? Children { get; init; }

    /// <summary>Gets the icon for the new page. Can be an emoji, external URL, or Notion-hosted file.</summary>
    [JsonPropertyName("icon")]
    public Icon? Icon { get; init; }

    /// <summary>Gets the cover image for the new page.</summary>
    [JsonPropertyName("cover")]
    public PageCover? Cover { get; init; }
}
