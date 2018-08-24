namespace CT.ApiGuidelines.Api.Test.Integration
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Models.Accounts;
    using Xunit;

    public class AccountsContollerShould : IClassFixture<ApiGuidelinesWebApplicationFactory>
    {
        private readonly ApiGuidelinesWebApplicationFactory _webApplicationFactory;

        public AccountsContollerShould(ApiGuidelinesWebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task ReturnAnAccountsCollection()
        {
            var client = _webApplicationFactory.CreateClient();

            var response = await client.GetAsync("/accounts").ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ReturnHttpStatus404WhenRequestingANonExistentAccount()
        {
            var client = _webApplicationFactory.CreateClient();

            var response = await client.GetAsync($"/accounts/{new Guid("4bf523c7-0ddc-424a-a64c-0ddc578cde72")}").ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ReturnHttpStatus400WhenPostingAnIncorrectPayload()
        {
            var client = _webApplicationFactory.CreateClient();

            var response = await client.PostAsync("/accounts", "Incorrect payload", new JsonMediaTypeFormatter()).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ReturnHttpStatus422WhenPostingAnIncorrectOwner()
        {
            var client = _webApplicationFactory.CreateClient();
            var incorrectOwnerId = new Guid("99999999-9999-9999-9999-999999999999");
            var dto = new AccountPostV1 { OwnerId = incorrectOwnerId };

            var response = await client.PostAsync("/accounts", dto, new JsonMediaTypeFormatter()).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task ReturnHttpStatus201WhenPostingACorrectPayload()
        {
            var client = _webApplicationFactory.CreateClient();

            var dto = new AccountPostV1 { OwnerId = new Guid("11111111-0000-0000-0000-000000000001") };

            var response = await client.PostAsync("/accounts", dto, new JsonMediaTypeFormatter()).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}
