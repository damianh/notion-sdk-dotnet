// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.RegularExpressions;

namespace DamianH.NotionClient.Helpers;

/// <summary>
/// Helpers for extracting Notion UUIDs from URLs or raw ID strings.
/// Mirrors the JS SDK's <c>extractNotionId</c>, <c>extractPageId</c>, etc.
/// </summary>
public static class IdHelpers
{
    // UUID v4 pattern: 32 hex chars, optionally separated by hyphens
    private static readonly Regex UuidPattern =
        new(@"([0-9a-f]{8})-?([0-9a-f]{4})-?([0-9a-f]{4})-?([0-9a-f]{4})-?([0-9a-f]{12})",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

    /// <summary>
    /// Extracts a Notion UUID from a URL or raw ID string.
    /// Supports:
    /// <list type="bullet">
    ///   <item>Bare UUID: <c>9a2e7a04-9b90-4f67-8f5b-12d4ad3cc94f</c></item>
    ///   <item>Unhyphenated: <c>9a2e7a049b904f678f5b12d4ad3cc94f</c></item>
    ///   <item>Page URL: <c>https://notion.so/workspace/My-Page-9a2e7a049b904f678f5b12d4ad3cc94f</c></item>
    ///   <item>Short URL: <c>https://notion.so/9a2e7a049b904f678f5b12d4ad3cc94f</c></item>
    /// </list>
    /// </summary>
    /// <param name="urlOrId">A Notion URL or UUID string (with or without hyphens).</param>
    /// <returns>A normalized UUID string with hyphens (e.g., <c>9a2e7a04-9b90-4f67-8f5b-12d4ad3cc94f</c>).</returns>
    /// <exception cref="ArgumentException">Thrown when no valid UUID is found.</exception>
    public static string ExtractNotionId(string urlOrId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(urlOrId);

        var match = UuidPattern.Match(urlOrId);
        if (!match.Success)
        {
            throw new ArgumentException($"Could not extract a Notion UUID from: {urlOrId}", nameof(urlOrId));
        }

        // Normalize to hyphenated UUID
        return $"{match.Groups[1].Value}-{match.Groups[2].Value}-{match.Groups[3].Value}-{match.Groups[4].Value}-{match.Groups[5].Value}".ToLowerInvariant();
    }

    /// <summary>
    /// Extracts a page ID from a Notion URL or raw ID string.
    /// </summary>
    public static string ExtractPageId(string urlOrId) => ExtractNotionId(urlOrId);

    /// <summary>
    /// Extracts a database ID from a Notion URL or raw ID string.
    /// </summary>
    public static string ExtractDatabaseId(string urlOrId) => ExtractNotionId(urlOrId);

    /// <summary>
    /// Extracts a block ID from a Notion URL or raw ID string.
    /// </summary>
    public static string ExtractBlockId(string urlOrId) => ExtractNotionId(urlOrId);

    /// <summary>
    /// Attempts to extract a Notion UUID without throwing.
    /// </summary>
    /// <returns><c>true</c> if extraction succeeded; <c>false</c> otherwise.</returns>
    public static bool TryExtractNotionId(string urlOrId, out string? id)
    {
        id = null;
        if (string.IsNullOrWhiteSpace(urlOrId))
        {
            return false;
        }

        var match = UuidPattern.Match(urlOrId);
        if (!match.Success)
        {
            return false;
        }

        id = $"{match.Groups[1].Value}-{match.Groups[2].Value}-{match.Groups[3].Value}-{match.Groups[4].Value}-{match.Groups[5].Value}".ToLowerInvariant();
        return true;
    }
}
