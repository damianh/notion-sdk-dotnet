// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion column block that represents a single column within a <see cref="ColumnListBlock"/>.
/// Child blocks of this block form the column's content.
/// </summary>
public sealed class ColumnBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "column";

    /// <summary>Gets the optional column layout metadata, such as the relative width ratio.</summary>
    [JsonPropertyName("column")]
    public ColumnContent? Column { get; init; }
}
