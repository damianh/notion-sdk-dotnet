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

internal static class DatabasesCommands
{
    internal static Command Build(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var databasesCmd = new Command("databases", "Operations on Notion databases.");
        databasesCmd.Subcommands.Add(BuildGet(tokenOption, verboseOption, noIndentOption));
        databasesCmd.Subcommands.Add(BuildCreate(tokenOption, verboseOption, noIndentOption));
        databasesCmd.Subcommands.Add(BuildUpdate(tokenOption, verboseOption, noIndentOption));
        databasesCmd.Subcommands.Add(BuildQuery(tokenOption, verboseOption, noIndentOption));
        return databasesCmd;
    }

    private static Command BuildGet(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var dbIdArg = new Argument<string>("database-id") { Description = "The database identifier." };
        var cmd = new Command("get", "Retrieve a database by its identifier.") { dbIdArg };
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var result = await client.Databases.Get(parseResult.GetValue(dbIdArg)!, ct);
                JsonOutputHelper.Write<Database>(result, !parseResult.GetValue(noIndentOption));
                return 0;
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleException(ex, parseResult.GetValue(verboseOption));
            }
        });
        return cmd;
    }

    private static Command BuildCreate(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var jsonOption = new Option<string?>("--json") { Description = "JSON body for CreateDatabaseRequest. Use @<path> to read from a file." };
        var cmd = new Command("create", "Create a new database as a child of the specified parent page.") { jsonOption };
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var json = JsonInputHelper.Read(parseResult.GetValue(jsonOption));
                var request = JsonSerializer.Deserialize<CreateDatabaseRequest>(json, NotionJsonSerializerOptions.Default)
                    ?? throw new InvalidOperationException("Failed to deserialize CreateDatabaseRequest from JSON.");
                var result = await client.Databases.Create(request, ct);
                JsonOutputHelper.Write<Database>(result, !parseResult.GetValue(noIndentOption));
                return 0;
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleException(ex, parseResult.GetValue(verboseOption));
            }
        });
        return cmd;
    }

    private static Command BuildUpdate(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var dbIdArg = new Argument<string>("database-id") { Description = "The database identifier." };
        var jsonOption = new Option<string?>("--json") { Description = "JSON body for UpdateDatabaseRequest. Use @<path> to read from a file." };
        var cmd = new Command("update", "Update a database's title, description, or property schema.") { dbIdArg, jsonOption };
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var json = JsonInputHelper.Read(parseResult.GetValue(jsonOption));
                var request = JsonSerializer.Deserialize<UpdateDatabaseRequest>(json, NotionJsonSerializerOptions.Default)
                    ?? throw new InvalidOperationException("Failed to deserialize UpdateDatabaseRequest from JSON.");
                var result = await client.Databases.Update(parseResult.GetValue(dbIdArg)!, request, ct);
                JsonOutputHelper.Write<Database>(result, !parseResult.GetValue(noIndentOption));
                return 0;
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleException(ex, parseResult.GetValue(verboseOption));
            }
        });
        return cmd;
    }

    private static Command BuildQuery(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var dbIdArg = new Argument<string>("database-id") { Description = "The database identifier." };
        var jsonOption = new Option<string?>("--json") { Description = "Optional JSON body for QueryDatabaseRequest. Use @<path> to read from a file." };
        var paging = new PaginationOptions();
        var cmd = new Command("query", "Query a database and return matching pages.") { dbIdArg, jsonOption };
        paging.AddTo(cmd);
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var dbId = parseResult.GetValue(dbIdArg)!;
                var indent = !parseResult.GetValue(noIndentOption);
                var rawJson = JsonInputHelper.ReadOptional(parseResult.GetValue(jsonOption));

                if (paging.IsAll(parseResult))
                {
                    var pagination = paging.GetParameters(parseResult);
                    var items = await PaginationHelpers.CollectAll(
                        (cursor, c) =>
                        {
                            var req = rawJson is not null
                                ? JsonSerializer.Deserialize<QueryDatabaseRequest>(rawJson, NotionJsonSerializerOptions.Default)
                                : null;
                            // Merge pagination into the request
                            req = MergePageCursor(req, cursor, pagination.PageSize);
                            return client.Databases.Query(dbId, req, c);
                        },
                        ct);
                    JsonOutputHelper.WriteList<Page>(items, indent);
                }
                else
                {
                    var request = rawJson is not null
                        ? JsonSerializer.Deserialize<QueryDatabaseRequest>(rawJson, NotionJsonSerializerOptions.Default)
                        : null;
                    request = MergePageCursor(request, paging.GetParameters(parseResult).StartCursor, paging.GetParameters(parseResult).PageSize);
                    var result = await client.Databases.Query(dbId, request, ct);
                    JsonOutputHelper.Write<PaginatedList<Page>>(result, indent);
                }
                return 0;
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleException(ex, parseResult.GetValue(verboseOption));
            }
        });
        return cmd;
    }

    private static QueryDatabaseRequest? MergePageCursor(QueryDatabaseRequest? request, string? cursor, int? pageSize)
    {
        if (cursor is null && pageSize is null)
        {
            return request;
        }
        return new QueryDatabaseRequest
        {
            Filter = request?.Filter,
            Sorts = request?.Sorts,
            StartCursor = cursor ?? request?.StartCursor,
            PageSize = pageSize ?? request?.PageSize,
        };
    }
}
