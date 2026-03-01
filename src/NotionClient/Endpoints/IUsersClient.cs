// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Models;
using DamianH.NotionClient.Models.Pagination;

namespace DamianH.NotionClient.Endpoints;

/// <summary>
/// Operations on Notion users — people and bot integrations that belong to a workspace.
/// <see href="https://developers.notion.com/reference/user"/>
/// </summary>
public interface IUsersClient
{
    /// <summary>Retrieves the bot user associated with the current integration token.</summary>
    Task<User> GetSelf(CancellationToken cancellationToken = default);

    /// <summary>Retrieves a user (person or bot) by their identifier.</summary>
    Task<User> Get(string userId, CancellationToken cancellationToken = default);

    /// <summary>Returns a paginated list of all users in the workspace.</summary>
    Task<PaginatedList<User>> List(PaginationParameters? pagination = null, CancellationToken cancellationToken = default);
}
