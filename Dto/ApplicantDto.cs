using System.Text.Json.Serialization;

namespace mortgage_application.Dto
{
    public class ApplicantDto
    {
        [JsonPropertyName("name")] public string ApplicantName { get; set; }
        [JsonPropertyName("principleAmount")] public long PrincipleAmount { get; set; }
        [JsonPropertyName("annualRate")] public double AnnualRate { get; set; }
        [JsonPropertyName("loanMonths")] public int LoanMonths { get; set; }
        [JsonPropertyName("startDate")] public DateTime StartDate { get; set; }
    }
}
