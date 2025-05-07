using System.ComponentModel.DataAnnotations.Schema;

namespace ContractAPI.Models
{
    [Table("kontrak")]
    public class Kontrak
    {
        [Column("kontrak_no")]
        public string KontrakNo { get; set; } = string.Empty;
        [Column("client_name")]
        public string ClientName { get; set; } = string.Empty;
        public decimal OTR { get; set; }
        public List<JadwalAngsuran> JadwalAngsurans { get; set; } = new();
    }
}