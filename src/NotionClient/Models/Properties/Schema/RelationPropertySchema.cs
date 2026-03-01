// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>relation</c> property, which links pages in one database to pages in another.
/// <see href="https://developers.notion.com/reference/property-object#relation"/>
/// </summary>
public sealed class RelationPropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "relation";

    /// <summary>The relation configuration specifying the linked database and relation type.</summary>
    [JsonPropertyName("relation")]
    public RelationConfig? Relation { get; init; }
}
