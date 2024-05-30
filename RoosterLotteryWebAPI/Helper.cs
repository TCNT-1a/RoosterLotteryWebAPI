using Microsoft.AspNetCore.Mvc;

namespace RoosterLotteryWebAPI
{
    public class Helper : ControllerBase
    {
        public static string FormatDate(DateTime d)
        {
            return String.Format("{0:dd-MM-yyyy}", d);
        }

    }
}
