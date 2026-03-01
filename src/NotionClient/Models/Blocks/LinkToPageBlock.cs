// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion link-to-page block that acts as a reference pointing to another Notion page, database, or comment.
/// </summary>
public sealed class LinkToPageBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "link_to_page";

    /// <summary>Gets the link target, which is a polymorphic object identifying the referenced page, database, or comment.</summary>
    [JsonPropertyName("link_to_page")]
    public LinkToPageContent LinkToPage { get; init; } = null!;
}
