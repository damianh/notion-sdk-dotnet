// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.CommandLine;
using DamianH.NotionClient.Models.Pagination;

namespace DamianH.NotionCli.Infrastructure;

internal sealed class PaginationOptions
{
    internal Option<string?> StartCursor { get; }
    internal Option<int?> PageSize { get; }
    internal Option<bool> All { get; }

    internal PaginationOptions()
    {
        StartCursor = new Option<string?>("--start-cursor") { Description = "Cursor from a previous page's next_cursor." };
        PageSize = new Option<int?>("--page-size") { Description = "Maximum number of results to return per page (1-100)." };
        All = new Option<bool>("--all") { Description = "Auto-paginate and collect all results into a single array." };
    }

    internal void AddTo(Command command) =>
        new List<Option> { StartCursor, PageSize, All }.ForEach(command.Options.Add);

    internal PaginationParameters GetParameters(ParseResult parseResult) =>
        new PaginationParameters
        {
            StartCursor = parseResult.GetValue(StartCursor),
            PageSize = parseResult.GetValue(PageSize),
        };

    internal bool IsAll(ParseResult parseResult) => parseResult.GetValue(All);
}
