// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DamianH.NotionClient;

public static class ServiceCollectionExtensions
{
    private static readonly string UserAgent = "notion-sdk-dotnet/"
        + typeof(ServiceCollectionExtensions).Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion ?? "0.0.0";

    /// <summary>
    /// Registers <see cref="INotionClient"/> in the DI container using the typed-client pattern.
    /// Returns an <see cref="IHttpClientBuilder"/> so callers can chain further configuration
    /// (e.g. <c>.ConfigureHttpClient(c => { c.BaseAddress = ...; c.DefaultRequestHeaders.Authorization = ...; })</c>).
    /// </summary>
    public static IHttpClientBuilder AddNotionClient(
        this IServiceCollection services,
        Action<NotionClientOptions>? configure = null)
    {
        if (configure is not null)
        {
            services.Configure(configure);
        }

        return services
            .AddHttpClient<INotionClient, NotionClient>((serviceProvider, httpClient) =>
            {
                var options = serviceProvider
                    .GetRequiredService<IOptions<NotionClientOptions>>()
                    .Value;

                httpClient.DefaultRequestHeaders.Add("Notion-Version", options.NotionVersion);
                httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            });
    }
}
