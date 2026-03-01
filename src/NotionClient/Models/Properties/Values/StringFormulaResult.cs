// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// A formula result containing a string value.
/// </summary>
public sealed class StringFormulaResult : FormulaResult
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string FormulaType => "string";

    /// <summary>The computed string value of this formula, or <c>null</c> if the formula produced no result.</summary>
    [JsonPropertyName("string")]
    public string? String { get; init; }
}
