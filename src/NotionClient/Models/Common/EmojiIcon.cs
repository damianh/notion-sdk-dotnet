// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Common;

/// <summary>A single Unicode emoji character used as a page or database icon.</summary>
public sealed class EmojiIcon : Icon
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "emoji";

    /// <summary>The emoji character, e.g. "🚀".</summary>
    [JsonPropertyName("emoji")]
    public required string Emoji { get; init; }
}
