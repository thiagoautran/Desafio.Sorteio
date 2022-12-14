using DoorPrize.ApplicationCore.Entities;
using DoorPrize.ApplicationCore.Exceptions;
using DoorPrize.ApplicationCore.Helper;
using DoorPrize.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DoorPrize.ApplicationCore.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IParticipantFile _participantFile;

        public ParticipantService(IParticipantRepository participantRepository, IParticipantFile participantFile) =>
            (_participantRepository, _participantFile) = (participantRepository, participantFile);

        public async Task FileUpload(IFormFile file)
        {
            if (!file.FileName.ToUpper().Contains(".CSV"))
                throw new BadRequestException("Arquivo inválido.");

            var participants = await _participantFile.Get(file);
            if (participants.Count() == 0)
                throw new BadRequestException("Não existem participantes elegíveis para cadastro.");

            Console.Write($"Quantidade de participantes: {participants.Count()} no arquivo csv.");

            foreach (var participant in participants)
            {
                if(!ValidCpf.Valid(participant.CPF.ToString(@"000\.000\.000\-00")))
                    continue;

                if (participant.Income < 1045m || participant.Income > 5225m)
                    continue;

                var year = DateTime.Now.Year - participant.BirthDate.Year;
                if (year < 15)
                    continue;

                if (participant.Quota.Trim().ToUpper() == "IDOSO" && year < 60)
                    continue;

                if (participant.Quota.Trim().ToUpper() == "DEIFICENTE FÍSICO" && string.IsNullOrEmpty(participant.CID))
                    continue;

                Console.Write($"Adicionado participante: {participant.CPF} - {participant.Name}");
                await _participantRepository.Insert(participant);
            }
        }

        public async Task<IEnumerable<ParticipantEntity>> ListElderly() =>
            await _participantRepository.ListElderly();

        public async Task<IEnumerable<ParticipantEntity>> ListPhysicallyHandicapped() =>
            await _participantRepository.ListPhysicallyHandicapped();

        public async Task<IEnumerable<ParticipantEntity>> ListGeneral() =>
            await _participantRepository.ListGeneral();

        public async Task<ParticipantEntity> WinnerElderly()
        {
            var list = await _participantRepository.ListElderly();
            if (list.Count() == 0)
                return null;

            int r = new Random().Next(list.Count());
            return list.ToArray()[r];
        }

        public async Task<ParticipantEntity> WinnerPhysicallyHandicapped()
        {
            var list = await _participantRepository.ListPhysicallyHandicapped();
            if (list.Count() == 0)
                return null;

            int r = new Random().Next(list.Count());
            return list.ToArray()[r];
        }

        public async Task<IEnumerable<ParticipantEntity>> WinnerGeneral()
        {
            var winnes = new List<ParticipantEntity>();

            var list = (await _participantRepository.ListGeneral()).ToList();
            if (list.Count() == 0)
                return new List<ParticipantEntity>();

            while (winnes.Count < 3 || winnes.Count <= list.Count)
            {
                int r = new Random().Next(list.Count());
                var winner = list.ToArray()[r];

                if (!winnes.Contains(winner))
                {
                    winnes.Add(winner);
                    list.Remove(winner);
                }
            }

            return winnes;
        }
    }
}