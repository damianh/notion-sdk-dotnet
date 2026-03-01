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

internal static class CommentsCommands
{
    internal static Command Build(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var commentsCmd = new Command("comments", "Operations on Notion comments.");
        commentsCmd.Subcommands.Add(BuildCreate(tokenOption, verboseOption, noIndentOption));
        commentsCmd.Subcommands.Add(BuildList(tokenOption, verboseOption, noIndentOption));
        return commentsCmd;
    }

    private static Command BuildCreate(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var jsonOption = new Option<string?>("--json") { Description = "JSON body for CreateCommentRequest. Use @<path> to read from a file." };
        var cmd = new Command("create", "Create a new comment on a page or inside an existing discussion thread.") { jsonOption };
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var json = JsonInputHelper.Read(parseResult.GetValue(jsonOption));
                var request = JsonSerializer.Deserialize<CreateCommentRequest>(json, NotionJsonSerializerOptions.Default)
                    ?? throw new InvalidOperationException("Failed to deserialize CreateCommentRequest from JSON.");
                var result = await client.Comments.Create(request, ct);
                JsonOutputHelper.Write<Comment>(result, !parseResult.GetValue(noIndentOption));
                return 0;
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleException(ex, parseResult.GetValue(verboseOption));
            }
        });
        return cmd;
    }

    private static Command BuildList(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var blockIdArg = new Argument<string>("block-id") { Description = "The block or page identifier to list comments for." };
        var paging = new PaginationOptions();
        var cmd = new Command("list", "Return a paginated list of unresolved comments on a block or page.") { blockIdArg };
        paging.AddTo(cmd);
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var blockId = parseResult.GetValue(blockIdArg)!;
                var indent = !parseResult.GetValue(noIndentOption);

                if (paging.IsAll(parseResult))
                {
                    var pagination = paging.GetParameters(parseResult);
                    var items = await PaginationHelpers.CollectAll(
                        (cursor, c) => client.Comments.List(blockId,
                            new PaginationParameters { StartCursor = cursor, PageSize = pagination.PageSize }, c),
                        ct);
                    JsonOutputHelper.WriteList<Comment>(items, indent);
                }
                else
                {
                    var result = await client.Comments.List(blockId, paging.GetParameters(parseResult), ct);
                    JsonOutputHelper.Write<PaginatedList<Comment>>(result, indent);
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
}
