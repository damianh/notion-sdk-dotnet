// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>The content payload of a <see cref="ToDoBlock"/>.</summary>
public sealed class ToDoContent
{
    /// <summary>Gets the rich-text items that form the to-do item's label text.</summary>
    [JsonPropertyName("rich_text")]
    public IReadOnlyList<RichTextItem> RichText { get; init; } = [];

    /// <summary>Gets the text or background color applied to the to-do block.</summary>
    [JsonPropertyName("color")]
    public ApiColor Color { get; init; } = ApiColor.Default;

    /// <summary>Gets a value indicating whether the to-do checkbox has been checked (completed).</summary>
    [JsonPropertyName("checked")]
    public bool Checked { get; init; }
}
