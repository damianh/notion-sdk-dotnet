// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>button</c> property, which renders a clickable button in a database view.
/// <see href="https://developers.notion.com/reference/property-object#button"/>
/// </summary>
public sealed class ButtonPropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "button";

    /// <summary>The button configuration object (currently empty in the Notion API).</summary>
    [JsonPropertyName("button")]
    public object? Button { get; init; }
}
