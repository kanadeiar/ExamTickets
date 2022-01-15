using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ExamTickets.Domain.Entities.Base;

namespace ExamTickets.Domain.Entities
{
    /// <summary> Экзаменационный билет </summary>
    public class Ticket : Entity
    {
        [Required(ErrorMessage = "Текст названия билета обязательно нужно ввести")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Текст названия билета должен быть длинной от 3 до 200 символов")]
        [Display(Name = "Билет")]
        public string TicketText { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Экзамен должен быть выбран")]
        [Display(Name = "Экзамен")]
        public int ExamenId { get; set; }
        /// <summary> Экзамен </summary>
        [ForeignKey(nameof(ExamenId))]
        public Examen Examen { get; set; }

        [Display(Name = "Вопросы экзаменационного билета")]
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
