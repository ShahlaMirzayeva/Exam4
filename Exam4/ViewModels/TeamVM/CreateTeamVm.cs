using Exam4.Models;

namespace Exam4.ViewModels.TeamVM
{
    public class CreateTeamVm
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public IFormFile Photo { get; set; }

        public int PositionId { get; set; }
    }
}
