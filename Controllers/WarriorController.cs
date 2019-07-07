using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrowserBattle.Models;

namespace BrowserBattle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarriorController : ControllerBase
    {
        private readonly WarriorContext _context;

        public WarriorController(WarriorContext context)
        {
            _context = context;

            if (_context.Warriors.Count() == 0)
            {
                // Create new warrior item if collection is empty
                _context.Warriors.Add(new Warrior { Name = "Warrior1", Health = 500, Attack = 250, Defense = 250 });
                _context.Warriors.Add(new Warrior { Name = "Warrior2", Health = 500, Attack = 250, Defense = 250 });
                _context.SaveChanges();
            }
        }

        // GET: api/Warrior
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Warrior>>> GetWarriors()
        {
            return await _context.Warriors.ToListAsync();
        }

        // PUT: api/Warrior/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarrior(long id, Warrior item)
        {
            System.Diagnostics.Debug.WriteLine("jo mama");
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}