using Api.Data;
using Api.Interfaces;
using Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Feedback : ControllerBase
    {
        private readonly PostgresContext _context;
        private readonly IAnalyseWithAzureLanguageService _analyser;
        
        public Feedback(
            PostgresContext context, 
            IAnalyseWithAzureLanguageService analyser
            )
        {
            _context = context;
            _analyser = analyser;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateFeedback(FeedbackMessage feedback)
        {
            if (feedback.UserId <= 0)
            {
                return BadRequest("User id can not be less than 0");
            }
            if (feedback.OrderId <= 0)
            {
                return BadRequest("Order id can not be less than 0");
            }
            if (feedback.Stars <= 0 || feedback.Stars > 5)
            {
                return BadRequest("Stars must be between 1 and 5");
            }
            if (feedback.Message.Length < 50)
            {
                return BadRequest("Message must be longer than 50 symbols");
            }
            if (feedback.Message.Length > 5120)
            {
                return BadRequest("Message must be no longer than 5210 symbols");
            }
            feedback.Id = 0;
            
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
            
            var feedbackWithAnalysis = _analyser.AnalyseSingleFeedback(feedback);
            if (feedbackWithAnalysis.AnalyseInfo != null)
            {
                _context.AzureLanguageAnalyseInfo.Add(feedbackWithAnalysis.AnalyseInfo);
                foreach (var item in feedbackWithAnalysis.AnalyseOpinions)
                {
                    _context.AzureLanguageAnalyseSubjectOpinion.Add(item);                
                }
                await _context.SaveChangesAsync();
            }

            return Created("", feedbackWithAnalysis);
        }
    }
}
