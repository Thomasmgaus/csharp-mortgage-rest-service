using System.Text.Json.Serialization;

namespace mortgage_application.Dto
{
    public class ApplicantDto
    {
        [JsonPropertyName("principleAmount")] public decimal PrincipleAmount { get; set; }
        [JsonPropertyName("annualRate")] public decimal AnnualRate { get; set; }
        [JsonPropertyName("loanYears")] public int LoanYears { get; set; }
        [JsonPropertyName("startDate")] public DateTime? StartDate { get; set; }
    }
}
