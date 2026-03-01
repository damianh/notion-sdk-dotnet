// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models;

/// <summary>
/// A bot owner indicating the bot is owned by a specific Notion user (user-level integration).
/// </summary>
public sealed class UserBotOwner : BotOwner
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string OwnerType => "user";

    /// <summary>Gets the user who owns this bot.</summary>
    [JsonPropertyName("user")]
    public required PartialUser User { get; init; }
}
