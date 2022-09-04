using ToDoMauiClient.Models;

namespace ToDoMauiClient.DataServices
{
    public interface IRestDataService
    {
        Task<List<ToDo>> GetAllToDosAsync();

        Task AddTodoAsync(ToDo toDo);

        Task UpdateDoto(ToDo toDo); 

        Task DeleteTodoAsync(int id);
    }
}