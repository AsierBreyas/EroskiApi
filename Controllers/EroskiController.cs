using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EroskiApi.Models;

namespace EroskiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EroskiController : ControllerBase
    {
        private readonly EroskiContext _context;

        public EroskiController(EroskiContext context)
        {
            _context = context;
        }

        // GET: api/Eroski
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departamento>>> GetEroski()
        {
            return await _context.Eroski.ToListAsync();
        }

        // GET: api/Eroski/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Departamento>> GetDepartamento(string id)
        {
            var departamento = await _context.Eroski.FindAsync(id);

            if (departamento == null)
            {
                return NotFound();
            }

            return departamento;
        }

        // PUT: api/Eroski/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartamento(string id, Departamento departamento)
        {
            if (id != departamento.nombre)
            {
                return BadRequest();
            }

            // _context.Entry(departamento).State = EntityState.Modified;
            _context.Eroski.Find(id).numero += 1;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartamentoExists(id))
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
        [HttpOptions("{id}")]
        public async Task<IActionResult> ResetDepartamento(string id, Departamento departamento){
            if(id != departamento.nombre){
                return BadRequest();
            }
            _context.Eroski.Find(id).numero = 0;
            try{
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException){
                if(!DepartamentoExists(id)){
                    return NotFound();
                }else{
                    throw;
                }
            }
            return NoContent();
        }

        // // POST: api/Eroski
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<Departamento>> PostDepartamento(Departamento departamento)
        // {
        //     _context.Eroski.Add(departamento);
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateException)
        //     {
        //         if (DepartamentoExists(departamento.nombre))
        //         {
        //             return Conflict();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return CreatedAtAction("GetDepartamento", new { id = departamento.nombre }, departamento);
        // }

        // // DELETE: api/Eroski/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteDepartamento(string id)
        // {
        //     var departamento = await _context.Eroski.FindAsync(id);
        //     if (departamento == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Eroski.Remove(departamento);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        private bool DepartamentoExists(string id)
        {
            return _context.Eroski.Any(e => e.nombre == id);
        }
    }
}
