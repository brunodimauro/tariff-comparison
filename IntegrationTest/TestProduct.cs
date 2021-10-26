using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TariffComparison;
using Xunit;

namespace IntegrationTest
{
    public class TestProduct
    {
        private readonly HttpClient _client;

        public TestProduct()
        {
            var server = new TestServer(new WebHostBuilder().UseEnvironment("Development").UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Theory]
        [InlineData("GET")]
        public async Task GetAllTestAsync(string method)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/v1/tariff/");

            var response = await _client.SendAsync(request);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("GET")]
        public async Task GetAllTestAsync(string method, decimal? consumption = null)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), $"/api/v1/tariff/{consumption}");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        [Theory]
        [InlineData("GET")]
        public async Task GetTestNegativeAsync(string method)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), $"/api/v1/tariff/-1");

            var response = await _client.SendAsync(request);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
