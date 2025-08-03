using System;

namespace BirthdayApp.Models
{
    /// <summary>
    /// Модель представления для главной страницы
    /// </summary>
    public class HomeViewModel
    {
        /// <summary>
        /// Сегодняшние дни рождения
        /// </summary>
        public List<Birthday> TodayBirthdays { get; set; } = new List<Birthday>();

        /// <summary>
        /// Предстоящие дни рождения в течение недели
        /// </summary>
        public List<Birthday> UpcomingBirthdays { get; set; } = new List<Birthday>();
    }
} 