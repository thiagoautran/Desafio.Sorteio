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
    public class GetPhysicallyHandicapped : BaseApiTest<Startup<DependencyInjectionConfigurationTest>, ParticipantEntity, ParticipantConfiguration>
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
                CPF = 94555397991,
                Name = "Sabrina Luna Laís Cavalcanti",
                BirthDate = new DateTime(1971, 11, 26),
                Income = 3000m,
                Quota = "DEIFICENTE FÍSICO",
                CID = "H90"
            });
            await participantRepository.AddAsync(new ParticipantEntity
            {
                Id = Guid.NewGuid(),
                CPF = 95861779040,
                Name = "Severino Igor Mário Barros",
                BirthDate = new DateTime(1994, 2, 21),
                Income = 4000m,
                Quota = "DEIFICENTE FÍSICO",
                CID = "H90"
            });
            await Repository.DbContext.SaveChangesAsync();

            var response = await HttpClient.Fluent().GetAsync("door.prize/v1/participant/physically_handicapped");
            var data = await response.DeserializeAsync<IEnumerable<ParticipantResponse>>();

            Assert.IsNotNull(data.HttpStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, data.HttpStatusCode.Value);
            Assert.AreEqual(2, data.Data.Count());
        }

        [TestMethod]
        public async Task NotFound()
        {
            var response = await HttpClient.Fluent().GetAsync("door.prize/v1/participant/physically_handicapped");

            Assert.IsNotNull(response.HttpStatusCode);
            Assert.AreEqual(HttpStatusCode.NotFound, response.HttpStatusCode.Value);
        }
    }
}