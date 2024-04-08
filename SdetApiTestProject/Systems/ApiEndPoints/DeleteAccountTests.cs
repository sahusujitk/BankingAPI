using Microsoft.AspNetCore.Mvc.Testing;
using Radancy.Interviews.Examples.Banking.Sdet.Api.Contracts;
using System.Net;
using System.Net.Http.Json;

namespace SdetApiTestProject.Systems.ApiEndPoints
{
    public class DeleteAccountTests
    {

        [Fact]
        public async Task DeleteAcountTest_Success()
        {
            await using var application = new WebApplicationFactory<Radancy.Interviews.Examples.Banking.Sdet.Api.Program>();
            using var client = application.CreateClient();
            var result = await client.PostAsJsonAsync("/accounts", new CreateAccountRequestDto
            {
                InitialFunds = 100
            });

            var data = await result.Content.ReadAsStringAsync();

            var res = UtilityClass.DeserializeObject<Radancy.Interviews.Examples.Banking.Sdet.Api.Contracts.AccountDto>(data);



            var deleteResult = await client.DeleteAsync("/accounts/" + res.Id);

            Assert.Equal(HttpStatusCode.OK, deleteResult.StatusCode);



        }

        [Fact]
        public async Task DeleteAcountTest_Fail()
        {
            await using var application = new WebApplicationFactory<Radancy.Interviews.Examples.Banking.Sdet.Api.Program>();
            using var client = application.CreateClient();
            var result = await client.PostAsJsonAsync("/accounts", new CreateAccountRequestDto
            {
                InitialFunds = 100
            });

            var data = await result.Content.ReadAsStringAsync();


            var res = UtilityClass.DeserializeObject<Radancy.Interviews.Examples.Banking.Sdet.Api.Contracts.AccountDto>(data);


            var deleteResult = await client.DeleteAsync("/accounts/" + Guid.NewGuid());


            Assert.Equal(HttpStatusCode.NotFound, deleteResult.StatusCode);



        }


    }


}

