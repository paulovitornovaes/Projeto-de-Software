using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Iduff.Models;

namespace Iduff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IduffContext _context;

        public AlunoController(IduffContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Certificado>>> GetCertificado()
        {
            return await _context.Certificado.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Certificado>> GetCertificado(long id)
        {
            var certificado = await _context.Certificado.FindAsync(id);

            if (certificado == null)
            {
                return NotFound();
            }

            return certificado;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCertificado(long id, Certificado certificado)
        {
            if (id != certificado.Id)
            {
                return BadRequest();
            }

            _context.Entry(certificado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificadoExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Certificado>> PostCertificado(Certificado certificado)
        {
            _context.Certificado.Add(certificado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCertificado", new { id = certificado.Id }, certificado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertificado(long id)
        {
            var certificado = await _context.Certificado.FindAsync(id);
            if (certificado == null)
            {
                return NotFound();
            }

            _context.Certificado.Remove(certificado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CertificadoExists(long id)
        {
            return _context.Certificado.Any(e => e.Id == id);
        }
    }
}
