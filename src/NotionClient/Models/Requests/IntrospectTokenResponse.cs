// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Requests;

/// <summary>
/// Response body returned by the Notion OAuth token introspection endpoint.
/// Contains the validity status of the access token and associated workspace information.
/// </summary>
public sealed class IntrospectTokenResponse
{
    /// <summary>Gets a value indicating whether the provided access token is currently active and valid.</summary>
    [JsonPropertyName("active")]
    public bool Active { get; init; }

    /// <summary>Gets the ID of the bot user associated with this access token, or <see langword="null"/> if not applicable.</summary>
    [JsonPropertyName("bot_id")]
    public string? BotId { get; init; }

    /// <summary>Gets information about the owner of the bot (workspace or user), or <see langword="null"/> if not available.</summary>
    [JsonPropertyName("owner")]
    public BotOwner? Owner { get; init; }

    /// <summary>Gets the ID of the Notion workspace this token grants access to, or <see langword="null"/> if not available.</summary>
    [JsonPropertyName("workspace_id")]
    public string? WorkspaceId { get; init; }

    /// <summary>Gets the display name of the Notion workspace this token grants access to, or <see langword="null"/> if not available.</summary>
    [JsonPropertyName("workspace_name")]
    public string? WorkspaceName { get; init; }
}
