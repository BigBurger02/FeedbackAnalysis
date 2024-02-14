using Api.Model;

namespace Api.Interfaces;

public interface IAnalyseWithAzureLanguageService
{
    FeedbackWithAnalysis AnalyseSingleFeedback(FeedbackMessage feedback);
}