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
    public class EntityFrameworkToDoListRepository : IToDoListRepository
    {
        private readonly TodoContext _dataContext;

        public EntityFrameworkToDoListRepository(TodoContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ToDoList> CreateListForUserAsync(string owner, ToDoList value)
        {
            if (string.IsNullOrEmpty(value.UserId)) value.UserId = owner;
            await _dataContext.ToDoLists.AddAsync(value);
            await _dataContext.SaveChangesAsync();
            return value;
        }

        public async Task DeleteListForUserAsync(string owner, long id)
        {
            var list = await _dataContext.ToDoLists.SingleAsync(i => i.ListId == id);
            _dataContext.ToDoLists.Remove(list);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<ToDoList> GetListByIdForUserAsync(string owner, long id)
        {
            return await _dataContext.ToDoLists.SingleAsync(i => i.ListId == id);            
        }

        public async Task<IEnumerable<ToDoList>> GetListsForUserAsync(string owner)
        {
            return await _dataContext.ToDoLists.Where(t => t.UserId == owner).ToListAsync();
        }

        public async Task<ToDoList> UpdateListForUserAsync(string owner, long id, ToDoList value)
        {
            var list = await _dataContext.ToDoLists.SingleAsync(i => i.ListId == id);
            list = value;
            await _dataContext.SaveChangesAsync();
            return list;
        }
    }
}
