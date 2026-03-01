// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Common;

/// <summary>
/// Select option colors (subset of ApiColor — no background variants).
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<SelectColor>))]
public enum SelectColor
{
    /// <summary>No color applied; uses the default option styling.</summary>
    [JsonPropertyName("default")]
    Default,

    /// <summary>Gray option badge color.</summary>
    [JsonPropertyName("gray")]
    Gray,

    /// <summary>Brown option badge color.</summary>
    [JsonPropertyName("brown")]
    Brown,

    /// <summary>Orange option badge color.</summary>
    [JsonPropertyName("orange")]
    Orange,

    /// <summary>Yellow option badge color.</summary>
    [JsonPropertyName("yellow")]
    Yellow,

    /// <summary>Green option badge color.</summary>
    [JsonPropertyName("green")]
    Green,

    /// <summary>Blue option badge color.</summary>
    [JsonPropertyName("blue")]
    Blue,

    /// <summary>Purple option badge color.</summary>
    [JsonPropertyName("purple")]
    Purple,

    /// <summary>Pink option badge color.</summary>
    [JsonPropertyName("pink")]
    Pink,

    /// <summary>Red option badge color.</summary>
    [JsonPropertyName("red")]
    Red,
}
