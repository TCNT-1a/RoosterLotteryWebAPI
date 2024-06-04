using System.ComponentModel.DataAnnotations;

namespace RoosterLotteryWebAPI.Controllers.RequestModel
{
    public class SearchPlayer
    {
        [MaxLength(15)]
        public string? phoneNumber { get; set; }
    }
   
}
