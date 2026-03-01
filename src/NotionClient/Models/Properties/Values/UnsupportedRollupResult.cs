// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// A rollup result indicating that the rollup type is not supported by this version of the API client.
/// </summary>
public sealed class UnsupportedRollupResult : RollupResult
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string RollupType => "unsupported";
}
