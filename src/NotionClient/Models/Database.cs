// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;
using DamianH.NotionClient.Models.Properties.Schema;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models;

/// <summary>
/// A Notion database object as returned by the API.
/// </summary>
/// <remarks>
/// See <see href="https://developers.notion.com/reference/database">Notion API — Database</see>.
/// </remarks>
public sealed class Database
{
    /// <summary>Gets the Notion object type, always <c>"database"</c>.</summary>
    [JsonPropertyName("object")]
    public string Object { get; init; } = "database";

    /// <summary>Gets the unique identifier (UUID) of the database.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Gets the ISO 8601 timestamp when the database was created.</summary>
    [JsonPropertyName("created_time")]
    public string? CreatedTime { get; init; }

    /// <summary>Gets the user who created the database.</summary>
    [JsonPropertyName("created_by")]
    public PartialUser? CreatedBy { get; init; }

    /// <summary>Gets the ISO 8601 timestamp of the most recent edit.</summary>
    [JsonPropertyName("last_edited_time")]
    public string? LastEditedTime { get; init; }

    /// <summary>Gets the user who last edited the database.</summary>
    [JsonPropertyName("last_edited_by")]
    public PartialUser? LastEditedBy { get; init; }

    /// <summary>Gets the rich text segments that form the database title.</summary>
    [JsonPropertyName("title")]
    public IReadOnlyList<RichTextItem> Title { get; init; } = [];

    /// <summary>Gets the rich text segments that form the database description.</summary>
    [JsonPropertyName("description")]
    public IReadOnlyList<RichTextItem> Description { get; init; } = [];

    /// <summary>Gets the database icon (emoji or external/file image), if set.</summary>
    [JsonPropertyName("icon")]
    public Icon? Icon { get; init; }

    /// <summary>Gets the database cover image, if set.</summary>
    [JsonPropertyName("cover")]
    public PageCover? Cover { get; init; }

    /// <summary>Gets the property schemas keyed by property name, defining the columns of this database.</summary>
    [JsonPropertyName("properties")]
    public IReadOnlyDictionary<string, PropertySchema> Properties { get; init; } =
        new Dictionary<string, PropertySchema>();

    /// <summary>Gets the parent of this database (a page, block, or workspace).</summary>
    [JsonPropertyName("parent")]
    public Parent? Parent { get; init; }

    /// <summary>Gets the Notion URL for the database.</summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>Gets the public URL for the database if it has been shared to the web, otherwise <see langword="null"/>.</summary>
    [JsonPropertyName("public_url")]
    public string? PublicUrl { get; init; }

    /// <summary>Gets a value indicating whether the database has been archived (soft-deleted).</summary>
    [JsonPropertyName("archived")]
    public bool Archived { get; init; }

    /// <summary>Gets a value indicating whether the database is in the trash.</summary>
    [JsonPropertyName("in_trash")]
    public bool InTrash { get; init; }

    /// <summary>Gets a value indicating whether this database is rendered inline within its parent page.</summary>
    [JsonPropertyName("is_inline")]
    public bool IsInline { get; init; }
}
