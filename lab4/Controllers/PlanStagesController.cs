using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab4.Models;

namespace lab4.Controllers
{
    public class PlanStagesController : Controller
    {
        private readonly PlansContext _context;

        public PlanStagesController(PlansContext context)
        {
            _context = context;
        }

        [ResponseCache(CacheProfileName = "PlansCacheProfile")]
        public async Task<IActionResult> Index()
        {
            var planStages = await _context.PlanStages
                .Include(ps => ps.Plan)
                .Include(ps => ps.Status)
                .ToListAsync();
            return View(planStages);
        }

        [ResponseCache(CacheProfileName = "PlansCacheProfile")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planStage = await _context.PlanStages
                .Include(ps => ps.Plan)
                .Include(ps => ps.Status)
                .FirstOrDefaultAsync(m => m.StageID == id);

            if (planStage == null)
            {
                return NotFound();
            }

            return View(planStage);
        }
    }
}
