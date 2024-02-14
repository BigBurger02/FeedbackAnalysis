namespace Client.Model;

public class AzureLanguageAnalyseSubjectOpinion
{
    public int Id { get; set; }
    public int FeedbackId { get; set; }
    public string Target { get; set; }
    public string Value { get; set; }
}