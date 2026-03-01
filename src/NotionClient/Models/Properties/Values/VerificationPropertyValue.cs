// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>verification</c> property, indicating whether the page content
/// has been verified and the associated verification details.
/// <see href="https://developers.notion.com/reference/property-value-object#verification-property-values"/>
/// </summary>
public sealed class VerificationPropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "verification";

    /// <summary>The verification state and metadata for this page.</summary>
    [JsonPropertyName("verification")]
    public VerificationInfo Verification { get; init; } = null!;
}
