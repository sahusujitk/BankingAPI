using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Radancy.Interviews.Examples.Banking.Sdet.Api.Contracts;
using System.Net;
using System.Net.Http.Json;


namespace SdetApiTestProject.Systems.ApiEndPoints
{
    public class CreateAccountTests
    {
        [Fact]
        public async Task CreateAcountTest_Sucess()
        {
            await using var application = new WebApplicationFactory<Radancy.Interviews.Examples.Banking.Sdet.Api.Program>();
            using var client = application.CreateClient();
            var result = await client.PostAsJsonAsync("/accounts", new CreateAccountRequestDto
            {
                InitialFunds = 100
            });

            var data = await result.Content.ReadAsStringAsync();


            var res = UtilityClass.DeserializeObject<Radancy.Interviews.Examples.Banking.Sdet.Api.Contracts.AccountDto>(data);



            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            Assert.Equal(100, res.Funds);


        }

        [Fact]
        public async Task CreateAcountTestwithlessthan100_Fail()
        {
            await using var application = new WebApplicationFactory<Radancy.Interviews.Examples.Banking.Sdet.Api.Program>();
            using var client = application.CreateClient();
            var result = await client.PostAsJsonAsync("/accounts", new CreateAccountRequestDto
            {
                InitialFunds = 10
            });

            var data = await result.Content.ReadAsStringAsync();


            var res = UtilityClass.DeserializeObject<ProblemDetails>(data);


            Assert.Equal(HttpStatusCode.UnprocessableEntity, result.StatusCode);
            Assert.Equal("Account must have at least 100$ at any moment in time.", res.Detail);


        }

        [Fact]
        public async Task CreateAcountTestwithNegativeamount_Fail()
        {
            await using var application = new WebApplicationFactory<Radancy.Interviews.Examples.Banking.Sdet.Api.Program>();
            using var client = application.CreateClient();
            var result = await client.PostAsJsonAsync("/accounts", new CreateAccountRequestDto
            {
                InitialFunds = -100
            });

            var data = await result.Content.ReadAsStringAsync();


            var res = UtilityClass.DeserializeObject<ProblemDetails>(data);


            Assert.Equal(HttpStatusCode.UnprocessableEntity, result.StatusCode);
            Assert.Equal("Account must have at least 100$ at any moment in time.", res.Detail);


        }


        [Fact]
        public async Task CreateAcountTestwithAmountgreaterthan10000_Fail()
        {
            await using var application = new WebApplicationFactory<Radancy.Interviews.Examples.Banking.Sdet.Api.Program>();
            using var client = application.CreateClient();
            var result = await client.PostAsJsonAsync("/accounts", new CreateAccountRequestDto
            {
                InitialFunds = 100001
            });

            var data = await result.Content.ReadAsStringAsync();


            var res = UtilityClass.DeserializeObject<ProblemDetails>(data);


            Assert.Equal(HttpStatusCode.UnprocessableEntity, result.StatusCode);
            Assert.Equal("Deposit limit is 10000$.", res.Detail);


        }



    }
}