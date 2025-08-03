using Microsoft.AspNetCore.Mvc;
using BirthdayApp.Models;
using BirthdayApp.Services;

namespace BirthdayApp.Controllers
{
    /// <summary>
    /// Контроллер для просмотра дней рождения
    /// </summary>
    public class BirthdayController : Controller
    {
        private readonly IBirthdayService _birthdayService;

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        /// <param name="birthdayService">Сервис для работы с днями рождения</param>
        public BirthdayController(IBirthdayService birthdayService)
        {
            _birthdayService = birthdayService;
        }

        /// <summary>
        /// Отображает список всех дней рождения для просмотра
        /// </summary>
        /// <returns>Представление со списком дней рождения</returns>
        public async Task<IActionResult> Index()
        {
            var birthdaysWithStatus = await _birthdayService.GetBirthdaysWithStatusAsync();
            return View(birthdaysWithStatus);
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
