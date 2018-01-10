using System.Collections.Generic;

namespace GWiLi.Areas.List.Data
{
    public interface IListRepository
    {
        IList<EntityFramework.List> GetMyLists();
        EntityFramework.List GetList(int id);
        EntityFramework.Item GetItem(int id);
        bool MarkItemPurchased(int itemId, int buyerUserId, int quantity);
        bool DELETE_AddItem(int listId, int categoryId, string name, int quantity, string link, string uri, string price, string md);
        IList<EntityFramework.Category> GetCategories();
    }
}
