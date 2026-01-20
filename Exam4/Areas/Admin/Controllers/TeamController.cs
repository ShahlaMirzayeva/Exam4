using Exam4.Data;
using Exam4.ViewModels.TeamVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam4.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;

        public TeamController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            TeamVm teamVm = new()
            {
                Teams = await _context.Team.Where(t => t.IsDeleted == false).Include(t => t.Position).ToListAsync(),
                Positions = await _context.Positions.Where(p=>p.IsDeleted==false).ToListAsync()

            };
            return View(teamVm);
        }
    }
}
