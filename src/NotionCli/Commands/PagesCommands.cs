// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.CommandLine;
using System.Text.Json;
using DamianH.NotionClient;
using DamianH.NotionClient.Models;
using DamianH.NotionClient.Models.Properties.Values;
using DamianH.NotionClient.Models.Requests;
using DamianH.NotionCli.Auth;
using DamianH.NotionCli.Infrastructure;

namespace DamianH.NotionCli.Commands;

internal static class PagesCommands
{
    internal static Command Build(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var pagesCmd = new Command("pages", "Operations on Notion pages.");
        pagesCmd.Subcommands.Add(BuildGet(tokenOption, verboseOption, noIndentOption));
        pagesCmd.Subcommands.Add(BuildCreate(tokenOption, verboseOption, noIndentOption));
        pagesCmd.Subcommands.Add(BuildUpdate(tokenOption, verboseOption, noIndentOption));
        pagesCmd.Subcommands.Add(BuildGetProperty(tokenOption, verboseOption, noIndentOption));
        return pagesCmd;
    }

    private static Command BuildGet(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var pageIdArg = new Argument<string>("page-id") { Description = "The page identifier." };
        var cmd = new Command("get", "Retrieve a page by its identifier.") { pageIdArg };
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var result = await client.Pages.Get(parseResult.GetValue(pageIdArg)!, ct);
                JsonOutputHelper.Write<Page>(result, !parseResult.GetValue(noIndentOption));
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
        var jsonOption = new Option<string?>("--json") { Description = "JSON body for CreatePageRequest. Use @<path> to read from a file." };
        var cmd = new Command("create", "Create a new page as a child of the specified parent.") { jsonOption };
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var json = JsonInputHelper.Read(parseResult.GetValue(jsonOption));
                var request = JsonSerializer.Deserialize<CreatePageRequest>(json, NotionJsonSerializerOptions.Default)
                    ?? throw new InvalidOperationException("Failed to deserialize CreatePageRequest from JSON.");
                var result = await client.Pages.Create(request, ct);
                JsonOutputHelper.Write<Page>(result, !parseResult.GetValue(noIndentOption));
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
        var pageIdArg = new Argument<string>("page-id") { Description = "The page identifier." };
        var jsonOption = new Option<string?>("--json") { Description = "JSON body for UpdatePageRequest. Use @<path> to read from a file." };
        var cmd = new Command("update", "Update page properties, cover, icon, or archive status.") { pageIdArg, jsonOption };
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var json = JsonInputHelper.Read(parseResult.GetValue(jsonOption));
                var request = JsonSerializer.Deserialize<UpdatePageRequest>(json, NotionJsonSerializerOptions.Default)
                    ?? throw new InvalidOperationException("Failed to deserialize UpdatePageRequest from JSON.");
                var result = await client.Pages.Update(parseResult.GetValue(pageIdArg)!, request, ct);
                JsonOutputHelper.Write<Page>(result, !parseResult.GetValue(noIndentOption));
                return 0;
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleException(ex, parseResult.GetValue(verboseOption));
            }
        });
        return cmd;
    }

    private static Command BuildGetProperty(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var pageIdArg = new Argument<string>("page-id") { Description = "The page identifier." };
        var propertyIdArg = new Argument<string>("property-id") { Description = "The property identifier." };
        var paging = new PaginationOptions();
        var cmd = new Command("get-property", "Retrieve a single page property value.") { pageIdArg, propertyIdArg };
        paging.AddTo(cmd);
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var result = await client.Pages.GetProperty(
                    parseResult.GetValue(pageIdArg)!,
                    parseResult.GetValue(propertyIdArg)!,
                    paging.GetParameters(parseResult),
                    ct);
                JsonOutputHelper.Write<PropertyValue>(result, !parseResult.GetValue(noIndentOption));
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
