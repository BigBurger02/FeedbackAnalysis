using Api.Interfaces;
using Api.Model;
using Azure;
using Azure.AI.TextAnalytics;

namespace Api.Services;

public class AnalyseWithAzureLanguageService : IAnalyseWithAzureLanguageService
{
    private readonly IConfiguration _config;

    private readonly AzureKeyCredential azureCredentials;
    private readonly Uri azureEndpoint;

    public AnalyseWithAzureLanguageService(IConfiguration config)
    {
        _config = config;
        azureEndpoint = new Uri(_config["AzureLanguageService:Endpoint"]);
        azureCredentials = new AzureKeyCredential(_config["AzureLanguageService:Key1"]);
    }

    public FeedbackWithAnalysis AnalyseSingleFeedback(FeedbackMessage feedback)
    {
        var feedbackWithAnalysis = new FeedbackWithAnalysis();
        feedbackWithAnalysis.FeedbackMessage = feedback;
        
        try
        {
            var documents = new List<string>();
            documents.Add(feedback.Message);

            var client = new TextAnalyticsClient(azureEndpoint, azureCredentials);
            AnalyzeSentimentResultCollection reviews = client.AnalyzeSentimentBatch(
                documents,
                options: new AnalyzeSentimentOptions()
                {
                    IncludeOpinionMining = true
                });
            AnalyzeSentimentResult review = reviews.First();

            feedbackWithAnalysis.AnalyseInfo = new AzureLanguageAnalyseInfo();
            feedbackWithAnalysis.AnalyseInfo.FeedbackId = feedback.Id;
            feedbackWithAnalysis.AnalyseInfo.FeedbackSentiment = review.DocumentSentiment.Sentiment.ToString();
            feedbackWithAnalysis.AnalyseInfo.PositiveScore = review.DocumentSentiment.ConfidenceScores.Positive;
            feedbackWithAnalysis.AnalyseInfo.NegativeScore = review.DocumentSentiment.ConfidenceScores.Negative;
            feedbackWithAnalysis.AnalyseInfo.NeutralScore = review.DocumentSentiment.ConfidenceScores.Neutral;
            feedbackWithAnalysis.AnalyseOpinions = new List<AzureLanguageAnalyseSubjectOpinion>();
            foreach (SentenceSentiment sentence in review.DocumentSentiment.Sentences)
            {
                foreach (SentenceOpinion sentenceOpinion in sentence.Opinions)
                {
                    feedbackWithAnalysis.AnalyseOpinions.Add(new AzureLanguageAnalyseSubjectOpinion()
                    {
                        FeedbackId = feedback.Id,
                        Target = sentenceOpinion.Target.Text,
                        Value = sentenceOpinion.Target.Sentiment.ToString(),
                    });
                }
            }
        }
        catch (RequestFailedException e)
        {
            return feedbackWithAnalysis;
        }

        return feedbackWithAnalysis;
    }
}