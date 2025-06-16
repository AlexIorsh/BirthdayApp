using System;

namespace BirthdayApp.Models
{
    public class Birthday
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string? PhotoPath { get; set; } // Путь к фотографии, допускает null
        public string? Hobbies { get; set; } // Хобби и интересы
    }
}
