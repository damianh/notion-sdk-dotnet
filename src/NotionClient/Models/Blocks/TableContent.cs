// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>The metadata of a <see cref="TableBlock"/> describing its structure and header settings.</summary>
public sealed class TableContent
{
    /// <summary>Gets a value indicating whether the first row of the table is treated as a column header row.</summary>
    [JsonPropertyName("has_column_header")]
    public bool HasColumnHeader { get; init; }

    /// <summary>Gets a value indicating whether the first column of the table is treated as a row header column.</summary>
    [JsonPropertyName("has_row_header")]
    public bool HasRowHeader { get; init; }

    /// <summary>Gets the number of columns in the table.</summary>
    [JsonPropertyName("table_width")]
    public int TableWidth { get; init; }
}
