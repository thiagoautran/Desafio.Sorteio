namespace DoorPrize.ApplicationCore.Entities
{
    public class ParticipantEntity : BaseEntity
    {
        public string Name { get; set; }
        public long CPF { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal? Income { get; set; }
        public string Quota { get; set; }
        public string CID { get; set; }
    }
}