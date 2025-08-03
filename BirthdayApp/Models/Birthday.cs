using System;

namespace BirthdayApp.Models
{
    /// <summary>
    /// Модель дня рождения
    /// </summary>
    public class Birthday
    {
        /// <summary>
        /// Уникальный идентификатор дня рождения
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя человека
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Путь к фотографии (опционально)
        /// </summary>
        public string? PhotoPath { get; set; }

        /// <summary>
        /// Хобби и интересы (опционально)
        /// </summary>
        public string? Hobbies { get; set; }
    }
}
