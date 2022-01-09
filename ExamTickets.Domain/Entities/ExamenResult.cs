using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ExamTickets.Domain.Entities.Base;

namespace ExamTickets.Domain.Entities
{
    /// <summary> Результаты экзамена </summary>
    public class ExamenResult : Entity
    {
        [Display(Name = "Датавремя проведения экзамена")]
        public DateTime DateTime { get; set; }
        [Display(Name = "Фамилия")]
        public string SurName { get; set; } = string.Empty;
        [Display(Name = "Имя")]
        public string FirstName { get; set; } = string.Empty;
        [Display(Name = "Отчетсво")]
        public string Patronymic { get; set; } = string.Empty;
        [Display(Name = "Дата рождения")]
        public DateTime Birthday { get; set; } = DateTime.Today.AddYears(-18);
        [Display(Name = "Название экзамена")]
        public string ExamenName { get; set; }
        [Display(Name = "Название билета")]
        public string TicketName { get; set; }

        [Display(Name = "Количетсво верных ответов")]
        public int ValidCount { get; set; }
        [Display(Name = "Количетсво ошибочных ответов")]
        public int ErrorCount { get; set; }
        [Display(Name = "Количество ответов")]
        public int Count { get; set; }

        [Display(Name = "Количество прошедшего времени с начала экзамена")]
        public TimeSpan SpanRunningExam { get; set; }
        [Display(Name = "Количество минут выделенное на экзамен")]
        public int CountMinutesToExam { get; set; }

        /// <summary> Статус результата экзамена </summary>
        public TypeStatusExam GetStatusExam
        {
            get
            {
                if (SpanRunningExam.TotalMinutes > CountMinutesToExam)
                    return TypeStatusExam.Untimed;
                if (ValidCount >= Count - MaxErrorsCount)
                    return TypeStatusExam.Good;
                if (ErrorCount > MaxErrorsCount)
                    return TypeStatusExam.Bad;
                return TypeStatusExam.Uncompleted;
            }
        }
        [NotMapped, Display(Name = "Статус результата экзамена")]
        public string GetStatusExamStr => GetStatusExam switch
        {
            TypeStatusExam.Untimed => "Закончилось время",
            TypeStatusExam.Good => "Успешно сдан",
            TypeStatusExam.Bad => "Не сдан",
            _ => "Незавершен",
        };

        /// <summary> Максимальное количество допустимых неправильных ответов </summary>
        public int MaxErrorsCount { get; set; }

        #region Вспомогательное

        /// <summary> Вид статуса результата экзамена </summary>
        public enum TypeStatusExam
        {
            /// <summary> Успешно сдан </summary>
            Good,
            /// <summary> Провален </summary>
            Bad,
            /// <summary> Незавершен </summary>
            Uncompleted,
            /// <summary> Закончилось время </summary>
            Untimed,
        }

        #endregion
    }
}
