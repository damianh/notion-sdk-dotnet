// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>The content payload of a <see cref="TableRowBlock"/>.</summary>
public sealed class TableRowContent
{
    /// <summary>Gets the ordered list of cells in this row, where each cell is itself a list of rich-text items.</summary>
    [JsonPropertyName("cells")]
    public IReadOnlyList<IReadOnlyList<RichTextItem>> Cells { get; init; } = [];
}
