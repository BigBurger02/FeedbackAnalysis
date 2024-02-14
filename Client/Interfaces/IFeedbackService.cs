using Client.Model;

namespace Client.Interfaces;

public interface IFeedbackService
{
    Task<FeedbackWithAnalysis> PostFeedback(FeedbackMessage feedback);
}