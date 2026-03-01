// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

namespace DamianH.NotionCli.Infrastructure;

internal static class JsonInputHelper
{
    /// <summary>
    /// Reads a JSON string from:
    /// 1. <paramref name="jsonArg"/> directly (if not starting with '@')
    /// 2. A file path if <paramref name="jsonArg"/> starts with '@'
    /// 3. Stdin if <paramref name="jsonArg"/> is null and stdin is redirected
    /// Throws <see cref="InvalidOperationException"/> if no input is available.
    /// </summary>
    internal static string Read(string? jsonArg)
    {
        if (jsonArg is not null)
        {
            if (jsonArg.StartsWith('@'))
            {
                var path = jsonArg[1..];
                if (!File.Exists(path))
                {
                    throw new InvalidOperationException($"JSON file not found: {path}");
                }
                return File.ReadAllText(path);
            }
            return jsonArg;
        }

        if (Console.IsInputRedirected)
        {
            return Console.In.ReadToEnd();
        }

        throw new InvalidOperationException(
            "No JSON input provided. Use --json <value>, --json @<file>, or pipe JSON to stdin.");
    }

    /// <summary>
    /// Reads optional JSON, returning null if none is available.
    /// Only reads stdin if it is redirected (non-interactive).
    /// </summary>
    internal static string? ReadOptional(string? jsonArg)
    {
        if (jsonArg is not null)
        {
            if (jsonArg.StartsWith('@'))
            {
                var path = jsonArg[1..];
                if (!File.Exists(path))
                {
                    throw new InvalidOperationException($"JSON file not found: {path}");
                }
                return File.ReadAllText(path);
            }
            return jsonArg;
        }

        if (Console.IsInputRedirected)
        {
            var content = Console.In.ReadToEnd();
            return string.IsNullOrWhiteSpace(content) ? null : content;
        }

        return null;
    }
}
