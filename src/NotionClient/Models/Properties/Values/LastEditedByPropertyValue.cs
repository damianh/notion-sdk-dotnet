// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>last_edited_by</c> property, containing the user who
/// most recently modified the page.
/// <see href="https://developers.notion.com/reference/property-value-object#last-edited-by-property-values"/>
/// </summary>
public sealed class LastEditedByPropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "last_edited_by";

    /// <summary>The user who last edited this page.</summary>
    [JsonPropertyName("last_edited_by")]
    public PartialUser LastEditedBy { get; init; } = null!;
}
