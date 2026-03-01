// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// A rollup result containing a date or date range value aggregated from the related pages.
/// </summary>
public sealed class DateRollupResult : RollupResult
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string RollupType => "date";

    /// <summary>The aggregated date or date range value, or <c>null</c> if no date was found.</summary>
    [JsonPropertyName("date")]
    public DateValue? Date { get; init; }
}
