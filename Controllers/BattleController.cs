using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrowserBattle.Models;
using BrowserBattle;

namespace BrowserBattle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattleController : Controller
    {
        private readonly WarriorContext _context;
        private static Random rnd = new Random();

        public BattleController(WarriorContext context)
        {
            _context = context;
        }

        // GET: api/Battle
        [HttpGet]
        public JsonResult GetBattle()
        {
            List<string> BattleSummary = BattleStart();
            return Json(BattleSummary);
        }

        // returns array of strings for BattleSummary
        public List<string> BattleStart()
        {
            // retrieve the only 2 warriors in Db
            var warrior = _context.Warriors.ToList();
            var player1 = warrior.First();
            var player2 = warrior.Last();
            var results = new List<string>();

            // loop giving each warrior a chance to attack and block and update results array
            while (true)
            {
                if (GetAttackResults(player1, player2, results) == "Game Over" 
                    || GetAttackResults(player2, player1, results) == "Game Over")
                {
                    results.Add("Game Over");
                    break;
                }
            } 
            
            return results;
        }

        // accept 2 warriors and array to update with results
        public static string GetAttackResults(Warrior warriorA, Warrior warriorB, List<string> result)
        {
            // calculate one warriors attack and one warriors defense values
            int attAmountA = Attack(warriorA.Attack);
            int defAmountB = Defend(warriorB.Defense);

            // get damage dealt to second warrior
            int damage2B = attAmountA - defAmountB;

            // if there was damage, subtract from second warriors health
            if (damage2B > 0)
            {
                warriorB.Health -= damage2B;
            }
            else damage2B = 0;

            // update results array with info of who attacked and for how much
            result.Add($"{warriorA.Name} attacks {warriorB.Name} and deals {damage2B}");
            result.Add($"{warriorB.Name} has {warriorB.Health} health");

            // check if warriors health fell to zero and if so, end loop
            if (warriorB.Health <= 0)
            {
                result.Add($"{warriorB.Name} has died and {warriorA.Name} is victorious");
                return "Game Over";
            }
            return "Fight Again";
        }

        public static int Attack(int attValue)
        {
            return rnd.Next(1, attValue);
        }

        public static int Defend(int defValue)
        {
            return rnd.Next(1, defValue);
        }
    }
}
