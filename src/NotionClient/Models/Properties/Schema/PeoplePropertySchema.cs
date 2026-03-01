// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>people</c> property, which stores references to one or more Notion users.
/// <see href="https://developers.notion.com/reference/property-object#people"/>
/// </summary>
public sealed class PeoplePropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "people";

    /// <summary>The people configuration object (currently empty in the Notion API).</summary>
    [JsonPropertyName("people")]
    public object? People { get; init; }
}
