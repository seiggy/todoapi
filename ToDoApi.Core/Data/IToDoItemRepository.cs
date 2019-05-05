using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoApi.Core.Model;

namespace ToDoApi.Core.Data
{
    public interface IToDoItemRepository
    {
        Task<IEnumerable<ToDoItem>> GetItemsForListAsync(long id);
        Task<ToDoItem> AddItemToListAsync(long listId, ToDoItem value);
        Task DeleteItemAsync(long id);
        Task<ToDoItem> UpdateListItemAsync(long id, ToDoItem value);
        Task<ToDoItem> GetItemByIdAsync(long id);
    }
}
