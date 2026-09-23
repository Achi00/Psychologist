namespace PsychologistSystem.Domain.Entity
{
    public sealed class BookingSettings
    {
        public string TimeZoneId { get; set; } = "Asia/Tbilisi";
        public int SlotLengthMinutes { get; set; } = 60;
        public int BookingWindowDays { get; set; } = 30;
        // can't book a slot starting in 5 min
        public int MinutesBeforeBookingAllowed { get; set; } = 60;  
        public int CancellationCutoffMinutes { get; set; } = 30;
    }
}
