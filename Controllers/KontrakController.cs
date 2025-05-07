using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContractAPI.Data;
using ContractAPI.DTOs;

namespace ContractAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KontrakController : ControllerBase
    {
        private readonly AppDbContext _context;

        public KontrakController(AppDbContext context){
            _context = context;
        }

        [HttpGet("angsuran-jatuh-tempo")]
        public async Task<ActionResult<IEnumerable<KontrakAngsuranDto>>> GetAngsuranJatuhTempo()
        {
            DateTime cutoffDate = new DateTime(2024, 8, 14).ToUniversalTime();
            var result = await _context.Kontraks
                        .Where(k => k.ClientName == "SUGUS")
                        .Include(k => k.JadwalAngsurans)
                        .Select(k => new KontrakAngsuranDto
                        {
                            KontrakNo = k.KontrakNo,
                            ClientName = k.ClientName,
                            TotalAngsuranJatuhTempo = k.JadwalAngsurans
                                .Where(j => j.TanggalJatuhTempo <= cutoffDate)
                                .Sum(j => j.AngsuranPerBulan)
                        })
                        .ToListAsync();

            if(result == null)
                return NotFound();            

            return Ok(result);
        }

    }
}