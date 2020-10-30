using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOS.UI.Models;

namespace TODOS.Tests.UnitTests
{
    public static class TodosTestData
    {
        public static List<ToDoItemModel> getTestTodos()
        {
            return new List<ToDoItemModel> {
                new ToDoItemModel()
                {
                    id = new Guid("ce7b282b-5382-42fc-86c6-f0b9082f8d4c"),
                    title = "Tape game",
                    content = "don't forget to tape tomorrow's game on TV",
                    lastModified = Convert.ToDateTime("2020-10-28T15:01:16.1563491-04:00")
                },
                new ToDoItemModel()
                {
                    id = new Guid("ac53e7bb-b986-4e61-8891-232dfe32c7c8"),
                    title = "Check phone covers",
                    content = "Check for a new phone cover for cellphone protection",
                    lastModified = Convert.ToDateTime("2020-10-28T16:03:12.4163981-04:00")
                },
                new ToDoItemModel()
                {
                    id = new Guid("1552ef26-50b0-4c00-bab0-6bf284609a0a"),
                    title = "Find a used bicycle",
                    content = "Check boards for a used bicycle in good state. Must have working breaks and shifter.",
                    lastModified = Convert.ToDateTime("2020-10-28T20:39:54.903768-04:00")
                }
            };
        }
    }
}
