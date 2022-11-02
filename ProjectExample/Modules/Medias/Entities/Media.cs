using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectExample.Modules.Medias.Entities
{
    public class Media
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public string File { get; set; } = default!;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public MediaStatus Status { get; set; } = MediaStatus.Active;
        public ICollection<Schedule>? Schedules { get; set; } 
    }
    public enum MediaStatus
    {
        Active,
        CopyrightInfringement
    }
}
