using Exam4.Models;

namespace Exam4.ViewModels.TeamVM
{
    public class TeamVm
    {
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<Position> Positions { get; set; }
    }
}
