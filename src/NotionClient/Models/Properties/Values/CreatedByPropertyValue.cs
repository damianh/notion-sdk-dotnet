// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>created_by</c> property, containing the user who created the page.
/// <see href="https://developers.notion.com/reference/property-value-object#created-by-property-values"/>
/// </summary>
public sealed class CreatedByPropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "created_by";

    /// <summary>The user who originally created this page.</summary>
    [JsonPropertyName("created_by")]
    public PartialUser CreatedBy { get; init; } = null!;
}
