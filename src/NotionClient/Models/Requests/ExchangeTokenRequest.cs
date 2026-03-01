// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Requests;

/// <summary>
/// Request body for the <see href="https://developers.notion.com/reference/create-a-token">Create a token</see> Notion OAuth endpoint.
/// Exchanges a temporary authorization code for a long-lived access token.
/// </summary>
public sealed class ExchangeTokenRequest
{
    /// <summary>Gets the OAuth 2.0 grant type; always <c>"authorization_code"</c> for the Notion authorization code flow.</summary>
    [JsonPropertyName("grant_type")]
    public string GrantType { get; init; } = "authorization_code";

    /// <summary>Gets the temporary authorization code received from the Notion OAuth redirect callback.</summary>
    [JsonPropertyName("code")]
    public required string Code { get; init; }

    /// <summary>Gets the redirect URI that was used in the original authorization request, or <see langword="null"/> if not required.</summary>
    [JsonPropertyName("redirect_uri")]
    public string? RedirectUri { get; init; }

    /// <summary>Gets the external account to associate with this integration, or <see langword="null"/> if not applicable.</summary>
    [JsonPropertyName("external_account")]
    public ExternalAccount? ExternalAccount { get; init; }
}
