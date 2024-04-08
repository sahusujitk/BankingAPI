using Microsoft.AspNetCore.Mvc.Testing;
using Radancy.Interviews.Examples.Banking.Sdet.Api;
using Radancy.Interviews.Examples.Banking.Sdet.Api.Contracts;
using System.Net;
using System.Net.Http.Json;

namespace SdetApiTestProject.Systems.ApiEndPoints
{
    public class GetAccountTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {

        [Fact]

        public async Task GetAllAccountTests_Success()

        {

            using var client = factory.CreateClient();

            var account1 = await CreateAccountToTest();
            var account2 = await CreateAccountToTest();


            var getResult = await client.GetAsync("/accounts");

            var data = await getResult.Content.ReadAsStringAsync();


            var checkAccount = UtilityClass.DeserializeObject<IList<AccountDto>>(data);
            var accountIds = checkAccount.Select(c => c.Id);


            Assert.Equal(HttpStatusCode.OK, getResult.StatusCode);

            Assert.Contains(account1.Id, accountIds);
            Assert.Contains(account2.Id, accountIds);

        }


        [Fact]
        public async Task GetAccountTests_Success()

        {

            using var client = factory.CreateClient();

            var account = await CreateAccountToTest();


            var getResult = await client.GetAsync("/accounts/" + account.Id);

            var data = await getResult.Content.ReadAsStringAsync();


            var checkAccount = UtilityClass.DeserializeObject<AccountDto>(data);


            Assert.Equal(HttpStatusCode.OK, getResult.StatusCode);
            Assert.Equal(account.Id, checkAccount.Id);
            Assert.Equal(account.Funds, checkAccount.Funds);


        }

        [Fact]
        public async Task GetAccountTests_Fail()

        {

            using var client = factory.CreateClient();

            var getResult = await client.GetAsync("/accounts/" + Guid.NewGuid());


            Assert.Equal(HttpStatusCode.NotFound, getResult.StatusCode);



        }



        [Fact]
        public async Task GetAllAccountWithNoAccount_Success()

        {

            using var client = factory.CreateClient();


            var getResult = await client.GetAsync("/accounts");

            var data = await getResult.Content.ReadAsStringAsync();


            var checkAccount = UtilityClass.DeserializeObject<IList<AccountDto>>(data);


            Assert.Equal(HttpStatusCode.OK, getResult.StatusCode);


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
