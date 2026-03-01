// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Common;

/// <summary>A custom workspace emoji used as an icon.</summary>
public sealed class CustomEmojiIcon : Icon
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "custom_emoji";

    /// <summary>Gets the custom emoji details, including its ID, name, and image URL.</summary>
    [JsonPropertyName("custom_emoji")]
    public required CustomEmojiInfo CustomEmoji { get; init; }
}
