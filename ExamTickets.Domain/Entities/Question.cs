using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExamTickets.Domain.Entities.Base;

namespace ExamTickets.Domain.Entities
{
    /// <summary> Вопрос с вариантами ответов и правильными ответами </summary>
    public class Question : Entity
    {
        [Required(ErrorMessage = "Текст вопроса обязательно нужно ввести")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Вопрос должен быть длинной от 3 до 200 символов")]
        [Display(Name = "Вопрос")]
        public string QuestionText { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Экзаменационный билет быть выбран")]
        [Display(Name = "Экзаменационный билет")]
        public int TicketId { get; set; }
        /// <summary> Экзаменационный билет </summary>
        [ForeignKey(nameof(TicketId))]
        public Ticket Ticket { get; set; }

        [Display(Name = "Варианты ответов с отметками")]
        public ICollection<QuestionOption> QuestionOptions { get; set; } = new List<QuestionOption>();
        
        #region Действия пользователя

        /// <summary> На этот вопрос пользователь ответил </summary>
        public bool Done { get; set; }

        #endregion
    }
}
