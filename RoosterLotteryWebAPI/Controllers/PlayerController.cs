using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RoosterLotteryWebAPI;
using Service.Models;

using System.ComponentModel.DataAnnotations;

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

        public IActionResult createPlayer([FromBody] Player p)
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
        public IActionResult bet([FromBody] PlayerBet b)
        {
            var playerId = new SqlParameter("@PlayerID", b.UserId);
            var betNumber = new SqlParameter("@BetNumber", b.UserId);
            var p = _context.Database
                .ExecuteSqlRaw("EXEC dbo.CreatePlayerBet @PlayerID, @BetNumber"
                , playerId, betNumber);
            return ResponseModel(p);
        }
        IActionResult ResponseModel(int p)
        {
            if (p > 0) return Ok(new { message = "Success to process", status = true });
            return BadRequest(new { message = "Fail to process", status = false });
        }

    }

    public class SearchPlayer
    {
        [MaxLength(15)]

        public string? phoneNumber { get; set; }
    }
    public class PlayerBet
    {
        public int UserId { get; set; }
        public int BetNumber { get; set; }
    }
}
