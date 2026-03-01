// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Requests;

/// <summary>
/// Request body for the Notion OAuth token revocation endpoint.
/// Used to invalidate an existing access token and revoke the integration's access to the workspace.
/// </summary>
public sealed class RevokeTokenRequest
{
    /// <summary>Gets the OAuth access token to revoke.</summary>
    [JsonPropertyName("token")]
    public required string Token { get; init; }
}
