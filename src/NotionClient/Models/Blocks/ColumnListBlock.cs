// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion column list block that acts as a container for two or more <see cref="ColumnBlock"/> children,
/// arranging them side by side in a multi-column layout.
/// </summary>
public sealed class ColumnListBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "column_list";

    /// <summary>Gets the column list data object. This is always an empty object in the Notion API.</summary>
    [JsonPropertyName("column_list")]
    public object? ColumnList { get; init; }
}
