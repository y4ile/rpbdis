using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab4.Models;

namespace lab4.Controllers
{
    public class DevelopmentDirectionsController : Controller
    {
        private readonly PlansContext _context;

        public DevelopmentDirectionsController(PlansContext context)
        {
            _context = context;
        }

        [ResponseCache(CacheProfileName = "PlansCacheProfile")]
        public async Task<IActionResult> Index()
        {
            var directions = await _context.DevelopmentDirections.Include(dd => dd.StudyPlans).ToListAsync();
            return View(directions);
        }

        [ResponseCache(CacheProfileName = "PlansCacheProfile")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var developmentDirection = await _context.DevelopmentDirections
                .Include(dd => dd.StudyPlans)
                .ThenInclude(sp => sp.User)
                .FirstOrDefaultAsync(m => m.DirectionID == id);

            if (developmentDirection == null)
            {
                return NotFound();
            }

            return View(developmentDirection);
        }
    }
}
