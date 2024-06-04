using System.ComponentModel.DataAnnotations;

namespace RoosterLotteryWebAPI.Controllers.RequestModel
{
    public class CreatePlayerBet
    {
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer")]
        public int UserId { get; set; }
        [Range(0, 9, ErrorMessage = "BetNumber must be between 0 and 9")]
        public int BetNumber { get; set; }
    }
}
