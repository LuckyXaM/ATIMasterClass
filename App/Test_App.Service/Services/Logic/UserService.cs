using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test_App.Data.Infrastructure.Interfaces;
using Test_App.Data.Models;
using Test_App.Data.Repositories.Interfaces;
using Test_App.Service.Services.Interfaces;

namespace Test_App.Service.Services.Logic
{
    /// <summary>
    /// Сервис пользователй
    /// </summary>
    public class UserService : IUserService
    {
        #region Поля, свойства

        /// <summary>
        /// Единица работы с БД
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Репозиторий пользователей
        /// </summary>
        private readonly IUserRepository _userRepository;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public UserService(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository
            )
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Добавляет пользоватея
        /// </summary>
        public async Task<User> AddAsync(string name)
        {
            var userId = Guid.NewGuid();

            var user = new User
            {
                UserId = userId,
                Name = name
            };

            _userRepository.Add(user);

            await _unitOfWork.SaveChangesAsync();

            return await _userRepository.GetAsync(u => u.UserId == userId);
        }

        /// <summary>
        /// Удаляет пользователя
        /// </summary>
        public async Task RemoveAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(u => u.UserId == userId);

            if (user != null)
            {
                _userRepository.Remove(user);

                await _unitOfWork.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Получает пользователя
        /// </summary>
        public async Task<User> GetAsync(Guid userId)
        {
            return await _userRepository.GetAsync(u => u.UserId == userId);
        }

        /// <summary>
        /// Получает пользователей
        /// </summary>
        public async Task<List<User>> GetListAsync()
        {
            return await _userRepository.GetListAsync(u => u.UserId != Guid.Empty);
        }

        /// <summary>
        /// Редактирует пользователя
        /// </summary>
        public async Task<User> UpdateAsync(Guid userId, string name)
        {
            var user = await _userRepository.GetAsync(u => u.UserId == userId);

            if (user != null)
            {
                user.Name = name;

                _userRepository.Update(user);

                await _unitOfWork.SaveChangesAsync();

                return user;
            }

            return new User();
        }

        #endregion
    }
}
