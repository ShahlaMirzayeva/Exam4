using Exam4.Data;
using Exam4.Models;
using Exam4.ViewModels;
using Exam4.ViewModels.PositionVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Exam4.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PositionController : Controller
    {
        private readonly AppDbContext _context;
        public PositionController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            PositionVm positionVm = new()
            {
                Positions = await _context.Positions.ToListAsync()
            };
            return View(positionVm);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePositionVm createPositionVm)
        {
            if (createPositionVm == null) return NotFound();
            if (!ModelState.IsValid)
            {
                return View(createPositionVm);
            }
            Position newPosition = new()
            {Name=createPositionVm.Name,
            IsDeleted=false

            };
         await   _context.Positions.AddAsync(newPosition);
      await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Position");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            var position = await _context.Positions.FindAsync(id);
            if (position == null) return NotFound();
            return View(position);
        }
    }
}
