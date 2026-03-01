// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// A formula result containing a date or date range value.
/// </summary>
public sealed class DateFormulaResult : FormulaResult
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string FormulaType => "date";

    /// <summary>The computed date or date range value, or <c>null</c> if the formula produced no result.</summary>
    [JsonPropertyName("date")]
    public DateValue? Date { get; init; }
}
