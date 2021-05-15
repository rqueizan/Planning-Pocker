using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Planning.Pocker.Api.NoAuth;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Planning.Pocker.Api.Test
{
    public class IntegrationTestServer : IDisposable
    {
        private readonly TestServer testServer;
        private readonly HttpClient client;

        public IntegrationTestServer()
        {
            var builder = new WebHostBuilder()
                .UseContentRoot(GetContentRoot())
                .UseConfiguration(GetConfigurationRoot())
                .UseEnvironment(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Cloud")
                .UseStartup<Startup>();
            //.UseApplicationInsights();

            testServer = new TestServer(builder);
            client = testServer.CreateClient();
            client.Timeout = TimeSpan.FromMinutes(20);
        }

        public async Task<Response<D>> GetAsync<D>(string uri)
            => await DecodeResponse<D>(await client.GetAsync(uri).ConfigureAwait(false)).ConfigureAwait(false);

        public async Task<Response<D>> PostAsync<D>(string uri, object @object)
            => await DecodeResponse<D>(await client.PostAsync(uri, CreateContent(@object)).ConfigureAwait(false));

        public async Task<Response<object>> PutAsync(string uri, object @object)
            => new Response<object>((await client.PutAsync(uri, CreateContent(@object)).ConfigureAwait(false)).StatusCode, default);

        public async Task<Response<D>> PutAsync<D>(string uri, object @object)
            => await DecodeResponse<D>(await client.PutAsync(uri, CreateContent(@object)).ConfigureAwait(false));

        public async Task DeleteAsync(string uri)
            => await client.DeleteAsync(uri).ConfigureAwait(false);

        public static HttpContent CreateContent<T>(T @object)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(@object, options);
            return new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        }

        public static async Task<Response<D>> DecodeResponse<D>(HttpResponseMessage httpResponseMessage)
        {
            var responseBody = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var @object = !string.IsNullOrWhiteSpace(responseBody) ? JsonSerializer.Deserialize<D>(responseBody, options) : default;
            return new Response<D>(httpResponseMessage.StatusCode, @object);
        }

        private static string GetContentRoot()
        {
            var testProjectPath = PlatformServices.Default.Application.ApplicationBasePath;
            var path = Path.Combine(testProjectPath, @"..\..\..\..\", "Planning.Pocker.Api.NoAuth");
            if (!Directory.Exists(path))
                path = path.Replace("\\", "/");
            return path;
        }

        public static IConfigurationRoot GetConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .SetBasePath(PlatformServices.Default.Application.ApplicationBasePath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Cloud"}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public void Dispose() => testServer.Dispose();
    }

    [CollectionDefinition("IntegrationTestServer")]
    public class DatabaseCollection : ICollectionFixture<IntegrationTestServer> { }
}
