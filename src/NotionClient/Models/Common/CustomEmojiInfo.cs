// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Common;

/// <summary>
/// Describes a custom emoji uploaded to a Notion workspace, used as an icon on pages or databases.
/// </summary>
public sealed class CustomEmojiInfo
{
    /// <summary>Gets the unique identifier of the custom emoji within the workspace.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Gets the display name of the custom emoji as it appears in the emoji picker.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Gets the URL of the image that represents this custom emoji.</summary>
    [JsonPropertyName("url")]
    public required string Url { get; init; }
}
