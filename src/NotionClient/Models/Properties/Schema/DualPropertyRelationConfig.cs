// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Relation configuration for a <c>dual_property</c> relation, where both the source and target databases
/// each have a visible property tracking the relationship.
/// <see href="https://developers.notion.com/reference/property-object#relation"/>
/// </summary>
public sealed class DualPropertyRelationConfig : RelationConfig
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string RelationType => "dual_property";

    /// <summary>Details about the mirrored property in the related database.</summary>
    [JsonPropertyName("dual_property")]
    public DualPropertyInfo? DualProperty { get; init; }
}
