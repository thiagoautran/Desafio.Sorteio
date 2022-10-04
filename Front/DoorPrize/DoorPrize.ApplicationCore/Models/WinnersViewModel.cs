namespace DoorPrize.ApplicationCore.Models
{
    public class WinnersViewModel
    {
        public ParticipantViewModel Elderly { get; set; }

        public ParticipantViewModel PhysicallyHandicapped { get; set; }

        public IEnumerable<ParticipantViewModel> General { get; set; }

        public int NumberParticipants { get; set; }
    }
}
