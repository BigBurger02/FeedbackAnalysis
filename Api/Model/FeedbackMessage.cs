namespace Api.Model;

public class FeedbackMessage
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OrderId { get; set; }
    public int Stars { get; set; }
    public string Message { get; set; } = String.Empty;
}