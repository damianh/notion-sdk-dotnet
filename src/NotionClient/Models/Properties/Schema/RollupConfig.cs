// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Configuration for a Notion <c>rollup</c> property, which aggregates values from a related database
/// using a specified function.
/// <see href="https://developers.notion.com/reference/property-object#rollup"/>
/// </summary>
public sealed class RollupConfig
{
    /// <summary>The name of the relation property used to traverse to the related database.</summary>
    [JsonPropertyName("relation_property_name")]
    public string? RelationPropertyName { get; init; }

    /// <summary>The ID of the relation property used to traverse to the related database.</summary>
    [JsonPropertyName("relation_property_id")]
    public string? RelationPropertyId { get; init; }

    /// <summary>The name of the property in the related database whose values are aggregated.</summary>
    [JsonPropertyName("rollup_property_name")]
    public string? RollupPropertyName { get; init; }

    /// <summary>The ID of the property in the related database whose values are aggregated.</summary>
    [JsonPropertyName("rollup_property_id")]
    public string? RollupPropertyId { get; init; }

    /// <summary>The aggregation function applied to the related property values (e.g., "count", "sum", "average").</summary>
    [JsonPropertyName("function")]
    public string? Function { get; init; }
}
