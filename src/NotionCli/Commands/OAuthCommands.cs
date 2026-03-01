// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.CommandLine;
using DamianH.NotionClient.Models.Requests;
using DamianH.NotionCli.Infrastructure;

namespace DamianH.NotionCli.Commands;

internal static class OAuthCommands
{
    internal static Command Build(Option<bool> noIndentOption)
    {
        var oauthCmd = new Command("oauth", "Notion OAuth 2.0 token management.");
        oauthCmd.Subcommands.Add(BuildExchangeToken(noIndentOption));
        oauthCmd.Subcommands.Add(BuildRevoke(noIndentOption));
        oauthCmd.Subcommands.Add(BuildIntrospect(noIndentOption));
        return oauthCmd;
    }

    private static Command BuildExchangeToken(Option<bool> noIndentOption)
    {
        var clientIdOption = new Option<string>("--client-id") { Description = "The OAuth client ID.", Required = true };
        var clientSecretOption = new Option<string>("--client-secret") { Description = "The OAuth client secret.", Required = true };
        var codeOption = new Option<string>("--code") { Description = "The temporary authorization code.", Required = true };
        var redirectUriOption = new Option<string?>("--redirect-uri") { Description = "The redirect URI used in the authorization request." };
        var verboseOption = new Option<bool>("--verbose") { Description = "Show diagnostic output on stderr." };

        var cmd = new Command("exchange-token", "Exchange a temporary authorization code for an access token.")
        {
            clientIdOption, clientSecretOption, codeOption, redirectUriOption, verboseOption,
        };
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var client = NotionClientFactory.CreateNoAuth();
                var request = new ExchangeTokenRequest
                {
                    Code = parseResult.GetValue(codeOption)!,
                    RedirectUri = parseResult.GetValue(redirectUriOption),
                };
                var result = await client.OAuth.ExchangeToken(
                    parseResult.GetValue(clientIdOption)!,
                    parseResult.GetValue(clientSecretOption)!,
                    request,
                    ct);
                JsonOutputHelper.Write<ExchangeTokenResponse>(result, !parseResult.GetValue(noIndentOption));
                return 0;
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleException(ex, parseResult.GetValue(verboseOption));
            }
        });
        return cmd;
    }

    private static Command BuildRevoke(Option<bool> noIndentOption)
    {
        var clientIdOption = new Option<string>("--client-id") { Description = "The OAuth client ID.", Required = true };
        var clientSecretOption = new Option<string>("--client-secret") { Description = "The OAuth client secret.", Required = true };
        var tokenOption = new Option<string>("--token") { Description = "The access token to revoke.", Required = true };
        var verboseOption = new Option<bool>("--verbose") { Description = "Show diagnostic output on stderr." };

        var cmd = new Command("revoke", "Revoke a previously issued access token.")
        {
            clientIdOption, clientSecretOption, tokenOption, verboseOption,
        };
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var client = NotionClientFactory.CreateNoAuth();
                var request = new RevokeTokenRequest
                {
                    Token = parseResult.GetValue(tokenOption)!,
                };
                await client.OAuth.Revoke(
                    parseResult.GetValue(clientIdOption)!,
                    parseResult.GetValue(clientSecretOption)!,
                    request,
                    ct);
                Console.WriteLine("{}");
                return 0;
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleException(ex, parseResult.GetValue(verboseOption));
            }
        });
        return cmd;
    }

    private static Command BuildIntrospect(Option<bool> noIndentOption)
    {
        var clientIdOption = new Option<string>("--client-id") { Description = "The OAuth client ID.", Required = true };
        var clientSecretOption = new Option<string>("--client-secret") { Description = "The OAuth client secret.", Required = true };
        var tokenOption = new Option<string>("--token") { Description = "The access token to introspect.", Required = true };
        var verboseOption = new Option<bool>("--verbose") { Description = "Show diagnostic output on stderr." };

        var cmd = new Command("introspect", "Introspect an access token to check its validity and metadata.")
        {
            clientIdOption, clientSecretOption, tokenOption, verboseOption,
        };
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var client = NotionClientFactory.CreateNoAuth();
                var request = new IntrospectTokenRequest
                {
                    Token = parseResult.GetValue(tokenOption)!,
                };
                var result = await client.OAuth.Introspect(
                    parseResult.GetValue(clientIdOption)!,
                    parseResult.GetValue(clientSecretOption)!,
                    request,
                    ct);
                JsonOutputHelper.Write<IntrospectTokenResponse>(result, !parseResult.GetValue(noIndentOption));
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
