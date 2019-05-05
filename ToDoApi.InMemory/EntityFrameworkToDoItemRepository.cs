using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApi.Core.Data;
using ToDoApi.Core.Model;
using ToDoApi.InMemory.EntityFramework;

namespace ToDoApi.InMemory
{
    public class EntityFrameworkToDoItemRepository : IToDoItemRepository
    {
        private readonly TodoContext _dataContext;

        public EntityFrameworkToDoItemRepository(TodoContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ToDoItem> AddItemToListAsync(long listId, ToDoItem value)
        {
            value.ToDoList = await _dataContext.ToDoLists.SingleAsync(i => i.ListId == listId);
            value.ToDoListId = listId;
            await _dataContext.ToDoItems.AddAsync(value);
            await _dataContext.SaveChangesAsync();
            return value;
        }

        public async Task DeleteItemAsync(long id)
        {
            var item = await _dataContext.ToDoItems.SingleAsync(i => i.ItemId == id);
            _dataContext.ToDoItems.Remove(item);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<ToDoItem> GetItemByIdAsync(long id)
        {
            return await _dataContext.ToDoItems.SingleAsync(i => i.ItemId == id);
        }

        public async Task<IEnumerable<ToDoItem>> GetItemsForListAsync(long id)
        {
            return await _dataContext.ToDoItems.Where(i => i.ToDoListId == id).ToListAsync();
        }

        public async Task<ToDoItem> UpdateListItemAsync(long id, ToDoItem value)
        {
            var item = await _dataContext.ToDoItems.SingleAsync(i => i.ItemId == id);
            item = value;
            if (item.ToDoList == null)
                item.ToDoList = await _dataContext.ToDoLists.SingleAsync(i => i.ListId == item.ToDoListId);
            await _dataContext.SaveChangesAsync();
            return item;
        }
    }
}
