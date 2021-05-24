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
    public class TestClientesController : ControllerBase
    {
        private readonly TIENDA_DBContext _context;

        public TestClientesController(TIENDA_DBContext context)
        {
            _context = context;
        }

        // GET: api/TestClientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestCliente>>> GetTestClientes()
        {
            return await _context.TestClientes.ToListAsync();
        }

        // GET: api/TestClientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestCliente>> GetTestCliente(int id)
        {
            var testCliente = await _context.TestClientes.FindAsync(id);

            if (testCliente == null)
            {
                return NotFound();
            }

            return testCliente;
        }

        //Método que devuelve un cliente buscándolo por tipo de documento
        // GET: api/TestClientes/Documento/1
        [ActionName("Documento")]
        [HttpGet("Documento/{documento}")]
        public ActionResult<TestCliente> GetTestClienteByID(int documento)
        {
            var testCliente = _context.TestClientes.Where(s => s.Identifiacion == documento).First(); ;

            if (testCliente == null)
            {
                return NotFound();
            }

            return testCliente;
        }

        // PUT: api/TestClientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestCliente(int id, TestCliente testCliente)
        {
            if (id != testCliente.IdCliente)
            {
                return BadRequest();
            }

            _context.Entry(testCliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestClienteExists(id))
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

        // POST: api/TestClientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TestCliente>> PostTestCliente(TestCliente testCliente)
        {
            _context.TestClientes.Add(testCliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTestCliente", new { id = testCliente.IdCliente }, testCliente);
        }

        // DELETE: api/TestClientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestCliente(int id)
        {
            var testCliente = await _context.TestClientes.FindAsync(id);
            if (testCliente == null)
            {
                return NotFound();
            }

            _context.TestClientes.Remove(testCliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TestClienteExists(int id)
        {
            return _context.TestClientes.Any(e => e.IdCliente == id);
        }
    }
}
