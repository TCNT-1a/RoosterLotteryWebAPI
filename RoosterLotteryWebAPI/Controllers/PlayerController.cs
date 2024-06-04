using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RoosterLotteryWebAPI.Controllers.RequestModel;
using Service.Models;


namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly RoosterLotteryContext _context;
        public PlayerController(RoosterLotteryContext context)
        {
            this._context = context;
        }

        [HttpPost("findPlayer")]

        public List<Player> findPlayerByPhoneNumber([FromBody] SearchPlayer s)
        {
            var phoneNumber = s.phoneNumber != null ? s.phoneNumber : "";

            var phoneNumberParameter = new SqlParameter("@PhoneNumber", phoneNumber);

            var p = _context.Players
                .FromSqlRaw("EXEC dbo.FindPlayerByPhoneNumber @PhoneNumber"
                , phoneNumberParameter).ToList();

            return p;

        }
        [HttpPost("createPlayer")]

        public IActionResult createPlayer([FromBody] CreatePlayer p)
        {

            var fullNameParas = new SqlParameter("@FullName", p.FullName);

            var dateOfBirthParas = new SqlParameter("@DateOfBirth", p.DateOfBirth);

            var phoneParas = new SqlParameter("@PhoneNumber", p.PhoneNumber);

            var c = _context.Database
               .ExecuteSqlRaw("EXEC dbo.CreatePlayer @FullName, @DateOfBirth, @PhoneNumber"
               , fullNameParas, dateOfBirthParas, phoneParas);
            return ResponseModel(c);

        }
        [HttpPost("bet")]
        public IActionResult bet([FromBody] CreatePlayerBet b)
        {
            var playerId = new SqlParameter("@PlayerID", b.UserId);
            var betNumber = new SqlParameter("@BetNumber", b.BetNumber);
            var p = _context.Database
                .ExecuteSqlRaw("EXEC dbo.CreatePlayerBet @PlayerID, @BetNumber"
                , playerId, betNumber);
            return ResponseModel(p);
        }

        [HttpGet("GetBoardBet")]
        public async Task<List<BoardBet>> getPlayerBets([FromQuery]int playerId)
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
