// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>status</c> property, which represents a workflow stage
/// chosen from a structured set of grouped options.
/// <see href="https://developers.notion.com/reference/property-object#status"/>
/// </summary>
public sealed class StatusPropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "status";

    /// <summary>The status configuration containing available options and their groups.</summary>
    [JsonPropertyName("status")]
    public StatusConfig? Status { get; init; }
}
