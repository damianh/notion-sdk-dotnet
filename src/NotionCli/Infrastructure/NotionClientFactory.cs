// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Net.Http.Headers;
using System.Reflection;
using DamianH.NotionClient;

namespace DamianH.NotionCli.Infrastructure;

internal static class NotionClientFactory
{
    private static readonly string s_userAgent = BuildUserAgent();

    internal static INotionClient Create(string token)
    {
        var httpClient = BuildHttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return new DamianH.NotionClient.NotionClient(httpClient);
    }

    internal static INotionClient CreateNoAuth()
    {
        var httpClient = BuildHttpClient();
        return new DamianH.NotionClient.NotionClient(httpClient);
    }

    private static HttpClient BuildHttpClient()
    {
        var options = new NotionClientOptions();
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.notion.com/v1/"),
        };
        httpClient.DefaultRequestHeaders.Add("Notion-Version", options.NotionVersion);
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(s_userAgent);
        return httpClient;
    }

    private static string BuildUserAgent()
    {
        var version = typeof(NotionClientFactory).Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion ?? "0.0.0";
        return $"DamianH.NotionCli/{version}";
    }
}
