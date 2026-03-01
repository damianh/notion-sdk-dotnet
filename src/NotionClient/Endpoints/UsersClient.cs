// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Models;
using DamianH.NotionClient.Models.Pagination;

namespace DamianH.NotionClient.Endpoints;

internal sealed class UsersClient : IUsersClient
{
    private readonly NotionClient _client;

    internal UsersClient(NotionClient client) => _client = client;

    public Task<User> GetSelf(CancellationToken cancellationToken = default)
        => _client.Send<User>(HttpMethod.Get, "users/me", cancellationToken: cancellationToken);

    public Task<User> Get(string userId, CancellationToken cancellationToken = default)
        => _client.Send<User>(HttpMethod.Get, $"users/{userId}", cancellationToken: cancellationToken);

    public Task<PaginatedList<User>> List(
        PaginationParameters? pagination = null,
        CancellationToken cancellationToken = default)
        => _client.Send<PaginatedList<User>>(
            HttpMethod.Get,
            "users",
            query: pagination?.ToQueryParams(),
            cancellationToken: cancellationToken);
}
