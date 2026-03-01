// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models;

/// <summary>
/// A Notion bot user (integration), containing the <see cref="Bot"/> details about the integration's owner and workspace.
/// </summary>
public sealed class BotUser : User
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string UserType => "bot";

    /// <summary>Gets additional bot-specific information such as the bot's owner.</summary>
    [JsonPropertyName("bot")]
    public BotInfo? Bot { get; init; }
}
