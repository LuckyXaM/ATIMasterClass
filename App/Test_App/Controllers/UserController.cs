using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test_App.Data.Models;
using Test_App.Service.Services.Interfaces;

namespace Test_App.Controllers
{
    /// <summary>
    /// API пользователй
    /// </summary>
    [Route("api/user")]
    [ApiExplorerSettings(GroupName = "testapp")]
    public class UserController : Controller
    {
        #region Поля, свойства

        /// <summary>
        /// Сервис пользователй
        /// </summary>
        private readonly IUserService _userService;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public UserController(
            IUserService userService
            )
        {
            _userService = userService;
        }

        #endregion

        #region API

        /// <summary>
        /// Добавляет пользователя
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <returns></returns>
        [HttpPost("add/{name}")]
        public async Task<User> AddAsync(string name)
        {
            return await _userService.AddAsync(name);
        }

        /// <summary>
        /// Удаляет пользователя
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <returns></returns>
        [HttpDelete("delete/{userId}")]
        public async Task RemoveAsync(Guid userId)
        {
            await _userService.RemoveAsync(userId);
        }

        /// <summary>
        /// Получает пользователя
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <returns></returns>
        [HttpGet("get/{userId}")]
        public async Task<User> GetAsync(Guid userId)
        {
            return await _userService.GetAsync(userId);
        }

        /// <summary>
        /// Получает всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<List<User>> GetListAsync()
        {
            return await _userService.GetListAsync();
        }

        /// <summary>
        /// Обновляет пользователя
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="name">Имя пользователя</param>
        /// <returns></returns>
        [HttpPut("update/{userId}/{name}")]
        public async Task<User> UpdateAsync(Guid userId, string name)
        {
            return await _userService.UpdateAsync(userId, name);
        }

        #endregion
    }
}