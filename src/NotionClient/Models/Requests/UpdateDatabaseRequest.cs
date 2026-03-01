// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;
using DamianH.NotionClient.Models.Properties.Schema;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models.Requests;

/// <summary>
/// Request body for the <see href="https://developers.notion.com/reference/update-a-database">Update a database</see> API endpoint.
/// All fields are optional; only provided fields are updated.
/// </summary>
public sealed class UpdateDatabaseRequest
{
    /// <summary>Gets the new title for the database, expressed as a list of rich text segments.</summary>
    [JsonPropertyName("title")]
    public IReadOnlyList<RichTextItem>? Title { get; init; }

    /// <summary>Gets the new description for the database, expressed as a list of rich text segments.</summary>
    [JsonPropertyName("description")]
    public IReadOnlyList<RichTextItem>? Description { get; init; }

    /// <summary>Gets updates to the database's property schema. Set a property's value to <see langword="null"/> to remove it.</summary>
    [JsonPropertyName("properties")]
    public IReadOnlyDictionary<string, PropertySchema?>? Properties { get; init; }

    /// <summary>Gets the new icon for the database. Can be an emoji, external URL, or Notion-hosted file.</summary>
    [JsonPropertyName("icon")]
    public Icon? Icon { get; init; }

    /// <summary>Gets the new cover image for the database.</summary>
    [JsonPropertyName("cover")]
    public PageCover? Cover { get; init; }

    /// <summary>Gets a value indicating whether to archive (soft-delete) or restore the database. Set to <see langword="true"/> to archive.</summary>
    [JsonPropertyName("archived")]
    public bool? Archived { get; init; }

    /// <summary>Gets a value indicating whether the database should be displayed as an inline database embedded within a page (rather than as a full-page database).</summary>
    [JsonPropertyName("is_inline")]
    public bool? IsInline { get; init; }
}
