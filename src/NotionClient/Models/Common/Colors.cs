// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Common;

/// <summary>
/// Text/background color values used by blocks, rich text, and select options.
/// Matches the JS SDK's ApiColor type.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<ApiColor>))]
public enum ApiColor
{
    /// <summary>No color applied; uses the default theme color.</summary>
    [JsonPropertyName("default")]
    Default,

    /// <summary>Gray foreground text or highlight color.</summary>
    [JsonPropertyName("gray")]
    Gray,

    /// <summary>Brown foreground text or highlight color.</summary>
    [JsonPropertyName("brown")]
    Brown,

    /// <summary>Orange foreground text or highlight color.</summary>
    [JsonPropertyName("orange")]
    Orange,

    /// <summary>Yellow foreground text or highlight color.</summary>
    [JsonPropertyName("yellow")]
    Yellow,

    /// <summary>Green foreground text or highlight color.</summary>
    [JsonPropertyName("green")]
    Green,

    /// <summary>Blue foreground text or highlight color.</summary>
    [JsonPropertyName("blue")]
    Blue,

    /// <summary>Purple foreground text or highlight color.</summary>
    [JsonPropertyName("purple")]
    Purple,

    /// <summary>Pink foreground text or highlight color.</summary>
    [JsonPropertyName("pink")]
    Pink,

    /// <summary>Red foreground text or highlight color.</summary>
    [JsonPropertyName("red")]
    Red,

    /// <summary>No background color applied; uses the default theme background.</summary>
    [JsonPropertyName("default_background")]
    DefaultBackground,

    /// <summary>Gray block background color.</summary>
    [JsonPropertyName("gray_background")]
    GrayBackground,

    /// <summary>Brown block background color.</summary>
    [JsonPropertyName("brown_background")]
    BrownBackground,

    /// <summary>Orange block background color.</summary>
    [JsonPropertyName("orange_background")]
    OrangeBackground,

    /// <summary>Yellow block background color.</summary>
    [JsonPropertyName("yellow_background")]
    YellowBackground,

    /// <summary>Green block background color.</summary>
    [JsonPropertyName("green_background")]
    GreenBackground,

    /// <summary>Blue block background color.</summary>
    [JsonPropertyName("blue_background")]
    BlueBackground,

    /// <summary>Purple block background color.</summary>
    [JsonPropertyName("purple_background")]
    PurpleBackground,

    /// <summary>Pink block background color.</summary>
    [JsonPropertyName("pink_background")]
    PinkBackground,

    /// <summary>Red block background color.</summary>
    [JsonPropertyName("red_background")]
    RedBackground,
}
