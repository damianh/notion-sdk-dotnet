// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>people</c> property, containing the list of assigned users.
/// <see href="https://developers.notion.com/reference/property-value-object#people-property-values"/>
/// </summary>
public sealed class PeoplePropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "people";

    /// <summary>The list of Notion users assigned to this property.</summary>
    [JsonPropertyName("people")]
    public IReadOnlyList<PartialUser> People { get; init; } = [];
}
