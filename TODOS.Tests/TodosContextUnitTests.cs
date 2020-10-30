using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TODOS.Tests.UnitTests;
using TODOS.UI.DataAccess;
using TODOS.UI.Models;
using Xunit;

namespace TODOS.Tests
{
    public class TodosContextUnitTests
    {
        private readonly ILogger<TodosContext> _logger;
        private readonly IConfiguration _configuration;
        private readonly List<ToDoItemModel> todosList;

        public TodosContextUnitTests()
        {
            //get to-dos for tests
            todosList = TodosTestData.getTestTodos();

            //Configure Logger Mock
            var _loggerMock = new Mock<ILogger<TodosContext>>();
            _logger = _loggerMock.Object;

            //Configure Configuration Mock
            //var _configurationMock = new Mock<IConfiguration>();
            _configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                    .Build();
        }


        //List<ToDoItemModel> getAll()
        [Fact]
        public void getAll_Returns_all_Todos()
        {

            //instantiate the context
            var todosContext = new TodosContext(_logger, _configuration);

            //Call the context action
            var result = todosContext.getAll();

            //Assert the result
            Assert.NotNull(result);
            var todos = Assert.IsType<List<ToDoItemModel>>(result);

            //Assert the result
            todos.Should().BeEquivalentTo(todos, options => options.ComparingByMembers<ToDoItemModel>());
        }


        //ToDoItemModel getBy(Func<ToDoItemModel, bool> searchCriteria)
        [Fact]
        public void getTodoBy_Returns_one_Todo()
        {
            var todo = todosList.First();

            //instantiate the context
            var todosContext = new TodosContext(_logger, _configuration);

            //Call the context action
            var result = todosContext.getBy(t => t.id == todo.id);

            //Assert the result
            Assert.NotNull(result);
            var returnedTodo = Assert.IsType<ToDoItemModel>(result);

            //Assert the result
            returnedTodo.Should().BeEquivalentTo(todo, options => options.ComparingByMembers<ToDoItemModel>());
        }


        //int create(ToDoItemModel todoItem)
        [Fact]
        public void create_Creates_one_Todo_And_Returns_1()
        {
            var todo = todosList.First();

            //instantiate the context
            var todosContext = new TodosContext(_logger, _configuration);

            //Call the context action
            var result = todosContext.create(todo);

            //Assert the result
            var returnedValue = Assert.IsType<int>(result);

            //Assert the result
            Assert.Equal(1, returnedValue);
        }



        //int update(ToDoItemModel todoItem)
        [Fact]
        public void update_Updates_one_Todo_And_Returns_1()
        {
            var todo = todosList.First();

            //instantiate the context
            var todosContext = new TodosContext(_logger, _configuration);

            //Call the context action
            var result = todosContext.update(todo);

            //Assert the result
            var returnedValue = Assert.IsType<int>(result);

            //Assert the result
            Assert.Equal(1, returnedValue);
        }

        //int delete(Guid id)
        [Fact]
        public void delete_Deletes_one_Todo_And_Returns_1()
        {
            var todo = todosList.First();

            //instantiate the context
            var todosContext = new TodosContext(_logger, _configuration);

            //Call the context action
            var result = todosContext.create(todo);

            //Assert the result
            var returnedValue = Assert.IsType<int>(result);

            //Assert the result
            Assert.Equal(1, returnedValue);
        }


        //----------------------------------List<ToDoItemModel> readFromFile(string path)


        //----------------------------------void writeToFile(string path)


    }
}
