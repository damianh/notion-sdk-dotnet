// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models;

/// <summary>
/// A Notion person user (a real human account), containing the <see cref="Person"/> details such as email.
/// </summary>
public sealed class PersonUser : User
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string UserType => "person";

    /// <summary>Gets person-specific information such as the user's email address.</summary>
    [JsonPropertyName("person")]
    public PersonInfo? Person { get; init; }
}
