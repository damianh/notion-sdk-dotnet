// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// A formula result containing a boolean (true/false) value.
/// </summary>
public sealed class BooleanFormulaResult : FormulaResult
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string FormulaType => "boolean";

    /// <summary>The computed boolean value of this formula, or <c>null</c> if the formula produced no result.</summary>
    [JsonPropertyName("boolean")]
    public bool? Boolean { get; init; }
}
