// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;
using DamianH.NotionClient.Models.Properties.Values;

namespace DamianH.NotionClient.Models;

/// <summary>
/// A search result representing a Notion page, returned by the <c>search</c> endpoint.
/// </summary>
public sealed class PageSearchResult : SearchResult
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string ObjectType => "page";

    /// <summary>Gets the ISO 8601 timestamp when the page was created.</summary>
    [JsonPropertyName("created_time")]
    public string? CreatedTime { get; init; }

    /// <summary>Gets the ISO 8601 timestamp of the most recent edit.</summary>
    [JsonPropertyName("last_edited_time")]
    public string? LastEditedTime { get; init; }

    /// <summary>Gets a value indicating whether the page has been archived.</summary>
    [JsonPropertyName("archived")]
    public bool Archived { get; init; }

    /// <summary>Gets a value indicating whether the page is in the trash.</summary>
    [JsonPropertyName("in_trash")]
    public bool InTrash { get; init; }

    /// <summary>Gets the page icon, if set.</summary>
    [JsonPropertyName("icon")]
    public Icon? Icon { get; init; }

    /// <summary>Gets the page cover image, if set.</summary>
    [JsonPropertyName("cover")]
    public PageCover? Cover { get; init; }

    /// <summary>Gets the property values keyed by property name or ID.</summary>
    [JsonPropertyName("properties")]
    public IReadOnlyDictionary<string, PropertyValue> Properties { get; init; } =
        new Dictionary<string, PropertyValue>();

    /// <summary>Gets the parent of this page.</summary>
    [JsonPropertyName("parent")]
    public Parent? Parent { get; init; }

    /// <summary>Gets the Notion URL for the page.</summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>Gets the public URL for the page if shared to the web, otherwise <see langword="null"/>.</summary>
    [JsonPropertyName("public_url")]
    public string? PublicUrl { get; init; }
}
