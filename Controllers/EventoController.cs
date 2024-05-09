using Iduff.Contracts;
using Iduff.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Iduff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EventoController : ControllerBase
    {
        
        private readonly IduffContext _context;

        public EventoController(IduffContext context)
        {
            _context = context;
        }
        
        [HttpPost("LoadFromCsv")]
        private async Task<OkObjectResult> LoadFromCsv(IFormFile file)
        {
            List<string[]> data = new List<string[]>();

            
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
    }
}
