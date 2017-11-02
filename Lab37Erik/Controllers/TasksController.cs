using Lab37Erik.Data;
using Lab37Erik.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab37Erik.Controllers
{
    //This route is using a routing token
    [Route("api/[Controller]")]
    public class TasksController : ControllerBase
    {
        private readonly GamesDbContext _context;

        public TasksController(GamesDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Produces("application/json")]
        public IEnumerable<Games> Get()
        {
            return _context.Games;

        }

        //Get
        //This code uses binding to pass in an id
        [HttpGet("{id:int?}")] 
        public IActionResult Get(int id)
        {
            var result = _context.Games.FirstOrDefault(h => h.Id == id);
            return Ok(result);
        }

        //Post 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody]Games item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = item.Id }, item);
        }
        //Put 
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody]Games item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var check = _context.Games.FirstOrDefault(i => i.Id == id);
            if (check != null)
            {
                check.Name = item.Name;
                check.Genre = item.Genre;
                _context.Update(check);
                await _context.SaveChangesAsync();
                return Ok();

            }
            return BadRequest();
        }


        //Delete 
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = _context.Games.FirstOrDefault(h => h.Id == id);
            if (result != null)
            {
                _context.Games.Remove(result);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

    }
}
