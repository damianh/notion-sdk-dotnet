// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// A mention that embeds a date or date range inline in rich text.
/// </summary>
public sealed class DateMention : Mention
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string MentionType => "date";

    /// <summary>The date or date range embedded in the mention.</summary>
    [JsonPropertyName("date")]
    public required DateValue Date { get; init; }
}
