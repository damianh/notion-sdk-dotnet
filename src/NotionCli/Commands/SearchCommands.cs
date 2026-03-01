// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.CommandLine;
using System.Text.Json;
using DamianH.NotionClient;
using DamianH.NotionClient.Helpers;
using DamianH.NotionClient.Models;
using DamianH.NotionClient.Models.Pagination;
using DamianH.NotionClient.Models.Requests;
using DamianH.NotionCli.Auth;
using DamianH.NotionCli.Infrastructure;

namespace DamianH.NotionCli.Commands;

internal static class SearchCommands
{
    internal static Command Build(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var queryOption = new Option<string?>("--query") { Description = "Text to search for across page and database titles." };
        var filterOption = new Option<string?>("--filter") { Description = "Restrict results to 'page' or 'database'." };
        var sortOption = new Option<string?>("--sort") { Description = "Sort direction: 'ascending' or 'descending'." };
        var jsonOption = new Option<string?>("--json") { Description = "Full SearchRequest JSON body (overrides individual options). Use @<path> to read from a file." };
        var paging = new PaginationOptions();

        var searchCmd = new Command("search", "Search pages and databases by title.")
        {
            queryOption, filterOption, sortOption, jsonOption,
        };
        paging.AddTo(searchCmd);

        searchCmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var indent = !parseResult.GetValue(noIndentOption);

                SearchRequest? request;
                var rawJson = JsonInputHelper.ReadOptional(parseResult.GetValue(jsonOption));

                if (rawJson is not null)
                {
                    request = JsonSerializer.Deserialize<SearchRequest>(rawJson, NotionJsonSerializerOptions.Default);
                }
                else
                {
                    var query = parseResult.GetValue(queryOption);
                    var filter = parseResult.GetValue(filterOption);
                    var sort = parseResult.GetValue(sortOption);
                    var pagination = paging.GetParameters(parseResult);

                    request = (query is not null || filter is not null || sort is not null || pagination.StartCursor is not null || pagination.PageSize is not null)
                        ? new SearchRequest
                        {
                            Query = query,
                            Filter = filter is not null ? new SearchFilter { Value = filter } : null,
                            Sort = sort is not null ? new SearchSort { Direction = sort } : null,
                            StartCursor = pagination.StartCursor,
                            PageSize = pagination.PageSize,
                        }
                        : null;
                }

                if (paging.IsAll(parseResult))
                {
                    var pagination = paging.GetParameters(parseResult);
                    var items = await PaginationHelpers.CollectAll(
                        (cursor, c) =>
                        {
                            var pageRequest = new SearchRequest
                            {
                                Query = request?.Query,
                                Filter = request?.Filter,
                                Sort = request?.Sort,
                                StartCursor = cursor,
                                PageSize = pagination.PageSize ?? request?.PageSize,
                            };
                            return client.Search.Search(pageRequest, c);
                        },
                        ct);
                    JsonOutputHelper.WriteList<SearchResult>(items, indent);
                }
                else
                {
                    var result = await client.Search.Search(request, ct);
                    JsonOutputHelper.Write<PaginatedList<SearchResult>>(result, indent);
                }
                return 0;
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleException(ex, parseResult.GetValue(verboseOption));
            }
        });

        return searchCmd;
    }
}
