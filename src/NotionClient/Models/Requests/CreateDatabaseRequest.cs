// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;
using DamianH.NotionClient.Models.Properties.Schema;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models.Requests;

/// <summary>
/// Request body for the <see href="https://developers.notion.com/reference/create-a-database">Create a database</see> API endpoint.
/// Creates a new database as a child of the specified parent page.
/// </summary>
public sealed class CreateDatabaseRequest
{
    /// <summary>Gets the parent page under which the new database will be created.</summary>
    [JsonPropertyName("parent")]
    public required Parent Parent { get; init; }

    /// <summary>Gets the title of the new database, expressed as a list of rich text segments.</summary>
    [JsonPropertyName("title")]
    public IReadOnlyList<RichTextItem>? Title { get; init; }

    /// <summary>Gets the description of the new database, expressed as a list of rich text segments.</summary>
    [JsonPropertyName("description")]
    public IReadOnlyList<RichTextItem>? Description { get; init; }

    /// <summary>Gets the property schema for the new database, keyed by property name.</summary>
    [JsonPropertyName("properties")]
    public IReadOnlyDictionary<string, PropertySchema>? Properties { get; init; }

    /// <summary>Gets the icon for the new database. Can be an emoji, external URL, or Notion-hosted file.</summary>
    [JsonPropertyName("icon")]
    public Icon? Icon { get; init; }

    /// <summary>Gets the cover image for the new database.</summary>
    [JsonPropertyName("cover")]
    public PageCover? Cover { get; init; }

    /// <summary>Gets a value indicating whether the database should be displayed inline within a page rather than as a full-page database.</summary>
    [JsonPropertyName("is_inline")]
    public bool? IsInline { get; init; }
}
