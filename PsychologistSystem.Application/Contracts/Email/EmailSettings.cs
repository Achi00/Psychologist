namespace PsychologistSystem.Application.Contracts.Email
{
    public sealed class EmailSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseTls { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FromAddress { get; set; }
    }
}
