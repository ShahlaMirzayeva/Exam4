using System.Diagnostics;
using Exam4.Data;
using Exam4.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam4.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            HomeVm homevm = new()
            {
                Teams = _context.Team.Include(p=>p.Position).ToList()
            };
            return View(homevm);
        }

      

    }
}
