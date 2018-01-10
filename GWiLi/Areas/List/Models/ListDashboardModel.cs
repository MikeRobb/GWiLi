using GWiLi.Data;
using System.Collections.Generic;
using System.Linq;

namespace GWiLi.Areas.List.Models
{
    public class ListDashboardModel
    {
        #region Property

        public IList<ListResultViewModel> Results { get; set; }

        #endregion

        #region Constructor
        public ListDashboardModel()
        {
            Results = new List<ListResultViewModel>();
        }
        #endregion

        #region Method
        public void PopulateDashboard(IList<EntityFramework.List> lists)
        {
            Results = lists.Select(x => new ListResultViewModel(x)).ToList();
        }
        #endregion

        #region SubClass
        public class ListResultViewModel
        {
            public int ListId { get; set; }
            public string Name { get; set; }
            public string Owner { get; set; }
            public string Status { get; set; }
            public string StatusPanelClass { get; set; }

            public ListResultViewModel(EntityFramework.List obj)
            {
                ListId = obj.Id;
                Name = obj.DisplayName;
                Owner = obj.Owner.GetDisplayName();
                Status = obj.Status.DisplayName;
                StatusPanelClass = GetStatusPanelClass(obj.StatusId);
            }

            private string GetStatusPanelClass(int listStatusId)
            {
                switch (listStatusId)
                {
                    case (int)StatusEnum.Active:
                        return "panel-primary";
                    case (int)StatusEnum.Inactive:
                        return "panel-warning";
                    case (int)StatusEnum.Deleted:
                        return "panel-danger";
                    default:
                        return "panel-default";
                }
            }
        }
        #endregion
    }
}