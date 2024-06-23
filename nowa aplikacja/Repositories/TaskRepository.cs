using nowa_aplikacja.Models;
using System.Linq;

namespace nowa_aplikacja.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagerContext _context;

        // Konstruktor przyjmujący kontekst bazy danych jako zależność
        public TaskRepository(TaskManagerContext context)
        {
            _context = context;
        }

        // Metoda do pobierania zadania na podstawie jego ID
        public TaskModel Get(int taskId)
            => _context.Tasks.SingleOrDefault(x => x.TaskId == taskId);

        // Metoda do pobierania wszystkich aktywnych zadań
        public IQueryable<TaskModel> GetAllActive()
            => _context.Tasks.Where(x => !x.Done);

        // Metoda do dodawania nowego zadania
        public void Add(TaskModel task)
        {
            _context.Tasks.Add(task); 
            _context.SaveChanges(); 
        }

        // Metoda do aktualizacji istniejącego zadania na podstawie jego ID
        public void Update(int taskId, TaskModel task)
        {
            var result = _context.Tasks.SingleOrDefault(x => x.TaskId == taskId); // Pobranie zadania do aktualizacji
            if (result != null)
            {
                result.Name = task.Name; 
                result.Description = task.Description; 
                result.Done = task.Done; 

                _context.SaveChanges(); 
            }
        }

        // Metoda do usuwania zadania na podstawie jego ID
        public void Delete(int taskId)
        {
            var result = _context.Tasks.SingleOrDefault(x => x.TaskId == taskId); // Pobranie zadania do usunięcia
            if (result != null)
            {
                _context.Tasks.Remove(result); 
                _context.SaveChanges(); 
            }
        }
    }
}
