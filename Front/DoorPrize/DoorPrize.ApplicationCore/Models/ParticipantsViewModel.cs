namespace DoorPrize.ApplicationCore.Models
{
    public class ParticipantsViewModel
    {
        public IEnumerable<ParticipantViewModel> Elderly { get; set; }

        public IEnumerable<ParticipantViewModel> PhysicallyHandicapped { get; set; }

        public IEnumerable<ParticipantViewModel> General { get; set; }

        public int NumberParticipants { get; set; }
    }
}
