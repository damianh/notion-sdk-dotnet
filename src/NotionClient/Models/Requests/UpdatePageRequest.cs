// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;
using DamianH.NotionClient.Models.Properties.Values;

namespace DamianH.NotionClient.Models.Requests;

/// <summary>
/// Request body for the <see href="https://developers.notion.com/reference/patch-page">Update page properties</see> API endpoint.
/// All fields are optional; only provided fields are updated.
/// </summary>
public sealed class UpdatePageRequest
{
    /// <summary>Gets the updated property values for the page, keyed by property name or ID. Set a value to its empty representation to clear it.</summary>
    [JsonPropertyName("properties")]
    public IReadOnlyDictionary<string, PropertyValue>? Properties { get; init; }

    /// <summary>Gets a value indicating whether to archive (soft-delete) or restore the page. Set to <see langword="true"/> to archive.</summary>
    [JsonPropertyName("archived")]
    public bool? Archived { get; init; }

    /// <summary>Gets a value indicating whether to move the page to the Notion trash. Set to <see langword="true"/> to trash the page.</summary>
    [JsonPropertyName("in_trash")]
    public bool? InTrash { get; init; }

    /// <summary>Gets the new icon for the page. Can be an emoji, external URL, or Notion-hosted file.</summary>
    [JsonPropertyName("icon")]
    public Icon? Icon { get; init; }

    /// <summary>Gets the new cover image for the page.</summary>
    [JsonPropertyName("cover")]
    public PageCover? Cover { get; init; }
}
