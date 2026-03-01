// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion bulleted list item block containing a single item in an unordered list.
/// </summary>
public sealed class BulletedListItemBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "bulleted_list_item";

    /// <summary>Gets the bulleted list item content including rich text, color, and optional nested child blocks.</summary>
    [JsonPropertyName("bulleted_list_item")]
    public RichTextWithColorAndChildren BulletedListItem { get; init; } = null!;
}
