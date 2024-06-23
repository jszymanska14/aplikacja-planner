using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nowa_aplikacja.Models
{
    [Table("Tasks")]
    public class TaskModel
    {
        [Key]
        public int TaskId { get; set; }

        [DisplayName("Nazwa")]
        [Required(ErrorMessage = "Pole Nazwa jest wymagane.")]
        [MaxLength(50, ErrorMessage = "Nazwa nie może być dłuższa niż 50 znaków.")]
        public string Name { get; set; }

        [DisplayName("Opis")]
        [MaxLength(2000, ErrorMessage = "Opis nie może być dłuższy niż 2000 znaków.")]
        public string Description { get; set; }

        public bool Done { get; set; }
    }
}