namespace ProjectExample.Modules.Medias.Response
{
    public abstract class ScheduleResponse
    {
        public TimeSpan? TimeStart { get; set; }
        public TimeSpan? TimeEnd { get; set; }
        public int? MediaId { get; set; }
    }
}
