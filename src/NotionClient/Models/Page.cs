// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;
using DamianH.NotionClient.Models.Properties.Values;

namespace DamianH.NotionClient.Models;

/// <summary>
/// A Notion page object as returned by the API.
/// </summary>
/// <remarks>
/// See <see href="https://developers.notion.com/reference/page">Notion API — Page</see>.
/// </remarks>
public sealed class Page
{
    /// <summary>Gets the Notion object type, always <c>"page"</c>.</summary>
    [JsonPropertyName("object")]
    public string Object { get; init; } = "page";

    /// <summary>Gets the unique identifier (UUID) of the page.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Gets the ISO 8601 timestamp when the page was created.</summary>
    [JsonPropertyName("created_time")]
    public string? CreatedTime { get; init; }

    /// <summary>Gets the user who created the page.</summary>
    [JsonPropertyName("created_by")]
    public PartialUser? CreatedBy { get; init; }

    /// <summary>Gets the ISO 8601 timestamp of the most recent edit.</summary>
    [JsonPropertyName("last_edited_time")]
    public string? LastEditedTime { get; init; }

    /// <summary>Gets the user who last edited the page.</summary>
    [JsonPropertyName("last_edited_by")]
    public PartialUser? LastEditedBy { get; init; }

    /// <summary>Gets a value indicating whether the page has been archived (soft-deleted).</summary>
    [JsonPropertyName("archived")]
    public bool Archived { get; init; }

    /// <summary>Gets a value indicating whether the page is in the trash.</summary>
    [JsonPropertyName("in_trash")]
    public bool InTrash { get; init; }

    /// <summary>Gets the page icon (emoji or external/file image), if set.</summary>
    [JsonPropertyName("icon")]
    public Icon? Icon { get; init; }

    /// <summary>Gets the page cover image, if set.</summary>
    [JsonPropertyName("cover")]
    public PageCover? Cover { get; init; }

    /// <summary>Gets the property values keyed by property name or ID.</summary>
    [JsonPropertyName("properties")]
    public IReadOnlyDictionary<string, PropertyValue> Properties { get; init; } =
        new Dictionary<string, PropertyValue>();

    /// <summary>Gets the parent of this page (a database, page, block, or workspace).</summary>
    [JsonPropertyName("parent")]
    public Parent? Parent { get; init; }

    /// <summary>Gets the Notion URL for the page.</summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>Gets the public URL for the page if it has been shared to the web, otherwise <see langword="null"/>.</summary>
    [JsonPropertyName("public_url")]
    public string? PublicUrl { get; init; }
}
