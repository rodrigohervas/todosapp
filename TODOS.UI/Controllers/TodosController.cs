using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TODOS.UI.DataAccess;
using TODOS.UI.Models;

namespace TODOS.UI.Controllers
{
    public class TodosController : Controller
    {
        /// <summary>
        /// Logger and TodosRepository variable declarations
        /// </summary>
        private readonly ILogger<TodosController> _logger;
        private readonly ITodosRepository _todosRepository;

        /// <summary>
        /// Constuctor with Logger and TodosRepository injected
        /// </summary>
        /// <param name="logger">ILogger<TodosController> object</param>
        /// <param name="todosRepository">TodosRepository object</param>
        public TodosController(ITodosRepository todosRepository, ILogger<TodosController> logger)
        {
            _logger = logger;
            _todosRepository = todosRepository;
        }

        /// <summary>
        /// handles get requests for Index action, and returns the index view with all the todos
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<ToDoItemModel> todos = null;

                if (ModelState.IsValid)
                {
                    _logger.LogInformation("TodosController.Index: Index view requested");

                    todos = _todosRepository.getAllTodos();

                    _logger.LogInformation("TodosController.Index: Index view ready for return");
                }
                else
                {
                    _logger.LogInformation($"TodosController.Index - Get: Modelstate Invalid.");
                }

                return View(todos);
            }
            catch (Exception ex)
            {
                _logger.LogError(LogMessage.generateMessage(ex));
                throw;
            }            
        }

        /// <summary>
        /// handles get requests for _Details action, and returns the _Details partial view with the data for one Todo
        /// </summary>
        /// <param name="id">Guid object - the Id of a Todo</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public IActionResult _Details(Guid id)
        {
            try
            {
                ToDoItemModel todo = null;

                if (ModelState.IsValid)
                {
                    _logger.LogInformation("TodosController._Details - Get: _Details partial view requested");

                    todo = _todosRepository.getTodoBy(todo => todo.id == id);

                    _logger.LogInformation("TodosController._Details: _Details partial view ready for return");
                }
                else
                {
                    _logger.LogInformation($"TodosController._Details - Get: Modelstate Invalid.");
                }

                return PartialView("_Details", todo);
            }
            catch (Exception ex)
            {
                _logger.LogError(LogMessage.generateMessage(ex));
                throw;
            }
        }

        /// <summary>
        /// handles get requests for _AddTodo action, and returns the _AddTodo partial view
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public IActionResult _AddTodo()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("TodosController._AddTodo - Get: _AddTodo partial view requested");
                }
                else
                {
                    _logger.LogInformation($"TodosController._AddTodo - Get: Modelstate Invalid.");
                }

                return PartialView();
            }
            catch (Exception ex)
            {
                _logger.LogError(LogMessage.generateMessage(ex));
                throw;
            }
        }

        /// <summary>
        /// handles post requests for _AddTodo action. Adds the todo to the Todos collection and redirects to Index action
        /// </summary>
        /// <param name="id">Guid object - the Id of a Todo</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult _AddTodo([FromForm] ToDoItemModel todoItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("TodosController._AddTodo - Post: _AddTodo partial view requested");

                    _todosRepository.add(todoItem);

                    _logger.LogInformation("TodosController._AddTodo - Post: _AddTodo partial view ready for return");
                }
                else 
                {
                    _logger.LogInformation($"TodosController._AddTodo - Post: Modelstate Invalid.");
                }

                return RedirectToAction("Index");
                
            }
            catch (Exception ex)
            {
                _logger.LogError(LogMessage.generateMessage(ex));
                throw;
            }
        }

        /// <summary>
        /// handles get requests for _UpdateTodo action, and returns the _UpdateTodo partial view with the data for the todo to update
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public IActionResult _UpdateTodo(Guid id)
        {
            try
            {
                ToDoItemModel todo = null;

                if (ModelState.IsValid)
                {
                    _logger.LogInformation("TodosController._UpdateTodo - Get: _UpdateTodo partial view requested");

                    todo = _todosRepository.getTodoBy(todo => todo.id == id);

                    _logger.LogInformation("TodosController._UpdateTodo - Get: _UpdateTodo partial view ready for return");
                }
                else
                {
                    _logger.LogInformation($"TodosController._UpdateTodo - Get: Modelstate Invalid.");
                }

                return PartialView("_UpdateTodo", todo);
            }
            catch (Exception ex)
            {
                _logger.LogError(LogMessage.generateMessage(ex));
                throw;
            }
            
        }

        /// <summary>
        /// handles post requests for _UpdateTodo action. Updates the todo in the Todos collection and redirects to Index action
        /// </summary>
        /// <param name="todoItem">ToDoItemModel - the todoItem to update</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public IActionResult _UpdateTodo([FromForm] ToDoItemModel todoItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("TodosController._UpdateTodo - Post: _UpdateTodo partial view requested");

                    _todosRepository.update(todoItem);

                    _logger.LogInformation("TodosController._UpdateTodo - Post: _UpdateTodo partial view ready for return");
                }
                else
                {
                    _logger.LogInformation($"TodosController._UpdateTodo - Post: Modelstate Invalid.");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogMessage.generateMessage(ex));
                throw;
            }
        }

        /// <summary>
        /// handles get requests for _DeleteTodo action, and returns the _DeleteTodo partial view with the data for the todo to delete
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public IActionResult _DeleteTodo(Guid id)
        {
            try
            {
                ToDoItemModel todo = null;
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("TodosController._DeleteTodo - Get: _DeleteTodo partial view requested");

                    todo = _todosRepository.getTodoBy(todo => todo.id == id);

                    _logger.LogInformation("TodosController._DeleteTodo - Get: _DeleteTodo partial view ready for return");
                }
                else
                {
                    _logger.LogInformation($"TodosController._DeleteTodo - Get: Modelstate Invalid.");
                }

                return PartialView(todo);
            }
            catch (Exception ex)
            {
                _logger.LogError(LogMessage.generateMessage(ex));
                throw;
            }
        }

        /// <summary>
        /// handles post requests for _DeleteTodo action. Deletes the todo from the Todos collection and redirects to Index action
        /// </summary>
        /// <param name="todoItem">ToDoItemModel - the todoItem to delete</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public IActionResult _DeleteTodo([FromForm] ToDoItemModel todoItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("TodosController._DeleteTodo - Post: _DeleteTodo partial view requested");

                    _todosRepository.delete(todoItem.id);

                    _logger.LogInformation("TodosController._DeleteTodo - Post: _DeleteTodo partial view ready for return");
                }
                else
                {
                    _logger.LogInformation($"TodosController._DeleteTodo - Post: Modelstate Invalid.");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogMessage.generateMessage(ex));
                throw;
            }
        }


        /// <summary>
        /// handles requests for the Error view, to display errors
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
