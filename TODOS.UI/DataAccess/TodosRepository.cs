using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOS.UI.Models;

namespace TODOS.UI.DataAccess
{
    public class TodosRepository: ITodosRepository
    {
        //variables declaration
        private ITodosContext _todosContext;
        private ILogger _logger;

        /// <summary>
        /// Constuctor with Logger and TodosContext injected
        /// </summary>
        /// <param name="logger">ILogger<TodosRepository> object</param>
        /// <param name="todosContext">TodosContext object</param>
        public TodosRepository(ITodosContext todosContext, ILogger<TodosRepository> logger)
        {
            _todosContext = todosContext;
            _logger = logger;
        }

        /// <summary>
        /// gets all To-dos
        /// </summary>
        /// <returns>List<TodoItemModel></returns>
        public List<ToDoItemModel> getAllTodos()
        {
            var list = _todosContext.getAll();

            _logger.LogInformation("TodosRepository.getAllTodos(): All to-dos returned");
            
            return list;
        }

        /// <summary>
        /// gets a to-do element for a search criteria
        /// </summary>
        /// <param name="searchCriteria">Func<in ToDoItemModel, out bool> searchCriteria - the search criteria</param>
        /// <returns>TodoItemModel</returns>
        public ToDoItemModel getTodoBy(Func<ToDoItemModel, bool> searchCriteria)
        {
            var todo = _todosContext.getBy(searchCriteria);

            _logger.LogInformation("TodosRepository.getTodoBy({searchCriteria}): To-do returned");
            
            return todo;
        }

        /// <summary>
        /// creates a To-do item
        /// </summary>
        /// <param name="todoItem">ToDoItemModel - the To-do item to create</param>
        /// <returns>int - 1 if successful</returns>
        public int add(ToDoItemModel todoItemModel)
        {
            todoItemModel.id = Guid.NewGuid();
            todoItemModel.lastModified = DateTime.Now;
            
            var result = _todosContext.create(todoItemModel);

            _logger.LogInformation($"TodosRepository.add(): To-do created");

            return result;
        }

        /// <summary>
        /// updates a To-do item
        /// </summary>
        /// <param name="todoItem">ToDoItemModel - the To-do item to update</param>
        /// <returns>int - 1 if successful</returns>
        public int update(ToDoItemModel todoItem)
        {
            todoItem.lastModified = DateTime.Now;
            
            var result = _todosContext.update(todoItem);

            _logger.LogInformation($"TodosRepository.update(): To-do updated");

            return result;
            
        }

        /// <summary>
        /// deletes a To-do item
        /// </summary>
        /// <param name="todoItem">Guid id - the id of the To-do item to delete</param>
        /// <returns>int - 1 if successful</returns>
        public int delete(Guid id)
        {
            var result = _todosContext.delete(id);

            _logger.LogInformation($"TodosRepository.delete(): To-do deleted");

            return result;
        }
    }
}
