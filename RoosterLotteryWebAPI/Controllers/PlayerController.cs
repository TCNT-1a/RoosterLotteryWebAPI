using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RoosterLotteryWebAPI.Controllers.RequestModel;
using Service.Models;


namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly RoosterLotteryContext _context;
        public PlayerController(RoosterLotteryContext context)
        {
            this._context = context;
        }
        [HttpPost("find")]

        public List<Player> FindPlayerByPhoneNumber([FromBody] SearchPlayer s)
        {
            var phoneNumber = s.phoneNumber != null ? s.phoneNumber : "";
            var phoneNumberParameter = new SqlParameter("@PhoneNumber", phoneNumber);
            var p = _context.Players
                .FromSqlRaw("EXEC dbo.FindPlayerByPhoneNumber @PhoneNumber"
                , phoneNumberParameter).ToList();
            return p;
        }
        [HttpPost("create")]

        public IActionResult CreatePlayer([FromBody] CreatePlayer p)
        {
            if (ModelState.IsValid)
            {
                var fullNameParas = new SqlParameter("@FullName", p.FullName);

                var dateOfBirthParas = new SqlParameter("@DateOfBirth", p.DateOfBirth);

                var phoneParas = new SqlParameter("@PhoneNumber", p.PhoneNumber);

                var c = _context.Database
                   .ExecuteSqlRaw("EXEC dbo.CreatePlayer @FullName, @DateOfBirth, @PhoneNumber"
                   , fullNameParas, dateOfBirthParas, phoneParas);
                return ResponseModel(c);
            }
            else
            {
                var errors = string.Join("; ", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));
                throw new ArgumentException(errors);
            }
        }
        [HttpPost("bet")]
        public IActionResult Bet([FromBody] CreatePlayerBet b)
        {
            var playerId = new SqlParameter("@PlayerID", b.UserId);
            var betNumber = new SqlParameter("@BetNumber", b.BetNumber);
            var p = _context.Database
                .ExecuteSqlRaw("EXEC dbo.CreatePlayerBet @PlayerID, @BetNumber"
                , playerId, betNumber);
            return ResponseModel(p);
        }

        [HttpGet("get-board-bets")]
        public  List<BoardBet> GetBoardBets([FromQuery]int playerId)
        {
            var pId = new SqlParameter("@PlayerID", playerId);
            var p =  _context.BoardBets
                .FromSqlRaw("EXEC dbo.GetPlayerBets @PlayerID"
                , pId).ToList();
            return p;
        }
        IActionResult ResponseModel(int p)
        {
            if (p > 0) return Ok(new { message = "Success to process", status = true });
            return BadRequest(new { message = "Fail to process", status = false });
        }

    }

    
    

}
