using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iduff.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Iduff.Models;
using Iduff.Services.Interfaces;

namespace Iduff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificadoController : ControllerBase
    {
        private readonly IduffContext _context;
        private readonly ICertificadoService _certificadoService;

        public CertificadoController(IduffContext context, ICertificadoService certificadoService)
        {
            _context = context;
            _certificadoService = certificadoService;
        }
        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<Certificado>>> GetCertificados(string? id)
        {
            List<Certificado> certificados;

            if (string.IsNullOrEmpty(id))
            {
                certificados = await _context.Certificados.ToListAsync();
            }
            else
            {
                certificados = await _context.Certificados.Where(c => c.AlunoId == id).ToListAsync();
            }

            return Ok(certificados);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Certificado>> GetCertificado(long id)
        {
            var certificado = await _context.Certificados.FindAsync(id);

            if (certificado == null)
            {
                return NotFound();
            }

            return certificado;
        }
        
        /*
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
        */
        
        /*
        [HttpPost]
        public async Task<ActionResult<Certificado>> PostCertificado(Certificado certificado)
        {
            _context.Certificados.Add(certificado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCertificado", new { id = certificado.Id }, certificado);
        }
        */
        /*
        [HttpPost("Load")]
        public async Task<ActionResult<Certificado>> LoadCertificados(Certificado certificado)
        {
            _context.Certificados.Add(certificado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCertificado", new { id = certificado.Id }, certificado);
        }
        */
        
        /*
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertificado(long id)
        {
            var certificado = await _context.Certificados.FindAsync(id);
            if (certificado == null)
            {
                return NotFound();
            }

            _context.Certificados.Remove(certificado);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        */
        
        
        /*
        [HttpPost("LoadFromCsv")]
        [Consumes("multipart/form-data")] // Adicionando o atributo Consumes para permitir upload de arquivo
        public async Task<OkObjectResult> LoadFromCsv(IFormFile file, [FromForm] EventoDto eventoDto)
        {
            
            
            List<string[]> data = new List<string[]>();
            var evento = await _certificadoService.MapearEvento(eventoDto);
            //await _certificadoService.MapearFormulario(file);
            
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var values = line.Split(',');
                    data.Add(values);
                }
            }

            return Ok("O arquivo CSV está no formato correto.");
        }
*/
        private bool CertificadoExists(long id)
        {
            return _context.Certificados.Any(e => e.Id == id);
        }
    }
}
