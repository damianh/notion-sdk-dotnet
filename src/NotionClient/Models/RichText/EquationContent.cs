// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// The content of an equation rich text segment, holding a KaTeX expression string.
/// </summary>
public sealed class EquationContent
{
    /// <summary>A KaTeX-compatible expression string.</summary>
    [JsonPropertyName("expression")]
    public required string Expression { get; init; }
}
