// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>rollup</c> property, which aggregates data from a related database
/// via a relation property.
/// <see href="https://developers.notion.com/reference/property-object#rollup"/>
/// </summary>
public sealed class RollupPropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "rollup";

    /// <summary>The rollup configuration specifying the relation, target property, and aggregation function.</summary>
    [JsonPropertyName("rollup")]
    public RollupConfig? Rollup { get; init; }
}
