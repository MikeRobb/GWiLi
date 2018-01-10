using GWiLi.Areas.List.Data;
using GWiLi.Areas.List.Models;
using GWiLi.Controllers;
using GWiLi.EntityFramework;
using System.Linq;
using System.Web.Mvc;
using static GWiLi.Areas.List.Models.ListViewModel;

namespace GWiLi.Areas.List.Controllers
{
    public class ListController : BaseController
    {
        #region Properties

        private IListRepository Repo;

        #endregion

        #region Constructors

        public ListController()
        {
            Repo = new ListRepository();
        }

        #endregion

        #region Methods

        public ActionResult Index()
        {
            var model = new ListDashboardModel();
            var lists = Repo.GetMyLists();
            model.PopulateDashboard(lists);
            return View(model);
        }

        public ActionResult List(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            var list = Repo.GetList(id.Value);
            if (list == null)
                return RedirectToAction("Index");

            var model = new ListViewModel(list);
            var categories = Repo.GetCategories();
            model.PopulateSelectLists(categories);
            return View("List", model);
        }

        public ActionResult MarkItemBought(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            var item = Repo.GetItem(id.Value);
            if (item == null)
                return RedirectToAction("Index");

            // TODO: Change this
            var DELETE_buyerUserId = 2;
            Repo.MarkItemPurchased(item.Id, DELETE_buyerUserId, 1);
            return RedirectToAction("List", new { id = item.ListId });
        }

        public ActionResult AddItem(AddItemModel model, int listId)
        {
            Repo.DELETE_AddItem(listId, model.newCategory, model.newName, model.newQuantity, model.newLink, model.newUri, model.newPrice, model.newMetadata);
            return RedirectToAction("List", new { id = listId });
        }

        #endregion
    }
}