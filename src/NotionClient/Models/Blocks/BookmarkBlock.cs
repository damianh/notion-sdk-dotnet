// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion bookmark block that stores a URL and renders it as a visual web-page card.
/// </summary>
public sealed class BookmarkBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "bookmark";

    /// <summary>Gets the bookmark content containing the bookmarked URL and an optional caption.</summary>
    [JsonPropertyName("bookmark")]
    public BookmarkContent Bookmark { get; init; } = null!;
}
