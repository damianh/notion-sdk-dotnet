// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Requests;

/// <summary>
/// Request body for the Notion OAuth token introspection endpoint.
/// Used to check whether an access token is still valid and retrieve its associated metadata.
/// </summary>
public sealed class IntrospectTokenRequest
{
    /// <summary>Gets the OAuth access token to introspect.</summary>
    [JsonPropertyName("token")]
    public required string Token { get; init; }
}
