using System.Threading.Tasks;
using Test_App.Data.Infrastructure.Interfaces;

namespace Test_App.Data.Infrastructure.Logic
{
    /// <summary>
    /// Единица работы с БД
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Поля, свойства

        /// <summary>
        /// Контекст для работы с БД
        /// </summary>
        private Test_AppContext _docflowContext;

        /// <summary>
        /// Контекст для работы с БД
        /// </summary>
        protected Test_AppContext DataContext
        {
            get { return _docflowContext; }
        }

        #endregion

        #region

        /// <summary>
        /// Единица работы с БД
        /// </summary>
        public UnitOfWork(Test_AppContext docflowContext)
        {
            _docflowContext = docflowContext;
        }
        #endregion

        #region Методы

        /// <summary>
        /// Сохраняет изменения в БД
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync()
        {
            await DataContext.SaveChangesAsync();
        }

        #endregion
    }
}
