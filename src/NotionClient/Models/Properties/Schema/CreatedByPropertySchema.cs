// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>created_by</c> property, which automatically stores the user who created the page.
/// <see href="https://developers.notion.com/reference/property-object#created-by"/>
/// </summary>
public sealed class CreatedByPropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "created_by";

    /// <summary>The created_by configuration object (currently empty in the Notion API).</summary>
    [JsonPropertyName("created_by")]
    public object? CreatedBy { get; init; }
}
