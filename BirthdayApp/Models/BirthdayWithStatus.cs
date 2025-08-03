using System;

namespace BirthdayApp.Models
{
    /// <summary>
    /// Модель дня рождения с дополнительной информацией о статусе
    /// </summary>
    public class BirthdayWithStatus
    {
        /// <summary>
        /// Основная информация о дне рождения
        /// </summary>
        public Birthday Birthday { get; set; } = new Birthday();

        /// <summary>
        /// Статус дня рождения
        /// </summary>
        public BirthdayStatus Status { get; set; }

        /// <summary>
        /// Количество дней до следующего дня рождения
        /// </summary>
        public int DaysUntilBirthday { get; set; }

        /// <summary>
        /// Возраст на следующий день рождения
        /// </summary>
        public int AgeOnNextBirthday { get; set; }

        /// <summary>
        /// Дата следующего дня рождения
        /// </summary>
        public DateTime NextBirthdayDate { get; set; }
    }

    /// <summary>
    /// Статус дня рождения
    /// </summary>
    public enum BirthdayStatus
    {
        /// <summary>
        /// Сегодня
        /// </summary>
        Today,

        /// <summary>
        /// Скоро (в течение недели)
        /// </summary>
        Soon,

        /// <summary>
        /// Позже (через неделю и более)
        /// </summary>
        Later,

        /// <summary>
        /// Прошел в этом году
        /// </summary>
        Passed
    }
} 