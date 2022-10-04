using Autransoft.Fluent.HttpClient.Lib.Extensions;
using Autransoft.Test.Lib.Extensions;
using Autransoft.Test.Lib.Program;
using DoorPrize.Api;
using DoorPrize.ApplicationCore.DTOs.Response.Participant;
using DoorPrize.ApplicationCore.Entities;
using DoorPrize.ApplicationCore.Interfaces;
using DoorPrize.Infrastructure.Data.Config;
using DoorPrize.IntegrationTest.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace DoorPrize.IntegrationTest
{
    [TestClass]
    public class GetElderly : BaseApiTest<Startup<DependencyInjectionConfigurationTest>, ParticipantEntity, ParticipantConfiguration>
    {
        [TestInitialize]
        public void TestInitialize() => base.Initialize();

        [TestCleanup]
        public void TestCleanup() => base.Dispose();

        public override void AddToDependencyInjection(IServiceCollection serviceCollection, IConfiguration configuration) =>
            serviceCollection.ReplaceTransient<IEFContext, SqlLiteContextTest>();

        [TestMethod]
        public async Task HappyDay()
        {
            var participantRepository = Repository.DbContext.Set<ParticipantEntity>();
            await participantRepository.AddAsync(new ParticipantEntity
            {
                Id = Guid.NewGuid(),
                CPF = 47989396505,
                Name = "Sérgio Vinicius Barros",
                BirthDate = new DateTime(1956, 7, 4),
                Income = 5225m,
                Quota = "IDOSO",
                CID = string.Empty
            });
            await participantRepository.AddAsync(new ParticipantEntity
            {
                Id = Guid.NewGuid(),
                CPF = 5324618780,
                Name = "Bruno Levi Dias",
                BirthDate = new DateTime(1941, 9, 13),
                Income = 2500m,
                Quota = "IDOSO",
                CID = string.Empty
            });
            await Repository.DbContext.SaveChangesAsync();

            var response = await HttpClient.Fluent().GetAsync("door.prize/v1/participant/elderly");
            var data = await response.DeserializeAsync<IEnumerable<ParticipantResponse>>();

            Assert.IsNotNull(data.HttpStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, data.HttpStatusCode.Value);
            Assert.AreEqual(2, data.Data.Count());
        }

        [TestMethod]
        public async Task NotFound()
        {
            var response = await HttpClient.Fluent().GetAsync("door.prize/v1/participant/elderly");

            Assert.IsNotNull(response.HttpStatusCode);
            Assert.AreEqual(HttpStatusCode.NotFound, response.HttpStatusCode.Value);
        }
    }
}