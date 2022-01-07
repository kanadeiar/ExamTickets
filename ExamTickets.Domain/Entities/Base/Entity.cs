using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExamTickets.Domain.Entities.Base
{
    /// <summary> Базовая сущность данных </summary>
    public abstract class Entity
    {
        /// <summary> Идентификатор </summary>
        [Key]
        public int Id { get; set; }
        /// <summary> Датовременной штамп </summary>
        [Timestamp]
        public byte[]? Timestamp { get; set; }
    }
}
