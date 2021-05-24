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
    public class TestFacturasController : ControllerBase
    {
        private readonly TIENDA_DBContext _context;

        public TestFacturasController(TIENDA_DBContext context)
        {
            _context = context;
        }

        // GET: api/TestFacturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestFactura>>> GetTestFacturas()
        {
            return await _context.TestFacturas.ToListAsync();
        }

        // GET: api/TestFacturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestFactura>> GetTestFactura(int id)
        {
            var testFactura = await _context.TestFacturas.FindAsync(id);

            if (testFactura == null)
            {
                return NotFound();
            }

            return testFactura;
        }

        // PUT: api/TestFacturas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestFactura(int id, TestFactura testFactura)
        {
            if (id != testFactura.IdFactura)
            {
                return BadRequest();
            }

            _context.Entry(testFactura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestFacturaExists(id))
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

        // POST: api/TestFacturas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TestFactura>> PostTestFactura(TestFactura testFactura)
        {
            _context.TestFacturas.Add(testFactura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTestFactura", new { id = testFactura.IdFactura }, testFactura);
        }

        // DELETE: api/TestFacturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestFactura(int id)
        {
            var testFactura = await _context.TestFacturas.FindAsync(id);
            if (testFactura == null)
            {
                return NotFound();
            }

            _context.TestFacturas.Remove(testFactura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TestFacturaExists(int id)
        {
            return _context.TestFacturas.Any(e => e.IdFactura == id);
        }
    }
}
