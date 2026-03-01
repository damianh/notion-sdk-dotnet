// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion equation block that renders a block-level math expression using KaTeX notation.
/// </summary>
public sealed class EquationBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "equation";

    /// <summary>Gets the equation content containing the KaTeX expression string.</summary>
    [JsonPropertyName("equation")]
    public EquationContent Equation { get; init; } = null!;
}
