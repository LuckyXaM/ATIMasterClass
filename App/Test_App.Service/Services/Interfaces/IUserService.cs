using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test_App.Data.Models;

namespace Test_App.Service.Services.Interfaces
{
    /// <summary>
    /// Сервис пользователй
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Добавляет пользоватея
        /// </summary>
        Task<User> AddAsync(string name);

        /// <summary>
        /// Удаляет пользователя
        /// </summary>
        Task RemoveAsync(Guid userId);

        /// <summary>
        /// Получает пользователя
        /// </summary>
        Task<User> GetAsync(Guid userId);

        /// <summary>
        /// Получает пользователей
        /// </summary>
        Task<List<User>> GetListAsync();

        /// <summary>
        /// Редактирует пользователя
        /// </summary>
        Task<User> UpdateAsync(Guid userId, string name);
    }
}
