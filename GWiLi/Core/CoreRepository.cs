using GWiLi.EntityFramework;

namespace GWiLi.Core
{
    public class CoreRepository : ICoreRepository
    {
        #region Properties

        private GWiLiEntities _dataContext;

        #endregion

        #region Constructors

        public CoreRepository(GWiLiEntities dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region Methods

        #endregion
    }
}