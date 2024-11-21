using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab4.Models;
using lab4.ViewModels;
using System.Threading.Tasks;
using System.Linq;

namespace lab4.Controllers
{
    public class StudyPlansController : Controller
    {
        private readonly PlansContext _context;

        public StudyPlansController(PlansContext context)
        {
            _context = context;
        }

        [ResponseCache(CacheProfileName = "PlansCacheProfile")]
        public async Task<IActionResult> Index()
        {
            var studyPlans = await _context.StudyPlans
            .Include(sp => sp.Direction)
            .Include(sp => sp.User)
            .Include(sp => sp.PlanStages)
            .Select(sp => new StudyPlanViewModel
            {
                PlanID = sp.PlanID,
                PlanName = sp.PlanName,
                DirectionName = sp.Direction.DirectionName,
                UserLogin = sp.User.Login,
                NumberOfStages = sp.PlanStages.Count()
            })
            .ToListAsync();

            return View(studyPlans);
        }

        [ResponseCache(CacheProfileName = "PlansCacheProfile")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyPlan = await _context.StudyPlans
                .Include(sp => sp.Direction)
                .Include(sp => sp.User)
                .Include(sp => sp.PlanStages)
                    .ThenInclude(ps => ps.Status)
                .FirstOrDefaultAsync(m => m.PlanID == id);

            if (studyPlan == null)
            {
                return NotFound();
            }

            return View(studyPlan);
        }
    }
}
