using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContractAPI.Models
{
    [Table("jadwal_angsuran")]
    public class JadwalAngsuran
    {
        [Column("kontrak_no")]
        [ForeignKey("Kontrak")]
        public string KontrakNo { get; set; } = string.Empty;
        [Column("angsuran_ke")]
        public int AngsuranKe { get; set; }
        [Column("angsuran_per_bulan")]
        public decimal AngsuranPerBulan { get; set; }
        [Column("tanggal_jatuh_tempo")]
        public DateTime TanggalJatuhTempo { get; set; }

        public Kontrak Kontrak { get; set; } = null!;
    }
}