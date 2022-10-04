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
    public class GetGeneral : BaseApiTest<Startup<DependencyInjectionConfigurationTest>, ParticipantEntity, ParticipantConfiguration>
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
                CPF = 92509164503,
                Name = "Oliver Ricardo Ribeiro",
                BirthDate = new DateTime(1980, 3, 24),
                Income = 2000m,
                Quota = "GERAL",
                CID = string.Empty
            });
            await participantRepository.AddAsync(new ParticipantEntity
            {
                Id = Guid.NewGuid(),
                CPF = 73519600897,
                Name = "Giovanna Gabriela da Mota",
                BirthDate = new DateTime(1995, 12, 18),
                Income = 1250m,
                Quota = "GERAL",
                CID = string.Empty
            });
            await participantRepository.AddAsync(new ParticipantEntity
            {
                Id = Guid.NewGuid(),
                CPF = 12802164848,
                Name = "Kaique Calebe Almeida",
                BirthDate = new DateTime(1972, 4, 27),
                Income = 4488m,
                Quota = "GERAL",
                CID = string.Empty
            });
            await Repository.DbContext.SaveChangesAsync();

            var response = await HttpClient.Fluent().GetAsync("door.prize/v1/participant/general");
            var data = await response.DeserializeAsync<IEnumerable<ParticipantResponse>>();

            Assert.IsNotNull(data.HttpStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, data.HttpStatusCode.Value);
            Assert.AreEqual(3, data.Data.Count());
        }

        [TestMethod]
        public async Task NotFound()
        {
            var response = await HttpClient.Fluent().GetAsync("door.prize/v1/participant/general");

            Assert.IsNotNull(response.HttpStatusCode);
            Assert.AreEqual(HttpStatusCode.NotFound, response.HttpStatusCode.Value);
        }
    }
}