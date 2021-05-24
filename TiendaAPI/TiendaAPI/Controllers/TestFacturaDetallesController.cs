using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaAPI.Models;

namespace TiendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestFacturaDetallesController : ControllerBase
    {
        private readonly TIENDA_DBContext _context;

        public TestFacturaDetallesController(TIENDA_DBContext context)
        {
            _context = context;
        }

        // GET: api/TestFacturaDetalles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestFacturaDetalle>>> GetTestFacturaDetalles()
        {
            return await _context.TestFacturaDetalles.ToListAsync();
        }

        // GET: api/TestFacturaDetalles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestFacturaDetalle>> GetTestFacturaDetalle(decimal id)
        {
            var testFacturaDetalle = await _context.TestFacturaDetalles.FindAsync(id);

            if (testFacturaDetalle == null)
            {
                return NotFound();
            }

            return testFacturaDetalle;
        }

        // PUT: api/TestFacturaDetalles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestFacturaDetalle(decimal id, TestFacturaDetalle testFacturaDetalle)
        {
            if (id != testFacturaDetalle.IdFacturaDetalle)
            {
                return BadRequest();
            }

            _context.Entry(testFacturaDetalle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestFacturaDetalleExists(id))
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

        // POST: api/TestFacturaDetalles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TestFacturaDetalle>> PostTestFacturaDetalle(TestFacturaDetalle testFacturaDetalle)
        {
            _context.TestFacturaDetalles.Add(testFacturaDetalle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTestFacturaDetalle", new { id = testFacturaDetalle.IdFacturaDetalle }, testFacturaDetalle);
        }

        // DELETE: api/TestFacturaDetalles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestFacturaDetalle(decimal id)
        {
            var testFacturaDetalle = await _context.TestFacturaDetalles.FindAsync(id);
            if (testFacturaDetalle == null)
            {
                return NotFound();
            }

            _context.TestFacturaDetalles.Remove(testFacturaDetalle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TestFacturaDetalleExists(decimal id)
        {
            return _context.TestFacturaDetalles.Any(e => e.IdFacturaDetalle == id);
        }
    }
}
