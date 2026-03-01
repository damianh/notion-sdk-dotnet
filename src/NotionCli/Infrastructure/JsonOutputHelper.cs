// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text;
using System.Text.Json;
using DamianH.NotionClient;

namespace DamianH.NotionCli.Infrastructure;

internal static class JsonOutputHelper
{
    private static readonly JsonSerializerOptions s_indented =
        new(NotionJsonSerializerOptions.Default) { WriteIndented = true };

    internal static void Write<T>(T value, bool indent = true)
    {
        Console.OutputEncoding = Encoding.UTF8;
        var options = indent ? s_indented : NotionJsonSerializerOptions.Default;
        var json = JsonSerializer.Serialize(value, typeof(T), options);
        Console.WriteLine(json);
    }

    internal static void WriteList<T>(IReadOnlyList<T> items, bool indent = true)
    {
        Console.OutputEncoding = Encoding.UTF8;
        var options = indent ? s_indented : NotionJsonSerializerOptions.Default;
        var json = JsonSerializer.Serialize(items, typeof(IReadOnlyList<T>), options);
        Console.WriteLine(json);
    }
}
