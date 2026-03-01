// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using DamianH.NotionClient.Endpoints;

namespace DamianH.NotionClient;

/// <summary>
/// Default <see cref="INotionClient"/> implementation that sends requests via
/// an <see cref="HttpClient"/> configured with a Notion API base address and
/// bearer-token authorization header.
/// </summary>
public sealed class NotionClient : INotionClient
{
    private readonly HttpClient _httpClient;

    private static readonly NotionJsonSerializerContext s_jsonContext = NotionJsonSerializerContext.Default;

    /// <summary>
    /// Creates a <see cref="NotionClient"/> backed by the provided <paramref name="httpClient"/>.
    /// The caller is responsible for configuring auth, base address, and timeout on the client.
    /// </summary>
    public NotionClient(HttpClient httpClient) => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    /// <inheritdoc />
    public IBlocksClient Blocks => _blocks ??= new BlocksClient(this);

    /// <inheritdoc />
    public IDatabasesClient Databases => _databases ??= new DatabasesClient(this);

    /// <inheritdoc />
    public IPagesClient Pages => _pages ??= new PagesClient(this);

    /// <inheritdoc />
    public IUsersClient Users => _users ??= new UsersClient(this);

    /// <inheritdoc />
    public ICommentsClient Comments => _comments ??= new CommentsClient(this);

    /// <inheritdoc />
    public ISearchClient Search => _search ??= new SearchClient(this);

    /// <inheritdoc />
    public IFileUploadsClient FileUploads => _fileUploads ??= new FileUploadsClient(this);

    /// <inheritdoc />
    public IDataSourcesClient DataSources => _dataSources ??= new DataSourcesClient(this);

    /// <inheritdoc />
    public IOAuthClient OAuth => _oauth ??= new OAuthClient(this);

    private BlocksClient? _blocks;
    private DatabasesClient? _databases;
    private PagesClient? _pages;
    private UsersClient? _users;
    private CommentsClient? _comments;
    private SearchClient? _search;
    private FileUploadsClient? _fileUploads;
    private DataSourcesClient? _dataSources;
    private OAuthClient? _oauth;

    /// <summary>
    /// Sends a request and deserializes the response body to <typeparamref name="TResponse"/>.
    /// </summary>
    internal async Task<TResponse> Send<TResponse>(
        HttpMethod method,
        string path,
        object? body = null,
        IDictionary<string, string?>? query = null,
        CancellationToken cancellationToken = default)
    {
        var url = BuildRelativeUrl(path, query);
        var request = new HttpRequestMessage(method, url);

        if (body is not null)
        {
            request.Content = new StringContent(
                JsonSerializer.Serialize(body, body.GetType(), s_jsonContext),
                Encoding.UTF8,
                "application/json");
        }

        var response = await _httpClient.SendAsync(request, cancellationToken);
        await EnsureSuccess(response, cancellationToken);

        return (await response.Content.ReadFromJsonAsync(GetRequiredTypeInfo<TResponse>(), cancellationToken))!;
    }

    /// <summary>
    /// Sends a request with Basic auth (for OAuth endpoints) and deserializes the response.
    /// </summary>
    internal async Task<TResponse> SendWithBasicAuth<TResponse>(
        HttpMethod method,
        string path,
        string clientId,
        string clientSecret,
        object? body = null,
        CancellationToken cancellationToken = default)
    {
        var url = BuildRelativeUrl(path, null);
        var request = new HttpRequestMessage(method, url);

        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

        if (body is not null)
        {
            request.Content = new StringContent(
                JsonSerializer.Serialize(body, body.GetType(), s_jsonContext),
                Encoding.UTF8,
                "application/json");
        }

        var response = await _httpClient.SendAsync(request, cancellationToken);
        await EnsureSuccess(response, cancellationToken);

        return (await response.Content.ReadFromJsonAsync(GetRequiredTypeInfo<TResponse>(), cancellationToken))!;
    }

    /// <summary>
    /// Sends a request with Basic auth (for OAuth endpoints) with no response deserialization.
    /// </summary>
    internal async Task SendNoResponse(
        HttpMethod method,
        string path,
        string clientId,
        string clientSecret,
        object? body = null,
        CancellationToken cancellationToken = default)
    {
        var url = BuildRelativeUrl(path, null);
        var request = new HttpRequestMessage(method, url);

        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

        if (body is not null)
        {
            request.Content = new StringContent(
                JsonSerializer.Serialize(body, body.GetType(), s_jsonContext),
                Encoding.UTF8,
                "application/json");
        }

        var response = await _httpClient.SendAsync(request, cancellationToken);
        await EnsureSuccess(response, cancellationToken);
    }

    /// <summary>
    /// Sends a multipart/form-data request and deserializes the response.
    /// </summary>
    internal async Task<TResponse> SendMultipart<TResponse>(
        string path,
        MultipartFormDataContent content,
        IDictionary<string, string?>? query = null,
        CancellationToken cancellationToken = default)
    {
        var url = BuildRelativeUrl(path, query);
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = content,
        };

        var response = await _httpClient.SendAsync(request, cancellationToken);
        await EnsureSuccess(response, cancellationToken);

        return (await response.Content.ReadFromJsonAsync(GetRequiredTypeInfo<TResponse>(), cancellationToken))!;
    }

    private static JsonTypeInfo<T> GetRequiredTypeInfo<T>() =>
        s_jsonContext.GetTypeInfo(typeof(T)) as JsonTypeInfo<T>
        ?? throw new InvalidOperationException($"Type '{typeof(T).FullName}' is not registered in {nameof(NotionJsonSerializerContext)}.");

    private static Uri BuildRelativeUrl(string path, IDictionary<string, string?>? query)
    {
        // Validate path — prevent traversal
        if (path.Contains(".."))
        {
            throw new ArgumentException($"Path \"{path}\" contains path traversal sequence.", nameof(path));
        }

        var relPath = path.TrimStart('/');

        if (query is not { Count: > 0 })
        {
            return new Uri(relPath, UriKind.Relative);
        }

        var qs = string.Join("&", query
            .Where(kv => kv.Value is not null)
            .Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value!)}"));
        relPath = $"{relPath}?{qs}";

        return new Uri(relPath, UriKind.Relative);
    }

    private static async Task EnsureSuccess(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        throw new NotionApiException(response.StatusCode, body);
    }
}
