// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion table block that renders a structured grid of cells.
/// Its child blocks are <see cref="TableRowBlock"/> instances, one per row.
/// </summary>
public sealed class TableBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "table";

    /// <summary>Gets the table metadata including dimensions and header configuration.</summary>
    [JsonPropertyName("table")]
    public TableContent Table { get; init; } = null!;
}
