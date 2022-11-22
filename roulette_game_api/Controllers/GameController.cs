using Dapper;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using roulette_game_api.Data;
using roulette_game_api.Model;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace roulette_game_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        [HttpPost("spin")]
        public async Task<ActionResult> Spin(PlaceBet placeBet)
        {
            List<string> cl = new()
            {
                "Black",
                "Red"
            }; //color
            int roll = new Random().Next(0, 37);
            string r_color = cl[new Random().Next(2)];
            string player_bet_type = placeBet.BetType;
            BetResults betResults = new BetResults();
            if (((player_bet_type == "Even") && (roll % 2 == 0))|| 
                (((player_bet_type == "Odd") && (roll % 2 == 1))) || 
                ((player_bet_type == "Red") && (r_color == "Red") ||
                ((player_bet_type == "Black") && (r_color == "Black"))))
            {
                betResults.Color = r_color;
                betResults.Rolled = roll;
                betResults.BetType = player_bet_type;
                betResults.Player = placeBet.PlayerName;
                betResults.BetAmount = BetMoney + placeBet.Amount * 2;
            }
            else if ((player_bet_type == "1 to 18") && ((roll >= 1) && (roll <= 18)))
            {
                betResults.Color = r_color;
                betResults.Rolled = roll;
                betResults.BetType = player_bet_type;
                betResults.Player = placeBet.PlayerName;
                betResults.BetAmount = BetMoney + placeBet.Amount * 2;
                betResults.Status = "Win!!";
            }
            else if ((player_bet_type == "19 to 36") 
                && ((roll > 18) && (roll < 37)))
            {
                betResults.Color = r_color;
                betResults.Rolled = roll;
                betResults.BetType = player_bet_type;
                betResults.Player = placeBet.PlayerName;
                betResults.BetAmount = BetMoney + placeBet.Amount * 2;
                betResults.Status = "Win";
            }
            else if ((player_bet_type == "1st 12") && 
                (roll > 0 && roll < 13) || 
                (player_bet_type == "2nd 12") &&
                (roll > 12 && roll < 25) || 
                (player_bet_type == "3rd 12") && 
                (roll > 24 && roll < 37))
            {
                betResults.Color = r_color;
                betResults.Rolled = roll;
                betResults.BetType = player_bet_type;
                betResults.Player = placeBet.PlayerName;
                betResults.BetAmount = BetMoney + placeBet.Amount * 3;
                betResults.Status = "Win";
            }
            else
            {
                betResults.Color = r_color;
                betResults.Rolled = roll;
                betResults.BetType = player_bet_type;
                betResults.Player = placeBet.PlayerName;
                betResults.BetAmount = placeBet.Amount;
                betResults.Status = "Lost";
            }
            //SqliteConnection conn = new SqliteConnection("data source=C:\\Users\\thimas\\source\\repos\\roulette_game_api\\roulette_game_api\\DB\\roullet_db.db");
            var conn = new SQLConnection().DbConnection();
            var res = await conn.ExecuteAsync(@"INSERT INTO BETRESULTS VALUES(@Id, @Player, @Color, @Rolled, @BetAmount, @BetType, @Status);",
                betResults
                );
            return Ok(res);
        }

       

        const double BetMoney = 300.00;//amount required to bet
        [HttpPost("placebet")]
        public ActionResult PlaceBet(PlaceBet placeBet)
        {
            if(placeBet == null)
            {
                return BadRequest("Missing fields");
            }
            if(placeBet.Amount >= BetMoney)
            {
                return Ok(placeBet);
            }
            return BadRequest("You don`t have enough credit");

        }
        [HttpPost("showspin")]
        public ActionResult ShowPreviousSpins() 
        {
            var conn = new SQLConnection().DbConnection();
            var data = conn.ExecuteAsync("SELECT * FROM BETRESULTS");
            return Ok();
        }
    }
}
