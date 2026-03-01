// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Configuration for a Notion <c>formula</c> property, defining the formula expression to evaluate.
/// <see href="https://developers.notion.com/reference/property-object#formula"/>
/// </summary>
public sealed class FormulaConfig
{
    /// <summary>
    /// Format of the number, e.g. "number", "number_with_commas", "percent",
    /// "dollar", "euro", "pound", etc.
    /// </summary>
    [JsonPropertyName("expression")]
    public string Expression { get; init; } = null!;
}
