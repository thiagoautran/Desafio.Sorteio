using DoorPrize.ApplicationCore.Entities;
using DoorPrize.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DoorPrize.Infrastructure.File
{
    public class ParticipantFile : IParticipantFile
    {
        public async Task<IList<ParticipantEntity>> Get(IFormFile file)
        {
            var participants = new List<ParticipantEntity>();

            using var memoryStream = new MemoryStream(new byte[file.Length]);
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            using var reader = new StreamReader(memoryStream);
            string[] headers = (await reader.ReadLineAsync()).Split(',');
            while (!reader.EndOfStream)
            {
                string[] rows = reader.ReadLine().Split(',');
                string name = rows[0].ToString();
                long.TryParse(rows[1].ToString().Replace(".", "").Replace("-", ""), out long cpf);

                var ardate = rows[2].ToString().Split("/");
                var strdate = ardate[0].Length == 1 ? $"0{ardate[0]}/" : $"{ardate[0]}/";
                strdate += ardate[1].Length == 1 ? $"0{ardate[1]}/" : $"{ardate[1]}/";
                strdate += ardate[2];

                DateTime.TryParseExact(strdate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime birthDate);
                if (birthDate == new DateTime())
                    continue;

                decimal.TryParse(rows[3].ToString().Replace(".", ","), out decimal income);
                string quota = rows[4].ToString();
                string cid = rows[5].ToString();

                participants.Add(new ParticipantEntity
                {
                    Name = name,
                    BirthDate = birthDate,
                    CID = cid,
                    CPF = cpf,
                    Income = income,
                    Quota = quota
                });
            }

            return participants;
        }
    }
}