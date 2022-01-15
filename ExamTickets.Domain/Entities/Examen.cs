using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ExamTickets.Domain.Entities.Base;

namespace ExamTickets.Domain.Entities
{
    /// <summary> Экзамен </summary>
    public class Examen : Entity
    {
        public int Order { get; set; }

        [Required(ErrorMessage = "Название экзамена обязательно нужно ввести")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Название экзамена должно быть длинной от 3 до 200 символов")]
        [Display(Name = "Название экзамена")]
        public string Name { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Группа экзаменов должы быть выбрана")]
        [Display(Name = "Группа экзаменов")]
        public int GroupId { get; set; }
        /// <summary> Группа экзаменов </summary>
        [ForeignKey(nameof(GroupId))]
        public Group Group { get; set; }

        [Display(Name = "Экзаменационные билеты")]
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
