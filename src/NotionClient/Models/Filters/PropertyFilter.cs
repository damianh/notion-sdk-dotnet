// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Filters;

/// <summary>A filter applied to a specific database property.</summary>
public sealed class PropertyFilter : Filter
{
    /// <summary>Gets the name or ID of the database property to filter on.</summary>
    [JsonPropertyName("property")]
    public required string Property { get; init; }

    // Each field below is mutually exclusive — only one is set per filter.

    /// <summary>Gets the text filter condition applied to a <c>title</c> property.</summary>
    [JsonPropertyName("title")]
    public TextFilterCondition? Title { get; init; }

    /// <summary>Gets the text filter condition applied to a <c>rich_text</c> property.</summary>
    [JsonPropertyName("rich_text")]
    public TextFilterCondition? RichText { get; init; }

    /// <summary>Gets the text filter condition applied to a <c>url</c> property.</summary>
    [JsonPropertyName("url")]
    public TextFilterCondition? Url { get; init; }

    /// <summary>Gets the text filter condition applied to an <c>email</c> property.</summary>
    [JsonPropertyName("email")]
    public TextFilterCondition? Email { get; init; }

    /// <summary>Gets the text filter condition applied to a <c>phone_number</c> property.</summary>
    [JsonPropertyName("phone_number")]
    public TextFilterCondition? PhoneNumber { get; init; }

    /// <summary>Gets the numeric filter condition applied to a <c>number</c> property.</summary>
    [JsonPropertyName("number")]
    public NumberFilterCondition? Number { get; init; }

    /// <summary>Gets the boolean filter condition applied to a <c>checkbox</c> property.</summary>
    [JsonPropertyName("checkbox")]
    public CheckboxFilterCondition? Checkbox { get; init; }

    /// <summary>Gets the filter condition applied to a <c>select</c> property.</summary>
    [JsonPropertyName("select")]
    public SelectFilterCondition? Select { get; init; }

    /// <summary>Gets the filter condition applied to a <c>multi_select</c> property.</summary>
    [JsonPropertyName("multi_select")]
    public MultiSelectFilterCondition? MultiSelect { get; init; }

    /// <summary>Gets the filter condition applied to a <c>status</c> property.</summary>
    [JsonPropertyName("status")]
    public StatusFilterCondition? Status { get; init; }

    /// <summary>Gets the date filter condition applied to a <c>date</c> property.</summary>
    [JsonPropertyName("date")]
    public DateFilterCondition? Date { get; init; }

    /// <summary>Gets the filter condition applied to a <c>people</c> property.</summary>
    [JsonPropertyName("people")]
    public PeopleFilterCondition? People { get; init; }

    /// <summary>Gets the filter condition applied to a <c>files</c> property.</summary>
    [JsonPropertyName("files")]
    public FilesFilterCondition? Files { get; init; }

    /// <summary>Gets the filter condition applied to a <c>relation</c> property.</summary>
    [JsonPropertyName("relation")]
    public RelationFilterCondition? Relation { get; init; }

    /// <summary>Gets the filter condition applied to a <c>formula</c> property.</summary>
    [JsonPropertyName("formula")]
    public FormulaFilterCondition? Formula { get; init; }

    /// <summary>Gets the filter condition applied to a <c>rollup</c> property.</summary>
    [JsonPropertyName("rollup")]
    public RollupFilterCondition? Rollup { get; init; }

    /// <summary>Gets the filter condition applied to a <c>unique_id</c> property.</summary>
    [JsonPropertyName("unique_id")]
    public UniqueIdFilterCondition? UniqueId { get; init; }
}
