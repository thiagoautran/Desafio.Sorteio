using Autransoft.Fluent.HttpClient.Lib.Extensions;
using Autransoft.Test.Lib.Extensions;
using Autransoft.Test.Lib.Program;
using DoorPrize.Api;
using DoorPrize.ApplicationCore.DTOs.Response.Participant.Get;
using DoorPrize.ApplicationCore.DTOs.Response.Participant.Winners;
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
    public class Get : BaseApiTest<Startup<DependencyInjectionConfigurationTest>, ParticipantEntity, ParticipantConfiguration>
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
            await AddElderly();
            await AddPhysicallyHandicapped();
            await AddGeneral();

            var response = await HttpClient.Fluent().GetAsync("door.prize/v1/participant");
            var data = await response.DeserializeAsync<GetResponse>();

            Assert.IsNotNull(data.HttpStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, data.HttpStatusCode.Value);
            Assert.AreEqual(2, data.Data.Elderly.Count());
            Assert.AreEqual(2, data.Data.PhysicallyHandicapped.Count());
            Assert.AreEqual(3, data.Data.General.Count());
            Assert.AreEqual(7, data.Data.NumberParticipants);
        }

        //[TestMethod]
        //public async Task NotFound()
        //{
        //    var response = await HttpClient.Fluent().GetAsync("door.prize/v1/participant/elderly");

        //    Assert.IsNotNull(response.HttpStatusCode);
        //    Assert.AreEqual(HttpStatusCode.NotFound, response.HttpStatusCode.Value);
        //}

        private async Task AddElderly()
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
        }

        private async Task AddPhysicallyHandicapped()
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
        }

        private async Task AddGeneral()
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
        }
    }
}