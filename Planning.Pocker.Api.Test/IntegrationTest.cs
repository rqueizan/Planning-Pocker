using Planning.Pocker.Api.NoAuth.Dtos;
using Planning.Pocker.Api.NoAuth.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Planning.Pocker.Api.Test
{
    [Collection("IntegrationTestServer")]
    [TestCaseOrderer("Planning.Pocker.Api.Test.PriorityOrderer", "Planning.Pocker.Api.Test")]

    public class IntegrationTest
    {
        private readonly IntegrationTestServer integrationTestServer;
        private readonly ITestOutputHelper output;

        #region Data
        private static DtoCarta carta;
        #endregion

        public IntegrationTest(IntegrationTestServer integrationTestServer, ITestOutputHelper output)
        {
            this.integrationTestServer = integrationTestServer;
            this.output = output;
        }

        [Fact, Priority(1)]
        public async Task Should_Create_Carta()
        {
            try
            {
                var command = new CreateCartaCommand { Valor = 5 };
                var response = await integrationTestServer.PostAsync<DtoCarta>("/api/Cartas", command);
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
                carta = response.Data;
            }
            catch (Exception ex)
            {
                output.WriteLine(ex.GetType().FullName);
                output.WriteLine(ex.Message);
                output.WriteLine(ex.StackTrace);
            }
        }

        [Fact, Priority(2)]
        public async Task Should_Update_Carta()
        {
            try
            {
                carta.Valor = 6;
                var command = new UpdateCartaCommand { Id = carta.Id, Valor = carta.Valor };
                var response = await integrationTestServer.PutAsync($"/api/Cartas/{carta.Id}", command);
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }
            catch (Exception ex)
            {
                output.WriteLine(ex.GetType().FullName);
                output.WriteLine(ex.Message);
                output.WriteLine(ex.StackTrace);
            }
        }

        [Fact, Priority(3, Orderer.Random)]
        public async Task Should_Get_Cartas()
        {
            try
            {
                var response = await integrationTestServer.GetAsync<List<DtoCarta>>($"/api/Cartas");
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.NotEmpty(response.Data);
                Assert.NotNull(response.Data.FirstOrDefault(c => c.Id == carta.Id && c.Valor == carta.Valor));
            }
            catch (Exception ex)
            {
                output.WriteLine(ex.GetType().FullName);
                output.WriteLine(ex.Message);
                output.WriteLine(ex.StackTrace);
            }
        }

        [Fact, Priority(3)]
        public async Task Should_Get_Cartas_With_Filters()
        {
            try
            {
                var command = new ListarCartasQuery { Min = 7, Max = 9 };
                var response = await integrationTestServer.GetAsync<List<DtoCarta>>($"/api/Cartas{command.Filter()}");
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Contains(response.Data, c => command.Min <= c.Valor && c.Valor <= command.Max);
            }
            catch (Exception ex)
            {
                output.WriteLine(ex.GetType().FullName);
                output.WriteLine(ex.Message);
                output.WriteLine(ex.StackTrace);
            }
        }
    }
}
