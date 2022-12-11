using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SticksAndStones.Models.DAL;
using System.Linq;
using System.Threading.Tasks;

namespace SticksAndStones.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SiteAdmin")]
    public class UsersController : Controller
    {
        private SiteDataContext _siteData;

        public UsersController(SiteDataContext siteData)
        {
            _siteData = siteData;
        }

        public IActionResult Index()
        {
            return View(_siteData.Users.ToList());
        }

        public IActionResult Details(string id)
        {
            if (_siteData.Users.Any(u => u.Id == id))
                return View(_siteData.Users.First(u => u.Id == id));
            else
                return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user
                = await _siteData.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _siteData.Users.FindAsync(id);
            _siteData.Users.Remove(user);
            await _siteData.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult DropTheBanhammer(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user
                = _siteData.Users
                .FirstOrDefault(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            user.DropBanhammer();
            _siteData.Users.Update(user);
            _siteData.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public IActionResult DropTheBanhammer(SticksAndStones.Areas.Admin.Models.Banhammer banhammer)
        {
            if (banhammer.VictimID == null)
            {
                return NotFound();
            }

            var user
                = _siteData.Users
                .FirstOrDefault(m => m.Id == banhammer.VictimID);
            if (user == null)
            {
                return NotFound();
            }

            banhammer.Victim = user;
            
            if (ModelState.IsValid)
            {
                banhammer.Victim.DropBanhammer(banhammer.BanExpires, banhammer.Message);
                _siteData.Users.Update(banhammer.Victim);
                _siteData.SaveChanges();

                return RedirectToActionPermanent(nameof(Index));
            }
            return View("banhammer", banhammer);
        }

        public IActionResult Unbanhammer(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user
                = _siteData.Users
                .FirstOrDefault(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            user.Banned = false;
            _siteData.Users.Update(user);
            _siteData.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Banhammer(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user
                = _siteData.Users
                .FirstOrDefault(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            var banhammer = new SticksAndStones.Areas.Admin.Models.Banhammer()
            {
                VictimID = id,
                Victim = user,
            };

            return View(banhammer);
        }

        public IActionResult OpUser(string userId)
        {
            var adminRole = _siteData.Roles.First(r => r.Name == "SiteAdmin");

            var newAdmin = new IdentityUserRole<string>
            {
                UserId = userId,
                RoleId = adminRole.Id,
            };

            _siteData.UserRoles.Add(newAdmin);
            _siteData.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult DeopUser(string userId)
        {
            var adminRole = _siteData.Roles.First(r => r.Name == "SiteAdmin");

            var adminToRemove = _siteData.UserRoles.Find(userId, adminRole.Id);

            _siteData.UserRoles.Remove(adminToRemove);
            _siteData.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
