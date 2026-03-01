// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Relation configuration for a <c>single_property</c> relation, where only the source database
/// has a visible property tracking the relationship.
/// <see href="https://developers.notion.com/reference/property-object#relation"/>
/// </summary>
public sealed class SinglePropertyRelationConfig : RelationConfig
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string RelationType => "single_property";

    /// <summary>The single_property configuration object (currently empty in the Notion API).</summary>
    [JsonPropertyName("single_property")]
    public object? SingleProperty { get; init; }
}
