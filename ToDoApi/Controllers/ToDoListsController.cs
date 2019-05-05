using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Core.Data;
using ToDoApi.Core.Model;
using ToDoApi.Utility;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListsController : ControllerBase
    {
        private readonly IToDoListRepository _toDoRepository;
        private readonly IToDoItemRepository _itemRepository;

        public ToDoListsController(IToDoListRepository toDoRepository, IToDoItemRepository itemRepository)
        {
            _toDoRepository = toDoRepository;
            _itemRepository = itemRepository;
        }

        // GET: api/ToDoLists
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<ToDoList>> GetLists()
        {
            string owner = UserUtility.GetUserId(User);
            return await _toDoRepository.GetListsForUserAsync(owner);
        }

        [HttpGet("{id}/items", Name = "GetItemsForList")]
        [Authorize]
        public async Task<IEnumerable<ToDoItem>> GetItemsForList(long id)
        {
            return await _itemRepository.GetItemsForListAsync(id);
        }

        // GET: api/ToDoLists/5
        [HttpGet("{id}", Name = "GetList")]
        public async Task<ToDoList> GetList(long id)
        {
            string owner = UserUtility.GetUserId(User);
            return await _toDoRepository.GetListByIdForUserAsync(owner, id);
        }

        // POST: api/ToDoLists
        [HttpPost]
        public async Task<ToDoList> PostList([FromBody] ToDoList value)
        {
            string owner = UserUtility.GetUserId(User);
            return await _toDoRepository.CreateListForUserAsync(owner, value);
        }

        [HttpPost("{id}/newitem", Name ="AddItemToList")]
        public async Task<ToDoItem> PostItemToList(long id, [FromBody] ToDoItem value)
        {
            return await _itemRepository.AddItemToListAsync(id, value);
        }

        // PUT: api/ToDoLists/5
        [HttpPut("{id}")]
        public async Task<ToDoList> PutList(long id, [FromBody] ToDoList value)
        {
            string owner = UserUtility.GetUserId(User);
            return await _toDoRepository.UpdateListForUserAsync(owner, id, value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(long id)
        {
            string owner = UserUtility.GetUserId(User);
            await _toDoRepository.DeleteListForUserAsync(owner, id);
            return NoContent();
        }
    }
}
