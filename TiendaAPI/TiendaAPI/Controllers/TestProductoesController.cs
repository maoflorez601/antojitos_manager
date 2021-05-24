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
    public class TestProductoesController : ControllerBase
    {
        private readonly TIENDA_DBContext _context;

        public TestProductoesController(TIENDA_DBContext context)
        {
            _context = context;
        }

        // GET: api/TestProductoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestProducto>>> GetTestProductos()
        {
            return await _context.TestProductos.ToListAsync();
        }

        // GET: api/TestProductoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestProducto>> GetTestProducto(int id)
        {
            var testProducto = await _context.TestProductos.FindAsync(id);

            if (testProducto == null)
            {
                return NotFound();
            }

            return testProducto;
        }

        // PUT: api/TestProductoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestProducto(int id, TestProducto testProducto)
        {
            if (id != testProducto.IdProducto)
            {
                return BadRequest();
            }

            _context.Entry(testProducto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestProductoExists(id))
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

        // POST: api/TestProductoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TestProducto>> PostTestProducto(TestProducto testProducto)
        {
            _context.TestProductos.Add(testProducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTestProducto", new { id = testProducto.IdProducto }, testProducto);
        }

        // DELETE: api/TestProductoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestProducto(int id)
        {
            var testProducto = await _context.TestProductos.FindAsync(id);
            if (testProducto == null)
            {
                return NotFound();
            }

            _context.TestProductos.Remove(testProducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TestProductoExists(int id)
        {
            return _context.TestProductos.Any(e => e.IdProducto == id);
        }
    }
}
