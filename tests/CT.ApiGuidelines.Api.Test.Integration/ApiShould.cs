namespace CT.ApiGuidelines.Api.Test.Integration
{
    using System.Net;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Xunit;

    public class ApiShould : IClassFixture<ApiGuidelinesWebApplicationFactory>
    {
        private readonly ApiGuidelinesWebApplicationFactory _webApplicationFactory;

        public ApiShould(ApiGuidelinesWebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task ReturnHttpStatus406WhenRequestionAnUnknownMediaType()
        {
            var client = _webApplicationFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

            var response = await client.GetAsync("/accounts").ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.NotAcceptable);
        }
    }
}
