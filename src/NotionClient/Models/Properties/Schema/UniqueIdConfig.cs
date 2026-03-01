// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Configuration for a Notion <c>unique_id</c> property, specifying the optional prefix
/// prepended to each auto-incremented number.
/// <see href="https://developers.notion.com/reference/property-object#unique-id"/>
/// </summary>
public sealed class UniqueIdConfig
{
    /// <summary>The optional string prefix prepended to each unique ID number (e.g., "TASK-").</summary>
    [JsonPropertyName("prefix")]
    public string? Prefix { get; init; }
}
