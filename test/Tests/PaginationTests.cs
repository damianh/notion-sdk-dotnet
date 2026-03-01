// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Helpers;
using DamianH.NotionClient.Models.Pagination;

namespace DamianH.NotionClient;

public sealed class PaginationTests
{
    [Fact]
    public async Task Enumerate_YieldsSinglePageItems()
    {
        var page = new PaginatedList<string>
        {
            Results = ["a", "b", "c"],
            HasMore = false,
            NextCursor = null,
        };

        var items = await PaginationHelpers.Enumerate<string>(
            (_, _) => Task.FromResult(page)).ToList();

        items.ShouldBe(["a", "b", "c"]);
    }

    [Fact]
    public async Task Enumerate_YieldsItemsAcrossMultiplePages()
    {
        var pages = new Queue<PaginatedList<string>>(
        [
            new() { Results = ["a", "b"], HasMore = true,  NextCursor = "cursor1" },
            new() { Results = ["c", "d"], HasMore = true,  NextCursor = "cursor2" },
            new() { Results = ["e"],      HasMore = false, NextCursor = null },
        ]);

        var items = await PaginationHelpers.Enumerate<string>(
            (_, _) => Task.FromResult(pages.Dequeue())).ToList();

        items.ShouldBe(["a", "b", "c", "d", "e"]);
    }

    [Fact]
    public async Task Enumerate_PassesCursorToNextFetch()
    {
        var capturedCursors = new List<string?>();

        var pages = new Queue<PaginatedList<string>>(
        [
            new() { Results = ["a"], HasMore = true,  NextCursor = "page2" },
            new() { Results = ["b"], HasMore = false, NextCursor = null },
        ]);

        await PaginationHelpers.Enumerate<string>((cursor, _) =>
        {
            capturedCursors.Add(cursor);
            return Task.FromResult(pages.Dequeue());
        }).ToList();

        capturedCursors.ShouldBe([null, "page2"]);
    }

    [Fact]
    public async Task Enumerate_StopsWhenHasMoreFalseEvenIfCursorPresent()
    {
        var callCount = 0;
        var page = new PaginatedList<string>
        {
            Results = ["x"],
            HasMore = false,
            NextCursor = "some-cursor",
        };

        var items = await PaginationHelpers.Enumerate<string>((_, _) =>
        {
            callCount++;
            return Task.FromResult(page);
        }).ToList();

        callCount.ShouldBe(1);
        items.ShouldBe(["x"]);
    }

    [Fact]
    public async Task Enumerate_ReturnsEmptyForEmptyResults()
    {
        var page = new PaginatedList<string>
        {
            Results = [],
            HasMore = false,
            NextCursor = null,
        };

        var items = await PaginationHelpers.Enumerate<string>(
            (_, _) => Task.FromResult(page)).ToList();

        items.ShouldBeEmpty();
    }

    [Fact]
    public async Task Enumerate_RespectsCancellation()
    {
        var cts = new CancellationTokenSource();

        var pages = new Queue<PaginatedList<string>>(
        [
            new() { Results = ["a"], HasMore = true, NextCursor = "c" },
        ]);

        await Should.ThrowAsync<OperationCanceledException>(async () =>
        {
            await foreach (var _ in PaginationHelpers.Enumerate<string>((cursor, ct) =>
            {
                if (cursor == "c")
                {
                    cts.Cancel();
                }
                ct.ThrowIfCancellationRequested();
                return Task.FromResult(pages.Dequeue());
            }, cts.Token))
            {
            }
        });
    }

    [Fact]
    public async Task CollectAll_ReturnsAllItemsAsList()
    {
        var pages = new Queue<PaginatedList<int>>(
        [
            new() { Results = [1, 2], HasMore = true,  NextCursor = "c" },
            new() { Results = [3],    HasMore = false, NextCursor = null },
        ]);

        var result = await PaginationHelpers.CollectAll<int>(
            (_, _) => Task.FromResult(pages.Dequeue()));

        result.ShouldBe([1, 2, 3]);
    }
}

internal static class AsyncEnumerableExtensions
{
    public static async Task<List<T>> ToList<T>(this IAsyncEnumerable<T> source, CancellationToken ct = default)
    {
        var list = new List<T>();
        await foreach (var item in source.WithCancellation(ct))
        {
            list.Add(item);
        }
        return list;
    }
}
