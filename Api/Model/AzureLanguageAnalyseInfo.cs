namespace Api.Model;

public class AzureLanguageAnalyseInfo
{
    public int Id { get; set; }
    public int FeedbackId { get; set; }
    public string FeedbackSentiment { get; set; } = String.Empty;
    public double PositiveScore { get; set; }
    public double NegativeScore { get; set; }
    public double NeutralScore { get; set; }
}