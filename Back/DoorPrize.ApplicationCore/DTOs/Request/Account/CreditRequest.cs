using System.ComponentModel.DataAnnotations;

namespace DoorPrize.ApplicationCore.DTOs.Request.Account
{
    public class CreditRequest
    {
        [Required]
        public uint Agency { get; set; }
        [Required]
        public uint AccountNumber { get; set; }
        [Required]
        public uint CurrentAccountDigit { get; set; }
        [Required]
        public decimal Value { get; set; }
    }
}