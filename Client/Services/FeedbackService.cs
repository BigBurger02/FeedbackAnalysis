using System.Net.Http.Json;
using Client.Interfaces;
using Client.Model;

namespace Client.Services;

public class FeedbackService : IFeedbackService
{
    private readonly HttpClient httpClient;

    public FeedbackService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<FeedbackWithAnalysis> PostFeedback(FeedbackMessage feedback)
    {
        var response = await httpClient.PostAsJsonAsync<FeedbackMessage>("api/Feedback", feedback);
        return await response.Content.ReadFromJsonAsync<FeedbackWithAnalysis>() ?? new FeedbackWithAnalysis();
    }
}