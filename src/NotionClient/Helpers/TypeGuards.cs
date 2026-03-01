// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Models;
using DamianH.NotionClient.Models.Blocks;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Helpers;

/// <summary>
/// Extension methods to distinguish "full" objects from partial references,
/// mirroring the JS SDK's type guard functions.
/// </summary>
public static class TypeGuards
{
    /// <summary>
    /// Returns <c>true</c> when the page has all required fields populated
    /// (i.e., it is not just a partial reference with only an id).
    /// </summary>
    public static bool IsFullPage(this Page page)
        => page is not null && page.CreatedTime is not null;

    /// <summary>
    /// Returns <c>true</c> when the block has all required fields populated.
    /// </summary>
    public static bool IsFullBlock(this Block block)
        => block is not null && block.CreatedTime is not null;

    /// <summary>
    /// Returns <c>true</c> when the database has all required fields populated.
    /// </summary>
    public static bool IsFullDatabase(this Database database)
        => database is not null && database.CreatedTime is not null;

    /// <summary>
    /// Returns <c>true</c> when the user has all required fields populated.
    /// </summary>
    public static bool IsFullUser(this User user)
        => user is not null && user.Name is not null;

    /// <summary>
    /// Returns <c>true</c> when the comment has all required fields populated.
    /// </summary>
    public static bool IsFullComment(this Comment comment)
        => comment is not null && comment.CreatedTime is not null;

    /// <summary>
    /// <see cref="PartialUser"/> is always a partial reference — this always returns <c>false</c>.
    /// Use the full <see cref="User"/> type for full user objects.
    /// </summary>
    public static bool IsFullUser(this PartialUser partialUser)
        => false;
}
