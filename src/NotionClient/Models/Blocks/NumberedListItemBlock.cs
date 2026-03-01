// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion numbered list item block containing a single item in an ordered (numbered) list.
/// </summary>
public sealed class NumberedListItemBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "numbered_list_item";

    /// <summary>Gets the numbered list item content including rich text, color, and optional nested child blocks.</summary>
    [JsonPropertyName("numbered_list_item")]
    public RichTextWithColorAndChildren NumberedListItem { get; init; } = null!;
}
