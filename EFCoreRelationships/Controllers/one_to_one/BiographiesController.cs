using EFCoreRelationships.Data.Entities.one_to_one;
using MailboxApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationships.Controllers.one_to_one
{
    // BiographiesController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class BiographiesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public BiographiesController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Biography>>> GetBiographies()
        {
            return await _context.Biographies.Include(b => b.Author).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Biography>> GetBiography(int id)
        {
            var biography = await _context.Biographies.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);

            if (biography == null)
            {
                return NotFound();
            }

            return biography;
        }

        [HttpPost]
        public async Task<ActionResult<Biography>> CreateBiography(Biography biography)
        {
            _context.Biographies.Add(biography);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBiography), new { id = biography.Id }, biography);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBiography(int id, Biography biography)
        {
            if (id != biography.Id)
            {
                return BadRequest();
            }

            _context.Entry(biography).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BiographyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBiography(int id)
        {
            var biography = await _context.Biographies.FindAsync(id);
            if (biography == null)
            {
                return NotFound();
            }

            _context.Biographies.Remove(biography);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BiographyExists(int id)
        {
            return _context.Biographies.Any(b => b.Id == id);
        }
    }

}
