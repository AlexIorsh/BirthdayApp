using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BirthdayApp.Models;
using BirthdayApp.Services;

namespace BirthdayApp.Controllers;

/// <summary>
/// Контроллер главной страницы
/// </summary>
public class HomeController : Controller
{
    private readonly IBirthdayService _birthdayService;

    /// <summary>
    /// Конструктор контроллера
    /// </summary>
    /// <param name="birthdayService">Сервис для работы с днями рождения</param>
    public HomeController(IBirthdayService birthdayService)
    {
        _birthdayService = birthdayService;
    }

    /// <summary>
    /// Отображает главную страницу с предстоящими и сегодняшними днями рождения
    /// </summary>
    /// <returns>Представление главной страницы</returns>
    public async Task<IActionResult> Index()
    {
        var todayBirthdays = await _birthdayService.GetTodayBirthdaysAsync();
        var upcomingBirthdays = await _birthdayService.GetUpcomingBirthdaysAsync();

        var viewModel = new HomeViewModel
        {
            TodayBirthdays = todayBirthdays.ToList(),
            UpcomingBirthdays = upcomingBirthdays.ToList()
        };

        return View(viewModel);
    }

    /// <summary>
    /// Отображает страницу конфиденциальности
    /// </summary>
    /// <returns>Представление страницы конфиденциальности</returns>
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Обрабатывает ошибки
    /// </summary>
    /// <returns>Представление ошибки</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
