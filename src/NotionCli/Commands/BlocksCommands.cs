// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.CommandLine;
using System.Text.Json;
using DamianH.NotionClient;
using DamianH.NotionClient.Helpers;
using DamianH.NotionClient.Models.Blocks;
using DamianH.NotionClient.Models.Pagination;
using DamianH.NotionClient.Models.Requests;
using DamianH.NotionCli.Auth;
using DamianH.NotionCli.Infrastructure;

namespace DamianH.NotionCli.Commands;

internal static class BlocksCommands
{
    internal static Command Build(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var blocksCmd = new Command("blocks", "Operations on Notion blocks.");
        blocksCmd.Subcommands.Add(BuildGet(tokenOption, verboseOption, noIndentOption));
        blocksCmd.Subcommands.Add(BuildUpdate(tokenOption, verboseOption, noIndentOption));
        blocksCmd.Subcommands.Add(BuildDelete(tokenOption, verboseOption, noIndentOption));
        blocksCmd.Subcommands.Add(BuildListChildren(tokenOption, verboseOption, noIndentOption));
        blocksCmd.Subcommands.Add(BuildAppendChildren(tokenOption, verboseOption, noIndentOption));
        return blocksCmd;
    }

    private static Command BuildGet(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var blockIdArg = new Argument<string>("block-id") { Description = "The block identifier." };
        var cmd = new Command("get", "Retrieve a block by its identifier.") { blockIdArg };
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var result = await client.Blocks.Get(parseResult.GetValue(blockIdArg)!, ct);
                JsonOutputHelper.Write<Block>(result, !parseResult.GetValue(noIndentOption));
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
        var blockIdArg = new Argument<string>("block-id") { Description = "The block identifier." };
        var jsonOption = new Option<string?>("--json") { Description = "JSON body representing the Block to update. Use @<path> to read from a file." };
        var cmd = new Command("update", "Update the content or appearance of an existing block.") { blockIdArg, jsonOption };
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var json = JsonInputHelper.Read(parseResult.GetValue(jsonOption));
                var block = JsonSerializer.Deserialize<Block>(json, NotionJsonSerializerOptions.Default)
                    ?? throw new InvalidOperationException("Failed to deserialize Block from JSON.");
                var result = await client.Blocks.Update(parseResult.GetValue(blockIdArg)!, block, ct);
                JsonOutputHelper.Write<Block>(result, !parseResult.GetValue(noIndentOption));
                return 0;
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleException(ex, parseResult.GetValue(verboseOption));
            }
        });
        return cmd;
    }

    private static Command BuildDelete(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var blockIdArg = new Argument<string>("block-id") { Description = "The block identifier." };
        var cmd = new Command("delete", "Archive (soft-delete) a block and its children.") { blockIdArg };
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var result = await client.Blocks.Delete(parseResult.GetValue(blockIdArg)!, ct);
                JsonOutputHelper.Write<Block>(result, !parseResult.GetValue(noIndentOption));
                return 0;
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleException(ex, parseResult.GetValue(verboseOption));
            }
        });
        return cmd;
    }

    private static Command BuildListChildren(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var blockIdArg = new Argument<string>("block-id") { Description = "The block identifier." };
        var paging = new PaginationOptions();
        var cmd = new Command("list-children", "Return a paginated list of child blocks.") { blockIdArg };
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
                        (cursor, c) => client.Blocks.ListChildren(blockId,
                            new PaginationParameters { StartCursor = cursor, PageSize = pagination.PageSize }, c),
                        ct);
                    JsonOutputHelper.WriteList<Block>(items, indent);
                }
                else
                {
                    var result = await client.Blocks.ListChildren(blockId, paging.GetParameters(parseResult), ct);
                    JsonOutputHelper.Write<PaginatedList<Block>>(result, indent);
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

    private static Command BuildAppendChildren(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var blockIdArg = new Argument<string>("block-id") { Description = "The block identifier." };
        var jsonOption = new Option<string?>("--json") { Description = "JSON body for AppendBlockChildrenRequest. Use @<path> to read from a file." };
        var cmd = new Command("append-children", "Append new child blocks to the specified parent block or page.") { blockIdArg, jsonOption };
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var json = JsonInputHelper.Read(parseResult.GetValue(jsonOption));
                var request = JsonSerializer.Deserialize<AppendBlockChildrenRequest>(json, NotionJsonSerializerOptions.Default)
                    ?? throw new InvalidOperationException("Failed to deserialize AppendBlockChildrenRequest from JSON.");
                var result = await client.Blocks.AppendChildren(parseResult.GetValue(blockIdArg)!, request, ct);
                JsonOutputHelper.Write<PaginatedList<Block>>(result, !parseResult.GetValue(noIndentOption));
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
