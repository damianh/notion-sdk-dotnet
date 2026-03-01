// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

namespace DamianH.NotionClient;

/// <summary>
/// Configuration options for the Notion API client.
/// </summary>
public sealed class NotionClientOptions
{
    /// <summary>
    /// Notion API version header. Defaults to 2025-09-03.
    /// </summary>
    public string NotionVersion { get; set; } = "2025-09-03";
}
