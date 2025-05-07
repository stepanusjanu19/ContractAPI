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
        private readonly DateTime cutoffDate;
        

        public KontrakController(AppDbContext context){
            this._context = context;
            this.cutoffDate = TypeValue.PenaltyRule.CutoffDate;
        }

        [HttpGet("angsuran-jatuh-tempo")]
        public async Task<ActionResult<IEnumerable<KontrakAngsuranDto>>> GetAngsuranJatuhTempo()
        {
            var result = await this._context.Kontraks
                        .Where(k => k.ClientName == TypeValue.SampleClient.ClientName)
                        .Include(k => k.JadwalAngsurans)
                        .Select(k => new KontrakAngsuranDto
                        {
                            KontrakNo = k.KontrakNo,
                            ClientName = k.ClientName,
                            TotalAngsuranJatuhTempo = k.JadwalAngsurans
                                .Where(j => j.TanggalJatuhTempo <= this.cutoffDate)
                                .Sum(j => j.AngsuranPerBulan)
                        })
                        .ToListAsync();

            if(result == null || !result.Any())
                return NotFound();            

            return Ok(result);
        }

        [HttpGet("angsuran-telat")]
        public async Task<ActionResult<IEnumerable<KontrakAngsuranDto>>> GetAngsuranTelatPembayaran()
        {
            var effectiveDate = TypeValue.Days.Today > this.cutoffDate ? TypeValue.Days.Today : this.cutoffDate;

            var rawData = await this._context.Kontraks
                .Where(k => k.ClientName == TypeValue.SampleClient.ClientName)
                .Include(k => k.JadwalAngsurans)
                .ToListAsync();

            if(rawData == null || !rawData.Any())
                return NotFound(); 

            var result = rawData
                .SelectMany(k => k.JadwalAngsurans
                    .Where(j => j.TanggalJatuhTempo < this.cutoffDate && j.AngsuranKe >= TypeValue.PenaltyRule.AngsuranKe)
                    .Select(j => new KontrakAngsuranDto
                    {
                        KontrakNo = k.KontrakNo,
                        ClientName = k.ClientName,
                        InstallmentNo = j.AngsuranKe,
                        HariKeterlambatan = (effectiveDate - j.TanggalJatuhTempo).Days,
                        TotalDenda = (effectiveDate - j.TanggalJatuhTempo).Days > 0
                            ? j.AngsuranPerBulan * TypeValue.PenaltyRule.DailyFineRate * (effectiveDate - j.TanggalJatuhTempo).Days
                            : 0
                    })
                )
                .Where(x => x.HariKeterlambatan > 0)
                .ToList();

            if(result == null || !result.Any())
                return NotFound();            

            return Ok(result);
        }

    }
}