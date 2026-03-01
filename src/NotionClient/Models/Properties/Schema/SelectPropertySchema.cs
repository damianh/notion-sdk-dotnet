// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>select</c> property, which allows choosing exactly one option
/// from a predefined set.
/// <see href="https://developers.notion.com/reference/property-object#select"/>
/// </summary>
public sealed class SelectPropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "select";

    /// <summary>The select configuration including the available options.</summary>
    [JsonPropertyName("select")]
    public SelectConfig? Select { get; init; }
}
