// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>status</c> property, containing the selected workflow stage option.
/// <see href="https://developers.notion.com/reference/property-value-object#status-property-values"/>
/// </summary>
public sealed class StatusPropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "status";

    /// <summary>The currently selected status option, or <c>null</c> if none is selected.</summary>
    [JsonPropertyName("status")]
    public StatusOption? Status { get; init; }
}
