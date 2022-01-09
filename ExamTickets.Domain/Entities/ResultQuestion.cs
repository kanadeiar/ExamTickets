using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ExamTickets.Domain.Entities.Base;

namespace ExamTickets.Domain.Entities
{
    /// <summary> Результат вопроса </summary>
    public class ResultQuestion : Entity
    {
        public string QuestionText { get; set; }
        public IEnumerable<ResultQuestionOption> QuestionOptions { get; set; } =
            new List<ResultQuestionOption>();
        public int Selected { get; set; }
        public int Correct { get; set; }

        public int ExamenResultId { get; set; }
        public ExamenResult ExamenResult { get; set; }
    }
}
