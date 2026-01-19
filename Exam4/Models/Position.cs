namespace Exam4.Models
{
    public class Position:BaseEntity
    {
        public ICollection<Team> Teams { get; set; }
    }
}
