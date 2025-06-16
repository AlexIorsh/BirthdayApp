using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BirthdayApp.Models;

namespace BirthdayApp.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Корневая страница: ближайшие дни рождения (по дню и месяцу, игнорируя год)
    public async Task<IActionResult> Index()
    {
        var today = DateTime.Today;
        var nextWeek = today.AddDays(7);
        var allBirthdays = await _context.Birthdays.ToListAsync();
        var upcomingBirthdays = allBirthdays
            .Where(b =>
                IsBirthdayInRange(b.BirthDate, today, nextWeek)
            )
            .OrderBy(b => GetNextBirthdayDate(b.BirthDate, today))
            .ToList();
        return View(upcomingBirthdays);
    }

    // Проверка, попадает ли день рождения в диапазон ближайшей недели (по дню и месяцу)
    private bool IsBirthdayInRange(DateTime birthDate, DateTime from, DateTime to)
    {
        var thisYearBirthday = new DateTime(from.Year, birthDate.Month, birthDate.Day);
        if (thisYearBirthday < from)
            thisYearBirthday = thisYearBirthday.AddYears(1);
        return thisYearBirthday >= from && thisYearBirthday <= to;
    }

    // Получить дату следующего дня рождения
    private DateTime GetNextBirthdayDate(DateTime birthDate, DateTime from)
    {
        var next = new DateTime(from.Year, birthDate.Month, birthDate.Day);
        if (next < from)
            next = next.AddYears(1);
        return next;
    }
}
