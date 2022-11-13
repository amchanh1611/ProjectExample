using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectExample.Modules.Medias.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public string Description { get; set; } = default!;
        public int MediaId { get; set; }
        public Media Media { get; set; } = default!;
    }
}
