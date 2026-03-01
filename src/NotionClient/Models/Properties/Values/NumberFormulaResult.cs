// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// A formula result containing a numeric value.
/// </summary>
public sealed class NumberFormulaResult : FormulaResult
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string FormulaType => "number";

    /// <summary>The computed numeric value of this formula, or <c>null</c> if the formula produced no result.</summary>
    [JsonPropertyName("number")]
    public double? Number { get; init; }
}
