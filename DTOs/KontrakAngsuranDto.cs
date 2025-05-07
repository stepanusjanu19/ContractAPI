using System.Text.Json.Serialization;

namespace ContractAPI.DTOs
{
    public class KontrakAngsuranDto
    {
        public string KontrakNo { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? TotalAngsuranJatuhTempo { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? InstallmentNo { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? HariKeterlambatan { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? TotalDenda { get; set; }
    }

}