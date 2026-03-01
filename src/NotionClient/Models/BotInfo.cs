// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models;

/// <summary>
/// Additional information about a bot user, including its owner and the workspace it belongs to.
/// </summary>
public sealed class BotInfo
{
    /// <summary>Gets the owner of this bot — either a workspace or a specific user.</summary>
    [JsonPropertyName("owner")]
    public BotOwner? Owner { get; init; }

    /// <summary>Gets the name of the workspace this bot belongs to.</summary>
    [JsonPropertyName("workspace_name")]
    public string? WorkspaceName { get; init; }
}
