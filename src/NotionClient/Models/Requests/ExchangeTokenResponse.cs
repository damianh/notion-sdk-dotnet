// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Requests;

/// <summary>
/// Response body returned when exchanging an OAuth authorization code for an access token
/// via the Notion OAuth token endpoint.
/// </summary>
public sealed class ExchangeTokenResponse
{
    /// <summary>Gets the OAuth 2.0 access token that can be used to authenticate Notion API requests.</summary>
    [JsonPropertyName("access_token")]
    public required string AccessToken { get; init; }

    /// <summary>Gets the token type; always <c>"bearer"</c> for Notion's OAuth flow.</summary>
    [JsonPropertyName("token_type")]
    public string TokenType { get; init; } = "bearer";

    /// <summary>Gets the ID of the Notion bot user created for this integration connection.</summary>
    [JsonPropertyName("bot_id")]
    public required string BotId { get; init; }

    /// <summary>Gets the ID of the Notion template that was duplicated during the authorization flow, or <see langword="null"/> if no template was duplicated.</summary>
    [JsonPropertyName("duplicated_template_id")]
    public string? DuplicatedTemplateId { get; init; }

    /// <summary>Gets information about the owner of the bot (workspace or user), or <see langword="null"/> if not available.</summary>
    [JsonPropertyName("owner")]
    public BotOwner? Owner { get; init; }

    /// <summary>Gets the URL of the workspace's icon image, or <see langword="null"/> if no icon is set.</summary>
    [JsonPropertyName("workspace_icon")]
    public string? WorkspaceIcon { get; init; }

    /// <summary>Gets the ID of the Notion workspace the integration was authorized against.</summary>
    [JsonPropertyName("workspace_id")]
    public required string WorkspaceId { get; init; }

    /// <summary>Gets the display name of the Notion workspace the integration was authorized against, or <see langword="null"/> if not available.</summary>
    [JsonPropertyName("workspace_name")]
    public string? WorkspaceName { get; init; }
}
