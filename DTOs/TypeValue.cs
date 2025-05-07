namespace ContractAPI.DTOs
{
    public class TypeValue
    {
        public struct SampleClient
        {
            public const string ClientName = "SUGUS";
        }

        public struct PenaltyRule
        {
            public static readonly DateTime CutoffDate = new DateTime(2024, 8, 14).ToUniversalTime();
            public const decimal DailyFineRate = 0.001m;
            public const int AngsuranKe = 6;
        }
        public struct Days {
            public static readonly DateTime Today = DateTime.Today;
        }
    }
}