using nowa_aplikacja.Models;
namespace nowa_aplikacja.Repositories
{
    public interface ITaskRepository
    {
        // Metoda do pobierania zadania na podstawie jego ID
        TaskModel Get(int taskId);
        // Metoda do pobierania wszystkich aktywnych zadań
        IQueryable<TaskModel> GetAllActive();

        void Add(TaskModel task);
        void Update(int taskId, TaskModel task);
        void Delete(int taskId);
    }
}
