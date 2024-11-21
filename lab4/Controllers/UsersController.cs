using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab4.Models;
using System.Threading.Tasks;
using System.Linq;

namespace lab4.Controllers
{
    public class UsersController : Controller
    {
        private readonly PlansContext _context;

        public UsersController(PlansContext context)
        {
            _context = context;
        }

        [ResponseCache(CacheProfileName = "PlansCacheProfile")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        [ResponseCache(CacheProfileName = "PlansCacheProfile")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
