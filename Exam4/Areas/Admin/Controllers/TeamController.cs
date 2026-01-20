using Exam4.Data;
using Exam4.Models;
using Exam4.ViewModels.TeamVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Exam4.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;

        public TeamController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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

        public IActionResult Create()
        {
            ViewBag.Positions = _context.Positions.Where(p => p.IsDeleted == false);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamVm team,int positionId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = _context.Positions.Where(p=>p.IsDeleted==false).ToList();
                return View(team);
            }

           
            if (team.Photo == null || team.Photo.Length == 0)
            {
                ModelState.AddModelError("Photo", "Şəkil seçilməyib");
                ViewBag.Positions = _context.Positions.ToList();
                return View(team);
            }

            if (!team.Photo.ContentType.StartsWith("image/"))
            {
                ModelState.AddModelError("Photo", "Yalnız şəkil fayllarına icazə verilir");
                return View(team);
            }

            if (team.Photo.Length / 1024.0 / 1024.0 > 2)
            {
                ModelState.AddModelError("Photo", "Şəklin ölçüsü maksimum 2 MB olmalıdır");
                return View(team);
            }

            // 🔹 FILE SAVE
            string extension = Path.GetExtension(team.Photo.FileName);
            string fileName = $"{Guid.NewGuid()}{extension}";
            string folder = "uploads";
            string path = Path.Combine(_webHostEnvironment.WebRootPath, folder);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fullPath = Path.Combine(path, fileName);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                await team.Photo.CopyToAsync(stream);
            }

            // 🔹 ENTITY YARAT
            Team newTeam = new Team
            {
                Name = team.Name,
                Surname = team.Surname,
                PositionId = team.PositionId,
                Image = $"{folder}/{fileName}"
            };

            await _context.Team.AddAsync(newTeam);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
