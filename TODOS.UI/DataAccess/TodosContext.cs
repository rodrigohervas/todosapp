using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TODOS.UI.Models;

namespace TODOS.UI.DataAccess
{
    public class TodosContext : ITodosContext
    {
        //variables declaration
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly string rootFolder;
        private readonly string filePath;
        private readonly List<ToDoItemModel> todosList;

        /// <summary>
        /// Constuctor with Logger and configuration injected
        /// </summary>
        /// <param name="logger">ILogger<TodosContext> object</param>
        /// <param name="configuration">IConfiguration object</param>
        public TodosContext(ILogger<TodosContext> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            rootFolder = Directory.GetCurrentDirectory();
            filePath = String.Concat(rootFolder, _configuration.GetValue<string>("todosFilePath"));
            todosList = readFromFile(filePath);
        }

        /// <summary>
        /// gets all To-dos
        /// </summary>
        /// <returns>List<TodoItemModel></returns>
        public List<ToDoItemModel> getAll()
        {
            var list = todosList;

            _logger.LogInformation("Context.getAll(): All to-dos returned");

            return list;
        }

        /// <summary>
        /// gets a to-do element for a search criteria
        /// </summary>
        /// <param name="searchCriteria">Func<in ToDoItemModel, out bool> searchCriteria - the search criteria</param>
        /// <returns>TodoItemModel</returns>
        public ToDoItemModel getBy(Func<ToDoItemModel, bool> searchCriteria)
        {
            var todo = todosList.First(searchCriteria);

            _logger.LogInformation($"Context.getBy({searchCriteria}): To-do returned");

            return todo;
        }

        /// <summary>
        /// creates a To-do item
        /// </summary>
        /// <param name="todoItem">ToDoItemModel - the To-do item to create</param>
        /// <returns>int - 1 if successful</returns>
        public int create(ToDoItemModel todoItem)
        {
            todosList.Add(todoItem);

            writeToFile(filePath);

            _logger.LogInformation($"Context.create(): To-do created");

            return 1;
        }

        /// <summary>
        /// updates a To-do item
        /// </summary>
        /// <param name="todoItem">ToDoItemModel - the To-do item to update</param>
        /// <returns>int - 1 if successful</returns>
        public int update(ToDoItemModel todoItem)
        {
            ToDoItemModel removableTodo = todosList.First(todo => todo.id == todoItem.id);
            todosList.Remove(removableTodo);

            todosList.Add(todoItem);

            writeToFile(filePath);

            _logger.LogInformation($"Context.update(): To-do updated");

            return 1;
        }

        /// <summary>
        /// deletes a To-do item
        /// </summary>
        /// <param name="todoItem">Guid id - the id of the To-do item to delete</param>
        /// <returns>int - 1 if successful</returns>
        public int delete(Guid id)
        {
            var todo = this.getBy(todo => todo.id == id);

            todosList.Remove(todo);

            writeToFile(filePath);

            _logger.LogInformation($"Context.delete(): To-do deleted");

            return 1;
        }

        /// <summary>
        /// gets to-dos from json formatted text file
        /// </summary>
        /// <param name="path">string - full path to the text file containing to-dos</param>
        /// <returns>List<ToDoItemModel></returns>
        private List<ToDoItemModel> readFromFile(string path)
        {
            if (File.Exists(path))
            {
                List<ToDoItemModel> todosList = new List<ToDoItemModel>();
                string jsonToDos = File.ReadAllText(filePath);
                if (jsonToDos != "")
                {
                    _logger.LogInformation($"Context.readFromFile(): There are To-dos stored in the text file");

                    todosList.AddRange(JsonConvert.DeserializeObject<List<ToDoItemModel>>(jsonToDos));
                }

                _logger.LogInformation($"Context.readFromFile(): To-dos read from text file");

                return todosList;
            }

            return null;
        }

        /// <summary>
        /// writes to-dos into a json formatted text file. If file doesn't exist it gets created.
        /// </summary>
        /// <param name="path">string - full path to the text file to write the to-dos to.</param>
        private void writeToFile(string path)
        {
            if (File.Exists(path))
            {
                string allTodos = JsonConvert.SerializeObject(todosList);
                File.WriteAllText(filePath, allTodos);

                _logger.LogInformation($"Context.writeToFile(): To-dos written to text file");
            }
        }
    }
}
