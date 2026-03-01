// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>rollup</c> property, containing the aggregated result
/// computed from related pages.
/// <see href="https://developers.notion.com/reference/property-value-object#rollup-property-values"/>
/// </summary>
public sealed class RollupPropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "rollup";

    /// <summary>The aggregated rollup result, which may be a number, date, or array of values.</summary>
    [JsonPropertyName("rollup")]
    public RollupResult Rollup { get; init; } = null!;
}
