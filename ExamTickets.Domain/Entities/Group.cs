using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ExamTickets.Domain.Entities.Base;

namespace ExamTickets.Domain.Entities
{
    /// <summary> Группа экзаменов </summary>
    public class Group : Entity
    {
        public int Order { get; set; }

        [Required(ErrorMessage = "Название группы экзаменов обязательно нужно ввести")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Название группы экзаменов должно быть длинной от 3 до 200 символов")]
        [Display(Name = "Название группы экзаменов")]
        public string Name { get; set; }

        [Display(Name = "Экзамены")]
        public ICollection<Examen> Examens { get; set; } = new List<Examen>();
    }
}
