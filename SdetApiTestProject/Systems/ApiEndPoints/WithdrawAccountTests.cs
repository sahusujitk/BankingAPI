using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Radancy.Interviews.Examples.Banking.Sdet.Api;
using Radancy.Interviews.Examples.Banking.Sdet.Api.Contracts;
using System.Net;
using System.Net.Http.Json;

namespace SdetApiTestProject.Systems.ApiEndPoints
{

    public class WithdrawAccountTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {

        [Fact]
        public async Task WithdrawAccountTests_Success()

        {

            using var client = factory.CreateClient();
            //await using var application = new WebApplicationFactory<Radancy.Interviews.Examples.Banking.Sdet.Api.Program>();

            var account = await CreateAccountToTest();


            var withdrawResult = await client.PostAsJsonAsync("/accounts/" + account.Id + "/transactions", new CreateTransactionRequest
            {
                Amount = -100
            });

            var data = await withdrawResult.Content.ReadAsStringAsync();


            var accountAfterWithdraw = UtilityClass.DeserializeObject<AccountDto>(data);


            Assert.Equal(HttpStatusCode.OK, withdrawResult.StatusCode);
            Assert.Equal(900, accountAfterWithdraw.Funds);



        }

        [Fact]
        public async Task WithdrawAccount90percentTests_Success()

        {
            var account = await CreateAccountToTest();
            using var client = factory.CreateClient();

            var withdrawResult = await client.PostAsJsonAsync("/accounts/" + account.Id + "/transactions", new CreateTransactionRequest
            {
                Amount = -900
            });

            var data = await withdrawResult.Content.ReadAsStringAsync();


            var accountAfterWithdraw = UtilityClass.DeserializeObject<AccountDto>(data);


            Assert.Equal(HttpStatusCode.OK, withdrawResult.StatusCode);
            Assert.Equal(100, accountAfterWithdraw.Funds);
        }

        [Fact]
        public async Task WithdrawAccountMorethan90percentTests_Fail()

        {
            var account = await CreateAccountToTest();
            using var client = factory.CreateClient();

            var withdrawResult = await client.PostAsJsonAsync("/accounts/" + account.Id + "/transactions", new CreateTransactionRequest
            {
                Amount = -901
            });

            var data = await withdrawResult.Content.ReadAsStringAsync();


            var result = UtilityClass.DeserializeObject<ProblemDetails>(data);

            Assert.Equal(HttpStatusCode.UnprocessableEntity, withdrawResult.StatusCode);
            Assert.Equal("More than 90% of the current funds cannot be withdrawn.", result.Detail);
        }


        private async Task<AccountDto> CreateAccountToTest()
        {
            using var client = factory.CreateClient();
            var result = await client.PostAsJsonAsync("/accounts", new CreateAccountRequestDto
            {
                InitialFunds = 1000
            });

            var dataString = await result.Content.ReadAsStringAsync();


            var account = UtilityClass.DeserializeObject<AccountDto>(dataString);

            return account;
        }


    }
}
