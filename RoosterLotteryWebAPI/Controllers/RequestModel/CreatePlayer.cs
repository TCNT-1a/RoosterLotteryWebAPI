using RoosterLotteryWebAPI.Anotation;
using System.ComponentModel.DataAnnotations;

namespace RoosterLotteryWebAPI.Controllers.RequestModel
{
    public class CreatePlayer
    {
        //public string FullName { get; set; }

        //public DateTime DateOfBirth { get; set; }
        //[PhoneNumber]
        //public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        //[Phone(ErrorMessage = "Invalid phone number format")]
        [PhoneNumber]
        public string? PhoneNumber { get; set; }
    }
 
}
