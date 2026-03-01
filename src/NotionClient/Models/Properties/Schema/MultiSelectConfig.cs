// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Configuration for a Notion <c>multi_select</c> property, containing the set of predefined options.
/// <see href="https://developers.notion.com/reference/property-object#multi-select"/>
/// </summary>
public sealed class MultiSelectConfig
{
    /// <summary>The list of predefined options available for selection in this property.</summary>
    [JsonPropertyName("options")]
    public IReadOnlyList<SelectOption> Options { get; init; } = [];
}
