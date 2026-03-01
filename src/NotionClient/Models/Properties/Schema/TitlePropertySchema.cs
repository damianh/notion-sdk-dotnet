// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>title</c> property, which is the required primary name/title column
/// that every database must have.
/// <see href="https://developers.notion.com/reference/property-object#title"/>
/// </summary>
public sealed class TitlePropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "title";

    /// <summary>The title configuration object (currently empty in the Notion API).</summary>
    [JsonPropertyName("title")]
    public object? Title { get; init; }
}
