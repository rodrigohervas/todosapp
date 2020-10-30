using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TODOS.Tests.UnitTests;
using TODOS.UI.DataAccess;
using TODOS.UI.Models;
using Xunit;

namespace TODOS.Tests
{
    public class TodosRepositoryUnitTests
    {
        private readonly List<ToDoItemModel> todosList;
        private readonly ILogger<TodosRepository> _logger;
        private readonly Mock<ITodosContext> _mockContext;

        public TodosRepositoryUnitTests()
        {
            //get to-dos for tests
            todosList = TodosTestData.getTestTodos();

            //Configure Logger Mock
            var _loggerMock = new Mock<ILogger<TodosRepository>>();
            _logger = _loggerMock.Object;

            //Mock Context initialization
            _mockContext = new Mock<ITodosContext>();
        }


        //List<ToDoItemModel> getAllTodos()
        [Fact]
        public void getAllTodos_Returns_all_Todos()
        {
            //specify the mockContext return
            _mockContext.Setup(context => context.getAll()).Returns(todosList);

            //instantiate the repository
            var todosRepository = new TodosRepository(_mockContext.Object, _logger);

            //Call the repository method
            var result = todosRepository.getAllTodos();

            //Assert the result
            Assert.NotNull(result);
            var todos = Assert.IsType<List<ToDoItemModel>>(result);
            
            //Assert the result
            todos.Should().BeEquivalentTo(todosList, options => options.ComparingByMembers<ToDoItemModel>());
        }


        //ToDoItemModel getTodoBy(Func<ToDoItemModel, bool> searchCriteria)
        [Fact]
        public void getTodoBy_Returns_one_Todo()
        {
            var todo = todosList.First();

            //specify the mockContext return
            _mockContext.Setup( context => context.getBy(It.IsAny<Func<ToDoItemModel, bool>>())).Returns(todo);

            //instantiate the repository
            var todosRepository = new TodosRepository(_mockContext.Object, _logger);

            //Call the repository method
            var result = todosRepository.getTodoBy(t => t.id == todo.id);

            //Assert the result
            Assert.NotNull(result);
            var returnedTodo = Assert.IsType<ToDoItemModel>(result);

            //Assert the result
            returnedTodo.Should().BeEquivalentTo(todo, options => options.ComparingByMembers<ToDoItemModel>());
        }


        //int add(ToDoItemModel todoItemModel)
        [Fact]
        public void add_Creates_one_Todo_And_Returns_1()
        {
            var todo = todosList.First();

            //specify the mockContext return
            _mockContext.Setup(context => context.create(todo)).Returns(1);

            //instantiate the repository
            var todosRepository = new TodosRepository(_mockContext.Object, _logger);

            //Call the repository method
            var result = todosRepository.add(todo);

            //Assert the result
            var returnedValue = Assert.IsType<int>(result);
            Assert.Equal(1, returnedValue);
        }



        //int update(ToDoItemModel todoItem)
        [Fact]
        public void update_Updates_one_Todo_And_Returns_1()
        {
            var todo = todosList.First();

            //specify the mockContext return
            _mockContext.Setup(context => context.update(todo)).Returns(1);

            //instantiate the repository
            var todosRepository = new TodosRepository(_mockContext.Object, _logger);

            //Call the repository method
            var result = todosRepository.update(todo);

            //Assert the result
            var returnedValue = Assert.IsType<int>(result);
            Assert.Equal(1, returnedValue);
        }

        //int delete(Guid id)
        [Fact]
        public void delete_Deletes_one_Todo_And_Returns_1()
        {
            var todo = todosList.First();

            //specify the mockContext return
            _mockContext.Setup(context => context.delete(It.IsAny<Guid>())).Returns(1);

            //instantiate the repository
            var todosRepository = new TodosRepository(_mockContext.Object, _logger);

            //Call the repository method
            var result = todosRepository.delete(todo.id);

            //Assert the result
            var returnedValue = Assert.IsType<int>(result);
            Assert.Equal(1, returnedValue);
        }


    }
}
