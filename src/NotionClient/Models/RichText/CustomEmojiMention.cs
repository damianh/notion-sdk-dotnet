// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// A mention that references a custom emoji inline in rich text.
/// </summary>
public sealed class CustomEmojiMention : Mention
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string MentionType => "custom_emoji";

    /// <summary>The custom emoji information, including its ID and display URL.</summary>
    [JsonPropertyName("custom_emoji")]
    public required CustomEmojiInfo CustomEmoji { get; init; }
}
