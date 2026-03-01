// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionCli;

// CLI-specific JSON source-gen context.
// Currently used only for AOT-safe serialization of JsonElement arrays (list output).
[JsonSerializable(typeof(System.Text.Json.JsonElement))]
[JsonSerializable(typeof(System.Text.Json.JsonElement[]))]
internal sealed partial class CliJsonContext : JsonSerializerContext;
