// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion table row block representing a single row of cells within a <see cref="TableBlock"/>.
/// </summary>
public sealed class TableRowBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "table_row";

    /// <summary>Gets the table row content containing the rich-text cells for this row.</summary>
    [JsonPropertyName("table_row")]
    public TableRowContent TableRow { get; init; } = null!;
}
