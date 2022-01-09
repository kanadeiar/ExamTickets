using System;
using System.Collections.Generic;
using System.Text;
using ExamTickets.Domain.Entities.Base;

namespace ExamTickets.Domain.Entities
{
    /// <summary> Резлутат - ответ на вопрос </summary>
    public class ResultQuestionOption : Entity
    {
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }

        public int ResultQuestionId { get; set; }
        public ResultQuestion ResultQuestion { get; set; }
    }
}
