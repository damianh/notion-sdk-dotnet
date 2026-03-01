// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>url</c> property, which stores a URL string.
/// <see href="https://developers.notion.com/reference/property-object#url"/>
/// </summary>
public sealed class UrlPropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "url";

    /// <summary>The url configuration object (currently empty in the Notion API).</summary>
    [JsonPropertyName("url")]
    public object? Url { get; init; }
}
