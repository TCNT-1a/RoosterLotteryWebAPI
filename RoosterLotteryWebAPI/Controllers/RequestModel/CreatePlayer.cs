using RoosterLotteryWebAPI.Anotation;

namespace RoosterLotteryWebAPI.Controllers.RequestModel
{
    public class CreatePlayer
    {
        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }
        [PhoneNumber]
        public string PhoneNumber { get; set; }
    }
}
