using System;

namespace BirthdayApp.Models
{
    /// <summary>
    /// Модель статистики дней рождения
    /// </summary>
    public class BirthdayStatistics
    {
        /// <summary>
        /// Общее количество дней рождения
        /// </summary>
        public int TotalBirthdays { get; set; }

        /// <summary>
        /// Количество сегодняшних дней рождения
        /// </summary>
        public int TodayBirthdays { get; set; }

        /// <summary>
        /// Количество предстоящих дней рождения в течение недели
        /// </summary>
        public int UpcomingBirthdays { get; set; }

        /// <summary>
        /// Количество дней рождения в этом месяце
        /// </summary>
        public int ThisMonthBirthdays { get; set; }

        /// <summary>
        /// Количество дней рождения в следующем месяце
        /// </summary>
        public int NextMonthBirthdays { get; set; }

        /// <summary>
        /// Средний возраст всех людей
        /// </summary>
        public double AverageAge { get; set; }

        /// <summary>
        /// Самый молодой возраст
        /// </summary>
        public int YoungestAge { get; set; }

        /// <summary>
        /// Самый старший возраст
        /// </summary>
        public int OldestAge { get; set; }

        /// <summary>
        /// Ближайший день рождения
        /// </summary>
        public Birthday? NextBirthday { get; set; }

        /// <summary>
        /// Количество дней до ближайшего дня рождения
        /// </summary>
        public int DaysUntilNextBirthday { get; set; }
    }
} 