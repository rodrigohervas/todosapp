using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOS.UI.Controllers;
using TODOS.UI.DataAccess;
using TODOS.UI.Models;
using Xunit;

namespace TODOS.Tests.UnitTests
{
    public class TodosControllerUnitTests
    {
        private readonly List<ToDoItemModel> todosList;
        private readonly ILogger<TodosController> _logger;
        private readonly Mock<ITodosRepository> _mockRepository;

        public TodosControllerUnitTests()
        {
            //get to-dos for tests
            todosList = TodosTestData.getTestTodos();

            //Configure Logger Mock
            var _loggerMock = new Mock<ILogger<TodosController>>();
            _logger = _loggerMock.Object;

            //Mock Repository initialization
            _mockRepository = new Mock<ITodosRepository>();
        }


        //[HttpGet] - Index()
        [Fact]
        public void Index_Returns_all_Todos()
        {
            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.getAllTodos()).Returns(todosList);

            //instantiate the controller
            var controller = new TodosController(_mockRepository.Object, _logger);

            //Call the controller action
            var result = controller.Index();

            //Assert the result
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = viewResult.Model;

            //Assert the model
            model.Should().BeEquivalentTo(todosList, options => options.ComparingByMembers<ToDoItemModel>());
        }


        //[HttpGet] - public IActionResult _Details(Guid id)
        [Fact]
        public void _Details_Returns_one_Todo()
        {
            var todo = todosList.First();

            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.getTodoBy(It.IsAny<Func<ToDoItemModel, bool>>())).Returns(todo);

            //instantiate the controller
            var controller = new TodosController(_mockRepository.Object, _logger);

            //Call the controller action
            var result = controller._Details(todo.id);

            //Assert the result
            Assert.NotNull(result);
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            var model = partialViewResult.Model;

            //Assert the model
            model.Should().BeEquivalentTo(todo, options => options.ComparingByMembers<ToDoItemModel>());
        }

        //[HttpGet] - public IActionResult _AddTodo()
        [Fact]
        public void _AddTodo_Creates_one_Todo_And_Returns_PartialView()
        {
            var todo = todosList.First();

            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.add(todo)).Returns(1);

            //instantiate the controller
            var controller = new TodosController(_mockRepository.Object, _logger);

            //Call the controller action
            var result = controller._AddTodo(todo);

            //Assert the result
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            var model = partialViewResult.Model;

            //Assert the model
            model.Should().BeEquivalentTo(todo, options => options.ComparingByMembers<ToDoItemModel>());
        }

        //[HttpGet] - public IActionResult _UpdateTodo(Guid id)
        [Fact]
        public void _UpdateTodo_Gets_one_Todo_And_Returns_PartialView()
        {
            var todo = todosList.First();

            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.getTodoBy(It.IsAny<Func<ToDoItemModel, bool>>())).Returns(todo);

            //instantiate the controller
            var controller = new TodosController(_mockRepository.Object, _logger);

            //Call the controller action
            var result = controller._UpdateTodo(todo.id);

            //Assert the result
            Assert.NotNull(result);
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            var model = partialViewResult.Model;

            //Assert the model
            model.Should().BeEquivalentTo(todo, options => options.ComparingByMembers<ToDoItemModel>());
        }

        //[HttpPost] - public IActionResult _UpdateTodo([FromForm] ToDoItemModel todoItem)
        [Fact]
        public void _UpdateTodo_Updates_one_Todo_And_Returns_A_PartialView()
        {
            var todo = todosList.First();

            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.update(todo)).Returns(1);

            //instantiate the controller
            var controller = new TodosController(_mockRepository.Object, _logger);

            //Call the controller action
            var result = controller._UpdateTodo(todo);

            //Assert the result
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            var model = partialViewResult.Model;

            //Assert the model
            model.Should().BeEquivalentTo(todo, options => options.ComparingByMembers<ToDoItemModel>());
        }


        //[HttpGet] - public IActionResult _DeleteTodo(Guid id)
        [Fact]
        public void _DeleteTodo_Gets_one_Todo_And_Returns_PartialView()
        {
            var todo = todosList.First();

            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.getTodoBy(It.IsAny<Func<ToDoItemModel, bool>>())).Returns(todo);

            //instantiate the controller
            var controller = new TodosController(_mockRepository.Object, _logger);

            //Call the controller action
            var result = controller._DeleteTodo(todo.id);

            //Assert the result
            Assert.NotNull(result);
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            var model = partialViewResult.Model;

            //Assert the model
            model.Should().BeEquivalentTo(todo, options => options.ComparingByMembers<ToDoItemModel>());
        }


        //[HttpPost] - public IActionResult _DeleteTodo([FromForm] ToDoItemModel todoItem)
        [Fact]
        public void _DeleteTodo_Deletes_one_Todo_And_Returns_PartialView()
        {
            var todo = todosList.First();

            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.delete(todo.id)).Returns(1);

            //instantiate the controller
            var controller = new TodosController(_mockRepository.Object, _logger);

            //Call the controller action
            var result = controller._DeleteTodo(todo);

            //Assert the result
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            var model = partialViewResult.Model;

            //Assert the model
            model.Should().BeEquivalentTo(todo, options => options.ComparingByMembers<ToDoItemModel>());
        }

    }
}
