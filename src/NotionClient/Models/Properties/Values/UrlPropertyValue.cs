// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>url</c> property, containing a URL string.
/// <see href="https://developers.notion.com/reference/property-value-object#url-property-values"/>
/// </summary>
public sealed class UrlPropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "url";

    /// <summary>The URL string stored in this property, or <c>null</c> if not set.</summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }
}
