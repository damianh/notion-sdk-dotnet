// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json;
using DamianH.NotionClient;

namespace DamianH.NotionCli.Infrastructure;

internal static class ErrorHandler
{
    internal static int HandleException(Exception ex, bool verbose)
    {
        switch (ex)
        {
            case NotionApiException apiEx:
                Console.Error.WriteLine($"Error {(int)apiEx.StatusCode}: {apiEx.Message}");
                if (apiEx.ErrorCode is not null)
                {
                    Console.Error.WriteLine($"Code: {apiEx.ErrorCode}");
                }
                if (apiEx.RequestId is not null)
                {
                    Console.Error.WriteLine($"Request-ID: {apiEx.RequestId}");
                }
                if (verbose)
                {
                    Console.Error.WriteLine($"Raw body: {apiEx.RawBody}");
                }
                return 1;

            case InvalidOperationException opEx:
                Console.Error.WriteLine($"Error: {opEx.Message}");
                return 1;

            case JsonException jsonEx:
                Console.Error.WriteLine($"Invalid JSON: {jsonEx.Message}");
                return 1;

            case HttpRequestException httpEx:
                Console.Error.WriteLine($"Network error: {httpEx.Message}");
                return 1;

            default:
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                if (verbose)
                {
                    Console.Error.WriteLine(ex.ToString());
                }
                return 1;
        }
    }
}
