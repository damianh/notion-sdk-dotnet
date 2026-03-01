// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Properties.Values;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Configuration for a Notion <c>status</c> property, containing the available status options
/// and their groupings.
/// <see href="https://developers.notion.com/reference/property-object#status"/>
/// </summary>
public sealed class StatusConfig
{
    /// <summary>The individual status options (e.g., "Not started", "In progress", "Done").</summary>
    [JsonPropertyName("options")]
    public IReadOnlyList<StatusOption> Options { get; init; } = [];

    /// <summary>Named groups that categorize the status options (e.g., "To-do", "In progress", "Complete").</summary>
    [JsonPropertyName("groups")]
    public IReadOnlyList<StatusGroup> Groups { get; init; } = [];
}
