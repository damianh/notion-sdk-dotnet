// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.CommandLine;
using DamianH.NotionClient.Helpers;
using DamianH.NotionClient.Models;
using DamianH.NotionClient.Models.Pagination;
using DamianH.NotionCli.Auth;
using DamianH.NotionCli.Infrastructure;

namespace DamianH.NotionCli.Commands;

internal static class UsersCommands
{
    internal static Command Build(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var usersCmd = new Command("users", "Operations on Notion users.");
        usersCmd.Subcommands.Add(BuildGetSelf(tokenOption, verboseOption, noIndentOption));
        usersCmd.Subcommands.Add(BuildGet(tokenOption, verboseOption, noIndentOption));
        usersCmd.Subcommands.Add(BuildList(tokenOption, verboseOption, noIndentOption));
        return usersCmd;
    }

    private static Command BuildGetSelf(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var cmd = new Command("get-self", "Retrieve the bot user associated with the current integration token.");
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var result = await client.Users.GetSelf(ct);
                JsonOutputHelper.Write<User>(result, !parseResult.GetValue(noIndentOption));
                return 0;
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleException(ex, parseResult.GetValue(verboseOption));
            }
        });
        return cmd;
    }

    private static Command BuildGet(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var userIdArg = new Argument<string>("user-id") { Description = "The user identifier." };
        var cmd = new Command("get", "Retrieve a user by their identifier.") { userIdArg };
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var result = await client.Users.Get(parseResult.GetValue(userIdArg)!, ct);
                JsonOutputHelper.Write<User>(result, !parseResult.GetValue(noIndentOption));
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
        var paging = new PaginationOptions();
        var cmd = new Command("list", "Return a paginated list of all users in the workspace.");
        paging.AddTo(cmd);
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var indent = !parseResult.GetValue(noIndentOption);

                if (paging.IsAll(parseResult))
                {
                    var pagination = paging.GetParameters(parseResult);
                    var items = await PaginationHelpers.CollectAll(
                        (cursor, c) => client.Users.List(
                            new PaginationParameters { StartCursor = cursor, PageSize = pagination.PageSize }, c),
                        ct);
                    JsonOutputHelper.WriteList<User>(items, indent);
                }
                else
                {
                    var result = await client.Users.List(paging.GetParameters(parseResult), ct);
                    JsonOutputHelper.Write<PaginatedList<User>>(result, indent);
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
