// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Common;

/// <summary>
/// Indicates that a Notion object (page, block, or database) is nested inside a page.
/// </summary>
public sealed class PageParent : Parent
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "page_id";

    /// <summary>Gets the ID of the parent page.</summary>
    [JsonPropertyName("page_id")]
    public required string PageId { get; init; }
}
