using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Radancy.Interviews.Examples.Banking.Sdet.Api;
using Radancy.Interviews.Examples.Banking.Sdet.Api.Contracts;
using System.Net;
using System.Net.Http.Json;

namespace SdetApiTestProject.Systems.ApiEndPoints
{
    public class DepositAccountTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {


        [Fact]
        public async Task DepositAcountTest_Success()

        {

            using var client = factory.CreateClient();
           

            var account = await CreateAccountToTest();


            var depositResult = await client.PostAsJsonAsync("/accounts/" + account.Id + "/transactions", new CreateTransactionRequest
            {
                Amount = 100
            });

            var data = await depositResult.Content.ReadAsStringAsync();


            var accountAfterDesposit = UtilityClass.DeserializeObject<AccountDto>(data);


            Assert.Equal(HttpStatusCode.OK, depositResult.StatusCode);
            Assert.Equal(200, accountAfterDesposit.Funds);



        }

        [Fact]
        public async Task Deposit10001AcountTest_Fail()

        {
            var account = await CreateAccountToTest();
            using var client = factory.CreateClient();

            var depositResult = await client.PostAsJsonAsync("/accounts/" + account.Id + "/transactions", new CreateTransactionRequest
            {
                Amount = 100001
            });

            var data = await depositResult.Content.ReadAsStringAsync();


            var result = UtilityClass.DeserializeObject<ProblemDetails>(data);

            Assert.Equal(HttpStatusCode.UnprocessableEntity, depositResult.StatusCode);
            Assert.Equal("Deposit limit is 10000$.", result.Detail);
        }

        [Fact]
        public async Task DepositAcountNegativeamountTest_Fail()

        {
            var account = await CreateAccountToTest();
            using var client = factory.CreateClient();

            var depositResult = await client.PostAsJsonAsync("/accounts/" + account.Id + "/transactions", new CreateTransactionRequest
            {
                Amount = -1000
            });

            var data = await depositResult.Content.ReadAsStringAsync();


            var result = UtilityClass.DeserializeObject<ProblemDetails>(data);

            Assert.Equal(HttpStatusCode.UnprocessableEntity, depositResult.StatusCode);
            Assert.Equal("More than 90% of the current funds cannot be withdrawn.", result.Detail);
        }


        private async Task<AccountDto> CreateAccountToTest()
        {
            using var client = factory.CreateClient();
            var result = await client.PostAsJsonAsync("/accounts", new CreateAccountRequestDto
            {
                InitialFunds = 100
            });

            var dataString = await result.Content.ReadAsStringAsync();


            var account = UtilityClass.DeserializeObject<AccountDto>(dataString);

            return account;
        }

    }
}
