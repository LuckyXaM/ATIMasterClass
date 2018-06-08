using System;

namespace Test_App.Data.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Ид
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
    }
}
