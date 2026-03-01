// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Filters;

/// <summary>
/// Filter conditions for a <c>formula</c> database property.
/// The nested condition type must correspond to the return type of the formula
/// (string, number, checkbox, or date). Only one condition should be set per filter.
/// </summary>
public sealed class FormulaFilterCondition
{
    /// <summary>Gets the text condition applied when the formula result is a string value.</summary>
    [JsonPropertyName("string")]
    public TextFilterCondition? String { get; init; }

    /// <summary>Gets the numeric condition applied when the formula result is a number value.</summary>
    [JsonPropertyName("number")]
    public NumberFilterCondition? Number { get; init; }

    /// <summary>Gets the checkbox condition applied when the formula result is a boolean value.</summary>
    [JsonPropertyName("checkbox")]
    public CheckboxFilterCondition? Checkbox { get; init; }

    /// <summary>Gets the date condition applied when the formula result is a date value.</summary>
    [JsonPropertyName("date")]
    public DateFilterCondition? Date { get; init; }
}
