// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Models.Pagination;

namespace DamianH.NotionClient.Helpers;

/// <summary>
/// Helpers for iterating paginated Notion API responses as <see cref="IAsyncEnumerable{T}"/>.
/// </summary>
public static class PaginationHelpers
{
    /// <summary>
    /// Iterates all pages of a paginated endpoint, yielding each item individually.
    /// </summary>
    /// <typeparam name="T">The item type.</typeparam>
    /// <param name="fetch">
    /// A delegate that accepts an optional <c>start_cursor</c> and returns the next page.
    /// Pass <c>null</c> to get the first page.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>An async sequence of all items across all pages.</returns>
    public static async IAsyncEnumerable<T> Enumerate<T>(
        Func<string?, CancellationToken, Task<PaginatedList<T>>> fetch,
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        string? cursor = null;

        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var page = await fetch(cursor, cancellationToken).ConfigureAwait(false);

            foreach (var item in page.Results)
            {
                yield return item;
            }

            if (!page.HasMore || page.NextCursor is null)
            {
                break;
            }

            cursor = page.NextCursor;
        }
    }

    /// <summary>
    /// Collects all items from all pages into a single list.
    /// </summary>
    public static async Task<IReadOnlyList<T>> CollectAll<T>(
        Func<string?, CancellationToken, Task<PaginatedList<T>>> fetch,
        CancellationToken cancellationToken = default)
    {
        var results = new List<T>();
        await foreach (var item in Enumerate(fetch, cancellationToken).ConfigureAwait(false))
        {
            results.Add(item);
        }
        return results;
    }
}
