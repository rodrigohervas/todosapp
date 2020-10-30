using System;
using System.Collections.Generic;
using TODOS.UI.Models;

namespace TODOS.UI.DataAccess
{
    public interface ITodosRepository
    {
        int add(ToDoItemModel todoItemModel);
        int delete(Guid id);
        List<ToDoItemModel> getAllTodos();
        ToDoItemModel getTodoBy(Func<ToDoItemModel, bool> searchCriteria);
        int update(ToDoItemModel todoItem);
    }
}