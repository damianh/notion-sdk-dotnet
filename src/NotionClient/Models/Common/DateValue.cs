// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Common;

/// <summary>
/// A Notion date value with optional end date and time zone.
/// </summary>
public sealed class DateValue
{
    /// <summary>ISO 8601 date or date-time string for the start of the range.</summary>
    [JsonPropertyName("start")]
    public required string Start { get; init; }

    /// <summary>ISO 8601 date or date-time string for the end of the range, if any.</summary>
    [JsonPropertyName("end")]
    public string? End { get; init; }

    /// <summary>IANA time zone identifier, e.g. "America/New_York".</summary>
    [JsonPropertyName("time_zone")]
    public string? TimeZone { get; init; }
}
