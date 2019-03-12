using ApiResource.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace ApiResource.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly List<ToDo> _toDos;
        private const string Key = "TODO_KEY";

        public ToDoController(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
            _toDos = new List<ToDo>
            {
                new ToDo
                {
                    Id = Guid.NewGuid(),
                    Title = "吃饭",
                    Completed = true
                },
                new ToDo
                {
                    Id = Guid.NewGuid(),
                    Title = "学习C#",
                    Completed = false
                },
                new ToDo
                {
                    Id = Guid.NewGuid(),
                    Title = "学习.NET Core",
                    Completed = false
                },
                new ToDo
                {
                    Id = Guid.NewGuid(),
                    Title = "学习 ASP.NET Core",
                    Completed = false
                },
                new ToDo
                {
                    Id = Guid.NewGuid(),
                    Title = "学习 Entity Framework",
                    Completed = false
                }
            };
            //如果缓存里没有，就将TODO的数据存到缓存里
            if (!_memoryCache.TryGetValue(Key, out List<ToDo> todos))
            {
                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromDays(1)); //缓存过期时间为1天
                _memoryCache.Set(Key, todos, options); //添加到缓存
            }
        }

        public IActionResult Get()
        {
            if (!_memoryCache.TryGetValue(Key, out List<ToDo> todos))
            {
                todos = _toDos;
                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromDays(1)); //缓存过期时间为1天
                _memoryCache.Set(Key, todos, options); //添加到缓存
            }

            return Ok(todos);
        }

        [HttpPost]
        public IActionResult Post([FromBody]ToDoEdit toDoEdit)
        {
            var todo = new ToDo
            {
                Id = Guid.NewGuid(),
                Title = toDoEdit.Title,
                Completed = toDoEdit.Completed
            };

            if (!_memoryCache.TryGetValue(Key, out List<ToDo> todos))
            {
                todos = _toDos;
            }
            todos.Add(todo);
            var options = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromDays(1));
            _memoryCache.Set(Key, todos, options);

            return Ok(todo);
        }



    }
}