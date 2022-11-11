namespace ProjectExample.Modules.Medias.Requests
{
    public class GetScheduleRequest : GetRequest
    {
        public DateTime DateFrom { get; set; } = DateTime.Now;
        public DateTime DateTo { get; set; } = DateTime.Now;
        public int? MediaId { get; set; }
    }
}