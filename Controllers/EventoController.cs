﻿using Iduff.Contracts;
using Iduff.Dtos;
using Iduff.Models;
using Iduff.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Iduff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EventoController : ControllerBase
    {
        
        private readonly IduffContext _context;
        private readonly IEventoService _eventoService;

        public EventoController(IduffContext context, IEventoService eventoService)
        {
            _context = context;
            _eventoService = eventoService;
        }
        
        [HttpPost("SalvaPresencaEvento")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SalvaPresencaEvento(IFormFile file, [FromForm] EventoDto eventoDto)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("O arquivo está vazio.");
            }

            var evento = await _eventoService.SalvaEvento(eventoDto);
            await _eventoService.SalvaPresencaEvento(file, evento);
    
            return Ok("Presenças contabilizadas.");
        }

    }
}
