// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>last_edited_by</c> property, which automatically stores the user
/// who last modified the page.
/// <see href="https://developers.notion.com/reference/property-object#last-edited-by"/>
/// </summary>
public sealed class LastEditedByPropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "last_edited_by";

    /// <summary>The last_edited_by configuration object (currently empty in the Notion API).</summary>
    [JsonPropertyName("last_edited_by")]
    public object? LastEditedBy { get; init; }
}
