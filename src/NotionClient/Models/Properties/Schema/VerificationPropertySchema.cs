// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>verification</c> property, which tracks whether a page has been
/// verified and by whom.
/// <see href="https://developers.notion.com/reference/property-object#verification"/>
/// </summary>
public sealed class VerificationPropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "verification";

    /// <summary>The verification configuration object (currently empty in the Notion API).</summary>
    [JsonPropertyName("verification")]
    public object? Verification { get; init; }
}
