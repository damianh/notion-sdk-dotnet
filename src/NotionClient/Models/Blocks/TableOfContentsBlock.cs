// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion table of contents block that auto-generates a navigable list of all headings on the page.
/// </summary>
public sealed class TableOfContentsBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "table_of_contents";

    /// <summary>Gets the table of contents content, which carries the optional color applied to the block.</summary>
    [JsonPropertyName("table_of_contents")]
    public TableOfContentsContent TableOfContents { get; init; } = null!;
}
