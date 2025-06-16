using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BirthdayApp.Models;

namespace BirthdayApp.Controllers
{
    public class BirthdayController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BirthdayController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Список всех дней рождения
        public async Task<IActionResult> Index()
        {
            var allBirthdays = await _context.Birthdays.OrderBy(b => b.BirthDate).ToListAsync();
            return View(allBirthdays);
        }

        // Страница добавления нового дня рождения
        public IActionResult Create()
        {
            return View();
        }

        // Обработка формы добавления
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,BirthDate,Hobbies")] Birthday birthday, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null && photo.Length > 0)
                {
                    var fileName = Path.GetFileName(photo.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }
                    birthday.PhotoPath = "/images/" + fileName;
                }
                _context.Add(birthday);
                await _context.SaveChangesAsync();
                // После добавления — переход на главную (список ближайших ДР)
                return RedirectToAction("Index", "Home");
            }
            return View(birthday);
        }

        // Страница редактирования
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var birthday = await _context.Birthdays.FindAsync(id);
            if (birthday == null)
                return NotFound();
            return View(birthday);
        }

        // Обработка формы редактирования
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BirthDate,PhotoPath,Hobbies")] Birthday birthday)
        {
            if (id != birthday.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(birthday);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BirthdayExists(birthday.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction("Index", "Home");
            }
            return View(birthday);
        }

        private bool BirthdayExists(int id)
        {
            return _context.Birthdays.Any(e => e.Id == id);
        }

        // Удаление дня рождения
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var birthday = await _context.Birthdays.FindAsync(id);
            if (birthday != null)
            {
                _context.Birthdays.Remove(birthday);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
