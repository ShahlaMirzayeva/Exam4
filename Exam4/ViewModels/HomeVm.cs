using Exam4.Models;
using System.Net.Http.Headers;

namespace Exam4.ViewModels
{
    public class HomeVm
    {
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<Position> Positions { get; set; }
    }
}
