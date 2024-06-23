using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace nowa_aplikacja.Models
{
    // Klasa kontekstu dla zarządzania zadaniami, dziedziczy po DbContext
    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext(DbContextOptions options) : base(options)
        {
        }
        // DbSet reprezentujący kolekcję zadań w bazie danych
        public DbSet<TaskModel> Tasks { get; set; }
    }
}
