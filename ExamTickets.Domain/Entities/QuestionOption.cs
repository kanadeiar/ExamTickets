using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExamTickets.Domain.Entities.Base;

namespace ExamTickets.Domain.Entities
{
    /// <summary> Вариант ответа с отметками </summary>
    public class QuestionOption : Entity
    {
        [Required(ErrorMessage = "Текст ответа обязательно нужно ввести")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Ответ должен быть длинной от 3 до 200 символов")]
        [Display(Name = "Ответ")]
        public string OptionText { get; set; }

        [Display(Name = "Признак правильного ответа на вопрос")]
        public bool IsCorrect { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Должен быть выбран вопрос")]
        [Display(Name = "Вопрос")]
        public int QuestionId { get; set; }
        /// <summary> Вопрос </summary>
        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; }

        #region Действия пользователя

        /// <summary> Отмечен пользователем как ответ </summary>
        public bool IsUserChecked { get; set; }

        #endregion
    }
}
