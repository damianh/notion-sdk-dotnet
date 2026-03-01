// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Verification details for a Notion <c>verification</c> property, recording the verification state,
/// who verified the page, and when.
/// </summary>
public sealed class VerificationInfo
{
    /// <summary>The current verification state (e.g., "verified" or "unverified").</summary>
    [JsonPropertyName("state")]
    public string State { get; init; } = null!;

    /// <summary>The user who verified this page, or <c>null</c> if not yet verified.</summary>
    [JsonPropertyName("verified_by")]
    public PartialUser? VerifiedBy { get; init; }

    /// <summary>The date and time the page was verified, or <c>null</c> if not yet verified.</summary>
    [JsonPropertyName("date")]
    public DateValue? Date { get; init; }
}
