using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Test_App.Data.Models;

namespace Test_App.Data.Repositories.Interfaces
{
    /// <summary>
    /// Репозиторий пользователей
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Добавляет пользоватея
        /// </summary>
        void Add(User user);

        /// <summary>
        /// Удаляет пользователя
        /// </summary>
        void Remove(User user);

        /// <summary>
        /// Получает пользователя
        /// </summary>
        Task<User> GetAsync(Expression<Func<User, bool>> predicate);

        /// <summary>
        /// Получает пользователей
        /// </summary>
        Task<List<User>> GetListAsync(Expression<Func<User, bool>> predicate);

        /// <summary>
        /// Редактирует пользователя
        /// </summary>
        void Update(User user);
    }
}
