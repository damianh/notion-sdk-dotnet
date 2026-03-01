// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json;

namespace DamianH.NotionCli.Auth;

internal static class TokenResolver
{
    private const string EnvVar = "NOTION_TOKEN";

    internal static string Resolve(string? tokenOption) =>
        Resolve(tokenOption, false);

    internal static string Resolve(string? tokenOption, bool verbose)
    {
        if (tokenOption is not null)
        {
            if (verbose)
            {
                Console.Error.WriteLine("notion: using token from --token option");
            }
            return tokenOption;
        }

        var envToken = Environment.GetEnvironmentVariable(EnvVar);
        if (envToken is not null)
        {
            if (verbose)
            {
                Console.Error.WriteLine($"notion: using token from {EnvVar} environment variable");
            }
            return envToken;
        }

        var configPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".notion",
            "config.json");

        if (File.Exists(configPath))
        {
            var configToken = ReadTokenFromConfig(configPath);
            if (configToken is not null)
            {
                if (verbose)
                {
                    Console.Error.WriteLine($"notion: using token from {configPath}");
                }
                return configToken;
            }
        }

        throw new InvalidOperationException(
            "No Notion API token found. Provide one via --token, the NOTION_TOKEN environment variable, " +
            $"or a config file at {configPath} with {{\"token\": \"...\"}}");
    }

    private static string? ReadTokenFromConfig(string configPath)
    {
        try
        {
            var json = File.ReadAllText(configPath);
            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("token", out var tokenEl))
            {
                return tokenEl.GetString();
            }
        }
        catch
        {
            // Ignore malformed config files.
        }
        return null;
    }
}
