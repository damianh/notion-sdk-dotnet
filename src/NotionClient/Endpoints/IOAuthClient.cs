// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Models.Requests;

namespace DamianH.NotionClient.Endpoints;

/// <summary>
/// Client for the Notion OAuth 2.0 endpoints used by public integrations to exchange,
/// introspect, and revoke access tokens on behalf of Notion users.
/// </summary>
/// <remarks>
/// See <see href="https://developers.notion.com/docs/authorization">Notion API — Authorization</see>.
/// All methods require the integration's OAuth client ID and client secret, which are sent
/// via HTTP Basic authentication.
/// </remarks>
public interface IOAuthClient
{
    /// <summary>
    /// Exchanges a temporary authorization code for a long-lived access token.
    /// This is the final step of the OAuth 2.0 authorization code flow.
    /// </summary>
    /// <param name="clientId">The OAuth client ID of the public integration.</param>
    /// <param name="clientSecret">The OAuth client secret of the public integration.</param>
    /// <param name="request">Contains the authorization <c>code</c> and <c>redirect_uri</c> to exchange.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>An <see cref="ExchangeTokenResponse"/> containing the access token and workspace information.</returns>
    Task<ExchangeTokenResponse> ExchangeToken(string clientId, string clientSecret, ExchangeTokenRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Revokes a previously issued access token, removing the integration's access to the user's workspace.
    /// </summary>
    /// <param name="clientId">The OAuth client ID of the public integration.</param>
    /// <param name="clientSecret">The OAuth client secret of the public integration.</param>
    /// <param name="request">Contains the access token to revoke.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    Task Revoke(string clientId, string clientSecret, RevokeTokenRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Introspects an access token to determine whether it is still valid and retrieve metadata
    /// such as the associated workspace and bot user.
    /// </summary>
    /// <param name="clientId">The OAuth client ID of the public integration.</param>
    /// <param name="clientSecret">The OAuth client secret of the public integration.</param>
    /// <param name="request">Contains the access token to introspect.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>An <see cref="IntrospectTokenResponse"/> with token validity and metadata.</returns>
    Task<IntrospectTokenResponse> Introspect(string clientId, string clientSecret, IntrospectTokenRequest request, CancellationToken cancellationToken = default);
}
