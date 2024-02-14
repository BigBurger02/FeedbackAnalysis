namespace Client.Model;

public class FeedbackWithAnalysis
{
    public FeedbackMessage FeedbackMessage { get; set; }
    public AzureLanguageAnalyseInfo AnalyseInfo { get; set; }
    public ICollection<AzureLanguageAnalyseSubjectOpinion> AnalyseOpinions { get; set; }
}