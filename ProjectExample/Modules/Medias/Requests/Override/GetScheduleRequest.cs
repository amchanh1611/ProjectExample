namespace ProjectExample.Modules.Medias.Requests
{
    public class GetScheduleRequest : GetRequest
    {
        public DateTime? DateFrom { get; set; } 
        public DateTime? DateTo { get; set; }
        public int? MediaId { get; set; }
    }
}