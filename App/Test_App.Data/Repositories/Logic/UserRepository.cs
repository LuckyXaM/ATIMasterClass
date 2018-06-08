using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Test_App.Data.Models;
using Test_App.Data.Repositories.Interfaces;

namespace Test_App.Data.Repositories.Logic
{
    /// <summary>
    /// Репозиторий пользователей
    /// </summary>
    public class UserRepository : IUserRepository
    {
        #region Свойства

        /// <summary>
        /// Контекст
        /// </summary>
        private readonly Test_AppContext _context;

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public UserRepository(Test_AppContext context)
        {
            _context = context;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Добавляет пользоватея
        /// </summary>
        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        /// <summary>
        /// Удаляет пользователя
        /// </summary>
        public void Remove(User user)
        {
            _context.Users.Remove(user);
        }

        /// <summary>
        /// Получает пользователя
        /// </summary>
        public async Task<User> GetAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users
                .FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Получает пользователей
        /// </summary>
        public async Task<List<User>> GetListAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users
                .Where(predicate)
                .ToListAsync();
        }

        /// <summary>
        /// Редактирует пользователя
        /// </summary>
        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        #endregion
    }
}
