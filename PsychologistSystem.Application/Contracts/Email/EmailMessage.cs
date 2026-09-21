namespace PsychologistSystem.Application.Contracts.Email
{
    public record EmailMessage(string To, string Subject, string HtmlBody, string? PlainTextBody = null);
}
