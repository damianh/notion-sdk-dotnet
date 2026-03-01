// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Abstract base class for the configuration of a Notion <c>relation</c> property.
/// Subtypes: <see cref="SinglePropertyRelationConfig"/> (single_property) and
/// <see cref="DualPropertyRelationConfig"/> (dual_property).
/// <see href="https://developers.notion.com/reference/property-object#relation"/>
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(SinglePropertyRelationConfig), "single_property")]
[JsonDerivedType(typeof(DualPropertyRelationConfig), "dual_property")]
public abstract class RelationConfig
{
    // Public parameterless constructor required by STJ polymorphic deserialization.
    public RelationConfig() { }

    /// <summary>The ID of the database this relation points to.</summary>
    [JsonPropertyName("database_id")]
    public string DatabaseId { get; init; } = null!;

    /// <summary>The relation type discriminator: "single_property" or "dual_property".</summary>
    [JsonIgnore]
    public virtual string RelationType => string.Empty;
}
