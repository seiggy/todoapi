using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoApi.Core.Model;

namespace ToDoApi.Core.Data
{
    public interface IToDoListRepository
    {
        Task<IEnumerable<ToDoList>> GetListsForUserAsync(string owner);
        Task<ToDoList> GetListByIdForUserAsync(string owner, long id);
        Task<ToDoList> CreateListForUserAsync(string owner, ToDoList value);
        Task DeleteListForUserAsync(string owner, long id);
        Task<ToDoList> UpdateListForUserAsync(string owner, long id, ToDoList value);
    }
}
