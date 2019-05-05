using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Core.Data;
using ToDoApi.Core.Model;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private IToDoItemRepository _itemRepository;

        public ToDoItemsController(IToDoItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        
        // GET: api/ToDoItems/5
        [HttpGet("{id}", Name = "GetItem")]
        public async Task<ToDoItem> GetItem(long id)
        {
            return await _itemRepository.GetItemByIdAsync(id);
        }
        
        // PUT: api/ToDoItems/5
        [HttpPut("{id}")]
        public async Task<ToDoItem> PutItem(long id, [FromBody] ToDoItem value)
        {
            return await _itemRepository.UpdateListItemAsync(id, value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(long id)
        {
            await _itemRepository.DeleteItemAsync(id);
            return NoContent();
        }
    }
}
