namespace Domain.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
