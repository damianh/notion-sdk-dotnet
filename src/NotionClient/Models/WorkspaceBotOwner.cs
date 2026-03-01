// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models;

/// <summary>
/// A bot owner indicating the bot is a workspace-level integration available to all workspace members.
/// </summary>
public sealed class WorkspaceBotOwner : BotOwner
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string OwnerType => "workspace";

    /// <summary>Gets a value that is always <see langword="true"/>, identifying this as a workspace owner.</summary>
    [JsonPropertyName("workspace")]
    public bool Workspace { get; init; } = true;
}
