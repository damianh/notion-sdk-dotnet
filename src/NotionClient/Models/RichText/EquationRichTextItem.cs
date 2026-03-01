// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// A rich text item containing an inline equation rendered using KaTeX.
/// </summary>
public sealed class EquationRichTextItem : RichTextItem
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "equation";

    /// <summary>The equation expression details for this segment.</summary>
    [JsonPropertyName("equation")]
    public required EquationContent Equation { get; init; }
}
