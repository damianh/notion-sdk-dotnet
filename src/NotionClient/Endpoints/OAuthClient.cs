// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Models.Requests;

namespace DamianH.NotionClient.Endpoints;

internal sealed class OAuthClient : IOAuthClient
{
    private readonly NotionClient _client;

    internal OAuthClient(NotionClient client) => _client = client;

    public Task<ExchangeTokenResponse> ExchangeToken(
        string clientId,
        string clientSecret,
        ExchangeTokenRequest request,
        CancellationToken cancellationToken = default)
        => _client.SendWithBasicAuth<ExchangeTokenResponse>(
            HttpMethod.Post,
            "oauth/token",
            clientId,
            clientSecret,
            body: request,
            cancellationToken: cancellationToken);

    public Task Revoke(
        string clientId,
        string clientSecret,
        RevokeTokenRequest request,
        CancellationToken cancellationToken = default) =>
        // Revoke returns 200 with empty body or no body — discard result.
        _client.SendNoResponse(
            HttpMethod.Post,
            "oauth/revoke",
            clientId,
            clientSecret,
            body: request,
            cancellationToken: cancellationToken);

    public Task<IntrospectTokenResponse> Introspect(
        string clientId,
        string clientSecret,
        IntrospectTokenRequest request,
        CancellationToken cancellationToken = default)
        => _client.SendWithBasicAuth<IntrospectTokenResponse>(
            HttpMethod.Post,
            "oauth/introspect",
            clientId,
            clientSecret,
            body: request,
            cancellationToken: cancellationToken);
}
