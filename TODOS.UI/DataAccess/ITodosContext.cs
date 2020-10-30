using System;
using System.Collections.Generic;
using TODOS.UI.Models;

namespace TODOS.UI.DataAccess
{
    public interface ITodosContext
    {
        int create(ToDoItemModel todoItem);
        int delete(Guid id);
        List<ToDoItemModel> getAll();
        ToDoItemModel getBy(Func<ToDoItemModel, bool> searchCriteria);
        int update(ToDoItemModel todoItem);
    }
}