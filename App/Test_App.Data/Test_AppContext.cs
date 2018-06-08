using Microsoft.EntityFrameworkCore;
using Test_App.Data.Models;

namespace Test_App.Data
{
    public class Test_AppContext :  DbContext
    {
        #region Свойства (Таблицы)

        /// <summary>
        /// Пользователи
        /// </summary>
        public DbSet<User> Users { get; set; }

        #endregion

        #region Конструктор

        /// <summary>
        /// Контекст для работы с БД
        /// </summary>
        public Test_AppContext(DbContextOptions<Test_AppContext> options) : base(options)
        {
        }

        #endregion
    }
}
