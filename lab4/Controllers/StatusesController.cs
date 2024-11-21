using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab4.Models;

namespace lab4.Controllers
{
    public class StatusesController : Controller
    {
        private readonly PlansContext _context;

        public StatusesController(PlansContext context)
        {
            _context = context;
        }

        [ResponseCache(CacheProfileName = "PlansCacheProfile")]
        public async Task<IActionResult> Index()
        {
            var statuses = await _context.Statuses.Include(s => s.PlanStages).ToListAsync();
            return View(statuses);
        }

        [ResponseCache(CacheProfileName = "PlansCacheProfile")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _context.Statuses
                .Include(s => s.PlanStages)
                .ThenInclude(ps => ps.Plan)
                .FirstOrDefaultAsync(m => m.StatusId == id);

            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }
    }
}
