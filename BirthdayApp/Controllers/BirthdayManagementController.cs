using Microsoft.AspNetCore.Mvc;
using BirthdayApp.Models;
using BirthdayApp.Services;

namespace BirthdayApp.Controllers
{
    /// <summary>
    /// Контроллер для управления днями рождения
    /// </summary>
    public class BirthdayManagementController : Controller
    {
        private readonly IBirthdayService _birthdayService;

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        /// <param name="birthdayService">Сервис для работы с днями рождения</param>
        public BirthdayManagementController(IBirthdayService birthdayService)
        {
            _birthdayService = birthdayService;
        }

        /// <summary>
        /// Отображает список всех дней рождения для управления
        /// </summary>
        /// <returns>Представление со списком дней рождения для управления</returns>
        public async Task<IActionResult> Index()
        {
            var birthdaysWithStatus = await _birthdayService.GetBirthdaysWithStatusAsync();
            return View(birthdaysWithStatus);
        }

        /// <summary>
        /// Отображает форму для создания нового дня рождения
        /// </summary>
        /// <returns>Представление формы создания</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Обрабатывает форму создания нового дня рождения
        /// </summary>
        /// <param name="birthday">Модель дня рождения</param>
        /// <param name="photo">Загруженная фотография</param>
        /// <returns>Результат обработки формы</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,BirthDate,Hobbies")] Birthday birthday, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                await _birthdayService.CreateBirthdayAsync(birthday, photo);
                return RedirectToAction("Index");
            }
            return View(birthday);
        }

        /// <summary>
        /// Отображает форму для редактирования дня рождения
        /// </summary>
        /// <param name="id">Идентификатор дня рождения</param>
        /// <returns>Представление формы редактирования</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var birthday = await _birthdayService.GetBirthdayByIdAsync(id.Value);
            if (birthday == null)
                return NotFound();

            return View(birthday);
        }

        /// <summary>
        /// Обрабатывает форму редактирования дня рождения
        /// </summary>
        /// <param name="id">Идентификатор дня рождения</param>
        /// <param name="birthday">Модель дня рождения</param>
        /// <returns>Результат обработки формы</returns>
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
                    await _birthdayService.UpdateBirthdayAsync(birthday);
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
                {
                    if (!await _birthdayService.BirthdayExistsAsync(birthday.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction("Index");
            }
            return View(birthday);
        }

        /// <summary>
        /// Удаляет день рождения
        /// </summary>
        /// <param name="id">Идентификатор дня рождения для удаления</param>
        /// <returns>Результат удаления</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _birthdayService.DeleteBirthdayAsync(id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Отображает детальную информацию о дне рождения
        /// </summary>
        /// <param name="id">Идентификатор дня рождения</param>
        /// <returns>Представление с детальной информацией</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var birthday = await _birthdayService.GetBirthdayByIdAsync(id.Value);
            if (birthday == null)
                return NotFound();

            var today = DateTime.Today;
            var nextBirthday = _birthdayService.GetNextBirthdayDate(birthday.BirthDate, today);
            var daysUntilBirthday = _birthdayService.CalculateDaysUntilBirthday(birthday.BirthDate, today);
            var ageOnNextBirthday = _birthdayService.CalculateAgeOnNextBirthday(birthday.BirthDate, today);
            var status = _birthdayService.GetBirthdayStatus(birthday.BirthDate, today);

            var birthdayWithDetails = new BirthdayWithStatus
            {
                Birthday = birthday,
                Status = status,
                DaysUntilBirthday = daysUntilBirthday,
                AgeOnNextBirthday = ageOnNextBirthday,
                NextBirthdayDate = nextBirthday
            };

            return View(birthdayWithDetails);
        }
    }
} 