// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Configuration for a Notion <c>number</c> property, specifying how the numeric value is displayed.
/// <see href="https://developers.notion.com/reference/property-object#number"/>
/// </summary>
public sealed class NumberConfig
{
    /// <summary>
    /// Format of the number, e.g. "number", "number_with_commas", "percent",
    /// "dollar", "euro", "pound", etc.
    /// </summary>
    [JsonPropertyName("format")]
    public string Format { get; init; } = "number";
}
