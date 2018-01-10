using GWiLi.Core;
using GWiLi.Data;
using GWiLi.EntityFramework;
using GWiLi.EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GWiLi.Areas.List.Data
{
    public class ListRepository : IListRepository
    {
        #region Properties

        private ICoreRepository _coreRepo;
        private GWiLiEntities _dataContext;

        #endregion

        #region Constructors

        public ListRepository()
        {
            _dataContext = new GWiLiEntities();
            _coreRepo = new CoreRepository(_dataContext);
        }

        #endregion

        #region Methods

        public IList<EntityFramework.List> GetMyLists()
        {
            // TODO: Figure out how the user's id will be passed
            var REMOVE_UserId = 1;
            return _dataContext.Lists.Where(x => x.OwnerId == REMOVE_UserId).ToList();
        }
        
        public EntityFramework.List GetList(int id) 
        {
            return _dataContext.Lists.FirstOrDefault(x => x.Id == id);
        }

        public Item GetItem(int id)
        {
            return _dataContext.Items.FirstOrDefault(x => x.Id == id);
        }

        public bool MarkItemPurchased(int itemId, int buyerUserId, int quantity)
        {
            try
            {
                _dataContext.ItemActivities.Add(new ItemActivity
                {
                    ItemId = itemId,
                    BuyerUserId = buyerUserId,
                    Quantity = quantity,
                });
                _dataContext.SaveChanges();

                return true;
            } catch(Exception e)
            {
                FileLog.Instance.WriteLine(e.GetFullExceptionMessage($"Failed to add {quantity} items to item {itemId}'s purchase list from user {buyerUserId}"));
                return false;
            }
        }
        
        public bool DELETE_AddItem(int listId, int categoryId, string name, int quantity, string link, string uri, string price, string md)
        {
            try
            {
                _dataContext.Items.Add(new Item
                {
                    ListId = listId,
                    CategoryId = categoryId,
                    StatusId = (int)StatusEnum.Active,
                    DisplayName = name,
                    Quantity = quantity,
                    LocationDisplayName = link,
                    LocationUri = uri,
                    Price = price,
                    Metadata = md,
                });
                _dataContext.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                FileLog.Instance.WriteLine(e.GetFullExceptionMessage("Failed to add an item"));
                return false;
            }
        }

        public IList<Category> GetCategories()
        {
            return _dataContext.Categories.ToList();
        }

        #endregion
    }
}